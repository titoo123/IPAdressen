using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    partial class Benutzer
    {
        public static DBaseDataContext db = new DBaseDataContext();

        public static int GetIdByBenutzer(Benutzer b) {

            var bid =
                from e in db.Benutzer
                where e.Name == b.Name && e.Vorname == b.Vorname
                select e.Id;

            return bid.First();
        
        }


        public static List<Benutzer> GetAllBenutzer() {

            var ben =
                from b in db.Benutzer
                select b;
            return ben.ToList();

        }

        public static DropDownList GetAllBenutzer(DropDownList ddl) {

            var ben =
                from b in db.Benutzer
                select new { b.Id, b.Vorname, b.Name };
            

            ddl.DataSource = ben;
            ddl.DataBind();
           
            return ddl;
        }

        public static Benutzer GetBenutzerById(int id)
        {
            var ger =
                  from g in db.Benutzer
                  where g.Id == id
                  select g;

            return ger.First();
        }
        public static Benutzer GetBenutzerById(string id)
        {
            return GetBenutzerById(Convert.ToInt16(id));
        }



        public static List <Benutzer> GetBenutzerByGerät(Geräte e)
        {
            List<Benutzer> b_list = new List<Benutzer>();

            var ger =
                from g in db.BenutzerGeräte
                where e.Id == g.Id_Gerät
                select g;

            foreach (var g in ger)
            {
                var ben =
                    from ge in db.Benutzer
                    where ge.Id == g.Id_Benutzer
                    select ge;
                b_list.Add(ben.First());
            }

            return b_list;
        }

        public static bool Exist(string vorname, string nachname) {

            var ben = from d in db.Benutzer
                      where d.Vorname == vorname
                      && d.Name == nachname
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