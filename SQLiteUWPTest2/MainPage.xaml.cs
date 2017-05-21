using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
// This sample from: https://code.msdn.microsoft.com/windowsapps/Implement-SQLite-Local-8b13a307 

namespace SQLiteUWPTest2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void buttonCreateDB_Click(object sender, RoutedEventArgs e)
        {
            LocalDatabase.CreateDatabase();
        }
    }
    public class LocalDatabase
    {
        public static void CreateDatabase()
        {
            var sqlpath = System.IO.Path.Combine
            (Windows.Storage.ApplicationData.Current.LocalFolder.Path,
            "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new
                SQLite.Net.SQLiteConnection(new
                SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.CreateTable<Contact>();
            }
        }
    }
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
    }
}
