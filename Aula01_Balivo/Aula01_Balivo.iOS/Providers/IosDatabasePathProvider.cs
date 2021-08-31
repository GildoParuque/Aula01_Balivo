using Aula01_Balivo.iOS.Providers;
using Aula01_Balivo.Providers;
using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosDatabasePathProvider))]


namespace Aula01_Balivo.iOS.Providers
{
    public class IosDatabasePathProvider: IDatabasePathProvider
    {
        public IosDatabasePathProvider()
        {
                
        }
        public string GetPath()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library", "Databases");

            if (Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return Path.Combine(folder, "BuscaCep");
        }
    }
}