using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    partial class Bereiche
    {
        public static DBaseDataContext db = new DBaseDataContext();

        public static List<Bereiche> GetAllBereiche()
        {
            var ber =
                   from b in db.Bereiche
                   select b;

            List<Bereiche> list = new List<Bereiche>(ber.ToList());

            return list;
        }

        public static Bereiche GetBereichById(int id)
        {
            var ber =
                      from b in db.Bereiche
                      where b.Id == id
                      select b;

            return ber.First();
        }
        public static Bereiche GetBereichById(string id)
        {
            return GetBereichById(Convert.ToInt16(id));
        }

        public static Bereiche GetBereichByName(string name)
        {
            var ber =
                      from b in db.Bereiche
                      where b.Name == name
                      select b;

            return ber.First();
        }

        public static DropDownList GetAllBereiche(DropDownList ddl) {

            var ber =
                   from b in db.Bereiche
                   select b.Name;

            ddl.DataSource = ber;
            ddl.DataBind();

            return ddl;
        }
        public static void LöscheGeräteFromBereich(Bereiche bereich)
        {
            var ger =
                from g in db.Geräte
                where g.Id_Bereich == bereich.Id
                select g;

            db.Geräte.DeleteAllOnSubmit(ger);
            db.SubmitChanges();

        }
    }


}