﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Models.Dtos
{
    public class CreateRole
    {
        [Required]
        public string Name { get; set; }
    }
}
