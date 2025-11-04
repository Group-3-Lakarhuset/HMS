using HMS.Data;
using HMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HMS.Services
{
    public class AdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region Patient Management

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.User)
                .Include(p => p.Appointments)
                .Include(p => p.Invoices)
                .OrderByDescending(p => p.Createdat)
                .ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int patientId)
        {
            return await _context.Patients
                .Include(p => p.User)
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.Staff)
                        .ThenInclude(s => s.User)
                .Include(p => p.Invoices)
                .FirstOrDefaultAsync(p => p.Id == patientId);
        }

        public async Task<(bool Success, string Message, Patient? Patient)> CreatePatientAsync(
            string email,
            string password,
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            string address,
            string contact,
            string bloodGroup,
            string? personalNumber,
            string preferences = "",
            string interests = "")
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Role = "Patient",
                    PersonalNumber = personalNumber,
                    EmailConfirmed = true // Auto-confirm for admin-created accounts
                };

                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return (false, $"Failed to create user account: {errors}", null);
                }

                var patient = new Patient
                {
                    UserId = user.Id,
                    Dateofbirth = dateOfBirth,
                    Address = address,
                    Contact = contact,
                    BloodGroup = bloodGroup,
                    Createdat = DateTime.UtcNow,
                    Preferences = preferences,
                    Interests = interests,
                    User = user
                };

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();

                return (true, "Patient created successfully", patient);
            }
            catch (Exception ex)
            {
                return (false, $"Error creating patient: {ex.Message}", null);
            }
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            try
            {
                _context.Patients.Update(patient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating patient: {ex.Message}");
                return false;
            }
        }

        public async Task<(bool Success, string Message)> DeletePatientAsync(int patientId)
        {
            try
            {
                var patient = await _context.Patients
                    .Include(p => p.User)
                    .Include(p => p.Appointments)
                    .Include(p => p.Invoices)
                    .FirstOrDefaultAsync(p => p.Id == patientId);

                if (patient == null)
                    return (false, "Patient not found");

                if (patient.Appointments.Any())
                    return (false, "Cannot delete patient with existing appointments");

                if (patient.Invoices.Any())
                    return (false, "Cannot delete patient with existing invoices");

                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();

                return (true, "Patient deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting patient: {ex.Message}");
            }
        }

        #endregion

        #region Staff Management

        public async Task<List<Staff>> GetAllStaffAsync()
        {
            return await _context.Staff
                .Include(s => s.User)
                .Include(s => s.Appointments)
                .Include(s => s.Schedules)
                .OrderByDescending(s => s.HiredDate)
                .ToListAsync();
        }

        public async Task<Staff?> GetStaffByIdAsync(int staffId)
        {
            return await _context.Staff
                .Include(s => s.User)
                .Include(s => s.Appointments)
                    .ThenInclude(a => a.Patient)
                        .ThenInclude(p => p.User)
                .Include(s => s.Schedules)
                .Include(s => s.TimeReports)
                .FirstOrDefaultAsync(s => s.Id == staffId);
        }

        public async Task<(bool Success, string Message, Staff? Staff)> CreateStaffAsync(
            string email,
            string password,
            string firstName,
            string lastName,
            string role,
            string contractForm,
            string department,
            decimal hourlyRate,
            string specialization = "",
            decimal taxes = 0,
            string bankDetails = "",
            int vacationDays = 25,
            string personalNumber = "")
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Role = role,
                    PersonalNumber = personalNumber,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return (false, $"Failed to create user account: {errors}", null);
                }

                var staff = new Staff
                {
                    UserId = user.Id,
                    ContractForm = contractForm,
                    Department = department,
                    Taxes = taxes,
                    Bankdetails = bankDetails,
                    Vacationdays = vacationDays,
                    Specialization = specialization,
                    HourlyRate = hourlyRate,
                    HiredDate = DateTime.UtcNow,
                    User = user
                };

                _context.Staff.Add(staff);
                await _context.SaveChangesAsync();

                return (true, "Staff member created successfully", staff);
            }
            catch (Exception ex)
            {
                return (false, $"Error creating staff: {ex.Message}", null);
            }
        }

        public async Task<bool> UpdateStaffAsync(Staff staff)
        {
            try
            {
                _context.Staff.Update(staff);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating staff: {ex.Message}");
                return false;
            }
        }

        public async Task<(bool Success, string Message)> DeleteStaffAsync(int staffId)
        {
            try
            {
                var staff = await _context.Staff
                    .Include(s => s.User)
                    .Include(s => s.Appointments)
                    .Include(s => s.Schedules)
                    .FirstOrDefaultAsync(s => s.Id == staffId);

                if (staff == null)
                    return (false, "Staff member not found");

                if (staff.Appointments.Any())
                    return (false, "Cannot delete staff member with existing appointments");

                if (staff.Schedules.Any())
                    return (false, "Cannot delete staff member with existing schedules");

                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();

                return (true, "Staff member deleted successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting staff: {ex.Message}");
            }
        }

        #endregion

        #region Schedule Management

        public async Task<List<Schedule>> GetAllSchedulesAsync()
        {
            return await _context.Schedules
                .Include(s => s.Staff)
                    .ThenInclude(st => st.User)
                .Include(s => s.Appointment)
                .Include(s => s.TimeReport)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<Schedule?> GetScheduleByIdAsync(int scheduleId)
        {
            return await _context.Schedules
                .Include(s => s.Staff)
                    .ThenInclude(st => st.User)
                .Include(s => s.Appointment)
                .Include(s => s.TimeReport)
                .FirstOrDefaultAsync(s => s.Id == scheduleId);
        }

        public async Task<List<Schedule>> GetSchedulesByStaffIdAsync(int staffId)
        {
            return await _context.Schedules
                .Include(s => s.Appointment)
                .Where(s => s.StaffId == staffId)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }


        public async Task<Schedule?> CreateScheduleAsync(Schedule schedule)
        {
            try
            {
                var staffExists = await _context.Staff.AnyAsync(s => s.Id == schedule.StaffId);
                if (!staffExists)
                    return null;

                var hasConflict = await _context.Schedules
                    .AnyAsync(s => s.StaffId == schedule.StaffId &&
                                   ((s.StartTime <= schedule.StartTime && s.EndTime > schedule.StartTime) ||
                                    (s.StartTime < schedule.EndTime && s.EndTime >= schedule.EndTime) ||
                                    (s.StartTime >= schedule.StartTime && s.EndTime <= schedule.EndTime)));

                if (hasConflict)
                    return null;

                schedule.CreatedAt = DateTime.UtcNow;
                _context.Schedules.Add(schedule);
                await _context.SaveChangesAsync();

                return schedule;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating schedule: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateScheduleAsync(Schedule schedule)
        {
            try
            {
                _context.Schedules.Update(schedule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating schedule: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteScheduleAsync(int scheduleId)
        {
            try
            {
                var schedule = await _context.Schedules
                    .Include(s => s.Appointment)
                    .Include(s => s.TimeReport)
                    .FirstOrDefaultAsync(s => s.Id == scheduleId);

                if (schedule == null)
                    return false;

                if (schedule.Appointment != null)
                    return false;

                if (schedule.TimeReport != null)
                    return false;

                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting schedule: {ex.Message}");
                return false;
            }
        }

        #endregion

    }
}
