using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DesktopContactsApp.Classes;
using SQLite;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            ContactModel contact = new ContactModel()
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                Phone = phoneTextBox.Text
            };

            // Using statement implements the IDisposable interface which automatically disposes of the object (connection) after the code leaves the block

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))   
                                                                                      
            {
                connection.CreateTable<ContactModel>();
                connection.Insert(contact);
            }            
            
            Close();
        }
                
    }
}
