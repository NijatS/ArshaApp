﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arsha.Core.Models
{
    public class Position:BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
