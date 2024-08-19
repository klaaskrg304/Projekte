using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media;
using System.Configuration;

namespace Raps
{
    public partial class MainWindow : Window
    {
        private Sponsors sponsorsManager;
        private SponsorData selectedSponsor;

        public ObservableCollection<SponsorData> Sponsors { get; set; }

        public MainWindow()
        {
            InitializeComponent();
           

            sponsorsManager = new Sponsors();

            LoginWindow loginWindow = new LoginWindow();

            

            

           
         
                if (loginWindow.ShowDialog() == true)
                {
                    LoadSponsorsFromDatabase();
                }
                else
                {
                    
                    Application.Current.Shutdown();
                    
                
                
                   
            }

                    
            
        }

        private void LoadSponsorsFromDatabase()
        {
            try
            {
                Sponsors = sponsorsManager.LoadSponsors();
                dataGrid_sponsoren.ItemsSource = Sponsors;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Daten: {ex.Message}");
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SponsorData newSponsor = new SponsorData
                {
                    Firma = tbx_Firma.Text,
                    Ansprechpartner = tbx_anspr.Text,
                    Adresse = tbx_adress.Text,
                    Betrag = double.Parse(tbx_betrag.Text)
                };

                sponsorsManager.AddSponsor(newSponsor);
                LoadSponsorsFromDatabase();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Einfügen der Daten: {ex.Message}");
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSponsor != null)
            {
                try
                {
                    sponsorsManager.DeleteSponsor(selectedSponsor.Firma);
                    LoadSponsorsFromDatabase();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Löschen der Daten: {ex.Message}");
                }
            }
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSponsor != null)
            {
                try
                {
                    SponsorData updatedSponsor = new SponsorData
                    {
                        Firma = tbx_Firma.Text,
                        Ansprechpartner = tbx_anspr.Text,
                        Adresse = tbx_adress.Text,
                        Betrag = double.Parse(tbx_betrag.Text)
                    };

                    sponsorsManager.UpdateSponsor(updatedSponsor, selectedSponsor.Firma);
                    LoadSponsorsFromDatabase();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Aktualisieren der Daten: {ex.Message}");
                }
            }
        }

      

       
        private void btn_print_Click_1(object sender, RoutedEventArgs e)
        {
            PrintHelper printHelper = new PrintHelper(dataGrid_sponsoren);
            List<SponsorData> sponsorList = printHelper.ExtractDataFromDataGrid();
            FixedDocument document = printHelper.CreatePrintableDocument(sponsorList);

            PrintPreviewWindow previewWindow = new PrintPreviewWindow(document);
            previewWindow.ShowDialog();
        }

        private void dataGrid_sponsoren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_sponsoren.SelectedItem is SponsorData sponsor)
            {
                selectedSponsor = sponsor;
                tbx_Firma.Text = sponsor.Firma;
                tbx_anspr.Text = sponsor.Ansprechpartner;
                tbx_adress.Text = sponsor.Adresse;
                tbx_betrag.Text = sponsor.Betrag.ToString();
            }
        }

        private void btn_betragaddieren_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double totalAmount = sponsorsManager.GetTotalAmount();
                string formattedAmount = totalAmount.ToString("N2", CultureInfo.GetCultureInfo("de-DE")) + " €";
                MessageBox.Show($"Die Gesamtsumme der Spenden beträgt: {formattedAmount}");
                tbx_sum.Text = formattedAmount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Berechnen der Gesamtsumme: {ex.Message}");
            }
        }

        private void ClearInputFields()
        {
            tbx_Firma.Clear();
            tbx_anspr.Clear();
            tbx_adress.Clear();
            tbx_betrag.Clear();
            selectedSponsor = null;
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
        }

        private void btn_umzug_Click(object sender, RoutedEventArgs e)
        {
            Window_umzug window_umzug = new Window_umzug();
            window_umzug.ShowDialog();
        }

        private void btn_umzug_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btn_mailmerge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_mailmerge_main_Click(object sender, RoutedEventArgs e)
        {
            
        
    }

        private void btn_mailmerge_main_DragOver(object sender, DragEventArgs e)
        {
           
        }

        private void btn_mailmerge_main_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private void btn_mailmerge_main_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
        }

        private void btn_mailmerge_main_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
        }

        private void btn_brief_main_Click(object sender, RoutedEventArgs e)
        {
            var application = new Microsoft.Office.Interop.Word.Application();
            var document = new Microsoft.Office.Interop.Word.Document();

            document = application.Documents.Add(Template: @"C:\\Users\\Klaas\\Desktop\\Briefvorlage Sternberg und MEHR e.V..docx");

            application.Visible = true;

            foreach (Microsoft.Office.Interop.Word.Field field in document.Fields)
            {
                if (field.Code.Text.Contains("Firma"))
                {
                    field.Select();
                    application.Selection.TypeText(tbx_Firma.Text);
                }

                else if (field.Code.Text.Contains("Name"))
                {
                    field.Select();
                    application.Selection.TypeText(tbx_anspr.Text);
                }

                else if (field.Code.Text.Contains("Adresse"))
                {
                    field.Select();
                    application.Selection.TypeText(tbx_adress.Text);
                }

            }

            document.SaveAs2(FileName: @"C:\\Users\\Klaas\\Desktop\\Testbrief.docx");
            document.Close();

            application.Quit();
        }

        private void sub_mail_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
        }

        
    }
}

public class ParticipantsData
{
    public int Position { get; set; }

    public string Firma { get; set; }
    public string Ansprechpartner { get; set; }
    public string Adresse { get; set; }
}   





public class SponsorData
    {
        public string Firma { get; set; }
        public string Ansprechpartner { get; set; }
        public string Adresse { get; set; }
        public double Betrag { get; set; }
    }



    

