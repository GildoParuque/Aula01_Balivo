using Aula01_Balivo.Data.Dtos;
using Aula01_Balivo.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aula01_Balivo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CepsPage : ContentPage
    {
        
        CepsViewModel ViewModel { get => (CepsViewModel)this.BindingContext; }
        public CepsPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            // await Navigation.PushAsync(new CepPages((CepDto)e.Item));

            //await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            ViewModel.SelecionarCommand.Execute(e.Item);

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    ViewModel.RefreshCommand.Execute(false);
        //}
    }
}
