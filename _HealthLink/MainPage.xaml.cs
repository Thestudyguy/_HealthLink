namespace _HealthLink;
using _HealthLink.Pages;
public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    private async void pendingAppointments_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PendingAppointment());
    }

    private async void Appointments_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AppointmentPage());
    }
}

