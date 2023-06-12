using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Filters
{
    public class CarFilter: DefoltFilter
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? MinYearOfRelease { get; set; }
        public int? MaxYearOfRelease { get; set; }
        public int? MinMileage { get; set; }
        public int? MaxMileage { get; set; }
        public string? Transmission { get; set; }
        public string? WheelDrive { get; set; }
        public string? Rudder { get; set; }
        public bool? IsRent { get; set; }//арендована
        public bool? IsRepair { get; set; }//в ремонте
        public CarFilter()
        {
            Offset = 0;
            SizePage = 20;
        }
        public CarFilter(int offset, int sizePage, string? brand, string? model, int? minYearOfRelease, int? maxYearOfRelease, int? minMileage, int? maxMileage, string? transmission, string? wheelDrive, string? rudder, bool? isRent, bool isRepair)
        {
            Offset = offset;
            SizePage = sizePage;
            Brand = brand;
            Model = model;
            MinMileage = minMileage;
            MaxMileage = maxMileage;
            MinYearOfRelease = minYearOfRelease;
            MaxYearOfRelease = maxYearOfRelease;
            Transmission = transmission;
            WheelDrive = wheelDrive;
            Rudder = rudder;
            IsRent = isRent;
            IsRepair = isRepair;
        }
    }
}
