using System.Configuration;

namespace VaultMigration.Config
{
    /// <summary>
    /// The class that holds onto each element returned by the configuration manager.
    /// </summary>
    public class VaultApplication : ConfigurationElement
    {
        [ConfigurationProperty("id", IsKey = true, IsRequired = true)]
        public int ID
        {
            get
            {
                return ((int)(base["id"]));
            }
            set
            {
                base["id"] = value;
            }
        }

        [ConfigurationProperty("isMigrated", DefaultValue = false, IsRequired = false)]
        public bool IsMigrated
        {
            get
            {
                return ((bool)(base["isMigrated"]));
            }
            set
            {
                base["isMigrated"] = value;
            }
        }

        [ConfigurationProperty("name", IsKey = false, IsRequired = true)]
        public string Name
        {
            get
            {
                return ((string)(base["name"]));
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("dataID", IsKey = false, IsRequired = false)]
        public int DataID
        {
            get
            {
                return ((int)(base["dataID"]));
            }
            set
            {
                base["dataID"] = value;
            }
        }
    }
}