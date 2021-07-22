using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSellingToy.Models;

namespace WebSellingToy.ViewModel
{
    public class UserLayoutViewModel
    {
        public IEnumerable<WebSellingToy.Models.GioHang> GioHangs { get; set; }

            
    }
}