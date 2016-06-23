using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen.Extensions
{
    public class RowEvent
    {
        //public delegate void RowDelegate(GridViewRowEventArgs e, Page p, GridView g);
        //public event EventHandler CustomRow;

        //protected virtual void OnCustomRow(EventArgs e) { }
        public static void OnCustomRow(object sender, GridViewRowEventArgs e)
        {
            GridView gridView = (GridView)sender;
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                //e.Row.ToolTip = "Click to select row";
                e.Row.Attributes["onclick"] = gridView.Page.ClientScript.GetPostBackClientHyperlink(gridView, "Select$" + e.Row.RowIndex);
            }
        }

        internal static GridViewRowEventHandler Effect()
        {
            throw new NotImplementedException();
        }
    }
}