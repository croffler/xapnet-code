using System;
using GigaSpaces.Core.Metadata;


namespace samples.model
{
    [SpaceClass]
    public class Account
    {
        [SpaceID]
        [SpaceRouting]
        public long? Id { set; get; }
        public String Number { set; get; }
        public double? Receipts { set; get; }
        public double? FeeAmount { set; get; }
        public Nullable<EAccountStatus> Status { set; get; }
        [SpaceVersion]
        public int Version { set; get; }

        public Account()
        {
            this.Status = EAccountStatus.INACTIVE;
        }
    }
}