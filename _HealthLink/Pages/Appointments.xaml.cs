namespace _HealthLink.Pages;
using _HealthLink.Model;
using static _HealthLink.App;
public partial class AppointmentPage : ContentPage
{
    private _Appointment apptLists = new();

    public AppointmentPage()
    {
        InitializeComponent();
        appointments.ItemsSource = apptLists.GetAppointmentLists();
    }

    private async void appointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        App.email = (e.CurrentSelection.FirstOrDefault() as _Appointment)?.Email;
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

    private async void add_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddAppointmentPage());
    }
    /*
     
     */
    private async void searchEngine_SearchButtonPressed(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(searchEngine.Text))
        {
            appointments.ItemsSource = apptLists.GetAppointmentLists();
            return;
        }
        else
        {
            appointments.ItemsSource = await apptLists.FindAppointment(searchEngine.Text);
            return;
        }
    }

    private async void searchEngine_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(searchEngine.Text))
        {
            appointments.ItemsSource = apptLists.GetAppointmentLists();
            return;
        }
        else
        {
            appointments.ItemsSource = await apptLists.FindAppointment(searchEngine.Text);
            return;
        }
    }
}