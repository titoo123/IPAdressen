using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    partial class AS400
    {
        public static DBaseDataContext db = new DBaseDataContext();

        public static List<AS400> GetAS400ById(int bId, int gId)
        {

            if (bId != 0 && gId != 0)
            {
                //Entfernt die Verbindungen des Gerätes mit dem Nutzer
                var beg =
                    from b in db.AS400
                    where b.Id_Benutzer == bId && b.Id_Geräte == gId
                    select b;
                return beg.ToList();
            }
            else if (gId != 0)
            {   //Entfernt die Verbindungen des Gerätes "nur"
                var beg =
                    from b in db.AS400
                    where b.Id_Geräte == gId
                    select b;
                return beg.ToList();
            }
            else // if (bId != 0)
            {   //Entfernt die Verbindungen des Benutzers "nur"
                var beg =
                    from b in db.AS400
                    where b.Id_Benutzer == bId
                    select b;
                return beg.ToList();
            }
        }

        internal static bool Exist(string p)
        {
            return false;
        }

        internal static void DeleteAll(int gId)
        {
            DBaseDataContext d = new DBaseDataContext();
            var beg =
                from b in d.AS400
                where b.Id_Geräte == gId
                select b;

            try
            {
                d.AS400.DeleteAllOnSubmit(beg);
                d.SubmitChanges();
            }
            catch (Exception)
            {
            }

        }
    }
}