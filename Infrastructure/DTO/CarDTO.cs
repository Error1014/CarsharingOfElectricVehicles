﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class CarDTO
    {
        public Guid BrandModelId { get; set; }
        public string Number { get; set; }
        public bool IsRent { get; set; }//арендована
        public bool IsRepair { get; set; }//в ремонте
    }
}
