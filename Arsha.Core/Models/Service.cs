using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsha.Core.Models
{
    public class Service:BaseModel
    {
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        [Required]
        public string Icon { get; set; }
    }
}
