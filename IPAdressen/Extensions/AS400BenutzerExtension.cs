using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    partial class AS400Benutzer
    {
        public static DBaseDataContext db = new DBaseDataContext();

        public static AS400Benutzer Get_AS400_Benutzer_By_Id(int id)
        {
            var anw =
                      from a in db.AS400Benutzer
                      where a.Id == id
                      select a;

            return anw.First();
        }
        internal static AS400Benutzer Get_AS400_Benutzer_By_Gerät(AS400Benutzer b)
        {
            var ger = from t in db.AS400Benutzer
                      where t.Kennung == b.Kennung && t.Passwort == b.Passwort
                      select t;

            return ger.First();
        }

        public static List<AS400Benutzer> Get_AS400_Benutzer_List_By_Gerät(AS400Geräte e)
        {
            List<AS400Benutzer> b_list = new List<AS400Benutzer>();

            var ger =
                from g in db.AS400
                where e.Id == g.Id_Geräte
                select g;

            foreach (var g in ger)
            {
                var ben =
                    from ge in db.AS400Benutzer
                    where ge.Id == g.Id_Benutzer
                    select ge;
                b_list.Add(ben.First());
            }

            return b_list;
        }
        public static bool Exist(string kennung)
        {

            var ben = from d in db.AS400Benutzer
                      where d.Kennung == kennung
                      select d;

            if (ben.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }

}