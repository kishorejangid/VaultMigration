using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VaultMigration
{
    public class VaultObject
    {
        public int ObjectID { get; set; }
        public int ParentID { get; set; }
        public int ApplicationID { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public DateTime CreateTime { get; set; }
        public string Path { get; set; }
        public byte[] Attachment { get; set; }
    }
}
