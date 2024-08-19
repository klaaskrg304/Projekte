using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace Rapsbluete
{
    internal class sponsoren
    {
        DBConnect connect = new DBConnect();

        //Sponsoren einfügen

        public bool insert_sponsor(string nachname, string vorname, string adresse, string telefonnummer, string betrag)
        {
            try
            {
                // Öffnen der Verbindung
                connect.open_connection();

                // Finde die höchste ID
                int maxID = 0;
                using (NpgsqlCommand getMaxIDCommand = new NpgsqlCommand("SELECT COALESCE(MAX(\"ID\"), 0) FROM tbl_sponsoren", connect.getConnection))
                {
                    maxID = (int)getMaxIDCommand.ExecuteScalar();
                }

                // Neue ID ist maxID + 1
                int newID = maxID + 1;

                // Füge die neue Zeile ein
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO tbl_sponsoren(\"ID\", \"Nachname\", \"Vorname\", \"Adresse\", \"Telefonnummer\", \"Betrag\") VALUES (@ID, @Nachname, @Vorname, @Adresse, @Telefonnummer, @Betrag)", connect.getConnection);

                command.Parameters.AddWithValue("ID", newID);
                command.Parameters.AddWithValue("Nachname", nachname);
                command.Parameters.AddWithValue("Vorname", vorname);
                command.Parameters.AddWithValue("Adresse", adresse);
                command.Parameters.AddWithValue("Telefonnummer", telefonnummer);
                command.Parameters.AddWithValue("Betrag", betrag);

                if (command.ExecuteNonQuery() == 1)
                {
                    connect.close_connection();
                    return true;
                }
                else
                {
                    connect.close_connection();
                    return false;
                }
            }
            catch (Npgsql.PostgresException ex)
            {
                MessageBox.Show("Fehler beim Hinzufügen: " + ex.Message);
                connect.close_connection();
                return false;
            }
        }


        public bool delete_sponsor(int id)
        {

            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM tbl_sponsoren WHERE \"ID\" = @ID", connect.getConnection);
            cmd.Parameters.AddWithValue("@ID", id);

            connect.open_connection();

            if (cmd.ExecuteNonQuery() == 1)
            {
                connect.close_connection();

                return true;
            }

            else
            {
                connect.close_connection();

                return false;


            }
        }

        public DataTable getsponsorenlist()
        {
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM tbl_sponsoren", connect.getConnection);

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);

            DataTable table = new DataTable();

            dataAdapter.Fill(table);

            return table;
        }

        public void RenumberIDs()
        {
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT renumber_ids()", connect.getConnection))
                {
                    connect.open_connection(); // Verbindung öffnen
                    command.ExecuteNonQuery();
                }
            }
            catch (Npgsql.PostgresException ex)
            {
                // Behandeln des speziellen PostgreSQL-Fehlers
                MessageBox.Show("Error renumbering IDs: " + ex.Message);
            }
            finally
            {
                connect.close_connection(); // Verbindung schließen, unabhängig vom Ergebnis
            }
        }

       public bool UpdateSponsor(int id, string nachname, string vorname, string adresse, string telefonnummer, string betrag)
        {
            try
            {

                {
                    string query = "UPDATE tbl_sponsoren SET \"Nachname\" = @Nachname, \"Vorname\" = @Vorname, \"Adresse\" = @Adresse, \"Telefonnummer\" = @Telefonnummer, \"Betrag\" = @Betrag WHERE \"ID\" = @ID";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connect.getConnection))
                    {
                        command.Parameters.AddWithValue("Nachname", nachname);
                        command.Parameters.AddWithValue("Vorname", vorname);
                        command.Parameters.AddWithValue("Adresse", adresse);
                        command.Parameters.AddWithValue("Telefonnummer", telefonnummer);
                        command.Parameters.AddWithValue("Betrag", betrag);
                        command.Parameters.AddWithValue("ID", id);

                        connect.open_connection();

                        int rowsAffected = command.ExecuteNonQuery();

                        connect.close_connection();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Npgsql.PostgresException ex)
            {
                MessageBox.Show("Fehler beim Aktualisieren der Daten: " + ex.Message);
                return false;
            }
        }
    }
}
