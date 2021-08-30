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
        BuscaCepViewModel ViewModel { get => ((BuscaCepViewModel)BindingContext); }
        public MainPage()
        {
            InitializeComponent();

            // = new BuscaCepViewModel();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ViewModel.CEP))
                    return;

               
            }catch(Exception ex)
            {
               await DisplayAlert("Oops","Algo de errado aconteceu", "Ok");
            }
           
        }
    }

    public class ViaCepDto
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string unidade { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
        public bool erro { get; set; } = false;
    }
}
