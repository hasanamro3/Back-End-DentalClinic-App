using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }//pk


        [Display(Name = "Patient Name", Prompt = "Enter patient name")]
        public int PatientId { get; set; }//fk


        [Display(Name = "Treatment Name", Prompt = "Enter treatment name")]
        public int TreatmentId { get; set; }//fk


        [Required(ErrorMessage = "Must choose Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public eTreatmentStatus TreatmentStatus { get; set; } = eTreatmentStatus.Pending;

        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }

        //NAV
        public Treatment? Treatment { get; set; }
        public Patient? Patient { get; set; }
    }
    public enum eTreatmentStatus
    {
        Pending,
        Completed,
        Cancelled
    }
}

