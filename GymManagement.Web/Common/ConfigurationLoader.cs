using GymManagement.Web.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Common
{
    public static class ConfigurationLoader
    {
        public static IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public static List<MembershipPackageViewModel> membershipPackageViewModels = new List<MembershipPackageViewModel>();


        public static void SetPackage()
        {
            var configurationSection = configuration.GetSection("MembershipPackages").GetChildren();
            foreach (var config in configurationSection)
            {
                membershipPackageViewModels.Add(
                    new MembershipPackageViewModel
                    {
                        Package = config.Key,
                        Amount = config.Value
                    }
                    );
            }
        }
    }
}
