using System;
using System.Collections.Generic;

using GigaSpaces.Core;
using GigaSpaces.Core.Metadata;
using GigaSpaces.Core.Executors;

using samples.model;

[Serializable]
public class MerchantUserTask : ISpaceTask<HashSet<long?>> {

	private long? merchantId;	 

	public MerchantUserTask(long? merchantId) {
		this.merchantId = merchantId;
	}

	public HashSet<long?> Execute(ISpaceProxy spaceProxy, ITransaction tx)  {
		SqlQuery<Payment> query = new SqlQuery<Payment>( "merchantId = ? ");
		query.SetParameter(1, merchantId);

		Payment[] payments = spaceProxy.ReadMultiple<Payment>(query, int.MaxValue);
		HashSet<long?> userIds = new HashSet<long?>();

		// Eliminate duplicate UserId
		if (payments != null) {
			for (int i = 0; i < payments.Length; i++) {
				userIds.Add(payments[i].UserId);
			}
		}
		return userIds;
	}
}
