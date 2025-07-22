using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Models
{
    public class Treatment
    {
        public int TreatmentId { get; set; }//pk

        [Column(TypeName = "nvarchar(100)")]
        public string? Description { get; set; }

        [Required]
        [Range(10, 300, ErrorMessage = "Cost should be between 10-150")]
        public double Cost { get; set; }

        [Required]
        public eTreat TretmentType { get; set; }
        //NAV
        public IEnumerable<Admin>? Admin { get; set; }

    }
    public enum eTreat
    {
        Dental_filling, //حشوة اسنان
        nerve_extraction, //سحب عصب 
        Dental_implants, //زراعة اسنان
        orthodontics, //تقويم اسنان
        Teeth_whitening, //تبييض اسنان
        Teeth_cleaning, //تنظيف اسنان
    }
}

