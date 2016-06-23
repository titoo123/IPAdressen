using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    partial class Adressen
    {
        public static DBaseDataContext db = new DBaseDataContext();

        public static List<Adressen> GetAll(int id)
        {
            var ber =
                   from b in db.Adressen
                   where b.Id == id
                   select b;

            return ber.ToList();
        }

        public static Adressen Get(int id)
        {
            var ber =
                      from b in db.Adressen
                      where b.Id == id
                      select b;

            return ber.First();
        }
        public static Adressen Get(string id)
        {
            return Get(Convert.ToInt16(id));
        }

        public static bool Exist(string ad1, string ad2, string ad3, string ad4, string ad5) {

            int a = Convert.ToInt32(ad4);
            
            var ipa = from i in db.Adressen
                      where
                         i.Adb1 == Convert.ToByte(ad1)
                      && i.Adb2 == Convert.ToByte(ad2)
                      && i.Adb3 == Convert.ToByte(ad3)
                      select i;

            if (ad5 == String.Empty)
            {
                foreach (var d in ipa)
                {
                    if (d.Adb4 == a)
                    {
                        return true;
                    }
                }
                                
            }
            else
            {
                int b = Convert.ToInt32(ad5);
                foreach (var l in ipa)
                {
                    if (l.Adb4 <= a && l.Adb5 >= b )
                    {
                        return true;
                    }
                }


            }
            return false;
            
        }

        public static void DeleteAll(int gId) {

            var gad = from k in db.Adressen
                      where k.Id_Geräte == gId
                      select k;


            foreach (var i in gad)
            {
                Delete(i.Id);
            }
        }

        internal static void Delete(int id) {

            //DELETE ASIPs
            IPAdressen.AS400Adressen.DeleteAllByIP(id);

            DBaseDataContext d = new DBaseDataContext();
            var ads = from z in d.Adressen
                      where z.Id == id
                      select z;



            try
            {
                d.Adressen.DeleteAllOnSubmit(ads);
                d.SubmitChanges();
            }
            catch (Exception)
            {

            }

        }

    }


}