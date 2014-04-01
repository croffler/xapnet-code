using System;

using GigaSpaces.Core.Metadata;

namespace samples.model
{
    [SpaceClass(FifoSupport = FifoSupport.Operation)]
    public class Payment
    {
        [SpaceID(AutoGenerate = true)]
        public String Id { set; get; }
        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public long? UserId { set; get; }
        [SpaceRouting]
        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public long? MerchantId { set; get; }
        public String Description { set; get; }
        public double? PaymentAmount { set; get; }
        public Nullable<ETransactionStatus> Status;
        [SpaceIndex(Type = SpaceIndexType.Basic)]
        public DateTime CreatedDate { set; get; }

        public Payment(String paymentId)
        {
            this.Id = paymentId;
        }

        public Payment()
        {
        }
    }
}
