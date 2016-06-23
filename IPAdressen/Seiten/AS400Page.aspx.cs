using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    public partial class AS400Page : System.Web.UI.Page
    {
        static bool neu_asg;
        static bool neu_asb;
        string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IPid"]!= null)
            {
                //Button_AS400Benutzer_Suchen.Enabled = false;
                //Button_AS400BenutzerSuchen_Hinzufügen.Enabled = false;
                //var ip =
                //from a in Adressen.db.Adressen
                //where a.Id == Convert.ToInt32(Session["IPid"])
                //select new { a.Id, a.Adb1, a.Adb2, a.Adb3, a.Adb4, a.MAC };

                Adressen ads = Adressen.Get(Convert.ToInt32(Session["IPid"]));
                //TextBox_AS400_IPAdresse.Text = ads.IP;
                TextBox_ad1.Text = ads.Adb1.Value.ToString();
                TextBox_ad2.Text = ads.Adb2.Value.ToString();
                TextBox_ad3.Text = ads.Adb3.Value.ToString();
                TextBox_ad4.Text = ads.Adb4.Value.ToString();

                TextMAC.Text = ads.MAC;

                if (TextBox_ad1.Text.Length > 0)
                {
                    Button_zum_Geraet.Enabled = true;
                    Button_AS400_Neu.Enabled = true;
                   // Button_AS400_Suchen.Enabled = true;

                    GridView_AS400_Verwalten_Refresh();


                }
            }


            
        }

        protected void GridView_AS400_Verwalten_Refresh()
        {
           
            DBaseDataContext db = new DBaseDataContext();
            var ben =
                from b in db.Adressen
                where b.Id == Convert.ToInt32(Session["IPid"])

                join AS400Adressen in db.AS400Adressen on b.Id equals AS400Adressen.Id_Adressen
                into AS400AdressenGroups
                from AS400Adressen in AS400AdressenGroups.DefaultIfEmpty()

                join AS400Geräte in db.AS400Geräte on AS400Adressen.Id_AS400Geräte equals AS400Geräte.Id
                into AS400GeräteGroups
                from AS400Geräte in AS400GeräteGroups.DefaultIfEmpty()

                select new { AS400Geräte.Id, AS400Geräte.Kennung };
            
            if (ben != null && ben.Count() > 0)
            {
                try
                {
                    GridView_AS400_Verwalten.DataSource = ben.ToList();
                    GridView_AS400_Verwalten.DataBind();
                }
                catch (Exception)
                {
                }
              
            }

        }

        //GridViews
        protected void GridView_AS400_Verwalten_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["AS400_Geräte_Id"] = GridView_AS400_Verwalten.GetItemIDByGridView(0).X;
            TextBox_AS400_Kennung.Text = GridView_AS400_Verwalten.Rows[GridView_AS400_Verwalten.SelectedIndex].Cells[1].Text;

            Button_AS400_Bearbeiten.Enabled = true;
            Button_AS400_Löschen.Enabled = true;
            Button_AS400Benutzer_Neu.Enabled = true;

            if (GridView_AS400_Verwalten.SelectedIndex != -1)
            {
                TextBox_AS400BenutzerSuchen_Kennung.Enabled = true;
            }
            Button_AS400Benutzer_Suchen.Enabled = true;
            GridView_AS400_Benutzer_Refresh();
        }
        protected void GridView_AS400_Benutzer_Suchen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView_AS400_Benutzer_Suchen.SelectedIndex != -1)
            {
                Session["AS400_BenutzerSuchen_Id"] = GridView_AS400_Benutzer_Suchen.GetItemIDByGridView(0).X;
                Button_AS400BenutzerSuchen_Hinzufügen.Enabled = true;
            }


        }
        protected void GridView_AS400_Benutzer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView_AS400_Benutzer.SelectedIndex != -1)
            {
                Session["AS400_Benutzer_Id"] = GridView_AS400_Benutzer.GetItemIDByGridView(0).X;
                Button_AS400Benutzer_Bearbeiten.Enabled = true;
                Button_AS400Benutzer_Löschen.Enabled = true;
                Button_AS400Benutzer_VerbindungEntfernen.Enabled = true;

                TextBox_AS400Benutzer_Kennung.Text = GridView_AS400_Benutzer.Rows[GridView_AS400_Benutzer.SelectedIndex].Cells[1].Text;
                TextBox_AS400Benutzer_Kennwort.Text = GridView_AS400_Benutzer.Rows[GridView_AS400_Benutzer.SelectedIndex].Cells[2].Text;

            }
        }

        //AS400Gerät
        protected void Button_AS400_Neu_Click(object sender, EventArgs e)
        {
            neu_asg = true;
            TextBox_AS400_Kennung.Enabled = true;
            Button_AS400_Speichern.Enabled = true;
        }
        protected void Button_AS400_Speichern_Click(object sender, EventArgs e)
        {
            if (neu_asg)
            {
                

                AS400Geräte asg = new AS400Geräte() { Kennung = TextBox_AS400_Kennung.Text };
                AS400Geräte.db.AS400Geräte.InsertOnSubmit(asg);

                try
                {
                    AS400Geräte.db.SubmitChanges();
                }
                catch (Exception)
                {

                }


                AS400Adressen asa = new AS400Adressen() {  Id_Adressen = Convert.ToInt32(Session["IPid"]), Id_AS400Geräte = AS400Geräte.GetIdByKennung(TextBox_AS400_Kennung.Text) };
                AS400Adressen.db.AS400Adressen.InsertOnSubmit(asa);

                try
                {
                    AS400Adressen.db.SubmitChanges();
                }
                catch (Exception)
                {

                }
            }
            else
            {
                AS400Geräte asg = AS400Geräte.GetAS400GeräteById(Convert.ToInt32( GridView_AS400_Verwalten.Rows[GridView_AS400_Verwalten.SelectedIndex].Cells[0].Text));
                asg.Kennung = TextBox_AS400_Kennung.Text;
                //...
            }
            try
            {
                AS400Geräte.db.SubmitChanges();
            }
            catch (Exception)
            {
                
            }

            TextBox_AS400_Kennung.Text = String.Empty;
            TextBox_AS400_Kennung.Enabled = false;

            GridView_AS400_Verwalten_Refresh();
        }
        protected void Button_AS400_Bearbeiten_Click(object sender, EventArgs e)
        {
            TextBox_AS400_Kennung.Enabled = true;
        }
        protected void Button_AS400_Löschen_Click(object sender, EventArgs e)
        {

            AS400Adressen.DeleteAllByG(    Convert.ToInt32(GridView_AS400_Verwalten.Rows[GridView_AS400_Verwalten.SelectedIndex].Cells[0].Text));
            AS400.DeleteAll(            Convert.ToInt32(GridView_AS400_Verwalten.Rows[GridView_AS400_Verwalten.SelectedIndex].Cells[0].Text));
            AS400Geräte.DeleteAll(      Convert.ToInt32(GridView_AS400_Verwalten.Rows[GridView_AS400_Verwalten.SelectedIndex].Cells[0].Text));



            TextBox_AS400_Kennung.Text = String.Empty;
            TextBox_AS400_Kennung.Enabled = false;

            GridView_AS400_Verwalten_Refresh();
            GridView_AS400_Benutzer_Refresh();
        }
       
        //AS400Benutzersuche
        protected void Button_AS400Benutzer_Suchen_Click(object sender, EventArgs e)
        {
            DBaseDataContext db = new DBaseDataContext();
            var ben =
                from b in db.AS400Benutzer
                where
                b.Kennung == null || b.Kennung.Contains(TextBox_AS400BenutzerSuchen_Kennung.Text)
                select new { b.Id, b.Kennung };

            GridView_AS400_Benutzer_Suchen.DataSource = ben;
            GridView_AS400_Benutzer_Suchen.DataBind();
        }
        protected void Button_AS400BenutzerSuchen_Hinzufügen_Click(object sender, EventArgs e)
        {
            if (!AS400.Exist(TextBox_AS400Benutzer_Kennung.Text))
            {
                DBaseDataContext d = new DBaseDataContext();

                AS400 as400 = new AS400() { Id_Benutzer = Convert.ToInt32(Session["AS400_BenutzerSuchen_Id"]), Id_Geräte = Convert.ToInt32(Session["AS400_Geräte_Id"]) };
                d.AS400.InsertOnSubmit(as400);
                try
                {
                    d.SubmitChanges();
                }
                catch (Exception)
                {
                }
                
             
            }
            GridView_AS400_Benutzer_Refresh();
        }

        //AS400Benutzerverwaltung
        protected void Button_AS400Benutzer_Neu_Click(object sender, EventArgs e)
        {
            neu_asb = true;
            TextBox_AS400Benutzer_Kennung.Enabled = true;
            TextBox_AS400Benutzer_Kennwort.Enabled = true;
            TextBox_AS400Benutzer_Bemerkung.Enabled = true;

            Button_AS400Benutzer_Speichern.Enabled = true;
        }
        protected void Button_AS400Benutzer_Speichern_Click(object sender, EventArgs e)
        {
            AS400Benutzer b;
            if (neu_asb)
            {
                if (!AS400Benutzer.Exist(TextBox_AS400Benutzer_Kennung.Text))
                {
                    b = new AS400Benutzer()
                    {
                        Kennung = TextBox_AS400Benutzer_Kennung.Text,
                        Passwort = TextBox_AS400Benutzer_Kennwort.Text,
                        Bemerkung = TextBox_AS400Benutzer_Bemerkung.Text

                    };


                    AS400Benutzer.db.AS400Benutzer.InsertOnSubmit(b);
                    try
                    {
                        AS400Benutzer.db.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                    //....
                    //...
                    //..
                    //.
                    AS400 as400 = new AS400() { Id_Benutzer = AS400Benutzer.Get_AS400_Benutzer_By_Gerät(b).Id, Id_Geräte = Convert.ToInt32(Session["AS400_Geräte_Id"]) };
                    AS400.db.AS400.InsertOnSubmit(as400);
                    AS400.db.SubmitChanges();
                    //message = "Benutzer erfolgreich hinzugefügt!";

                }
                else
                {
                    message = "Kennung existiert bereits!";
                }
            }
            else
            {
                b = AS400Benutzer.Get_AS400_Benutzer_By_Id(Convert.ToInt32(Session["AS400_Benutzer_Id"]));
                b.Kennung = TextBox_AS400Benutzer_Kennung.Text;
                b.Passwort = TextBox_AS400Benutzer_Kennwort.Text;
                b.Bemerkung = TextBox_AS400Benutzer_Bemerkung.Text;
                AS400Benutzer.db.SubmitChanges();

                //AS400 as400 = AS400.GetAS400ById(Convert.ToInt32(Session["AS400_Benutzer_Id"]), Convert.ToInt32(Session["AS400_Geräte_Id"])).First();
                //AS400.db.SubmitChanges();
                message = "Kennung erfolgreich bearbeitet!";
            }
            

            TextBox_AS400Benutzer_Kennung.Text = String.Empty;
            TextBox_AS400Benutzer_Kennwort.Text = String.Empty;
            TextBox_AS400Benutzer_Bemerkung.Text = String.Empty;

            TextBox_AS400Benutzer_Kennung.Enabled = false;
            TextBox_AS400Benutzer_Kennwort.Enabled = false;
            TextBox_AS400Benutzer_Bemerkung.Enabled = false;
            Button_AS400Benutzer_Speichern.Enabled = false;

            GridView_AS400_Benutzer_Refresh();

            //Button_AS400Benutzer_Suchen_Click(sender, e);
        }
        protected void Button_AS400Benutzer_Bearbeiten_Click(object sender, EventArgs e)
        {
            TextBox_AS400Benutzer_Kennung.Enabled = true;
            TextBox_AS400Benutzer_Kennwort.Enabled = true;
            TextBox_AS400Benutzer_Bemerkung.Enabled = true;
        }
        protected void Button_AS400Benutzer_Löschen_Click(object sender, EventArgs e)
        {
            Button_AS400Benutzer_VerbindungEntfernen_Click(sender, e);

            try
            {
                AS400Benutzer as400benutzer = AS400Benutzer.Get_AS400_Benutzer_By_Id(Convert.ToInt32(Session["AS400_Benutzer_Id"]));
                AS400Benutzer.db.AS400Benutzer.DeleteOnSubmit(as400benutzer);
                AS400Benutzer.db.SubmitChanges();
            }
            catch (Exception)
            {
                
            }


            Button_AS400Benutzer_Suchen_Click(sender, e);

            GridView_AS400_Benutzer_Refresh();

            TextBox_AS400Benutzer_Kennung.Text = String.Empty;
            TextBox_AS400Benutzer_Kennwort.Text = String.Empty;
            TextBox_AS400Benutzer_Bemerkung.Text = String.Empty;

            TextBox_AS400Benutzer_Kennung.Enabled = false;
            TextBox_AS400Benutzer_Kennwort.Enabled = false;
            TextBox_AS400Benutzer_Bemerkung.Enabled = false;
            Button_AS400Benutzer_Löschen.Enabled = false;

        }
        protected void Button_AS400Benutzer_VerbindungEntfernen_Click(object sender, EventArgs e)
        {
            AS400 as400 = AS400.GetAS400ById(Convert.ToInt32(Session["AS400_Benutzer_Id"]), Convert.ToInt32(Session["AS400_Geräte_Id"])).First();
            AS400.db.AS400.DeleteOnSubmit(as400);

            try
            {
                AS400.db.SubmitChanges();
            }
            catch (Exception)
            {

            }
            GridView_AS400_Benutzer_Refresh();

        }
        protected void GridView_AS400_Benutzer_Suchen_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_AS400_Benutzer_Suchen.PageIndex = e.NewPageIndex;
            Button_AS400Benutzer_Suchen_Click(sender, e);
        }
        protected void GridView_AS400_Benutzer_Refresh()
        {
            GridView_AS400_Benutzer.DataSource = AS400Benutzer.Get_AS400_Benutzer_List_By_Gerät(AS400Geräte.GetAS400GeräteById(Convert.ToInt32(Session["AS400_Geräte_Id"])));
            GridView_AS400_Benutzer.DataBind();
        }  
       
        //Sonstige
        protected void Button_zum_Geraet_Click(object sender, EventArgs e)
        {
            Server.Transfer("GerätePage.aspx");
        }
 

        protected void GridView_AS400_Verwalten_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                //e.Row.ToolTip = "Click to select row";
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.GridView_AS400_Verwalten, "Select$" + e.Row.RowIndex);
            }
        }

        protected void GridView_AS400_Benutzer_Suchen_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                //e.Row.ToolTip = "Click to select row";
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.GridView_AS400_Benutzer_Suchen, "Select$" + e.Row.RowIndex);
            }
        }

        protected void GridView_AS400_Benutzer_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                //e.Row.ToolTip = "Click to select row";
                e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.GridView_AS400_Benutzer, "Select$" + e.Row.RowIndex);
            }
        }

        protected void GridView_AS400_Verwalten_DataBound(object sender, EventArgs e)
        {
            if (GridView_AS400_Verwalten.Columns.Count > 0)
            {
                GridView_AS400_Verwalten.SetColumnsVisabiltiyFalse(0);
            }
           
        }
        protected void GridView_AS400_Benutzer_Suchen_DataBound(object sender, EventArgs e)
        {
            if (GridView_AS400_Benutzer_Suchen.Columns.Count > 0)
            {
                GridView_AS400_Benutzer_Suchen.SetColumnsVisabiltiyFalse(0);
            }
           
        }
        protected void GridView_AS400_Benutzer_DataBound(object sender, EventArgs e)
        {
            if (GridView_AS400_Benutzer.Columns.Count > 0)
            {
                GridView_AS400_Benutzer.SetColumnsVisabiltiyFalse(0);
            }           
        }
    }
}