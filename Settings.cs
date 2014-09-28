using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using VaultMigration.Config;

namespace VaultMigration
{
    internal static class Settings
    {
        private static readonly Configuration ConfigFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private static readonly VaultMigrationSection VaultMigrationSection = (VaultMigrationSection) ConfigFile.GetSection("vaultMigration");
        public static string UserName
        {
            get { return ConfigurationManager.AppSettings["UserName"]; }
        }

        public static string Password
        {
            get { return ConfigurationManager.AppSettings["Password"]; }
        }

        public static IEnumerable<VaultApplication> Applications
        {
            get
            {
                return VaultMigrationSection.VaultApplications.OfType<VaultApplication>();
            }
        }

        public static string DownloadPath {
            get
            {
                var element = VaultMigrationSection.Settings.OfType<VaultMigration.Config.SettingElement>().FirstOrDefault<VaultMigration.Config.SettingElement>(x => x.Key == "DownloadPath");                
                if (
                    element != null)
                    return element.Value;
                return null;                                
            }
        }

        public static void UpdateApplication(VaultApplication application)
        {
            try
            {
                VaultMigrationSection.VaultApplications.OfType<VaultApplication>()
                    .Single(x => x.ID == application.ID)
                    .IsMigrated =
                    application.IsMigrated;
                ConfigFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("vaultMigration");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }   
}