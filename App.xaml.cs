
using SQLite;
using StudentApp.Services;

namespace StudentApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override async void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
