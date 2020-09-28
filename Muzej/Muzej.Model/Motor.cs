using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Muzej.Model
{
    public class Motor
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public int Torque { get; set; }
        public string Configuration { get; set; }

        public FuleType Type { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }

    public enum FuleType { Petrol, Diesel, Electricity}
}
