using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPass.Services.Models.DTOs.Zones
{
    public class UpdateZoneDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AccessLevelId { get; set; }
        public string? Description { get; set; }
    }
}
