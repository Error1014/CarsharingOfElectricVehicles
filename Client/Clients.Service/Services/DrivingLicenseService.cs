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
    public class DrivingLicenseService: IDrivingLicenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;

        public DrivingLicenseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }

        public async Task AddDrivingLicense(DrivingLicenseDTO drivingLicenseDTO)
        {
            var drivingLicense = _map.Map<DrivingLicense>(drivingLicenseDTO);
            await _unitOfWork.DrivingLicenses.AddEntities(drivingLicense);
            await _unitOfWork.DrivingLicenses.SaveChanges();
        }

        public async Task<DrivingLicenseDTO> GetDrivingLicense(Guid id)
        {
            var drivingLicense = await _unitOfWork.DrivingLicenses.GetEntity(id);
            if (drivingLicense == null)
            {
                throw new NotFoundException("Водительские права не найдены");
            }
            var drivingLicenseDTO = _map.Map<DrivingLicenseDTO>(drivingLicense);
            return drivingLicenseDTO;
        }

        public async Task<IEnumerable<DrivingLicenseDTO>> GetDrivingLicenses(PageFilter pageFilter)
        {
            var drivingLicenses = await _unitOfWork.DrivingLicenses.GetPage(pageFilter);
            var drivingLicenseDTO = _map.Map<IEnumerable<DrivingLicenseDTO>>(drivingLicenses);
            return drivingLicenseDTO;
        }

        public async Task RemoveDrivingLicense(Guid id)
        {
            var drivingLicense = await _unitOfWork.DrivingLicenses.GetEntity(id);
            if (drivingLicense == null)
            {
                throw new NotFoundException("Водительские права не найдены");
            }
            _unitOfWork.DrivingLicenses.RemoveEntities(drivingLicense);
            await _unitOfWork.DrivingLicenses.SaveChanges();
        }

        public async Task UpdateDrivingLicense(Guid id, DrivingLicenseDTO drivingLicenseDTO)
        {
            var drivingLicense = _map.Map<DrivingLicense>(drivingLicenseDTO);
            _unitOfWork.DrivingLicenses.UpdateEntities(drivingLicense);
            await _unitOfWork.DrivingLicenses.SaveChanges();
        }
    }
}