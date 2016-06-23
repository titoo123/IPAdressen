using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    partial class BenutzerAnwendung
    {
        public static DBaseDataContext db = new DBaseDataContext();

        public static List<BenutzerAnwendung> GetBGByBIdAAid(int bId, int aId)
        {

            var beg =
                    from b in db.BenutzerAnwendung
                    where b.Id_Benutzer == bId && b.Id_Anwendung == aId
                    select b;
            return beg.ToList();
        }
    }


}