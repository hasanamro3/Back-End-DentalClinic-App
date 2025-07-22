using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class Patient
    {
        public int PatientId { get; set; }//pk

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be between 3 and 50")]
        [Display(Name ="Patient Name")]
        public string? PatientName { get; set; } = string.Empty;

        [Display(Name = "Patient Age")]
        [Required(ErrorMessage = "Age is required")]
        public int PatientAge { get; set; }

      
        [Phone]
        public string? Phone { get; set; } 

        [Required]
        public eGender Gender { get; set; }

        //NAV
        public IEnumerable<Admin>? Admin { get; set; }
       
    }
    public enum eGender
    {
        Male,
        Female,
    }
}
