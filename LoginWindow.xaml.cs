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
using System.Windows.Shapes;

namespace Raps
{
    
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private int attempts = 0; // Variable, um die Anzahl der Versuche zu zählen
        private const int maxAttempts = 3; // Maximale Anzahl der erlaubten Versuche

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {


            if (passwordBox.Password == "1234")
            {
                MessageBox.Show("Login erfolgreich!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                attempts++; // Anzahl der Versuche erhöhen
                if (attempts >= maxAttempts)
                {
                    MessageBox.Show("Zu viele fehlgeschlagene Versuche! Der Zugang ist gesperrt.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close(); // Fenster schließen oder andere Sperrmaßnahmen ergreifen
                }
                else
                {
                    MessageBox.Show($"Falsches Passwort! Versuche verbleibend: {maxAttempts - attempts}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    passwordBox.Clear();
                }
            }



        }
    }
}
