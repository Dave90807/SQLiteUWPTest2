using SQLite.Net;
using SQLite;
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
using Windows.Storage;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
namespace Test3
{
    // An empty page that can be used on its own or navigated to within a Frame.
    // This examples comes from:  https://social.msdn.microsoft.com/Forums/windowsapps/en-US/c9cb96e0-74f7-453f-bd4a-2fcad68539e5/uwphelp-needed-windows-10-ubiversal-app-display-sqlitenet-records-in-a-list-box?forum=wpdevelop
    public sealed partial class MainPage : Page  // Original "...class SampleDemo : Page
    {
        public List<User> users = new List<User>();
        public string DBPath { get; set; }
        public int CurrentCustomerId { get; set; }
        public MainPage()  // Original " ...SampleDemo()
        {
            this.InitializeComponent();
            InitialData();
            BindindListview();
        }
        private void buttonDelCreateTable_Click(object sender, RoutedEventArgs e)
        {
            // Get a reference to the SQLite database
            this.DBPath = Path.Combine(
               Windows.Storage.ApplicationData.Current.LocalFolder.Path, "user.sqlite");
            // Initialize (Open w New Connection) the database if necessary
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DBPath))
            {
                // Remove Existing Table
                db.DropTable<User>();
                // Create and Populate new table
                InitialData();
                users = db.Table<User>().ToList();
            }
            this.LV1.ItemsSource = users;
        }
     private void buttonDelRow_Click(object sender, RoutedEventArgs e)
    {
        // Get a reference to the SQLite database
        this.DBPath = Path.Combine(
           Windows.Storage.ApplicationData.Current.LocalFolder.Path, "user.sqlite");
        // Initialize (Open w New Connection) the database if necessary
        var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DBPath);
        using (db)
        {
            // Remove Specific Record in Table If Exists
            db.Execute("DELETE FROM[User] WHERE UserName = 'Contoso'");
            users = db.Table<User>().ToList();
        }
        this.LV1.ItemsSource = users;
    }
    private async void buttonOpenDB_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder local_CacheFolder = ApplicationData.Current.LocalCacheFolder;
            await Windows.System.Launcher.LaunchFolderAsync(local_CacheFolder);
        }
        private void BindindListview()
        {
            this.DBPath = Path.Combine(
               Windows.Storage.ApplicationData.Current.LocalFolder.Path, "user.sqlite");
            // Initialize the database if necessary
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DBPath))
            {
                users = db.Table<User>().ToList();
            }
            this.LV1.ItemsSource = users;
        }
        private void InitialData()

        {
            // Get a reference to the SQLite database
            this.DBPath = Path.Combine(
                Windows.Storage.ApplicationData.Current.LocalFolder.Path, "user.sqlite");
            // Initialize the database if necessary
            using (var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DBPath))
            {
                // Create the tables if they don't exist
                db.CreateTable<User>();
                db.Insert(new User()
                {
                   // UserID = null,
                    UserName = "Adventure Works"
                });
                db.Insert(new User()
                {
                   // UserID = 2,
                    UserName = "Contoso"
                });
                db.Insert(new User()
                {
                   // UserID = 3,
                    UserName = "Fabrikam"
                });
                db.Insert(new User()
                {
                   // UserID = 4,
                    UserName = "Tailspin Toys"
                });
            }
        }
        public class User
        {
            [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
            public int UserID { get; set; }
            public string UserName { get; set; }
        }

        private void tbxDelName_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            // Get a reference to the SQLite database
            this.DBPath = Path.Combine(
               Windows.Storage.ApplicationData.Current.LocalFolder.Path, "user.sqlite");
            // Initialize (Open w New Connection) the database if necessary
            var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DBPath);
            using (db)
            {
                // Remove Specific Record in Table If Exists
                db.Execute("DELETE FROM[User] WHERE UserName = '" + tbxDelName.Text + "'");
                users = db.Table<User>().ToList();
                tbxDelName.Text = "";
            }
            this.LV1.ItemsSource = users;
        }
        private void tbxAddName_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            // Get a reference to the SQLite database
            this.DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "user.sqlite");
            // Initialize (Open w New Connection) the database if necessary
            var db = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DBPath);
            using (db)
            {
                // Add Record in Table If Exists
                // Create SQLite Query String
                string sqlQStrng = "INSERT INTO User (UserName) VALUES ('" + tbxAddName.Text + "')";
                db.Execute(sqlQStrng);
                users = db.Table<User>().ToList();
                tbxAddName.Text = "";
            }
            this.LV1.ItemsSource = users;
        }
    }
}

