using DesktopContactsApp.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace DesktopContactsApp.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {



        public ContactModel Contact
        {
            get { return (ContactModel)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contact.  This enables animation, styling, binding, etc...
        // This will register our string (Contact) which is of type "ContactModel" what it's a type of (ContactControl) and define the method that will be executed when the property changes.
        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact", typeof(ContactModel), typeof(ContactControl), new PropertyMetadata(SetText));

        // The "Event handler" which sets our setText when the text is changed
        // d sets control as our Contact Control dependency object which will give us control of the textBlocks
        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactControl control = d as ContactControl;

            if (control!= null)
            {
                control.nameTextBlock.Text = (e.NewValue as ContactModel).Name; // Everytime the text is changed, the new value is assigned to Name
                control.emailTextBlock.Text = (e.NewValue as ContactModel).Email;
                control.phoneTextBlock.Text = (e.NewValue as ContactModel).Phone;
            }
        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}
