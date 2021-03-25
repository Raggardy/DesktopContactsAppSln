using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLite;
using DesktopContactsApp.Classes;


namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ContactModel> contacts; // List is accessable from within the whole class
        public MainWindow()
        {
            InitializeComponent();

            

            contacts = new List<ContactModel>();

            ReadDatabase(); // As soon as the application is opened, the database is read
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog(); // Nothing will be returned until the window is closed
                                           // by clicking on the save button or closing it manually

            ReadDatabase(); // This will be called AFTER the window is closed.
                            // That way, after a new contact has been entered,
                            // the  updated table is immediately read
        }

        void ReadDatabase()
        {
            

            using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<ContactModel>();
                contacts = conn.Table<ContactModel>()
                    .ToList()
                    .OrderBy(c => c.Name)
                    .ToList();          

            }
            if (contacts != null) // If there is something IN the list, we can read it
            {
                contactsListView.ItemsSource = contacts; // This is the named element from xaml, "contactsListView. It's saying that the list needs to be populated by the source "contacts" . The display in the view will be whatever is inside contacts.
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TextBox is cast as the object sender. SearchTextBox will be pointing at the TextBox in our xaml code
            TextBox searchTextBox = (TextBox)sender;            

            // Filtered so that a certain string ( Name ) "contains" elements from another string ( Text )
            var filteredList = contacts
                .Where(c => c.Name.ToLower()
                .Contains(searchTextBox.Text.ToLower()))
                .ToList();

            // Alternative LINQ query syntax
            var filteredList2 = (from c2 in contacts
                                where c2.Name.ToLower().Contains(searchTextBox.Text.ToLower())
                                orderby c2.Email
                                select c2).ToList();

            // This time, the ItemsSource is the filtered list we just created.
            contactsListView.ItemsSource = filteredList;
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Whichever contact in the list view is clicked will be passed into the selecteditem
            ContactModel selectedContact = (ContactModel)contactsListView.SelectedItem; 

            // To ensure a contact is chosen, we can use the following code
            if (selectedContact != null)
            {
                ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedContact);
                contactDetailsWindow.ShowDialog();

                ReadDatabase();
            }
        }
    }
}
