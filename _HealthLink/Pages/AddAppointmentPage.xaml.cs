namespace _HealthLink.Pages;
using _HealthLink.Model;
using static _HealthLink.App;
public partial class AddAppointmentPage : ContentPage
{
    private Appointment _Appointment = new();
	public AddAppointmentPage()
	{
		InitializeComponent();
	}

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
    }

}