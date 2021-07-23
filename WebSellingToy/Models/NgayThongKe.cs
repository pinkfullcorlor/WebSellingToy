using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSellingToy.Models
{
    public class NgayThongKe
    {
        public DateTime dtTuNgay { get; set; }
        public DateTime dtDenNgay { get; set; }

        public NgayThongKe()
        {
            dtTuNgay = Convert.ToDateTime("01/01/2010");
            dtDenNgay = Convert.ToDateTime("12/31/3099");
        }
    }
}