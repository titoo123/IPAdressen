using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    partial class Anwendungen
    {
        public static DBaseDataContext db = new DBaseDataContext();


        public static Anwendungen GetAnwendungenById(int id)
        {
            var anw =
                      from a in db.Anwendungen
                      where a.Id == id
                      select a;

            return anw.First();
        }
        public static Anwendungen GetAnwendungenById(string id)
        {
            return GetAnwendungenById(Convert.ToInt16(id));
        }
        public static List <Anwendungen> GetAnwendungenByBenutzer(Benutzer benutzer)
        {
            List<Anwendungen> a_list = new List<Anwendungen>();

            var bea =
                from ba in db.BenutzerAnwendung
                where ba.Id_Benutzer == benutzer.Id
                select ba;

            foreach (var a in bea)
            {
                var anw =
                    from aw in db.Anwendungen
                    where aw.Id == a.Id_Anwendung
                    select aw;
                a_list.Add(anw.First());
            }
          
            return a_list;
        }

    }


}