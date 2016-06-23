using System;
using System.Collections.Generic;
using System.Linq;


namespace IPAdressen
{
    partial class Geräte
    {
        public static DBaseDataContext db = new DBaseDataContext();


        public static List<Geräte> GetAll()
        {
            var ger =
                    from g in db.Geräte
                    select g;

            List<Geräte> list = new List<Geräte>(ger.ToList());

            return list;
        }

        public static Geräte Get(int id)
        {
            var ger =
                  from g in db.Geräte
                  where g.Id == id
                  select g;

            return ger.First();
        }
        public static Geräte Get(string id)
        {
            return Get(Convert.ToInt16(id));
        }

        public static int GetId(Geräte e) 
        {
            var ger =
                from g in db.Geräte
                where (g.Name == e.Name && g.Standort == e.Standort && g.Art == e.Art && g.Kommentar == e.Kommentar )
                select g.Id;
            return ger.First(); 
        }

        internal static bool Exist(string n) {

            DBaseDataContext d = new DBaseDataContext();

            var m = from s in d.Geräte
                    where s.Name == n
                    select s;

            if (m.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        internal static void Delete(int id) {

        DBaseDataContext d = new DBaseDataContext();

        //DELETE BGs
        IPAdressen.BenutzerGeräte.DeleteAll(id);



        //DELETE ADRESSEN

        IPAdressen.Adressen.DeleteAll(id);

        //DELETE GERÄT
        var ger =   from g in d.Geräte
                    where g.Id == id
                    select g;



        d.Geräte.DeleteAllOnSubmit(ger);

        try
        {
            d.SubmitChanges();
        }
        catch (Exception)
        {
            
        }

        }

    }

}