using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_ping.ApiModels
{
    public class SiteWithStatusDto
    {
        public string URL { get; set; }
        public bool IsActive { get; set; }
    }
}
