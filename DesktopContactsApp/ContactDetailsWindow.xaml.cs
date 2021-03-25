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
    /// Interaction logic for ContactDetailsWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        ContactModel contact;
        public ContactDetailsWindow(ContactModel contact) // Whenever we open a ContactDetailsWindow, we will need to pass a Contact
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            this.contact = contact;
            nameTextBox.Text = contact.Name;
            emailTextBox.Text = contact.Email;
            phoneTextBox.Text = contact.Phone;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            contact.Name = nameTextBox.Text;
            contact.Email = emailTextBox.Text;
            contact.Phone = phoneTextBox.Text;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))

            {
                connection.CreateTable<ContactModel>();
                connection.Update(contact);
            }
            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))

            {
                connection.CreateTable<ContactModel>();
                connection.Delete(contact);
            }
            Close();
        }
    }
}
