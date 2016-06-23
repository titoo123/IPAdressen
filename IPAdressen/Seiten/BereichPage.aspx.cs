using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.Linq;

namespace IPAdressen
{
    public partial class BereichPage : System.Web.UI.Page
    {
        static bool neu;
        public static string message;

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                BereichGridView.DataSource = Bereiche.GetAllBereiche();
                BereichGridView.DataBind();
                TextBox_Bereich_IP_Anzahl.Text = "1";
            }
            else
            {
                Session["id"] = null;
                Session["IPid"] = null;
                Session["vBid"] = null;
                Session["BEid"] = null;
            }
        }

        protected void BereichGridView_OnDataBound(object sender, EventArgs e)
        {

            BereichGridView.SetColumnsVisabiltiyFalse(1);
        }

        protected void BereichGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["bId"] = BereichGridView.GetItemIDByGridView(1).X;

            TestBox_Bereich_Name.Text = BereichGridView.Rows[BereichGridView.SelectedIndex].Cells[2].Text;
            Textbox_Bereich_IP3.Text = BereichGridView.Rows[BereichGridView.SelectedIndex].Cells[3].Text;
            Textbox_Bereich_IP2.Text = BereichGridView.Rows[BereichGridView.SelectedIndex].Cells[4].Text;
            Textbox_Bereich_IP1.Text = BereichGridView.Rows[BereichGridView.SelectedIndex].Cells[5].Text;
            BereichBearbeitenButton.Enabled = true;

            if (BereichGridView.SelectedIndex != -1)
            {
                Button_Bereich_SuchenIP.Enabled = true;
            }
        }

        protected void BereichBearbeitenButton_Click(object sender, EventArgs e)
        {

            Textbox_Bereich_IP3.Enabled = true;
            Textbox_Bereich_IP2.Enabled = true;
            Textbox_Bereich_IP1.Enabled = true;

            TestBox_Bereich_Name.Enabled = true;

            BereichSpeichernButton.Enabled = true;

            neu = false;
        }
        protected void BereichNeuButton_Click(object sender, EventArgs e)
        {
            TestBox_Bereich_Name.Enabled = true;
            Textbox_Bereich_IP3.Enabled = true;
            Textbox_Bereich_IP2.Enabled = true;
            Textbox_Bereich_IP1.Enabled = true;
            BereichSpeichernButton.Enabled = true;

            TestBox_Bereich_Name.Text = null;
            Textbox_Bereich_IP3.Text = null;
            Textbox_Bereich_IP2.Text = null;
            Textbox_Bereich_IP1.Text = null;
            neu = true;
        }
        protected void BereichSpeichernButton_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(neu);

            Bereiche b;

            if (neu)
            {
                b = new Bereiche()
                {
                    Name = TestBox_Bereich_Name.Text,
                    IP_Bereich1 = Convert.ToByte(Textbox_Bereich_IP1.Text),
                    IP_Bereich2 = Convert.ToByte(Textbox_Bereich_IP2.Text),
                    IP_Bereich3 = Convert.ToByte(Textbox_Bereich_IP3.Text)
                };


                Bereiche.db.Bereiche.InsertOnSubmit(b);
                message = "Erfolgreich hinzugefügt!";

            }
            else
            {
                b = Bereiche.GetBereichById(Session["bId"].ToString());
                b.Name = TestBox_Bereich_Name.Text;
                b.IP_Bereich3 = Convert.ToByte(Textbox_Bereich_IP3.Text);
                b.IP_Bereich2 = Convert.ToByte(Textbox_Bereich_IP2.Text);
                b.IP_Bereich1 = Convert.ToByte(Textbox_Bereich_IP1.Text);

                message = "Erfolgreich gespeichert!";

            }


            Bereiche.db.SubmitChanges();
            Server.Transfer("BereichPage.aspx");
            RefreshMessage();

        }
        protected void BereichLöschenButton_Click(object sender, EventArgs e)
        {
            if (Session["bId"] != null)
            {
                Bereiche ber = Bereiche.GetBereichById(Session["bId"].ToString());

                try
                {
                    Bereiche.LöscheGeräteFromBereich(ber);
                    Bereiche.db.Bereiche.DeleteOnSubmit(ber);
                    Bereiche.db.SubmitChanges();
                    message = "Erfolgreich gelöscht!";
                }
                catch (ChangeConflictException)
                {
                    message = "Löschen war nicht erfolgreich! :" + e.ToString();
                }

                message = "Erfolgreich gelöscht!";
                Server.Transfer("BereichPage.aspx");
                RefreshMessage();
            }
        }


        protected void Button_Bereich_SuchenIP_Click(object sender, EventArgs e)
        {
            if (Textbox_Bereich_IP3.Text.Length > 0)
            {
                DBaseDataContext db = new DBaseDataContext();

                List<int> bList = new List<int>();



                List<Adressen> adList = new List<Adressen>();
                List<Adressen> adList5 = new List<Adressen>();

                var ber = from b in db.Adressen
                          where
                               b.Adb3 == Convert.ToByte(Textbox_Bereich_IP3.Text)
                          && b.Adb2 == Convert.ToByte(Textbox_Bereich_IP2.Text)
                          && b.Adb1 == Convert.ToByte(Textbox_Bereich_IP1.Text)
                          select new { b.Adb1, b.Adb2, b.Adb3, b.Adb4, b.Adb5 };

                //Vergleicht Adressen Berich 4
                for (int i = 0; i < 255; i++)
                {
                    bList.Add(i + 1);

                    foreach (var b in ber)
                    {

                        if (i + 1 == b.Adb4)
                        {
                            bList.Remove(i + 1);
                        }

                    }
                }
                List<int> t_bList = new List<int>();
                foreach (var l in ber)
                {
                    if (l.Adb5 != null)
                    {
                        foreach (int c in bList)
                        {
                            if (c <= l.Adb5 && c >= l.Adb4)
                            {
                                t_bList.Add(c);
                            }
                        }
                    }
                }
                foreach (var y in t_bList)
                {
                    if (bList.Contains(y))
                    {
                        bList.Remove(y);
                    }
                }

                foreach (int item in bList)
                {
                    Adressen a = new Adressen()
                    {
                        Adb1 = Convert.ToByte(Textbox_Bereich_IP1.Text),
                        Adb2 = Convert.ToByte(Textbox_Bereich_IP2.Text),
                        Adb3 = Convert.ToByte(Textbox_Bereich_IP3.Text),
                        Adb4 = Convert.ToByte(item)
                    };
                    adList.Add(a);
                }

                //Bei mehreren Adressen
                int n = 0;

                if (Int32.TryParse(TextBox_Bereich_IP_Anzahl.Text, out n))
                {
                    if (n > 1)
                    {
                        adList.Clear();


                        List<Tuple<int, int>> b = new List<Tuple<int, int>>();

                        foreach (int y in bList)
                        {
                            if (Helper.HatXNachfolger(bList, y, n))
                            {
                                b.Add(new Tuple<int, int>((y - 1), (y + n - 2)));
                            }
                        }

                        foreach (Tuple<int, int> t in b)
                        {
                            adList.Add(new Adressen() { Adb1 = Convert.ToByte(Textbox_Bereich_IP1.Text),
                                Adb2 = Convert.ToByte(Textbox_Bereich_IP2.Text), Adb3 = Convert.ToByte(Textbox_Bereich_IP3.Text), Adb4 = Convert.ToByte(t.Item1 ), Adb5 = Convert.ToByte( t.Item2 ) });
                        }
                    }



                }
                else
                {
                    //Nachricht keine gültige Zahl!
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alertScript", "alert('" + "Bitte geben sie eine gültige Zahl ein!" + "');", true);
                }



                GridView_Bereich_IPSuche.DataSource = adList;
                GridView_Bereich_IPSuche.DataBind();

                if (bList.Count() < 1)
                {
                    message = "Keine freien IP's im Bereich vorhanden!";
                }
                else
                {
                    // GridView_Bereich_IPSuche.HeaderRow.Cells[1].Text = "IP's";
                    message = "Freie IP's berechnet!";
                }



                RefreshMessage();


            }

        }
        protected void GridView_Bereich_IPSuche_OnDataBound(object sender, EventArgs e)
        {

            GridView_Bereich_IPSuche.SetColumnsVisabiltiyFalse(1);
            GridView_Bereich_IPSuche.SetColumnsVisabiltiyFalse(2);
            GridView_Bereich_IPSuche.SetColumnsVisabiltiyFalse(3);
            //GridView_Bereich_IPSuche.SetColumnsVisabiltiyFalse(4);
            GridView_Bereich_IPSuche.SetByteNumberFormat(4);
            GridView_Bereich_IPSuche.SetByteNumberFormat(5);
            GridView_Bereich_IPSuche.SetByteNumberFormat(6);
            GridView_Bereich_IPSuche.SetByteNumberFormat(7);

            try
            {
                GridView_Bereich_IPSuche.SetByteNumberFormat(8);
            }
            catch (Exception)
            {
            }
        }

        protected void GridView_Bereich_IPSuche_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView_Bereich_IPSuche.PageIndex = e.NewPageIndex;
            Button_Bereich_SuchenIP_Click(sender, e);
        }
        private void RefreshMessage()
        {
            Status.Text = message;
        }

        protected void BereichGridView_ipsuche_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button_neues_Gerät.Enabled = true;
        }

        protected void Button_neues_Gerät_Click(object sender, EventArgs e)
        {
            Session["newDevice"] = "true";
            Session["ip1"] = GridView_Bereich_IPSuche.Rows[GridView_Bereich_IPSuche.SelectedIndex].Cells[4].Text;
            Session["ip2"] = GridView_Bereich_IPSuche.Rows[GridView_Bereich_IPSuche.SelectedIndex].Cells[5].Text;
            Session["ip3"] = GridView_Bereich_IPSuche.Rows[GridView_Bereich_IPSuche.SelectedIndex].Cells[6].Text;
            Session["ip4"] = GridView_Bereich_IPSuche.Rows[GridView_Bereich_IPSuche.SelectedIndex].Cells[7].Text;

            if ( Convert.ToInt32( TextBox_Bereich_IP_Anzahl.Text) > 1)
            {
                Session["ip5"] = GridView_Bereich_IPSuche.Rows[GridView_Bereich_IPSuche.SelectedIndex].Cells[8].Text;
            }
            Server.Transfer("GerätePage.aspx");
        }


    }
}