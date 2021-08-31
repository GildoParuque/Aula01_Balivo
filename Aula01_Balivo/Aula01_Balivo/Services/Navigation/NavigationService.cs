using Aula01_Balivo.Pages;
using Aula01_Balivo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aula01_Balivo.Services.Navigation
{
    sealed class NavigationService
    {
        private static Lazy<NavigationService> _Lazy = new Lazy<NavigationService>(() => new NavigationService());
        private Dictionary<Type, Type> _Mapping;

        public static NavigationService Current { get => _Lazy.Value; }

        private NavigationService()
        {
            _Mapping = new Dictionary<Type, Type>();

            CreateViewModelMapping();
        }

        private void CreateViewModelMapping()
        {
            _Mapping.Add(typeof(CepsViewModel), typeof(CepsPage));
            _Mapping.Add(typeof(CepViewModel), typeof(CepPages));

        }

        private Application CurrentApplication => App.Current;
        private INavigation Navigation { 
            get => ((CustomNavigation)CurrentApplication.MainPage).Navigation; }

        internal Task Navigate<TViewModel>(object parameter = null) where TViewModel : BasePageViewModel => InternalNaviagate(typeof(TViewModel), parameter);

        private async Task InternalNaviagate(Type viewModelType, object parameter = null)
        {
            try
            {
                Page page = null;
                //Verificar se estou indo para pagina inicial
                if(viewModelType == typeof(CepsViewModel))
                {
                    if (!Navigation.NavigationStack.Any(lbda => lbda.BindingContext.GetType() == typeof(CepsViewModel)))
                    {
                        page = CreateAndBindPage(viewModelType);

                        var pagesToRemove = Navigation.NavigationStack.ToList();

                        await Navigation.PushAsync(page);

                        foreach(var pageToRemove in pagesToRemove)
                        {
                            Navigation.RemovePage(pageToRemove);
                        }
                    }
                    else
                    {
                       await GoBack(toRoot: true);
                    }
                }
                else
                {
                    page = CreateAndBindPage(viewModelType);

                    if (viewModelType.BaseType == typeof(BaseModalPageViewModel))
                        await Navigation.PushModalAsync(page, true);
                    else
                        await Navigation.PushAsync(page, true);
                }

                if (!(page is null))
                    await (page.BindingContext as BasePageViewModel).InitializeAsync(parameter);

            }catch(Exception ex)
            {
                throw;
            }
        }

        private Task GoBack(bool toRoot = false, bool animated = true)
        {
            if (toRoot)
                return Navigation.PopToRootAsync(animated);

            if (Navigation.ModalStack.Count > 0)
                return Navigation.PopModalAsync(animated);

            return Navigation.PopAsync(animated);
        }

        private Page CreateAndBindPage(Type viewModelType)
        {
            try
            {
                Type pageType = GetPageTypeForViewModel(viewModelType);

                if (pageType == null)
                    throw new Exception($"Mapping type for ${viewModelType} is not a page");

                Page page = Activator.CreateInstance(pageType) as Page;
                page.BindingContext = Activator.CreateInstance(viewModelType) as BasePageViewModel;

                return page;
           
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void  InitializeAsync(object args = null)
        {
            CurrentApplication.MainPage = new CustomNavigation();

            Device.BeginInvokeOnMainThread(async () => {
                await Navigate<CepsViewModel>(args);
            });
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            try
            {
                if (!_Mapping.ContainsKey(viewModelType))
                    throw new KeyNotFoundException($"No map for ${viewModelType} wa found on navigation mappings");
                return _Mapping[viewModelType];
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
