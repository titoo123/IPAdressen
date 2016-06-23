using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    partial class BenutzerGeräte
    {
        public static DBaseDataContext db = new DBaseDataContext();

        public static List <BenutzerGeräte> GetBGByBId( int bId, int gId) {

            if (bId != 0 && gId != 0)
            {
                //Entfernt die Verbindungen des Gerätes mit dem Nutzer
                var beg =
                    from b in Benutzer.db.BenutzerGeräte
                    where b.Id_Benutzer == bId && b.Id_Gerät == gId
                    select b;

                return beg.ToList();
            }
            else if (gId != 0)
            {   //Entfernt die Verbindungen des Gerätes "nur"
                var beg =
                    from b in Benutzer.db.BenutzerGeräte
                    where b.Id_Gerät == gId
                    select b;
                return beg.ToList();
            }
            else // if (bId != 0)
            {   //Entfernt die Verbindungen des Benutzers "nur"
                var beg =
                    from b in Benutzer.db.BenutzerGeräte
                    where b.Id_Benutzer == bId
                    select b;
                return beg.ToList();
            }
            
        }
        public static void Delete_BG_From_DB(int bId, int gId)
        {
             DBaseDataContext d = new DBaseDataContext();
             var beg =
                from b in d.BenutzerGeräte
                where b.Id_Benutzer == bId && b.Id_Gerät == gId
                select b;

             try
             {
                 d.BenutzerGeräte.DeleteAllOnSubmit(beg);
                 d.SubmitChanges();
             }
             catch (Exception)
             {
             }

        }

        internal static void Delete_BG_From_DB(int bId)
        {
            DBaseDataContext d = new DBaseDataContext();
            var beg =
                from b in d.BenutzerGeräte
                where b.Id_Benutzer == bId
                select b;

            try
            {
                d.BenutzerGeräte.DeleteAllOnSubmit(beg);
                d.SubmitChanges();
            }
            catch (Exception)
            {
            }

        }

        internal static void DeleteAll(int gId)
        {
            DBaseDataContext d = new DBaseDataContext();
            var beg =
                from b in d.BenutzerGeräte
                where b.Id_Gerät == gId
                select b;

            try
            {
                d.BenutzerGeräte.DeleteAllOnSubmit(beg);
                d.SubmitChanges();
            }
            catch (Exception)
            {
            }

        }
    }


}