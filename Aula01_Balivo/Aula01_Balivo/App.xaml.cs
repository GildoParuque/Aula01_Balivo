using Aula01_Balivo.Pages;
using Aula01_Balivo.Services.Navigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aula01_Balivo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ///MainPage = new BuscaCepPage();
            //MainPage = new CustomNavigation(new CepsPage());

            NavigationService.Current.InitializeAsync();

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
