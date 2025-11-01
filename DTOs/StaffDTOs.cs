using System.ComponentModel.DataAnnotations;

namespace HMS.DTOs
{
  
    public class CreateStaffDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string ContractForm { get; set; } 

        [Required]
        [StringLength(100)]
        public string Department { get; set; }

        [Range(0, 100)]
        public decimal Taxes { get; set; }

        [Required]
        [StringLength(200)]
        public string Bankdetails { get; set; }

        [Range(0, 365)]
        public int Vacationdays { get; set; } = 25; 

        [StringLength(100)]
        public string Specialization { get; set; }

        [Range(0, double.MaxValue)]
        public decimal HourlyRate { get; set; }

        public DateTime HiredDate { get; set; } = DateTime.UtcNow;
    }

    
    public class UpdateStaffDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string ContractForm { get; set; }

        [StringLength(100)]
        public string Department { get; set; }

        [Range(0, 100)]
        public decimal? Taxes { get; set; }

        [StringLength(200)]
        public string Bankdetails { get; set; }

        [Range(0, 365)]
        public int? Vacationdays { get; set; }

        [StringLength(100)]
        public string Specialization { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? HourlyRate { get; set; }
    }

    
    public class StaffDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ContractForm { get; set; }
        public string Department { get; set; }
        public decimal Taxes { get; set; }
        public string Bankdetails { get; set; }
        public int Vacationdays { get; set; }
        public int UsedVacationDays { get; set; }
        public int RemainingVacationDays { get; set; }
        public string Specialization { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime HiredDate { get; set; }
        public int TotalAppointments { get; set; }
        public decimal TotalHoursWorked { get; set; }
    }

    
    public class StaffProfileDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ContractForm { get; set; }
        public string Department { get; set; }
        public string Specialization { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime HiredDate { get; set; }
        public int Vacationdays { get; set; }
        public int UsedVacationDays { get; set; }
        public int RemainingVacationDays { get; set; }
        public List<AppointmentSummaryDto> UpcomingAppointments { get; set; }
        public List<ScheduleSummaryDto> WeekSchedule { get; set; }
    }

  
    public class AppointmentSummaryDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int DurationMinutes { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }

   
    public class ScheduleSummaryDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ShiftType { get; set; }
        public string Status { get; set; }
    }

    
    public class StaffDashboardDto
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }

      
        public int TodayAppointments { get; set; }
        public int TodayCompletedAppointments { get; set; }
        public decimal TodayHoursWorked { get; set; }

      
        public int WeekAppointments { get; set; }
        public decimal WeekHoursWorked { get; set; }
        public decimal WeekEarnings { get; set; }

       
        public int MonthAppointments { get; set; }
        public decimal MonthHoursWorked { get; set; }
        public decimal MonthEarnings { get; set; }

        
        public int RemainingVacationDays { get; set; }
        public List<VacationRequestDto> PendingVacations { get; set; }

       
        public List<ScheduleSummaryDto> UpcomingSchedules { get; set; }

        
        public List<AppointmentSummaryDto> UpcomingAppointments { get; set; }
    }

   
    public class VacationRequestDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public string Status { get; set; } 
        public string Notes { get; set; }
    }

   
    public class TimeReportSummaryDto
    {
        public int Id { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public decimal HoursWorked { get; set; }
        public string ActivityType { get; set; }
        public string Status { get; set; } 
    }

    
    public class CreateTimeReportDto
    {
        public int StaffId { get; set; }
        public int? ScheduleId { get; set; }
        public DateTime ClockIn { get; set; } = DateTime.UtcNow;
        public string ActivityType { get; set; }
        public string Notes { get; set; }
    }

   
    public class CompleteTimeReportDto
    {
        public int Id { get; set; }
        public DateTime ClockOut { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; }
    }
}