using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Abstractions;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Arsha.Core.Models
{
    public class Team:BaseModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Photo { get; set; }
        [NotMapped]
        public IFormFile? file { get; set; }
        [Required]
        public int PositionId { get; set; }
        public Position? Position { get; set; }
        public List<Social>? Socials { get; set; }
    }
}
