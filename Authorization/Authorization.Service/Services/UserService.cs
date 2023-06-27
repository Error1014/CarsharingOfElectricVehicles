using Authorization.Repository.Entities;
using Authorization.Repository.Interfaces;
using Authorization.Service.Interfaces;
using AutoMapper;
using Infrastructure;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XAct;
using System.ComponentModel.DataAnnotations;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using static System.Net.WebRequestMethods;

namespace Authorization.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task<Guid> AddUser(UserDTO userDTO)
        {
            var email = new EmailAddressAttribute();
            if (!email.IsValid(userDTO.Login))
            {
                throw new BadRequestException("Неверный адрес электронной почты");
            }
            var user = await _unitOfWork.Users.Find(u => u.Login == userDTO.Login);
            if (user !=null)
            {
                throw new BadRequestException("Пользователь с таким логином уже существует");
            }
            user = _map.Map<User>(userDTO);
            user.Password = GeneratorHash.GetHash(userDTO.Password);
            user.IsEmailСonfirmed = false;
            await _unitOfWork.Users.AddEntities(user);
            await _unitOfWork.Users.SaveChanges();
            HttpClient httpClient = new HttpClient();
            var getToken = _configuration.GetSection("EndPoint:Authorize").Get<UriEndPoint>();
            httpClient.BaseAddress = new Uri(getToken.BaseAddress);
            var loginDTO = new LoginDTO() { Login = userDTO.Login, Password = userDTO.Password};
            var response = await httpClient.PostAsync(getToken.Uri, JsonContent.Create(loginDTO));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var authorizeToken = JsonSerializer.Deserialize<AuthorizeDTO>(responseBody, options);
            var emailConfirmation = _configuration.GetSection("EndPoint:EmailConfirmation").Get<UriEndPoint>();
            await _emailService.SendEmailAsync(userDTO.Login, "Каршеринг. Подтверждение электронной почты", $"Подтвердите регистрацию, перейдя по ссылке: <a href='{emailConfirmation.BaseAddress+emailConfirmation.Uri}?userId={user.Id}&token={authorizeToken.Token}'>link</a>");
            return user.Id;
        }
        public async Task ConfirmationEmail(Guid userId, string code)
        {
            var user = await _unitOfWork.Users.GetEntity(userId);
            user.IsEmailСonfirmed = true;
            _unitOfWork.Users.UpdateEntities(user);
            await _unitOfWork.Users.SaveChanges();
        }

        public async Task<Dictionary<Guid, UserDTO>> GetUsers(DefoltFilter pageFilter)
        {
            var list = await _unitOfWork.Users.GetPage(pageFilter);
            Dictionary<Guid, UserDTO> result = new Dictionary<Guid, UserDTO>();
            foreach (var user in list)
            {
                result.Add(user.Id, _map.Map<UserDTO>(user));
            }
            return result;
        }

        public async Task<UserDTO> GetUser(Guid Id)
        {
            var user = await _unitOfWork.Users.GetEntity(Id);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден");
            }
            var userDTO = _map.Map<UserDTO>(user);
            return userDTO;
        }
        public async Task<User> GetUserByLogin(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.Users.GetUserByLogin(loginDTO);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден");
            }
            return user;
        }

        public async Task RemoveUser(Guid Id)
        {
            var user = await _unitOfWork.Users.GetEntity(Id);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден");
            }
            _unitOfWork.Users.RemoveEntities(user);
            await _unitOfWork.Users.SaveChanges();
        }

        public async Task UpdateUser(Guid id, LoginDTO userDTO)
        {
            var user = _map.Map<User>(userDTO);
            user.Id = id;
            user.Password = GeneratorHash.GetHash(userDTO.Password);
            _unitOfWork.Users.UpdateEntities(user);
            await _unitOfWork.Users.SaveChanges();
        }
        public async Task SetClientRole(Guid id)
        {
            var user = await _unitOfWork.Users.GetEntity(id);
            user.RoleId = _unitOfWork.Roles.Find(x => x.Name == "Client").Id;
            _unitOfWork.Users.UpdateEntities(user);
            await _unitOfWork.Users.SaveChanges();
        }
        public async Task<string> GetRole(Guid userId)
        {
            return await _unitOfWork.Users.GetRole(userId);
        }
    }
}
