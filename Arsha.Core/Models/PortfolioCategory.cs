using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsha.Core.Models
{
    public class PortfolioCategory:BaseModel
    {
        public string Name { get; set; }
        public List<PortfolioItem>? Items { get; set; }
    }
}
