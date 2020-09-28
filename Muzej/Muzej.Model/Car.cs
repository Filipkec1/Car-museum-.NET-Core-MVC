using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Muzej.Model
{
    public
        class Car
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Automobil obavezno mora imati naziv modela.")]
        [MinLength(3, ErrorMessage = "Naziv modela automobila mora imati minimalno 3 znaka.")]
        [MaxLength(30, ErrorMessage = "Naziv modela automobila mora imati minimalno 30 znakova.")]
        public string ModelName { get; set; }

        public string Color { get; set; }

        [Required(ErrorMessage = "Automobil obavezno mora imati datum proizvodnje.")]
        public DateTime ManufactureDate { get; set; }

        [Required(ErrorMessage = "Automobil obavezno mora imati proizvođača.")]
        [ForeignKey(nameof(Maker))]
        public int? MakerID { get; set; }
        public Maker Maker { get; set; }

        [ForeignKey(nameof(Owner))]
        public int? OwnerID { get; set; }
        public Owner Owner { get; set; }

        [ForeignKey(nameof(Motor))]
        public int? MotorID { get; set; }
        public Motor Motor { get; set; }

    }
}
