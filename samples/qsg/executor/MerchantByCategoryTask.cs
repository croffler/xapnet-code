using System;
using System.Collections.Generic;

using GigaSpaces.Core;
using GigaSpaces.Core.Metadata;
using GigaSpaces.Core.Executors;

using samples.model;

[Serializable]
public class MerchantByCategoryTask : IDistributedSpaceTask<List<Merchant>, Merchant[]> {

	private ECategoryType categoryType;

	public MerchantByCategoryTask(ECategoryType categoryType) {
		this.categoryType = categoryType;
	}
	
	public Merchant[] Execute(ISpaceProxy spaceProxy, ITransaction tx)   {
		SqlQuery<Merchant> query = new SqlQuery<Merchant>( "category = ?");
		query.SetParameter(1, categoryType);
		return spaceProxy.ReadMultiple<Merchant>(query, int.MaxValue);
	}
		
	public List<Merchant> Reduce(SpaceTaskResultsCollection<Merchant[]> results){

		List<Merchant> list = new List<Merchant>();
		 
		foreach (SpaceTaskResult<Merchant[]>  result in results) {
			if (result.Exception != null) {
				throw result.Exception;
			}

			foreach (Merchant res in result.Result) {
				list.Add (res);
			}
	    }
		return list;
	}
}


 