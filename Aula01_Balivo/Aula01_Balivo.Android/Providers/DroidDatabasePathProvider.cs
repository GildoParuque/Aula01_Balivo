using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Aula01_Balivo.Droid.Providers;
using Aula01_Balivo.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidDatabasePathProvider))]


namespace Aula01_Balivo.Droid.Providers
{
    public class DroidDatabasePathProvider : IDatabasePathProvider
    {
        public DroidDatabasePathProvider()
        {
                
        }

        public string GetPath() 
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BuscaCep");
        
    }
}