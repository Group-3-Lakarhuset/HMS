namespace HMS.Models;

public class LeaveBalance
{
    public int Id { get; set; }
    public int StaffId { get; set; }
    public int Year { get; set; }

    public int VacationDaysTotal { get; set; }
    public decimal VacationDaysUsed { get; set; }
    public decimal VacationDaysRemaining { get; set; }

    public decimal SickDaysUsed { get; set; }
    public decimal VABDaysUsed { get; set; }
    public decimal ParentalDaysUsed { get; set; }

    public DateTime LastUpdated { get; set; }

    public Staff Staff { get; set; }
}