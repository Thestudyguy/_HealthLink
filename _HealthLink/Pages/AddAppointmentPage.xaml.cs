namespace _HealthLink.Pages;
using _HealthLink.Model;
using System.ComponentModel;
using System.Globalization;
using static _HealthLink.App;
public partial class AddAppointmentPage : ContentPage
{
    private _Appointment _appointment = new();
	public AddAppointmentPage()
	{
		InitializeComponent();
	}

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(FullNameEntry.Text) ||
            !string.IsNullOrEmpty(EmailEntry.Text) ||
            !string.IsNullOrEmpty(DepartmentEntry.Text) ||
            DateDatePicker.Date >= DateTime.Today.Date ||
            TimeTimePicker.Time >= DateTime.Now.TimeOfDay)
        {
            var timeConverter = new TimeOnlyConverter();
            TimeOnly selectedTime = (TimeOnly)timeConverter.ConvertFromInvariantString(TimeTimePicker.Time.ToString());
            var push = await _appointment.AddAppointment(FullNameEntry.Text, EmailEntry.Text, DepartmentEntry.Text, DateDatePicker.Date, selectedTime);
            try
            {
            if (push)
            {
                await DisplayAlert("Information", "Appointment Created", "Ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Information", "Something is wrong", "Ok");
            }
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Check Email, Date or Time there might be a duplicate", "Ok");
                client.Dispose();
                await Navigation.PopAsync();
            }

        }
    }


}