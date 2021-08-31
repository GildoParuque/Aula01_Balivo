using Aula01_Balivo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aula01_Balivo
{
    public partial class MainPage : ContentPage
    {
       // BuscaCepViewModel ViewModel { get => ((BuscaCepViewModel)BindingContext); }
        public MainPage()
        {
            InitializeComponent();

            // = new BuscaCepViewModel();
        }
        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //    if (string.IsNullOrWhiteSpace(ViewModel.CEP))
        //    //        return;

               
        //    //}catch(Exception ex)
        //    //{
        //    //   await DisplayAlert("Oops","Algo de errado aconteceu", "Ok");
        //    //}
           
       // }
    }

   
}
