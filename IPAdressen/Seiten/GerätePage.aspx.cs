using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Collections;
using System.Reflection;
using IPAdressen.Extensions;

namespace IPAdressen.Seiten
{
    public partial class GerätePage : System.Web.UI.Page
    {
        private string id;
        private int intId;

        static bool neu_gerät;
        static bool neu_ip;
        static bool neu_benutzer;




        public static string message;
        DBaseDataContext dbContent = new DBaseDataContext();

        User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView_IPAdressen.RowCreated += RowEvent.OnCustomRow;
            GridView_Benutzer_BenutzerVerwalten.RowCreated += RowEvent.OnCustomRow;
            GridView_BenutzerSuchen.RowCreated += RowEvent.OnCustomRow;
            GridView_Anwendungen.RowCreated += RowEvent.OnCustomRow;
            // GridView_IPAdressen_OnRowCreated += RowEvent.cu;
            user = SiteMaster.us;

            if (user.Rechte != "" && user.Rechte != "Lesen")
            {
                Button_Gerät_Neu.Enabled = true;
                //

                Button_BenutzerVerwalten_Neu.Enabled = true;
                Button_IPAdressen_Neu.Enabled = true;
            }

            BereichPage.message = "";
            Status.Text = message;
            if (!IsPostBack)
            {
                if (DropDownList_Gerät_Bereich.Items.Count < 1)
                {
                    DropDownList_Gerät_Bereich.DataSource = Bereiche.GetAllBereiche();
                    DropDownList_Gerät_Bereich.DataBind();
                }
            }

            if (Session["id"] != null)
            {
                Button_Gerät_Bearbeiten.Enabled = true;
                id = Session["id"].ToString();
                intId = Convert.ToInt16(id);


                //Liest Geräte von Session Id aus
                var det =
                    from Geräte in dbContent.Geräte
                    where Geräte.Id == intId

                    join BenutzerGeräte in dbContent.BenutzerGeräte on Geräte.Id equals BenutzerGeräte.Id_Gerät
                    into BenutzerGeräteGroups
                    from BenutzerGeräte in BenutzerGeräteGroups.DefaultIfEmpty()

                    join Benutzer in dbContent.Benutzer on BenutzerGeräte.Id equals Benutzer.Id
                    into BenutzerGroups
                    from Benutzer in BenutzerGroups.DefaultIfEmpty()

                    join BenutzerAnwendung in dbContent.BenutzerAnwendung on Benutzer.Id equals BenutzerAnwendung.Id_Benutzer
                    into BenutzerAnwendungenGroups
                    from BenutzerAnwendung in BenutzerAnwendungenGroups.DefaultIfEmpty()

                    join Anwendungen in dbContent.Anwendungen on BenutzerAnwendung.Id_Anwendung equals Anwendungen.Id
                    into AnwendungenGroups
                    from Anwendungen in AnwendungenGroups.DefaultIfEmpty()


                    select new
                    {
                        Geräte.Id,
                        Geräte.Name,
                        Geräte.Art,
                        Geräte.Typ,
                        Geräte.Standort,
                        Geräte.Id_Bereich,
                        Geräte.AlteIP,
                        Geräte.VNC_Port,
                        Geräte.VNC_Passwort,
                        Geräte.Kommentar,

                        Nachname = Benutzer.Name,
                        Benutzer.Vorname,
                    };





                GridView_BenutzerVerwaltung_Refresh();

                EnableBenutzerSuchen();

                if (!IsPostBack)
                {
                    //Lade Bereich des Gerätes

                    var beg =
                from b in dbContent.Bereiche
                where b.Id == det.First().Id_Bereich
                select new { b.Name };

                if (DropDownList_Gerät_Bereich.Items.Count > 1)
                {
                    //Lade Bereich
                    DropDownList_Gerät_Bereich.ClearSelection();
                    DropDownList_Gerät_Bereich.Items.FindByText(beg.First().Name).Selected = true;
                }

                    //Trägt Felder ein
                    TextBox_Gerät_Name.Text = det.First().Name;
                    TextBox_Gerät_Art.Text = det.First().Art;
                    TextBox_Gerät_Typ.Text = det.First().Typ;
                    TextBox_Gerät_Standort.Text = det.First().Standort;
                    TextBox_Gerät_AlteIP.Text = det.First().AlteIP;
                    TextBox_Gerät_VNCPort.Text = det.First().VNC_Port;
                    TextBox_Gerät_VNCPasswort.Text = det.First().VNC_Passwort;
                    TextBox_Gerät_Kommentar.Text = det.First().Kommentar;


                    GridView_IPAdressen_Refresh();


                }

            }
            else
            {
                if (Convert.ToString(Session["newDevice"]) == "true")
                {
                    Button_Gerät_Neu_Click(sender, e);
                    Button_IPAdresse_Neu_Click(sender, e);

                    TextBox_IPAdressen_ad1.Text = Convert.ToString(Session["ip1"]);
                    TextBox_IPAdressen_ad2.Text = Convert.ToString(Session["ip2"]);
                    TextBox_IPAdressen_ad3.Text = Convert.ToString(Session["ip3"]);
                    TextBox_IPAdressen_ad4.Text = Convert.ToString(Session["ip4"]);

                    if (Convert.ToString(Session["ip5"]).Length > 0)
                    {
                        TextBox_IPAdressen_ad5.Text = Convert.ToString(Session["ip5"]);
                    }

                    Button_IPAdressen_Speichern.Enabled = false;
                }
            }

        }


