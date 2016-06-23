using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen.Seiten
{
    public partial class AnwendungenPage : System.Web.UI.Page
    {
        static bool neu_anwendung;
        static string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Aid"]!=null)
                {
                   Anwendungen a = Anwendungen.GetAnwendungenById(Convert.ToInt32(Session["Aid"]));
                   TextBox_Anwendungen_Name.Text = a.Name ;
                   TextBox_Anwendungen_Login.Text = a.Login ;
                   TextBox_Anwendungen_Passwort.Text = a.Passwort ;

                   Button_Anwendungen_Bearbeiten.Enabled = true;
                }
            }

            Label_Anwendungen.Text = message;
        }

        protected void Button_Anwendungen_Suchen_Click(object sender, EventArgs e)
        {
            DBaseDataContext db = new DBaseDataContext();
            var anw =
                from a in db.Anwendungen
                where
                (a.Name == null || a.Name.Contains(TextBox_AnwendungenSuchen_Name.Text))
             && (a.Login == null || a.Login.Contains(TextBox_AnwendungenSuchen_Login.Text))
                select new { a.Id, a.Name, a.Login, a.Passwort };

            Anwendungen_Suchen_GridView.DataSource = anw;
            Anwendungen_Suchen_GridView.DataBind();
        }

        protected void Anwendungen_Suchen_GridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Aid"] = Anwendungen_Suchen_GridView.Rows[Anwendungen_Suchen_GridView.SelectedIndex].Cells[1].Text;

            TextBox_Anwendungen_Name.Text = Anwendungen_Suchen_GridView.Rows[Anwendungen_Suchen_GridView.SelectedIndex].Cells[2].Text;
            TextBox_Anwendungen_Login.Text = Anwendungen_Suchen_GridView.Rows[Anwendungen_Suchen_GridView.SelectedIndex].Cells[3].Text;
            TextBox_Anwendungen_Passwort.Text = Anwendungen_Suchen_GridView.Rows[Anwendungen_Suchen_GridView.SelectedIndex].Cells[4].Text;

            Button_Anwendungen_Bearbeiten.Enabled = true;
        }

        protected void Button_Anwendungen_Neu_Click(object sender, EventArgs e)
        {
            TextBox_Anwendungen_Name.Enabled = true;
            TextBox_Anwendungen_Login.Enabled = true;
            TextBox_Anwendungen_Passwort.Enabled = true;

            neu_anwendung = true;
            Button_Anwendungen_Speichern.Enabled = true;
        }
        protected void Button_Anwendungen_Speichern_Click(object sender, EventArgs e)
        {
            Anwendungen anw;
            if (neu_anwendung)
            {
                anw = new Anwendungen()
                {
                    Login = TextBox_Anwendungen_Login.Text,
                    Name = TextBox_Anwendungen_Name.Text,
                    Passwort = TextBox_Anwendungen_Passwort.Text
                 
                };

                Anwendungen.db.Anwendungen.InsertOnSubmit(anw);
                message = "Anwendung erfolgreich hinzugefügt!";
            }
            else
            {
                anw = Anwendungen.GetAnwendungenById(Convert.ToInt32(Session["Aid"]));
                anw.Login = TextBox_Anwendungen_Login.Text;
                anw.Name = TextBox_Anwendungen_Name.Text;
                anw.Passwort = TextBox_Anwendungen_Passwort.Text;
                message = "Anwendung erfolgreich bearbeitet!";
            }

            try
            {
                Anwendungen.db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }

            Refresh();


            TextBox_Anwendungen_Login.Enabled = false;
            TextBox_Anwendungen_Name.Enabled = false;
            TextBox_Anwendungen_Passwort.Enabled = false;

        }
        protected void Button_Anwendungen_Bearbeiten_Click(object sender, EventArgs e)
        {
            Anwendungen anw = Anwendungen.GetAnwendungenById(Convert.ToInt32(Session["Aid"]));

            TextBox_Anwendungen_Login.Text = anw.Login;
            TextBox_Anwendungen_Name.Text = anw.Name;
            TextBox_Anwendungen_Passwort.Text = anw.Passwort;

            TextBox_Anwendungen_Login.Enabled = true;
            TextBox_Anwendungen_Name.Enabled = true;
            TextBox_Anwendungen_Passwort.Enabled = true;

            Button_Anwendungen_Speichern.Enabled=true;
           
        }
        protected void Button_Anwendungen_Löschen_Click(object sender, EventArgs e)
        {
            Anwendungen.db.Anwendungen.DeleteOnSubmit(Anwendungen.GetAnwendungenById(Convert.ToInt32(Session["Aid"])));
            Anwendungen.db.SubmitChanges();
            
            Session["Aid"] = 0;
            message = "Anwendung erfolgreich gelöscht!";
            Refresh();

        }

        private void Refresh()
        {
            Server.Transfer("AnwendungenPage.aspx");
        }
    }
}