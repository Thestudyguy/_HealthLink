using _HealthLink.Model;
using System.ComponentModel;
namespace _HealthLink.Pages;
using static _HealthLink.App;
using System.Threading;

public partial class MoveCreateDeleteAppointment : ContentPage
{
	private _Appointment apptlists = new();
    public MoveCreateDeleteAppointment()
    {
        InitializeComponent();
    }

        protected override void OnAppearing()
    {
        base.OnAppearing();
        _FullNameEntry.Text = fullname;
        _EmailEntry.Text = email;
        _DepartmentEntry.Text = department;
        _DateDatePicker.Date = date;
    }



    private async void moveAppointment_Clicked(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(_FullNameEntry.Text) ||
             !string.IsNullOrEmpty(_EmailEntry.Text) ||
             !string.IsNullOrEmpty(_DepartmentEntry.Text) ||
             _DateDatePicker.Date >= DateTime.Today.Date ||
             _TimeTimePicker.Time >= DateTime.Now.TimeOfDay)
        {
            var timeConverter = new TimeOnlyConverter();
            TimeOnly selectedTime = (TimeOnly)timeConverter.ConvertFromInvariantString(_TimeTimePicker.Time.ToString());
            var push = await apptlists.AddAppointment(_FullNameEntry.Text, _EmailEntry.Text, _DepartmentEntry.Text, _DateDatePicker.Date, selectedTime);

            if (push)
            {
                await DisplayAlert("Information", "Appointment Moved", "Ok");
                await Navigation.PopAsync();

               await Task.Delay(1000);
               await apptlists.DeletePendingAppointment();
            }
            else
            {
                await DisplayAlert("Information", "Something is wrong", "Ok");
            }
        }
    }
}
