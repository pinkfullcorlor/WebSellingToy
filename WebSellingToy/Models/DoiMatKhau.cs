using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSellingToy.Models
{
    public class DoiMatKhau
    {
        public string MKCu { get; set; }
        public string MKMoi { get; set; }
        public string NhapLaiMK { get; set; }

        public DoiMatKhau()
        {
            MKCu = "";
            MKMoi = "";
            NhapLaiMK = "";
        }
    }
}