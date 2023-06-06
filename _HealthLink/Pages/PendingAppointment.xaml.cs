using _HealthLink.Model;
namespace _HealthLink.Pages;

public partial class PendingAppointment : ContentPage
{
    private _Appointment apptLists = new();

    public PendingAppointment()
	{
		InitializeComponent();
        pendingAppointments.ItemsSource = apptLists.GetPendingAppointmentLists();

    }

    private async void pendingAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        App.email = (e.CurrentSelection.FirstOrDefault() as _Appointment)?.Email;
        App.key = await apptLists.GetPendingAppointmentKey(App.email);
    }

    private async void DeletePending_Clicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Information", "Are you sure you want to delete this?", "Yes", "No");
        if (result)
        {
            await apptLists.DeletePendingAppointment();
        }
    }

    private async void SearchEngine_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(SearchEngine.Text))
        {
            pendingAppointments.ItemsSource = apptLists.GetPendingAppointmentLists();
            return;
        }
        else
        {
            pendingAppointments.ItemsSource = await apptLists.FindPendingAppointment(SearchEngine.Text);
            return;
        }
    }
}