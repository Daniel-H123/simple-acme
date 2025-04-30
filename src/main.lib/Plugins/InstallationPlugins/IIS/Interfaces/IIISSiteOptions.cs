using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKISharp.WACS.Plugins.InstallationPlugins.Interfaces
{
    internal interface IIISSiteOptions
    {
        public string? NewBindingIp { get; set; }
        public int? NewBindingPort { get; set; }
        public bool UpdateOnly { get; set; }
    }
}
