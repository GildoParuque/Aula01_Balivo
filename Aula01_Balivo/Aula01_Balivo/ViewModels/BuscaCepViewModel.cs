using Aula01_Balivo.Data.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aula01_Balivo.ViewModels
{
    public class BuscaCepViewModel : ViewModelBase
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

        private CepDto viaCepDto = null;

        public bool HasCep { get => !(viaCepDto is null); }
        public string Logradouro{get => viaCepDto?.logradouro; } 
        public string Complemento{get => viaCepDto?.complemento; }
        public string Bairro{ get => viaCepDto?.bairro;}
        public string Localidade{ get => viaCepDto?.localidade;}
        public string UF{ get => viaCepDto?.uf; }

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

                        viaCepDto = JsonConvert.DeserializeObject<CepDto>(content);

                        if (viaCepDto.erro)
                            throw new InvalidOperationException();

                       
                    }
                }
            }
            catch (Exception ex)
            {
                viaCepDto = null;
                await App.Current.MainPage.DisplayAlert("Oops", "Algo de errado aconteceu", "Ok");
            }
            finally
            {
                OnPropertyChanged(nameof(HasCep));
                OnPropertyChanged(nameof(Logradouro));
                OnPropertyChanged(nameof(Complemento));
                OnPropertyChanged(nameof(Bairro));
                OnPropertyChanged(nameof(Localidade));
                OnPropertyChanged(nameof(UF));
                isBusy = false;
                BuscarCommand.ChangeCanExecute();
            }
        }
    }
}
