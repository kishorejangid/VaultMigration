using System.Configuration;

namespace VaultMigration.Config
{
    public class VaultMigrationSection : ConfigurationSection
    {
        /// <summary>
        /// The value of the property here "Folders" needs to match that of the config file section
        /// </summary>
        [ConfigurationProperty("applications")]
        public VaultApplications VaultApplications
        {
            get { return ((VaultApplications)(base["applications"])); }
            set { base["applications"] = value; }
        }

        [ConfigurationProperty("settings")]
        public SettingCollection Settings
        {
            get { return (SettingCollection)base["settings"]; }
            set { base["settings"] = value; }
        }        
    }
}