using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    partial class AS400Geräte
    {
        public static DBaseDataContext db = new DBaseDataContext();

        public static AS400Geräte GetAS400GeräteById(int id)
        {
            var anw =
                      from a in db.AS400Geräte
                      where a.Id == id
                      select a;

            return anw.First();
        }
        public static int GetIdByKennung(string s) {

            var das = from e in db.AS400Geräte
                      where e.Kennung == s
                      select e;

            return das.First().Id;
        }

        internal static void DeleteAll(int gId)
        {
            DBaseDataContext d = new DBaseDataContext();
            var beg =
                from b in d.AS400Geräte
                where b.Id == gId
                select b;

            try
            {
                d.AS400Geräte.DeleteAllOnSubmit(beg);
                d.SubmitChanges();
            }
            catch (Exception)
            {
            }

        }
    }

}