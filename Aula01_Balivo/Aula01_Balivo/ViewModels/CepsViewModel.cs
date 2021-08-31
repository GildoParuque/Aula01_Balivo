using Aula01_Balivo.Data;
using Aula01_Balivo.Data.Dtos;
using Aula01_Balivo.Pages;
using Aula01_Balivo.Services.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aula01_Balivo.ViewModels
{
    sealed class CepsViewModel : BasePageViewModel
    {
        private string _CEP;
        public string CEP
        {
            get => _CEP;
            set
            {
                _CEP = value;
                OnPropertyChanged();
                BuscarCommand.ChangeCanExecute(); 
            }
        }


        private Command _BuscarCommand;
        public Command BuscarCommand
        {
            get
            {
                if (_BuscarCommand is null)
                    _BuscarCommand = new Command(async () => await BuscarCommandExecute(),() => BuscarCommandCanExecute());

                return _BuscarCommand;
            }
        }

        private bool BuscarCommandCanExecute() => !string.IsNullOrWhiteSpace(CEP) && CEP.Length >= 8 && IsNotBusy;


        //public Command BuscarCommand => _BuscarCommand ?? (_BuscarCommand = new Command(async () => await BuscarCommandExecute()));

        private async Task BuscarCommandExecute()
        {
             try
            {
                //if (string.IsNullOrWhiteSpace(CEP))
                //    return;
                if (isBusy)
                    return;

                if(MobileDatabaseService.Current.Get<CepDto>(lbda => lbda.cep.Equals(Regex.Replace(CEP,@"[^\d]", string.Empty))).Any())
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "O cep ja foi Pesquisado", "Ok");
                    return;
                }


                isBusy = true;
                BuscarCommand.ChangeCanExecute();

                using (var client = new HttpClient())
                {
                    //
                    using (var response = await client.GetAsync($"https://viacep.com.br/ws/{CEP}/json/"))
                    {
                        response.EnsureSuccessStatusCode();

                        var content = await response.Content.ReadAsStringAsync();

                        if (string.IsNullOrWhiteSpace(content))
                            throw new InvalidOperationException();

                        var cepDto = JsonConvert.DeserializeObject<CepDto>(content);

                        if (cepDto.erro)
                            throw new InvalidOperationException();

                      MobileDatabaseService.Current.Save(cepDto);

                        RefreshCommand.Execute(true); 
                       
                    }
                }
            }
            catch (Exception ex)
            {
                
                await App.Current.MainPage.DisplayAlert("Oops", "Algo de errado aconteceu", "Ok");
            }
            finally
            {
                isBusy = false;
                BuscarCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<CepDto> Ceps { get; private set; } = new ObservableCollection<CepDto>();

        private Command _RefreshCommand;
        public Command RefreshCommand
                => _RefreshCommand ?? (_RefreshCommand = new Command<bool>(
                    async (args) => await RefreshCommandExecute(args),
                    (args) => RefreshCommandCanExecute()
                    ));


        private bool RefreshCommandCanExecute() => IsNotBusy;


        //public Command BuscarCommand => _BuscarCommand ?? (_BuscarCommand = new Command(async () => await BuscarCommandExecute()));

        private async Task RefreshCommandExecute(bool force = false)
        {
            try
            {
                //if (string.IsNullOrWhiteSpace(CEP))
                //    return;
                if (!force && isBusy)
                    return;

                isBusy = true;
                BuscarCommand.ChangeCanExecute();

                Ceps.Clear();

                await Task.Factory.StartNew(() =>
                {
                    foreach(var cep in MobileDatabaseService.Current.Get<CepDto>())
                    {
                        Ceps.Add(cep);
                    }
                });
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Oops", "Algo de errado aconteceu", "Ok");
            }
            finally
            {
                isBusy = false;
                BuscarCommand.ChangeCanExecute();
            }
        }


        private Command _SelecionarCommand;
        public Command SelecionarCommand 
                => _SelecionarCommand ?? (_SelecionarCommand = new Command<Object>(
                    async (args) => await SelecionarCommandExecute(args)));

        public object Navigation { get; private set; }

        private Task SelecionarCommandExecute(Object dto) 
                => NavigationService.Current.Navigate<CepViewModel>(dto);

        internal override Task InitializeAsync(object parameter)
        => Task.Factory.StartNew(() => RefreshCommand.Execute(true));
    }
}
