using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Muzej.Model
{
    public class Owner
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [RegularExpression("[0-9]{10}")]
        public string OIB { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
