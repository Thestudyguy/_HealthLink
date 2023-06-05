using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _HealthLink.App;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using Firebase.Database;


namespace _HealthLink.Model
{
    internal class Appointment
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
                var evaluateEmail = (await client.Child("Appointments").OnceAsync<Appointment>()).FirstOrDefault(a => a.Object.Email == email);
                if (evaluateEmail == null)
                {
                    var appointments = new Appointment()
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
        public ObservableCollection<Appointment> GetAppointmentLists()
        {
            var appointmentLists = client.Child("Appointments").AsObservable<Appointment>().AsObservableCollection();
            return appointmentLists;
        }
        public ObservableCollection<Appointment> GetPendingAppointmentLists()
        {
            var PendingappointmentLists = client.Child("Pending Appointments").AsObservable<Appointment>().AsObservableCollection();
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
                var getuserkey = (await client.Child("Appointments").OnceAsync<Appointment>()).FirstOrDefault(a => a.Object.Email == mail);
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
                var getuserkey = (await client.Child("Pending Appointments").OnceAsync<Appointment>()).FirstOrDefault(a => a.Object.Email == mail);
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
    }
}