using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_ping.ApiModels
{
    public class SiteToCheckDto
    {
        public string URL { get; set; }
        public bool CheckDomainOnly { get; set; }
    }
}
