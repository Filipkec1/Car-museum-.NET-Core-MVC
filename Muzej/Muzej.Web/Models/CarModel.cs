using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muzej.Web.Models
{
    public class CarFilterModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Year { get; set; }
        public string Maker { get; set; }
        public string Owner { get; set; }
    }
}
