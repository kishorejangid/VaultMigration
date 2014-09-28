using System.Configuration;

namespace VaultMigration.Config
{
    /// <summary>
    /// The class that holds onto each element returned by the configuration manager.
    /// </summary>
    public class SettingElement : ConfigurationElement
    {
        [ConfigurationProperty("key", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Key
        {
            get
            {
                return ((string)(base["key"]));
            }
            set
            {
                base["key"] = value;
            }
        }

        [ConfigurationProperty("value", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Value
        {
            get
            {
                return ((string)(base["value"]));
            }
            set
            {
                base["value"] = value;
            }
        }
    }
}