using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.ClientDTOs
{
    public class ClientContactDTO
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }

    }
}
