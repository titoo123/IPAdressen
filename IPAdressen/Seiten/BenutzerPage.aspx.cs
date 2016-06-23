using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen.Seiten
{
    public partial class BenutzerPage : System.Web.UI.Page
    {
        static bool neu_benutzer;
        static bool neu_anwendung;

        static string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Status.Text = message;
            }
            if ( Session["BEid"] != null)
            {
                int bId = Convert.ToInt32( Session["BEid"].ToString());

                Benutzer b = Benutzer.GetBenutzerById(bId);

                TextBox_BenutzerBearbeiten_Nachname.Text = b.Name;
                TextBox_BenutzerBearbeiten_Vorname.Text = b.Vorname;
                
                try
                {
                    DropDownList_Anrede.SelectedValue = b.Anrede;
                    
                }
                catch (Exception)
                {
                    DropDownList_Anrede.SelectedValue = " ";

                }

                try
                {
                    DropDownList_Internet.SelectedValue = b.Internet;
                }
                catch (Exception)
                {
                    DropDownList_Internet.SelectedValue = "0";
                }

                Button_BenutzerBearbeiten_Bearbeiten.Enabled = true;

                GridView_BenutzerAnwendungen.DataSource = Anwendungen.GetAnwendungenByBenutzer(b);
                GridView_BenutzerAnwendungen.DataBind();
            }
        }

        //GridViews
        protected void GridView_Suchen_Benutzer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["BEid"] = GridView_Suchen_Benutzer.GetItemIDByGridView(1).X;

            TextBox_BenutzerBearbeiten_Nachname.Text = GridView_Suchen_Benutzer.Rows[GridView_Suchen_Benutzer.SelectedIndex].Cells[4].Text;

            if (GridView_Suchen_Benutzer.Rows[GridView_Suchen_Benutzer.SelectedIndex].Cells[3].Text != "&nbsp;")
            {
                TextBox_BenutzerBearbeiten_Vorname.Text = GridView_Suchen_Benutzer.Rows[GridView_Suchen_Benutzer.SelectedIndex].Cells[3].Text;
            }
            

            try
            {
                DropDownList_Anrede.SelectedValue = GridView_Suchen_Benutzer.Rows[GridView_Suchen_Benutzer.SelectedIndex].Cells[2].Text;
            }
            catch (Exception)
            {
                DropDownList_Anrede.SelectedValue = "";
            }

            try
            {
                DropDownList_Internet.SelectedValue = GridView_Suchen_Benutzer.Rows[GridView_Suchen_Benutzer.SelectedIndex].Cells[5].Text;
            }
            catch (Exception)
            {
                DropDownList_Internet.SelectedValue = "0";
            }

            if (GridView_Suchen_Benutzer.SelectedIndex != -1)
            {
                Button_BenutzerAnwendungen_Löschen.Enabled = true;
            }
            


            
            Button_BenutzerBearbeiten_Bearbeiten.Enabled = true;
            Button_BenutzerAnwendungen_Neu.Enabled = true;
            CheckUser();



        }
        protected void GridView_BenutzerAnwendungen_Suchen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button_BenutzerAnwendungen_Hinzufügen.Enabled = true;
            Session["hAid"] = GridView_BenutzerAnwendungen_Suchen.GetItemIDByGridView(1).X;
        }
        protected void GridView_BenutzerAnwendungen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Aid"] = GridView_BenutzerAnwendungen.GetItemIDByGridView(1).X;

            TextBox_BenutzerAnwendungen_Name.Text = GridView_BenutzerAnwendungen.Rows[GridView_BenutzerAnwendungen.SelectedIndex].Cells[2].Text;
            TextBox_BenutzerAnwendungen_Login.Text = GridView_BenutzerAnwendungen.Rows[GridView_BenutzerAnwendungen.SelectedIndex].Cells[3].Text;
            TextBox_BenutzerAnwendungen_Passwort.Text = GridView_BenutzerAnwendungen.Rows[GridView_BenutzerAnwendungen.SelectedIndex].Cells[4].Text;

            Button_BenutzerAnwendungen_Bearbeiten.Enabled = true;
            Button_BenutzerAnwendungen_Verbindungentfernen.Enabled = true;
            Button_BenutzerAnwendungen_Details.Enabled = true;

            //CheckUser();
        }

        //Benutzer
        protected void Button_Benutzer_Suchen_Click(object sender, EventArgs e)
        {
            DBaseDataContext db = new DBaseDataContext();
            var ben =
                from b in db.Benutzer
                where
                (b.Name == null || b.Name.Contains(TextBox_BenutzerSuchen_Nachname.Text))
             && (b.Vorname == null || b.Vorname.Contains(TextBox_BenutzerSuchen_Vorname.Text))
                select new { b.Id,b.Anrede, b.Vorname, b.Name, b.Internet };

            GridView_Suchen_Benutzer.DataSource = ben;
            GridView_Suchen_Benutzer.DataBind();
        }
        
        protected void Button_BenutzerBearbeiten_Neu_Click(object sender, EventArgs e)
        {
            neu_benutzer = true;
            Button_BenutzerBearbeiten_Speichern.Enabled = true;

            TextBox_BenutzerBearbeiten_Nachname.Enabled = true;
            TextBox_BenutzerBearbeiten_Vorname.Enabled = true;
       
        }
        protected void Button_BenutzerBearbeiten_Speichern_Click(object sender, EventArgs e)
        {
            Benutzer b;

            if (neu_benutzer)
            {
                b = new Benutzer() { Name = TextBox_BenutzerBearbeiten_Nachname.Text, Vorname = TextBox_BenutzerBearbeiten_Vorname.Text , 
                                     Internet = DropDownList_Internet.SelectedValue, Anrede= DropDownList_Anrede.SelectedValue };

                Benutzer.db.Benutzer.InsertOnSubmit(b);
                Benutzer.db.SubmitChanges();


                message = "Benutzer erfolgreich hinzugefügt!";
                //BenutzerGeräte bg = new BenutzerGeräte() { Id_Gerät = Convert.ToInt32(Session["id"]), Id_Benutzer = Benutzer.GetIdByBenutzer(b) };
                //Benutzer.db.BenutzerGeräte.InsertOnSubmit(bg);
            }
            else
            {
                b = Benutzer.GetBenutzerById(Convert.ToInt32(Session["BEid"]));
                b.Name = TextBox_BenutzerBearbeiten_Nachname.Text;
                b.Vorname = TextBox_BenutzerBearbeiten_Vorname.Text;
                b.Anrede = DropDownList_Anrede.SelectedValue;
                b.Internet = DropDownList_Internet.SelectedValue;

                message = "Benutzer erfolgreich bearbeitet!";

            }
            Benutzer.db.SubmitChanges();
            GridView_BenutzerAnwendungenRefresh();
        }
        protected void Button_BenutzerBearbeiten_Bearbeiten_Click(object sender, EventArgs e)
        {
            Benutzer b = Benutzer.GetBenutzerById(Convert.ToInt32(Session["BEid"]));
            b.Name = TextBox_BenutzerBearbeiten_Nachname.Text;
            b.Vorname = TextBox_BenutzerBearbeiten_Vorname.Text;

            b.Internet = DropDownList_Internet.SelectedValue;
            b.Anrede = DropDownList_Anrede.SelectedValue;

            TextBox_BenutzerBearbeiten_Nachname.Enabled = true;
            TextBox_BenutzerBearbeiten_Vorname.Enabled = true;

            DropDownList_Anrede.Enabled = true;
            DropDownList_Internet.Enabled = true;

            Button_BenutzerBearbeiten_Speichern.Enabled = true;
          
        }
        protected void Button_BenutzerBearbeiten_Löschen_Click(object sender, EventArgs e)
        {
            BenutzerGeräte.Delete_BG_From_DB(Convert.ToInt32(Session["BEid"]));
            Benutzer.db.Benutzer.DeleteOnSubmit(Benutzer.GetBenutzerById(Convert.ToInt32(Session["BEid"])));
            Benutzer.db.SubmitChanges();

        }

        //Anwendungen
        protected void Button_BenutzerAnwendungen_Suchen_Click(object sender, EventArgs e)
        {
            DBaseDataContext db = new DBaseDataContext();
            var anw =
                from a in db.Anwendungen
                where
                (a.Name == null || a.Name.Contains(TextBox_BenutzerAnwendungenSuchen_Name.Text))
             && (a.Login == null || a.Login.Contains(TextBox_BenutzerAnwendungenSuchen_Login.Text))
                select new { a.Id, a.Name, a.Login, a.Passwort };

            GridView_BenutzerAnwendungen_Suchen.DataSource = anw;
            GridView_BenutzerAnwendungen_Suchen.DataBind();
        }
    
        protected void Button_BenutzerAnwendungen_Hinzufügen_Click(object sender, EventArgs e)
        {
            BenutzerAnwendung ba = new BenutzerAnwendung() { Id_Benutzer = Convert.ToInt32( Session["BEid"]) , Id_Anwendung= Convert.ToInt32(Session["hAid"]) };
            BenutzerAnwendung.db.BenutzerAnwendung.InsertOnSubmit(ba);
            BenutzerAnwendung.db.SubmitChanges();

            GridView_BenutzerAnwendungenRefresh();
        }
        protected void Button_BenutzerAnwendungen_Neu_Click(object sender, EventArgs e)
        {
            neu_anwendung = true;
            Button_BenutzerAnwendungen_Speichern.Enabled = true;

            TextBox_BenutzerAnwendungen_Login.Enabled = true;
            TextBox_BenutzerAnwendungen_Name.Enabled = true;
            TextBox_BenutzerAnwendungen_Passwort.Enabled = true;
        }
        protected void Button_BenutzerAnwendungen_Speichern_Click(object sender, EventArgs e)
        {
            Anwendungen anw;
            if (neu_anwendung)
            {
                anw = new Anwendungen()
                {
                    Login = TextBox_BenutzerAnwendungen_Login.Text,
                    Name = TextBox_BenutzerAnwendungen_Name.Text,
                    Passwort = TextBox_BenutzerAnwendungen_Passwort.Text

                };

                Anwendungen.db.Anwendungen.InsertOnSubmit(anw);
                Button_BenutzerAnwendungen_Hinzufügen_Click(sender,e);
                message = "Anwendung erfolgreich hinzugefügt!";
            }
            else
            {
                anw = Anwendungen.GetAnwendungenById(Convert.ToInt32(Session["Aid"]));
                anw.Login = TextBox_BenutzerAnwendungen_Login.Text;
                anw.Name = TextBox_BenutzerAnwendungen_Name.Text;
                anw.Passwort = TextBox_BenutzerAnwendungen_Passwort.Text;
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

            GridView_BenutzerAnwendungenRefresh();


            TextBox_BenutzerAnwendungen_Login.Enabled = false;
            TextBox_BenutzerAnwendungen_Name.Enabled = false;
            TextBox_BenutzerAnwendungen_Passwort.Enabled = false;
        }
        protected void Button_BenutzerAnwendungen_Bearbeiten_Click(object sender, EventArgs e)
        {
            Anwendungen anw = Anwendungen.GetAnwendungenById(Convert.ToInt32(Session["Aid"]));

            TextBox_BenutzerAnwendungen_Login.Text = anw.Login;
            TextBox_BenutzerAnwendungen_Name.Text = anw.Name;
            TextBox_BenutzerAnwendungen_Passwort.Text = anw.Passwort;

            TextBox_BenutzerAnwendungen_Login.Enabled = true;
            TextBox_BenutzerAnwendungen_Name.Enabled = true;
            TextBox_BenutzerAnwendungen_Passwort.Enabled = true;

            Button_BenutzerAnwendungen_Speichern.Enabled = true;
            Button_BenutzerAnwendungen_Löschen.Enabled = true;

            Button_BenutzerAnwendungen_Details.Enabled = true;
            Button_BenutzerAnwendungen_Hinzufügen.Enabled = true;
         
        }
        protected void Button_BenutzerAnwendungen_Löschen_Click(object sender, EventArgs e)
        {
            Button_BenutzerAnwendungen_Verbindungentfernen_Click(sender, e);
            Anwendungen.db.Anwendungen.DeleteOnSubmit(Anwendungen.GetAnwendungenById(Convert.ToInt32(Session["Aid"])));
            Anwendungen.db.SubmitChanges();

            Session["Aid"] = 0;
            message = "Anwendung erfolgreich gelöscht!";
            GridView_BenutzerAnwendungenRefresh();
        }
        
        protected void Button_BenutzerAnwendungen_Details_Click(object sender, EventArgs e)
        {
            Server.Transfer("AnwendungenPage.aspx");
        }
        protected void Button_BenutzerAnwendungen_Verbindungentfernen_Click(object sender, EventArgs e)
        {
            BenutzerAnwendung.db.BenutzerAnwendung.DeleteAllOnSubmit( BenutzerAnwendung.GetBGByBIdAAid(Convert.ToInt32(Session["BEid"]),Convert.ToInt32(Session["Aid"])));
            BenutzerAnwendung.db.SubmitChanges();

            message = "Verbindung erfolgreich gelöscht!";
            GridView_BenutzerAnwendungenRefresh();
        }


        void CheckUser()
        {

            if (Session["BEid"] != null)
            {
                Button_BenutzerAnwendungen_Hinzufügen.Enabled = true;
            }
        }
        void GridView_BenutzerAnwendungenRefresh() {

            //Lade Anwendungen
            GridView_BenutzerAnwendungen.DataSource = Anwendungen.GetAnwendungenByBenutzer(Benutzer.GetBenutzerById(Session["BEid"].ToString()));
            GridView_BenutzerAnwendungen.DataBind();
        
        }


        protected void DropDownList_IstAnsprechpartner_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList_Anrede_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList_Internet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CleanPersons_Click(object sender, EventArgs e)
        {
            DBaseDataContext db = new DBaseDataContext();

            var p = from b in db.Benutzer
                    select b;

            foreach (Benutzer b in p.ToList())
            {
                if (b.Anrede != null)
                {
                    b.Anrede = b.Anrede.Trim();
                }
                if (b.Internet != null)
                {
                    b.Internet = b.Internet.Trim();
                }
                if (b.Name != null)
                {
                    b.Name = b.Name.Trim();
                }
                if (b.Vorname != null)
                {
                    b.Vorname = b.Vorname.Trim();
                }
                
                
                
            }
            db.SubmitChanges();
        }

       
    }
}