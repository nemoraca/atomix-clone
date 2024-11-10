using System.Windows;

namespace AtomixClone
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ColourConverter ColourConverter { get; } = new ColourConverter();

        public static T[] Ar<T>(params T[] ar)
        {
            return ar;
        }
    }
}
