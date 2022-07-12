using System.Collections.Generic;

namespace ClientAddress.Models
{
    public class ClientElement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int AddressCount { get; set; }
    }

    public class ClientForCreateEditModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }

    public class AddressForCreateEditModel
    {
        public int ClientId { get; set; }
        public string Description { get; set; }
    }

    public class AddressElement
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public string Description { get; set; }
    }

    public class AddressViewModel
    {
        public int ClientId { get; set; }
        public IEnumerable<AddressElement> Elements { get; set; }
    }
}