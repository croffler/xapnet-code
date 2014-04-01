using System;
using System.Collections.Generic;

using GigaSpaces.Core.Metadata;

namespace samples.model
{
    [CompoundSpaceIndex(Paths = new[] { "Name", "CreditLimit" })]
    [SpaceClass]
    public class User
    {
        [SpaceID(AutoGenerate = false)]
        [SpaceRouting]
        public long? Id { set; get; }
        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public String Name { set; get; }
        public double? Balance { set; get; }
        [SpaceIndex(Type = SpaceIndexType.Extended)]
        public double? CreditLimit { set; get; }

        [SpaceProperty(StorageType = StorageType.Document)]
        public Nullable<EAccountStatus> Status { set; get; }

        [SpaceProperty(StorageType = StorageType.Document)]
        [SpaceIndex(Path = "ZipCode", Type = SpaceIndexType.Basic)]
        public Address Address { set; get; }

        //	[SpaceIndex(Path = "[*]")]
        //    private String[] Comment { set; get; }

        [SpaceProperty(StorageType = StorageType.Document)]
        [SpaceIndex(Path = "HOME")]
        public Dictionary<String, String> Contacts { set; get; }

        [SpaceProperty(StorageType = StorageType.Document)]
        [SpaceIndex(Path = "[*].Id")]
        public List<Group> Groups { set; get; }

        [SpaceProperty(StorageType = StorageType.Document)]
        [SpaceIndex(Path = "[*]")]
        public List<int?> Ratings { set; get; }

        public User()
        {
        }

        public void addContact(EContactType type, String contact)
        {
            if (Contacts == null)
            {
                Contacts = new Dictionary<String, String>();
            }
            Contacts.Add(type.ToString(), contact);
        }

    }
}