using System;
using System.ComponentModel.DataAnnotations;
 
namespace PROG6212.Models
{
    public class Claim
    {
        [Key]
        public int ClaimID { get; set; }

        public int Id { get; set; }

        public int LecturerID { get; set; }

        [Required]
        public string LecturerName { get; set; }

        [Required]
        public double HoursWorked { get; set; }

        [Required]
        public double RatePerHour { get; set; }

        public double TotalAmount => HoursWorked * RatePerHour;

        public string Status { get; set; }   // Default status is 'Pending'

        public string SupportingDocumentFilePath { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}
      