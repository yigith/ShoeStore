using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "* required"), MaxLength(180)]
        public string Street { get; set; }

        [Required(ErrorMessage = "* required"), MaxLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "* required"), MaxLength(60)]
        public string State { get; set; }

        [Required(ErrorMessage = "* required"), MaxLength(18)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "* required"), MaxLength(90)]
        public string Country { get; set; }

        [Required(ErrorMessage = "* required"), MaxLength(50)]
        public string CCOwner { get; set; }

        [Required(ErrorMessage = "* required"), MaxLength(16)]
        public string CCNumber { get; set; }

        [Required(ErrorMessage = "* required"), MaxLength(5)]
        public string CCExpiration { get; set; }

        [Required(ErrorMessage = "* required"), MaxLength(4)]
        public string CCCvv { get; set; }
    }
}
