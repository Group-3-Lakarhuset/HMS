using HMS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models
{
    public class Staff
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ContractForm { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Department { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 1)]
        public decimal Taxes { get; set; } = 0.30m;

        public string? Bankdetails { get; set; }

        [Range(0, 365)]
        public int Vacationdays { get; set; } = 20;

        [StringLength(100)]
        public string? Specialization { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal HourlyRate { get; set; }

        [Required]
        public DateTime HiredDate { get; set; } = DateTime.UtcNow;

        // Appointment booking related fields
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal ConsultationFee { get; set; } = 50.00m;

        public bool IsAcceptingPatients { get; set; } = true;

        [Range(1, 100)]
        public int MaxDailyAppointments { get; set; } = 20;

        // Navigation properties
        public ApplicationUser User { get; set; } = null!; // One-to-One with AspNetUsers
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>(); // One-to-Many
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>(); // One-to-Many
        public ICollection<TimeReport> TimeReports { get; set; } = new List<TimeReport>(); // One-to-Many
        public ICollection<AppointmentSlot> AppointmentSlots { get; set; } = new List<AppointmentSlot>(); // One-to-Many
        public ICollection<AppointmentBlock> AppointmentBlocks { get; set; } = new List<AppointmentBlock>(); // One-to-Many
        public ICollection<AppointmentSlotConfiguration> SlotConfigurations { get; set; } = new List<AppointmentSlotConfiguration>(); // One-to-Many
    }
}
