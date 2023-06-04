namespace _HealthLink.Pages;
using _HealthLink.Model;
using static _HealthLink.App;
public partial class AppointmentPage : ContentPage
{
    private Appointment apptLists = new();

    public AppointmentPage()
	{
		InitializeComponent();
        appointments.ItemsSource = apptLists.GetAppointmentLists();
    }

    private async void appointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        App.email = (e.CurrentSelection.FirstOrDefault() as Appointment)?.Email;
        App.key = await apptLists.GetAppointmentKey(App.email);
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Information", "Are you sure you want to delete this?", "Yes", "No");
        if (result)
        {
            await apptLists.DeleteAppointment();
        }
    }
}
