namespace HMS.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public int StaffId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan? BreakStart { get; set; }
                
        public TimeSpan? BreakEnd { get; set; }

        public string ShiftType { get; set; }

        public string Status { get; set; }

        public bool SlotsGenerated { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Staff Staff { get; set; }
        public virtual TimeReport? TimeReport { get; set; }
        public virtual ICollection<AppointmentSlot> AppointmentSlots { get; set; } = new List<AppointmentSlot>();

        public virtual Appointment? Appointment { get; set; }
    }
}
