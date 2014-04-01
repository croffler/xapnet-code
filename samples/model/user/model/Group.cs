
using System;

using GigaSpaces.Core.Metadata;

[Serializable]
public class Group   {

    [SpaceProperty(StorageType = StorageType.Document)]
    public long? Id { get; set; }

    public String Name { get; set; }

}