        //  GridViews
        protected void GridView_Benutzer_BenutzerVerwalten_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView_Benutzer_BenutzerVerwalten.SelectedIndex != -1)
            {
                Session["BEid"] = GridView_Benutzer_BenutzerVerwalten.GetItemIDByGridView(0).X;

                TextBox_BenutzerVerwalten_Nachname.Text = GridView_Benutzer_BenutzerVerwalten.Rows[GridView_Benutzer_BenutzerVerwalten.SelectedIndex].Cells[3].Text;
                TextBox_BenutzerVerwalten_Vorname.Text = GridView_Benutzer_BenutzerVerwalten.Rows[GridView_Benutzer_BenutzerVerwalten.SelectedIndex].Cells[2].Text;

                try
                {
                    DropDownList_Anrede.SelectedValue = GridView_Benutzer_BenutzerVerwalten.Rows[GridView_Benutzer_BenutzerVerwalten.SelectedIndex].Cells[1].Text;
                    DropDownList_Internet.SelectedValue = GridView_Benutzer_BenutzerVerwalten.Rows[GridView_Benutzer_BenutzerVerwalten.SelectedIndex].Cells[4].Text;
                }
                catch (Exception)
                {
                    DropDownList_Anrede.SelectedValue = " ";

                }

                //Ansprechpartner?
                DBaseDataContext dbContent = new DBaseDataContext();
                var ans = from a in dbContent.BenutzerGeräte
                          where (a.Id_Benutzer == Convert.ToInt32(GridView_Benutzer_BenutzerVerwalten.Rows[GridView_Benutzer_BenutzerVerwalten.SelectedIndex].Cells[0].Text)
                          && a.Id_Gerät == intId)
                          select a;

                if (ans.First().Ansprechpartner == true)
                {
                    DropDownList_IstAnsprechpartner.SelectedValue = "true";
                }
                else
                {
                    DropDownList_IstAnsprechpartner.SelectedValue = "false";
                }

                //Lade Anwendungen
                GridView_Anwendungen.DataSource = Anwendungen.GetAnwendungenByBenutzer(Benutzer.GetBenutzerById(Session["BEid"].ToString()));
                GridView_Anwendungen.DataBind();

                EnableBenutzerButtons();
                //  EnableBenutzer();
            }
            else
            {
                DisableBenutzer();
            }

        }
        protected void GridView_IPAdressen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["IPid"] = GridView_IPAdressen.Rows[GridView_IPAdressen.SelectedIndex].Cells[0].Text;

            if (GridView_IPAdressen.Rows[GridView_IPAdressen.SelectedIndex].Cells[5].Text != "&nbsp;")
            {
                TextBox_IPAdressen_MAC.Text = GridView_IPAdressen.Rows[GridView_IPAdressen.SelectedIndex].Cells[6].Text;
            }

            TextBox_IPAdressen_ad1.Text = GridView_IPAdressen.Rows[GridView_IPAdressen.SelectedIndex].Cells[1].Text;
            TextBox_IPAdressen_ad2.Text = GridView_IPAdressen.Rows[GridView_IPAdressen.SelectedIndex].Cells[2].Text;
            TextBox_IPAdressen_ad3.Text = GridView_IPAdressen.Rows[GridView_IPAdressen.SelectedIndex].Cells[3].Text;
            TextBox_IPAdressen_ad4.Text = GridView_IPAdressen.Rows[GridView_IPAdressen.SelectedIndex].Cells[4].Text;

            try
            {
                TextBox_IPAdressen_ad5.Text = GridView_IPAdressen.Rows[GridView_IPAdressen.SelectedIndex].Cells[5].Text;
            }
            catch (Exception)
            {
            }

            EnableIP();
            Button_IPAdressen_Speichern.Enabled = false;
            TextBox_IPAdressen_ad1.Enabled = false;
            TextBox_IPAdressen_ad2.Enabled = false;
            TextBox_IPAdressen_ad3.Enabled = false;
            TextBox_IPAdressen_ad4.Enabled = false;
            TextBox_IPAdressen_ad5.Enabled = false;

            TextBox_IPAdressen_MAC.Enabled = false;


            if (user.Rechte != "" && user.Rechte != "Lesen")
            {
                Button_Geräte_IPAdressen_AS400.Enabled = true;
            }
            //// Sucht AS400 Daten

            // var as4 =
            //     from a in AS400Geräte.db.AS400Geräte
            //     where a.Id_Adressen == Convert.ToInt32(Session["IPid"])
            //     select new { a.Id, a.Kennung };

            // GridView_AS400.DataSource = as4;
            // GridView_AS400.DataBind();
            TextBox_IPAdressen_MAC.Enabled = true;
            this.Page.SetFocus(TextBox_IPAdressen_MAC);
            TextBox_IPAdressen_MAC.Enabled = false;
        }
        protected void GridView_IPAdressen_OnDataBound(object sender, EventArgs e)
        {

            try
            {
                GridView_IPAdressen.SetColumnsVisabiltiyFalse(0);
                GridView_IPAdressen.SetByteNumberFormat(1);
                GridView_IPAdressen.SetByteNumberFormat(2);
                GridView_IPAdressen.SetByteNumberFormat(3);
                GridView_IPAdressen.SetByteNumberFormat(4);
                GridView_IPAdressen.SetByteNumberFormat(5);
                GridView_IPAdressen.SetStringMACFormat(6);
            }
            catch (Exception)
            {
            }

        }
        protected void GridView_Benutzer_BenutzerVerwalten_OnDataBound(object sender, EventArgs e)
        {
            //GridView_Benutzer_BenutzerVerwalten.SetColumnsVisabiltiyFalse(0);
        }
        protected void GridView_BenutzerSuchen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["vBid"] = GridView_BenutzerSuchen.Rows[GridView_BenutzerSuchen.SelectedIndex].Cells[0].Text;
            Button_Benutzersuchen_Hinzufügen.Enabled = true;
        }
        protected void GridView_BenutzerSuchen_OnDataBound(object sender, EventArgs e)
        {
            GridView_BenutzerSuchen.SetColumnsVisabiltiyFalse(0);
        }
        protected void GridView_Anwendungen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Geräte
        protected void Button_Gerät_Neu_Click(object sender, EventArgs e)
        {
            //DropDownList_Gerät_Bereich.DataSource = Bereiche.GetAllBereiche();
            //DropDownList_Gerät_Bereich.DataBind();

            EnableGerät();
            neu_gerät = true;

        }
        protected void Button_Gerät_Speichern_Click(object sender, EventArgs e)
        {
            bool exist = false;
            if (TextBox_Gerät_Name.Enabled == true)
            {
                
                Geräte g;
             
                if (neu_gerät)
                {
                    if (!Geräte.Exist(TextBox_Gerät_Name.Text))
                    {
                        g = new Geräte()
                        {
                            Name = TextBox_Gerät_Name.Text,
                            Art = TextBox_Gerät_Art.Text,
                            Typ = TextBox_Gerät_Typ.Text,
                            Standort = TextBox_Gerät_Standort.Text,
                            AlteIP = TextBox_Gerät_AlteIP.Text,
                            Kommentar = TextBox_Gerät_Kommentar.Text,
                            VNC_Port = TextBox_Gerät_VNCPort.Text,
                            VNC_Passwort = TextBox_Gerät_VNCPasswort.Text,
                            Id_Bereich = Bereiche.GetBereichByName(DropDownList_Gerät_Bereich.SelectedValue).Id
                        };

                        Geräte.db.Geräte.InsertOnSubmit(g);

                        message = "Erfolgreich hinzugefügt!";

                        try
                        {
                            Geräte.db.SubmitChanges();
                        }
                        catch (Exception)
                        {
                            message = "Keine Datenbankverbindung!";
                        }

                        try
                        {
                            Session["id"] = Geräte.GetId(g);
                        }
                        catch (Exception)
                        {
                            message = "Fehler! Gerät nicht gefunden!";
                        }
                    }
                    else
                    {
                        exist = true;
                        MessageBox.Show(this, "Name bereits vergeben!");
                    }
                }
                else
                {
                    g = Geräte.Get(Convert.ToInt32(Session["id"]));

                    g.Name = TextBox_Gerät_Name.Text;
                    g.Art = TextBox_Gerät_Art.Text;
                    g.Typ = TextBox_Gerät_Typ.Text;
                    g.Standort = TextBox_Gerät_Standort.Text;
                    g.AlteIP = TextBox_Gerät_AlteIP.Text;
                    g.Kommentar = TextBox_Gerät_Kommentar.Text;
                    g.VNC_Port = TextBox_Gerät_VNCPort.Text;
                    g.VNC_Passwort = TextBox_Gerät_VNCPasswort.Text;
                    g.Id_Bereich = Bereiche.GetBereichByName(DropDownList_Gerät_Bereich.SelectedValue).Id;

                    message = "Erfolgreich gespeichert!";
                    //test
                    try
                    {
                        Geräte.db.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        message = "Keine Datenbankverbindung!";
                    }

                    try
                    {
                        Session["id"] = Geräte.GetId(g);
                    }
                    catch (Exception)
                    {
                        message = "Fehler! Gerät nicht gefunden!";
                    }
                }

                if (!exist)
                {
                    DisableGerät();
                    Button_Gerät_Bearbeiten.Enabled = true;
                    if (Convert.ToString(Session["newDevice"]) == "true")
                    {
                        Button_IPAdressen_Speichern.Enabled = true;
                        Session["newDevice"] = "false";
                    }
                }


            }
        }
        protected void Button_Gerät_Bearbeiten_Click(object sender, EventArgs e)
        {

            EnableGerät();
            EnableGerätButton();
            neu_gerät = false;
        }
        protected void Button_Gerät_Löschen_Click(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                Geräte.Delete(Convert.ToInt32(Session["id"]));

                message = "Erfolgreich gelöscht!";

                DisableBenutzer();
                DisableBenutzerSuchen();
                DisableGerät();
                Session["id"] = null;
                Refresh();
            }
        }

        // IP-Adressen
        protected void Button_IPAdresse_Neu_Click(object sender, EventArgs e)
        {
            TextBox_IPAdressen_ad1.Text = String.Empty;
            TextBox_IPAdressen_ad2.Text = String.Empty;
            TextBox_IPAdressen_ad3.Text = String.Empty;
            TextBox_IPAdressen_ad4.Text = String.Empty;
            TextBox_IPAdressen_ad5.Text = String.Empty;

            TextBox_IPAdressen_MAC.Text = String.Empty;

            TextBox_IPAdressen_ad1.Enabled = true;
            TextBox_IPAdressen_ad2.Enabled = true;
            TextBox_IPAdressen_ad3.Enabled = true;
            TextBox_IPAdressen_ad4.Enabled = true;
            TextBox_IPAdressen_ad5.Enabled = true;

            TextBox_IPAdressen_MAC.Enabled = true;

            Button_IPAdressen_Speichern.Enabled = true;

            neu_ip = true;
        }
        protected void Button_IPAdressen_Speichern_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                Adressen ads;
                if (neu_ip)
                {
                    if (!Adressen.Exist(TextBox_IPAdressen_ad1.Text, TextBox_IPAdressen_ad2.Text, TextBox_IPAdressen_ad3.Text, TextBox_IPAdressen_ad4.Text, TextBox_IPAdressen_ad5.Text))
                    {
                        if (TextBox_IPAdressen_ad5.Text != String.Empty)
                        {
                            ads = new Adressen()
                            {
                                Adb1 = Convert.ToByte(TextBox_IPAdressen_ad1.Text),
                                Adb2 = Convert.ToByte(TextBox_IPAdressen_ad2.Text),
                                Adb3 = Convert.ToByte(TextBox_IPAdressen_ad3.Text),
                                Adb4 = Convert.ToByte(TextBox_IPAdressen_ad4.Text),
                                Adb5 = Convert.ToByte(TextBox_IPAdressen_ad5.Text),

                                MAC = TextBox_IPAdressen_MAC.Text,
                                Id_Geräte = Convert.ToInt32(Session["id"])
                            };
                        }
                        else
                        {
                            ads = new Adressen()
                            {
                                Adb1 = Convert.ToByte(TextBox_IPAdressen_ad1.Text),
                                Adb2 = Convert.ToByte(TextBox_IPAdressen_ad2.Text),
                                Adb3 = Convert.ToByte(TextBox_IPAdressen_ad3.Text),
                                Adb4 = Convert.ToByte(TextBox_IPAdressen_ad4.Text),

                                MAC = TextBox_IPAdressen_MAC.Text,
                                Id_Geräte = Convert.ToInt32(Session["id"])
                            };
                        }


                        Adressen.db.Adressen.InsertOnSubmit(ads);

                        TextBox_IPAdressen_ad1.Enabled = false;
                        TextBox_IPAdressen_ad2.Enabled = false;
                        TextBox_IPAdressen_ad3.Enabled = false;
                        TextBox_IPAdressen_ad4.Enabled = false;
                        TextBox_IPAdressen_ad5.Enabled = false;

                        TextBox_IPAdressen_MAC.Enabled = false;
                        neu_ip = false;
                    }
                    else
                    {
                        MessageBox.Show(this, "Adresse bereits vergeben!");
                    }
                }
                else
                {
                    ads = Adressen.Get(Convert.ToInt32(Session["IPid"]));

                    ads.Adb1 = Convert.ToByte(TextBox_IPAdressen_ad1.Text);
                    ads.Adb2 = Convert.ToByte(TextBox_IPAdressen_ad2.Text);
                    ads.Adb3 = Convert.ToByte(TextBox_IPAdressen_ad3.Text);
                    ads.Adb4 = Convert.ToByte(TextBox_IPAdressen_ad4.Text);
                    ads.Adb4 = Convert.ToByte(TextBox_IPAdressen_ad5.Text);

                    ads.MAC = TextBox_IPAdressen_MAC.Text;

                    TextBox_IPAdressen_ad1.Enabled = false;
                    TextBox_IPAdressen_ad2.Enabled = false;
                    TextBox_IPAdressen_ad3.Enabled = false;
                    TextBox_IPAdressen_ad4.Enabled = false;
                    TextBox_IPAdressen_ad5.Enabled = false;

                    TextBox_IPAdressen_MAC.Enabled = false;
                }

                try
                {
                    Adressen.db.SubmitChanges();
                }
                catch (Exception)
                {

                }

                GridView_IPAdressen_Refresh();

            }
        }
        protected void Button_IPAdressen_Bearbeiten_Click(object sender, EventArgs e)
        {
            Adressen ads = Adressen.Get(Convert.ToInt32(Session["IPid"]));

            TextBox_IPAdressen_ad1.Text = ads.Adb1.Value.ToString();
            TextBox_IPAdressen_ad2.Text = ads.Adb2.Value.ToString();
            TextBox_IPAdressen_ad3.Text = ads.Adb3.Value.ToString();
            TextBox_IPAdressen_ad4.Text = ads.Adb4.Value.ToString();

            try
            {
                TextBox_IPAdressen_ad5.Text = ads.Adb5.Value.ToString();
            }
            catch (Exception)
            {
            }


            TextBox_IPAdressen_MAC.Text = ads.MAC;

            TextBox_IPAdressen_ad1.Enabled = true;
            TextBox_IPAdressen_ad2.Enabled = true;
            TextBox_IPAdressen_ad3.Enabled = true;
            TextBox_IPAdressen_ad4.Enabled = true;
            TextBox_IPAdressen_ad5.Enabled = true;

            TextBox_IPAdressen_MAC.Enabled = true;

            Button_IPAdressen_Speichern.Enabled = true;
            Button_IPAdressen_Löschen.Enabled = true;
        }
        protected void Button_IPAdressen_Löschen_Click(object sender, EventArgs e)
        {
            Adressen.db.Adressen.DeleteOnSubmit(Adressen.Get(Convert.ToInt32(Session["IPid"])));
            Adressen.db.SubmitChanges();
            GridView_IPAdressen_Refresh();
        }
        protected void GridView_IPAdressen_Refresh()
        {
            //Sucht dazugehörige IPAdressen
            var ads =
                from Adressen in dbContent.Adressen
                where Adressen.Id_Geräte == Convert.ToInt32(Session["id"])
                select new { Adressen.Id, Adressen.Adb1, Adressen.Adb2, Adressen.Adb3, Adressen.Adb4, Adressen.Adb5, Adressen.MAC };

            //Trägt IPAdressen ein
            GridView_IPAdressen.DataSource = ads;
            GridView_IPAdressen.DataBind();
        }

        // Benutzer suchen
        protected void Button_BenutzerSuchen_Suchen_Click(object sender, EventArgs e)
        {
            DBaseDataContext db = new DBaseDataContext();
            var ben =
                from b in db.Benutzer
                where
                (b.Name == null || b.Name.Contains(TextBox_BenutzerSuchen_Nachname.Text))
             && (b.Vorname == null || b.Vorname.Contains(TextBox_BenutzerSuchen_Vorname.Text))
                select new { b.Id, b.Anrede, b.Vorname, b.Name };

            try
            {
                GridView_BenutzerSuchen.DataSource = ben;
                GridView_BenutzerSuchen.DataBind();
            }
            catch (Exception)
            {
            }

        }
        protected void Button_Benutzersuchen_Hinzufügen_Click(object sender, EventArgs e)
        {

            BenutzerGeräte bg = new BenutzerGeräte() { Id_Gerät = Convert.ToInt32(Session["id"]), Id_Benutzer = Convert.ToInt32(Session["vBid"]) };
            BenutzerGeräte.db.BenutzerGeräte.InsertOnSubmit(bg);
            BenutzerGeräte.db.SubmitChanges();
            //Benutzer.db.BenutzerGeräte.InsertOnSubmit(bg);
            //Benutzer.db.SubmitChanges();
            GridView_BenutzerVerwaltung_Refresh();
        }
        protected void GridView_BenutzerSuchen_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_BenutzerSuchen.PageIndex = e.NewPageIndex;
        }

        //Benutzer verwalten
        protected void Button_BenutzerVerwalten_Neu_Click(object sender, EventArgs e)
        {
            TextBox_BenutzerVerwalten_Nachname.Enabled = true;
            TextBox_BenutzerVerwalten_Vorname.Enabled = true;
            Button_BenutzerVerwalten_Speichern.Enabled = true;
            DropDownList_Internet.Enabled = true;
            DropDownList_Anrede.Enabled = true;
            DropDownList_IstAnsprechpartner.Enabled = true;

            neu_benutzer = true;
        }
        protected void Button_BenutzerVerwalten_Speichern_Click(object sender, EventArgs e)
        {

            Benutzer b;

            if (neu_benutzer)
            {
                if (!Benutzer.Exist(TextBox_BenutzerVerwalten_Vorname.Text, TextBox_BenutzerVerwalten_Nachname.Text))
                {
                    b = new Benutzer()
                    {
                        Name = TextBox_BenutzerVerwalten_Nachname.Text,
                        Vorname = TextBox_BenutzerVerwalten_Vorname.Text,
                        Internet = DropDownList_Internet.SelectedValue,
                        Anrede = DropDownList_Anrede.SelectedValue
                    };


                    Benutzer.db.Benutzer.InsertOnSubmit(b);
                    Benutzer.db.SubmitChanges();



                    BenutzerGeräte bg = new BenutzerGeräte() { Id_Gerät = Convert.ToInt32(Session["id"]), Id_Benutzer = Benutzer.GetIdByBenutzer(b), Ansprechpartner = Convert.ToBoolean(DropDownList_IstAnsprechpartner.SelectedValue) };
                    Benutzer.db.BenutzerGeräte.InsertOnSubmit(bg);
                    message = "Benutzer erfolgreich hinzugefügt!";

                    Button_BenutzerVerwalten_Speichern.Enabled = false;
                    TextBox_BenutzerVerwalten_Nachname.Enabled = false;
                    TextBox_BenutzerVerwalten_Vorname.Enabled = false;
                    DropDownList_Anrede.Enabled = false;
                    DropDownList_Internet.Enabled = false;
                    DropDownList_IstAnsprechpartner.Enabled = false;
                }
                else
                {
                    message = "Benutzer existiert bereits!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alertScript", "alert('" + message + "');", true);

                }
            }
            else
            {
                b = Benutzer.GetBenutzerById(Convert.ToInt32(Session["BEid"]));
                b.Name = TextBox_BenutzerVerwalten_Nachname.Text;
                b.Vorname = TextBox_BenutzerVerwalten_Vorname.Text;
                b.Anrede = DropDownList_Anrede.SelectedValue;
                b.Internet = DropDownList_Internet.SelectedValue;

                BenutzerGeräte bg = BenutzerGeräte.GetBGByBId(Convert.ToInt32(Session["BEid"]), Convert.ToInt32(Session["id"])).First();
                bg.Ansprechpartner = Convert.ToBoolean(DropDownList_IstAnsprechpartner.SelectedValue);

                message = "Benutzer erfolgreich bearbeitet!";

                Button_BenutzerVerwalten_Speichern.Enabled = false;
                TextBox_BenutzerVerwalten_Nachname.Enabled = false;
                TextBox_BenutzerVerwalten_Vorname.Enabled = false;
                DropDownList_Anrede.Enabled = false;
                DropDownList_Internet.Enabled = false;
                DropDownList_IstAnsprechpartner.Enabled = false;
            }
            Benutzer.db.SubmitChanges();


            GridView_BenutzerVerwaltung_Refresh();
        }
        protected void Button_BenutzerVerwalten_Bearbeiten_Click(object sender, EventArgs e)
        {
            neu_benutzer = false;
            TextBox_BenutzerVerwalten_Nachname.Enabled = true;
            TextBox_BenutzerVerwalten_Vorname.Enabled = true;

            DropDownList_Internet.Enabled = true;
            DropDownList_Anrede.Enabled = true;
            DropDownList_IstAnsprechpartner.Enabled = true;

            Button_BenutzerVerwalten_Speichern.Enabled = true;
        }
        protected void Button_BenutzerVerwalten_Löschen_Click(object sender, EventArgs e)
        {
            BenutzerGeräte.Delete_BG_From_DB(Convert.ToInt32(Session["BEid"]));

            Benutzer.db.Benutzer.DeleteOnSubmit(Benutzer.GetBenutzerById(Convert.ToInt32(Session["BEid"])));
            Benutzer.db.SubmitChanges();
            GridView_BenutzerVerwaltung_Refresh();
            if (GridView_BenutzerSuchen.SelectedIndex != -1)
            {
                Button_BenutzerSuchen_Suchen_Click(sender, e);
            }

            Button_BenutzerVerwalten_Bearbeiten.Enabled = false;
            Button_BenutzerVerwalten_Details.Enabled = false;
            Button_BenutzerVerwalten_Löschen.Enabled = false;
            Button_BenutzerVerwaltung_VerbindungEntfernen.Enabled = false;
            Button_BenutzerVerwalten_Speichern.Enabled = false;

            DropDownList_Anrede.Enabled = false;
            DropDownList_Internet.Enabled = false;
            DropDownList_IstAnsprechpartner.Enabled = false;

            TextBox_BenutzerVerwalten_Nachname.Text = String.Empty;
            TextBox_BenutzerVerwalten_Vorname.Text = String.Empty;


        }
        protected void Button_BenutzerVerwaltung_VerbindungEntfernen_Click(object sender, EventArgs e)
        {
            BenutzerGeräte.Delete_BG_From_DB(Convert.ToInt32(Session["BEid"]), Convert.ToInt32(Session["id"]));
            GridView_BenutzerVerwaltung_Refresh();
        }
        protected void GridView_BenutzerVerwaltung_Refresh()
        {
            GridView_Benutzer_BenutzerVerwalten.DataSource = Benutzer.GetBenutzerByGerät(Geräte.Get(intId));
            GridView_Benutzer_BenutzerVerwalten.DataBind();
        }

        //Switches
        void EnableGerät()
        {
            if (user.Rechte != "" && user.Rechte != "Lesen")
            {
                Button_Gerät_Speichern.Enabled = true;
                TextBox_Gerät_Name.Enabled = true;
                TextBox_Gerät_Art.Enabled = true;
                TextBox_Gerät_Typ.Enabled = true;
                TextBox_Gerät_Standort.Enabled = true;
                TextBox_Gerät_AlteIP.Enabled = true;
                TextBox_Gerät_VNCPort.Enabled = true;
                TextBox_Gerät_VNCPasswort.Enabled = true;
                TextBox_Gerät_Kommentar.Enabled = true;
                DropDownList_Gerät_Bereich.Enabled = true;
            }



        }
        void EnableGerätButton()
        {
            if (user.Rechte != "" && user.Rechte != "Lesen")
            {
                Button_Gerät_Bearbeiten.Enabled = true;
                Button_Gerät_Löschen.Enabled = true;
                Button_Gerät_Speichern.Enabled = true;
            }
        }
        void EnableIP()
        {
            if (user.Rechte != "" && user.Rechte != "Lesen")
            {
                Button_IPAdressen_Bearbeiten.Enabled = true;
                //Button_IPAdressen_Löschen.Enabled = true;
                Button_IPAdressen_Speichern.Enabled = true;
            }

        }
        void EnableBenutzerButtons()
        {
            if (user.Rechte != "" && user.Rechte != "Lesen")
            {
                Button_BenutzerVerwaltung_VerbindungEntfernen.Enabled = true;
                Button_BenutzerVerwalten_Details.Enabled = true;
                Button_BenutzerVerwalten_Löschen.Enabled = true;
                Button_BenutzerVerwalten_Bearbeiten.Enabled = true;
            }
        }
        void EnableBenutzerSuchen()
        {
            if (user.Rechte != "" && user.Rechte != "Lesen")
            {
                GridView_Benutzer_BenutzerVerwalten.Enabled = true;
                Button_BenutzerSuchen_Suchen.Enabled = true;
                TextBox_BenutzerSuchen_Nachname.Enabled = true;
                TextBox_BenutzerSuchen_Vorname.Enabled = true;
            }
        }

        void DisableGerätButton()
        {

            Button_Gerät_Bearbeiten.Enabled = false;
            Button_Gerät_Löschen.Enabled = false;
            Button_Gerät_Speichern.Enabled = false;
        }
        void DisableGerät()
        {

            TextBox_Gerät_Name.Enabled = false;
            TextBox_Gerät_Art.Enabled = false;
            TextBox_Gerät_Typ.Enabled = false;
            TextBox_Gerät_Standort.Enabled = false;
            TextBox_Gerät_AlteIP.Enabled = false;
            TextBox_Gerät_VNCPort.Enabled = false;
            TextBox_Gerät_VNCPasswort.Enabled = false;
            TextBox_Gerät_Kommentar.Enabled = false;
            DropDownList_Gerät_Bereich.Enabled = false;

            DisableGerätButton();
        }
        void DisableBenutzerSuchen()
        {

            GridView_Benutzer_BenutzerVerwalten.Enabled = false;
            Button_BenutzerSuchen_Suchen.Enabled = false;
            TextBox_BenutzerSuchen_Nachname.Enabled = false;
            TextBox_BenutzerSuchen_Vorname.Enabled = false;
            Button_Benutzersuchen_Hinzufügen.Enabled = false;
        }
        void DisableBenutzer()
        {
            Button_BenutzerVerwalten_Neu.Enabled = false;
            Button_BenutzerVerwalten_Bearbeiten.Enabled = false;
            Button_BenutzerVerwalten_Speichern.Enabled = false;
            Button_BenutzerVerwalten_Löschen.Enabled = false;
            Button_BenutzerVerwalten_Details.Enabled = false;

            TextBox_BenutzerVerwalten_Nachname.Enabled = false;
            TextBox_BenutzerVerwalten_Vorname.Enabled = false;
        }



        //Sonstige Funktionen
        void Refresh()
        {
            Server.Transfer("GerätePage.aspx");
        }

        protected void DropDownList_Anrede_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DropDownList_IstAnsprechpartner_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DropDownList_Internet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button_Geräte_IPAdressen_AS400_Click(object sender, EventArgs e)
        {
            Server.Transfer("AS400Page.aspx");
        }
        protected void Button_BenutzerVerwalten_Details_Click(object sender, EventArgs e)
        {
            Server.Transfer("BenutzerPage.aspx");
        }
        protected void Button_LetzteSuche_Click(object sender, EventArgs e)
        {
            Session["LetzteSuche"] = "true";
            Server.Transfer("Suche.aspx");
        }

        //protected void DropDownList_Gerät_Bereich_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //ddl_string = DropDownList_Gerät_Bereich.SelectedValue;
        //}
    }

}