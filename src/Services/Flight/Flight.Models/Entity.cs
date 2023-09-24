﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight.Models
{
    public class Entity : IEntity
    {
        public string? CreatedBy { get ; set ; }
        public DateTime? CreatedDate { get ; set ; }
        public string? LastModifiedBy { get ; set ; }
        public DateTime? LastModifiedDate { get ; set ; }
    }
}
