using ClientDirectory.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClientDirectory.Core
{
    public class Client : BaseEntity
    {
        public string Identification { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public ICollection<Address> Addresses { get; set; }
    }
}
