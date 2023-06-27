using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsha.Core.Models
{
    public class PortfolioItem:BaseModel
    {
        public string Name { get; set; }
        public int PortfolioCategoryId { get; set; }
        public PortfolioCategory? PortfolioCategory { get; set; }
        public string? Photo { get; set; }
        [NotMapped]
        public IFormFile? file { get; set; }
    }
}
