using Firebase.Database;

namespace _HealthLink;

public partial class App : Application
{
    public static FirebaseClient client = new("https://lagrosa-dc2bc-default-rtdb.asia-southeast1.firebasedatabase.app/");
    public static string fullname, email, department, key;
    public static DateTime date;
    public static TimeOnly time;
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
