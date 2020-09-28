using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Muzej.Model
{
    public class Maker
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Proizvođač obavezno mora imati naziv.")]
        //[StringLength(30, MinimumLength = 3, ErrorMessage = "Naziv proizvođaća mora imati minimalno 3, a maksimalno 30 znakova")]
        [MinLength(3, ErrorMessage = "Naziv proizvođaća mora imati minimalno 3 znaka.")]
        [MaxLength(30, ErrorMessage = "Naziv proizvođaća mora imati maksimalno 30 znakova.")]
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        [Required(ErrorMessage = "Proizvođač obavezno mora imati državu podrijetla.")]
        [ForeignKey(nameof(Country))]
        public int? CountryID { get; set; }
        public Country Country { get; set; }

    }
}
