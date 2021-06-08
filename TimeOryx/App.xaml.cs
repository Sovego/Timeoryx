using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeOryx
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDQyNzY0QDMxMzkyZTMxMmUzMFlxWHZPTFFHSVlObGdQVnpFdzlCdzIrWlNXQ3JIWTBLUmZoeWVVZEc1RDA9");
            InitializeComponent();
            
            MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
