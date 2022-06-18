using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StylesLibrary
{
    public static class Styles
    {
        public static Style brdImage = null;
        public static Style tbAll = null;
        public static Style tbStyle = null;

        public static void SetStyles()
        {
            ResourceDictionary resource = new ResourceDictionary();
            resource.Source = new Uri("/Pages/Styles.xaml", UriKind.Relative);

            tbAll = resource["tbAll"] as Style;
            tbStyle = resource["tbStyle"] as Style;
            brdImage = resource["brdImage"] as Style;
        }
    }
}
