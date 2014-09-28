using System.Configuration;

namespace VaultMigration.Config
{
    [ConfigurationCollection(typeof(SettingElement))]
    public class SettingCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SettingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SettingElement)(element)).Key;
        }

        public SettingElement this[int idx]
        {
            get
            {
                return (SettingElement)BaseGet(idx);
            }
        }
    }
}