using AutoMapper;
using Cars.Repository.Interfaces;
using Cars.Service.Interfaces;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Service.Services
{
    public class CarCharacteristicService: ICarCharacteristicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _map;
        public CarCharacteristicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }
    }
}
