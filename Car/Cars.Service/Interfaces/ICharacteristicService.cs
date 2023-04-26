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
        Task<IEnumerable<CharacteristicDTO>> GetCharacteristics();
        Task AddCharacteristic(CharacteristicDTO characteristicDTO);
        Task UpdateCharacteristic(Guid id, CharacteristicDTO characteristicDTO);
        Task RemoveCharacteristic(Guid id);
    }
}
