using System;
using GigaSpaces.Core.Metadata;
using GigaSpaces.Core.Document;

namespace samples.model
{
    [SpaceClass]
    public class Merchant
    {
        [SpaceID(AutoGenerate = false)]
        [SpaceRouting]
        public long? Id { set; get; }
        public String Name { set; get; }
        public Double Receipts { set; get; }
        public Double FeeAmount { set; get; }
        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public Nullable<ECategoryType> Category { set; get; }
        public Nullable<EAccountStatus> Status { set; get; }
        [SpaceDynamicProperties]
        public DocumentProperties ExtraInfo { set; get; }

    }
}