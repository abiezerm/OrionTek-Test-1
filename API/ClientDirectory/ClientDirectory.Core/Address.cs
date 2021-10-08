using ClientDirectory.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClientDirectory.Core
{
    public class Address : BaseEntity
    {
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public string StreetName { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Client Client { get; set; }
    }
}
