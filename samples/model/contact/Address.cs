using System;

using GigaSpaces.Core.Metadata;

[Serializable]
public class Address
{

    public String Street { set; get; }
    public String City { set; get; }
    public String State { set; get; }
    public Nullable<ECountry> Country { set; get; }

    [SpaceProperty(StorageType = StorageType.Document)]
    public int? ZipCode { set; get; }

}