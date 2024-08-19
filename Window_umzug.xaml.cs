using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Office.Interop.Word;

namespace Raps
{
    /// <summary>
    /// Interaktionslogik für Window_umzug.xaml
    /// </summary>
    public partial class Window_umzug : System.Windows.Window
    {
        private participants participantsManager;
        private ParticipantsData selectedParticipant;

        public ObservableCollection<ParticipantsData> Participants { get; set; }

        public Window_umzug()
        {
            InitializeComponent();
            participantsManager = new participants();
            LoadParticipantsFromDatabase();
        }

        private void LoadParticipantsFromDatabase()
        {
            try
            {
                Participants = participantsManager.Loadparticipants();
                dataGrid_umzug.ItemsSource = Participants;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Daten: {ex.Message}");
            }
        }

        private void dataGrid_umzug_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_umzug.SelectedItem is ParticipantsData participant)
            {
                selectedParticipant = participant;
                tbx_Firma.Text = participant.Firma;
                tbx_anspr.Text = participant.Ansprechpartner;
                tbx_adress.Text = participant.Adresse;
                tbx_pos.Text = participant.Position.ToString();
            }
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {

            PrintHelper printHelper = new PrintHelper(dataGrid_umzug);
            List<SponsorData> participantslist = printHelper.ExtractDataFromDataGrid();
            FixedDocument document = printHelper.CreatePrintableDocument(participantslist);

            PrintPreviewWindow previewWindow = new PrintPreviewWindow(document);
            previewWindow.ShowDialog();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedParticipant != null)
            {
                try
                {
                    participantsManager.DeleteParticipant(selectedParticipant.Firma);
                    LoadParticipantsFromDatabase();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Löschen der Daten: {ex.Message}");
                }
            }
        }

        private void ClearInputFields()
        {
            tbx_Firma.Clear();
            tbx_anspr.Clear();
            tbx_adress.Clear();
            tbx_pos.Clear();
            selectedParticipant = null;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ParticipantsData newParticipant = new ParticipantsData
                {
                    Firma = tbx_Firma.Text,
                    Ansprechpartner = tbx_anspr.Text,
                    Adresse = tbx_adress.Text,
                    Position = int.Parse(tbx_pos.Text)
                };

                participantsManager.AddParticipant(newParticipant);
                LoadParticipantsFromDatabase();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Einfügen der Daten: {ex.Message}");
            }
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            if (selectedParticipant != null)
            {
                try
                {
                    ParticipantsData updatedParticipant = new ParticipantsData
                    {
                        Firma = tbx_Firma.Text,
                        Ansprechpartner = tbx_anspr.Text,
                        Adresse = tbx_adress.Text,
                        Position = int.Parse(tbx_pos.Text)
                    };

                    participantsManager.UpdateParticipant(updatedParticipant, selectedParticipant.Firma);
                    LoadParticipantsFromDatabase();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Aktualisieren der Daten: {ex.Message}");
                }
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
        }

        private void btn_planung_Click(object sender, RoutedEventArgs e)
        {
            // Logik für die Planung hinzufügen
        }

        private void btn_kosten_Click(object sender, RoutedEventArgs e)
        {
            // Logik für die Kosten hinzufügen
        }

        private void btn_print_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_mailmerge_Click(object sender, RoutedEventArgs e)
        {
            var application = new Microsoft.Office.Interop.Word.Application();
            var document = new Microsoft.Office.Interop.Word.Document();

            document = application.Documents.Add(Template: @"C:\\Users\\Klaas\\Desktop\\Briefvorlage Sternberg und MEHR e.V..docx");

            application.Visible = true;

            foreach(Microsoft.Office.Interop.Word.Field field  in document.Fields)
            {
                if(field.Code.Text.Contains("Firma"))
                {
                    field.Select();
                    application.Selection.TypeText(tbx_Firma.Text);
                }

                else if(field.Code.Text.Contains("Name"))
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
    }
}
