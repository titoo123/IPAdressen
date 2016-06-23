using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPAdressen
{
    partial class AS400Adressen
    {
        public static DBaseDataContext db = new DBaseDataContext();

        internal static void DeleteAllByG(int gId)
        {
            DBaseDataContext d = new DBaseDataContext();
            var beg =
                from b in d.AS400Adressen
                where b.Id_AS400Geräte == gId
                select b;

            try
            {
                d.AS400Adressen.DeleteAllOnSubmit(beg);
                d.SubmitChanges();
            }
            catch (Exception)
            {
            }

        }
        internal static void DeleteAllByIP(int gId)
        {
            DBaseDataContext d = new DBaseDataContext();
            var beg =
                from b in d.AS400Adressen
                where b.Id_Adressen == gId
                select b;

            try
            {
                d.AS400Adressen.DeleteAllOnSubmit(beg);
                d.SubmitChanges();
            }
            catch (Exception)
            {
            }

        }
    }
}