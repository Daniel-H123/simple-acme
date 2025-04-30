using Fclp.Internals.Extensions;
using PKISharp.WACS.Clients.IIS;
using PKISharp.WACS.Plugins.Base.Options;
using PKISharp.WACS.Plugins.InstallationPlugins.Interfaces;
using PKISharp.WACS.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PKISharp.WACS.Plugins.InstallationPlugins
{
    internal class IISOptions : InstallationPluginOptions, IIISSiteOptions
    {
        public List<IISSiteOptions> Sites { get; set; } = new List<IISSiteOptions>();
        public long? SiteId { get; set; }
        public string? NewBindingIp { get; set; }
        public int? NewBindingPort { get; set; }
        public bool UpdateOnly { get; set; } = IISClient.DefaultUpdateOnly;

        /// <summary>
        /// Show details to the user
        /// </summary>
        /// <param name="input"></param>
        public override void Show(IInputService input)
        {
            base.Show(input);

            input.Show($"Global settings");
            if (SiteId != null)
            {
                input.Show("SiteId", SiteId.ToString(), level: 2);
            }
            if (NewBindingIp != null)
            {
                input.Show("NewBindingIp", NewBindingIp, level: 2);
            }
            if (NewBindingPort != null)
            {
                input.Show("NewBindingPort", NewBindingPort.ToString(), level: 2);
            }
            if (UpdateOnly)
            {
                input.Show("UpdateOnly", UpdateOnly.ToString(), level: 2);
            }
            input.CreateSpace();

            if (Sites != null)
            {
                input.Show($"Sites");
                input.CreateSpace();

                var siteLimit = 10;
                var s = 0;
                foreach (var site in Sites.Take(siteLimit))
                {
                    input.Show($"Site {++s}/{Sites.Count}");
                    site.Show(input);
                    input.CreateSpace();
                }
            }
        }
    }

    internal class IISSiteOptions : IIISSiteOptions
    {
        public long SiteId { get; set; }
        public string? NewBindingIp { get; set; }
        public int? NewBindingPort { get; set; }
        public bool UpdateOnly { get; set; } = IISClient.DefaultUpdateOnly;

        public void Show(IInputService input)
        {
            input.Show("SiteId", SiteId.ToString(), level: 3);
            if (NewBindingIp != null)
            {
                input.Show("NewBindingIp", NewBindingIp, level: 3);
            }
            if (NewBindingPort != null)
            {
                input.Show("NewBindingPort", NewBindingPort.ToString(), level: 3);
            }
            if (UpdateOnly)
            {
                input.Show("UpdateOnly", UpdateOnly.ToString(), level: 3);
            }
        }

        public BindingOptions BindingOptions(BindingOptions? bindingOptions)
        {
            bindingOptions ??= new BindingOptions();
            bindingOptions.WithSiteId(SiteId);

            if (!string.IsNullOrEmpty(NewBindingIp))
            {
                bindingOptions.WithIP(NewBindingIp);
            }
            if (NewBindingPort.HasValue)
            {
                bindingOptions.WithPort(NewBindingPort.Value);
            }
            if (UpdateOnly)
            {
                bindingOptions.WithUpdateOnly(UpdateOnly);
            }

            return bindingOptions;
        }
    }
}