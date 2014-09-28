using System.Configuration;

namespace VaultMigration.Config
{
    /// <summary>
    /// The collection class that will store the list of each element/item that
    /// is returned back from the configuration manager.
    /// </summary>
    [ConfigurationCollection(typeof(VaultApplication), AddItemName = "application")]
    public class VaultApplications : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new VaultApplication();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((VaultApplication)(element)).ID;
        }

        public VaultApplication this[int idx]
        {
            get
            {
                return (VaultApplication)BaseGet(idx);
            }
        }

       
    }
}