﻿using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Service.Interfaces
{
    public interface ICharacteristicService
    {
        Task<CharacteristicDTO> GetCharacteristicByCarId(Guid carId);
        Task<Guid> AddCharacteristicByCarId(CharacteristicDTO characteristicDTO);
        Task UpdateCharacteristicByCarId(Guid carId, CharacteristicDTO characteristicDTO);
        Task RemoveCharacteristicByCarId(Guid carId);
    }
}
