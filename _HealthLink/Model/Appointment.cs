using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _HealthLink.App;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using Firebase.Database;
using LiteDB;


namespace _HealthLink.Model
{
    internal class _Appointment
    {
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public TimeOnly Time { get; set; }
        public string Department { get; set; }


        public async Task<bool> AddAppointment(string fullname, string email, string department, DateTime date, TimeOnly time)
        {
            try
            {
                var appointments = await client.Child("Appointments").OnceAsync<_Appointment>();

                var duplicateAppointment = appointments.FirstOrDefault(appointment =>
                    appointment.Object.Email == email ||
                    appointment.Object.Time == time ||
                    appointment.Object.Date == date
                );

                if (duplicateAppointment == null)
                {
                    var newAppointment = new _Appointment()
                    {
                        Fullname = fullname,
                        Email = email,
                        Date = date,
                        Time = time,
                        Department = department
                    };

                    await client.Child("Appointments").PostAsync(newAppointment);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> MovePendingAppointmentToApprovedAppointment(string fullname, string email, string department, DateTime date, TimeOnly time)
        {
            try
            {
                var evaluateEmail = (await client.Child("Appointments").OnceAsync<_Appointment>()).FirstOrDefault(a => a.Object.Email == email);
                if (evaluateEmail == null)
                {
                    var appointments = new _Appointment()
                    {
                        Fullname = fullname,
                        Email = email,
                        Date = date,
                        Time = time,
                        Department = department
                    };
                    await client
                        .Child("Appointments")
                        .PostAsync(appointments);
                    client.Dispose();
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch
            {

                return false;
            }
        }
        public ObservableCollection<_Appointment> GetAppointmentLists()
        {
            var appointmentLists = client.Child("Appointments").AsObservable<_Appointment>().AsObservableCollection();
            return appointmentLists;
        }
        public ObservableCollection<_Appointment> GetPendingAppointmentLists()
        {
            var PendingappointmentLists = client.Child("Pending Appointments").AsObservable<_Appointment>().AsObservableCollection();
            return PendingappointmentLists;
        }
     /*   public ObservableCollection<Appointment> GetCancelledAppointmentLists()
        {
            var CancelledappointmentLists = client
                .Child("Cancelled Appointments")
                .AsObservable<Appointment>()
                .AsObservableCollection();
            return CancelledappointmentLists;
        }
        
          public void MoveToCancelledAppointments()
         {
             var appointments = GetAppointmentLists();
             var cancelledAppointments = GetCancelledAppointmentLists();
             var currentDate = DateTime.Today;
             for (int i = appointments.Count - 1; i >= 0; i--)
             {
                 var appointmentDate = DateTime.Parse(appointments[i].Date);
                 if (appointmentDate < currentDate)
                 {
                     var appointmentToMove = appointments[i];
                     appointments.RemoveAt(i);
                     cancelledAppointments.Add(appointmentToMove);
                 }
             }
         }
         */
        public async Task<string> DeleteAppointment()
        {
            try
            {
                await client.Child($"Appointments/{key}").DeleteAsync();
                return "Removed";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
        public async Task<string> DeletePendingAppointment()
        {
            try
            {
                await client.Child($"Pending Appointments/{key}").DeleteAsync();
                return "Removed";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
        public async Task<string> GetAppointmentKey(string mail)
        {
            try
            {
                var getuserkey = (await client.Child("Appointments").OnceAsync<_Appointment>()).FirstOrDefault(a => a.Object.Email == mail);
                if (getuserkey == null) return null;
                date = getuserkey.Object.Date.Date;
                fullname = getuserkey.Object.Fullname;
                time = getuserkey.Object.Time;
                department = getuserkey.Object.Department;
                return getuserkey?.Key;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<string> GetPendingAppointmentKey(string mail)
        {
            try
            {
                var getuserkey = (await client.Child("Pending Appointments").OnceAsync<_Appointment>()).FirstOrDefault(a => a.Object.Email == mail);
                if (getuserkey == null) return null;
                date = getuserkey.Object.Date;
                fullname = getuserkey.Object.Fullname;
                time = getuserkey.Object.Time;
                department = getuserkey.Object.Department;
                return getuserkey?.Key;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<_Appointment>> GetAllData()
        {

            return (await client
                .Child("Appointments")
                .OnceAsync<_Appointment>()).Select(item => new _Appointment
                {
                    Fullname = item.Object.Fullname,
                    Email = item.Object.Email,
                    Time = item.Object.Time,
                    Date = item.Object.Date,
                    Department = item.Object.Department

                }).ToList();
        }
        public async Task<List<_Appointment>> FindAppointment(string fname)
        {
            try
            {
                var queryAppointments = await GetAllData();
                await client
                    .Child("Appointments")
                    .OnceAsync<_Appointment>();
                
                return queryAppointments.Where(a => string.Equals(a.Fullname, fname, StringComparison.CurrentCultureIgnoreCase)).ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<_Appointment>> FindPendingAppointment(string fname)
        {
            try
            {
                var queryAppointments = await GetAllData();
                await client
                    .Child("Pending Appointments")
                    .OnceAsync<_Appointment>();

                return queryAppointments.Where(a => string.Equals(a.Fullname, fname, StringComparison.CurrentCultureIgnoreCase)).ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}