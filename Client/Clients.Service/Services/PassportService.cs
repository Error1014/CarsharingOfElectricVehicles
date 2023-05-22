﻿using AutoMapper;
using Clients.Repository.Entities;
using Clients.Repository.Interfaces;
using Clients.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Service.Services
{
    public class PassportService:IPassportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;

        public PassportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }

        public async Task AddPassport(PassportDTO passportDTO)
        {
            var passport = _map.Map<Passport>(passportDTO);
            await _unitOfWork.Passports.AddEntities(passport);
            await _unitOfWork.Passports.SaveChanges();
        }

        public async Task<PassportDTO> GetPassport(Guid id)
        {
            var passport = await _unitOfWork.Passports.GetEntity(id);
            if (passport==null)
            {
                throw new NotFoundException("Пасспорт не найден");
            }
            var passportDTO = _map.Map<PassportDTO>(passport);
            return passportDTO;
        }

        public async Task<Dictionary<Guid, PassportDTO>> GetPassports(PageFilter pageFilter)
        {
            var passports = await _unitOfWork.Passports.GetPage(pageFilter);
            Dictionary<Guid, PassportDTO> result = new Dictionary<Guid, PassportDTO>();
            foreach (var item in passports)
            {
                result.Add(item.Id, _map.Map<PassportDTO>(item));
            }
            return result;
        }

        public async Task RemovePassport(Guid id)
        {
            var passport = await _unitOfWork.Passports.GetEntity(id);
            if (passport == null)
            {
                throw new NotFoundException("Пасспорт не найден");
            }
            _unitOfWork.Passports.RemoveEntities(passport);
            await _unitOfWork.Passports.SaveChanges();
        }

        public async Task UpdatePassport(Guid id, PassportDTO passportDTO)
        {
            var passport = _map.Map<Passport>(passportDTO);
            _unitOfWork.Passports.UpdateEntities(passport);
            await _unitOfWork.Passports.SaveChanges();
        }
    }
}
