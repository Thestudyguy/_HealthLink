using _HealthLink.Model;
using System.ComponentModel;
namespace _HealthLink.Pages;
using static _HealthLink.App;

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
        _TimeTimePicker = time;
    }



    private async void moveAppointment_Clicked(object sender, EventArgs e)
    {

        var timeConverter = new TimeOnlyConverter();
        TimeOnly selectedTime = (TimeOnly)timeConverter.ConvertFromInvariantString(_TimeTimePicker.Time.ToString());
        var push = await apptlists.AddAppointment(_FullNameEntry.Text, _EmailEntry.Text, _DepartmentEntry.Text, _DateDatePicker.Date, selectedTime);

        if (push)
            {
                await DisplayAlert("Information", "Appointment Moved", "Ok");
                await Navigation.PopAsync();
                
                await Task.Delay(500);
                
            await apptlists.DeleteAppointment();

        }
        else
            {
                await DisplayAlert("Information", "Something is wrong", "Ok");
            }
    }
}
