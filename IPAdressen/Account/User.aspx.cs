using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen.Account
{
    public partial class User : System.Web.UI.Page
    {
        DBaseDataContext db = new DBaseDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var usr = from u in db.User
                          join d in db.Bereiche on u.Distrikt equals d.Id
                          into dg
                          from d in dg.DefaultIfEmpty()
                          select new { u.Id, u.Name, u.Rechte, Bereich = d.Name };

                try
                {
                    GridView_User.DataSource = usr;
                    GridView_User.DataBind();
                }
                catch (Exception)
                {

                }

                var dis = from d in db.Bereiche
                          select new { d.Name };

                List<String> dList = new List<string>();
                dList.Add("");
                foreach (var d in dis)
                {
                    dList.Add(d.Name);
                }

                DropDownList_Distrikt.DataSource = dList;
                DropDownList_Distrikt.DataBind();
            }
            
        }

        protected void GridView_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button_Bearbeiten.Enabled = true;
            //Button_Löschen.Enabled = true;

            try
            {
                Label_Name.Text = "Name: " + GridView_User.Rows[GridView_User.SelectedIndex].Cells[2].Text;
            }
            catch (Exception)
            {
                Label_Name.Text = "Name: ";
            }
            

            try
            {
                DropDownList_Rechte.SelectedValue = GridView_User.Rows[GridView_User.SelectedIndex].Cells[3].Text;
            }
            catch (Exception)
            {
                DropDownList_Rechte.SelectedValue = "";
            }

            try
            {
                DropDownList_Distrikt.SelectedValue = GridView_User.Rows[GridView_User.SelectedIndex].Cells[4].Text;
            }
            catch (Exception)
            {
                DropDownList_Distrikt.SelectedValue = "";
            }
        }

        protected void Button_Speichern_Click(object sender, EventArgs e)
        {
            var usr = from u in db.User
                      where u.Id == Convert.ToInt32(GridView_User.Rows[GridView_User.SelectedIndex].Cells[1].Text)
                      select u;

            var dis = from d in db.Bereiche
                      where d.Name == DropDownList_Distrikt.SelectedValue
                      select d;

            usr.First().Rechte = DropDownList_Rechte.SelectedValue;

            if (dis.Count() > 0)
            {
                usr.First().Distrikt = dis.First().Id;
            }
            else
            {
                usr.First().Distrikt = null;
            }

            db.SubmitChanges();
            Refresh();
        }

        protected void Button_Bearbeiten_Click(object sender, EventArgs e)
        {
            DropDownList_Rechte.Enabled = true;
            DropDownList_Distrikt.Enabled = true;
            Button_Löschen.Enabled = true;
            Button_Speichern.Enabled = true;

        }

        protected void Button_Löschen_Click(object sender, EventArgs e)
        {
            var usr = from u in db.User
                      where u.Id == Convert.ToInt32(GridView_User.Rows[GridView_User.SelectedIndex].Cells[1].Text)
                      select u;

            db.User.DeleteAllOnSubmit(usr);
            db.SubmitChanges();
        }

        protected void DropDownList_Rechte_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Button_Speichern.Enabled = true;
            //Refresh();
        }
        protected void DropDownList_Distrikt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void Refresh() {

            Server.Transfer("User.aspx");
        }


    

    }
}