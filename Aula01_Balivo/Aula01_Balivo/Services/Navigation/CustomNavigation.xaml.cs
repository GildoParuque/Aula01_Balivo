using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aula01_Balivo.Services.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomNavigation : NavigationPage
    {
        public CustomNavigation()
        {
            InitializeComponent();
        }

        public CustomNavigation(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
} 