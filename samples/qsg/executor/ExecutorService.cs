using System.Collections.Generic;

using GigaSpaces.Core;
using GigaSpaces.Core.Executors;

using samples.model;

public class ExecutorService {

	private ISpaceProxy proxy;

    public ExecutorService(ISpaceProxy proxy)
    {
        this.proxy = proxy;
    }
	public void executeTask()  {
		MerchantUserTask task = new MerchantUserTask(2);

		HashSet<long?> result = proxy.Execute(task,2);
	}

	public void executeAsyncTask()   {
		MerchantUserTask task = new MerchantUserTask(2);
		//	AsyncListener l = new AsyncListener();
		
		//proxy.execute(task, l);
	}

	//	public void executeTaskWithRouting()  {
	//	MerchantUserTask task = new MerchantUserTask(2);
	//	Merchant merchant = new Merchant ();
	//	merchant.setId (2);

	//	HashSet<int?>  result =proxy.Execute(task,merchant);

		// Route the task to partion 2
		//	AsyncFuture<HashSet<Long>> result = proxy.execute(task, 2);
		//HashSet<Long> hashSet = result.get();
	//}

	public void executeTaskWithRoutingPOJO(){
		MerchantUserTask task = new MerchantUserTask(2);

		Merchant merchant = new Merchant();
		merchant.Id=2;

		HashSet<long?>  result = proxy.Execute(task,merchant);

		//	AsyncFuture<HashSet<Long>> result = proxy.execute(task, merchant);
		//HashSet<Long> hashSet = result.get();
	}

	//	public void executeTaskRoutingAnnoation() {
	//	MerchantUserTask task = new MerchantUserTask(2);

		//AsyncFuture<HashSet<Long>> result = proxy.execute(task);
		//HashSet<Long> hashSet = result.get();
	//}

	public void executeDistributedTask(){
		MerchantByCategoryTask task = new MerchantByCategoryTask(
				ECategoryType.AUTOMOTIVE);

		//Execute the task on all the primary nodes with in the cluster
		List<Merchant> result = proxy.Execute(task);
	}

	public void executeDistributedTaskPartitions(){
		MerchantByCategoryTask task = new MerchantByCategoryTask(
			ECategoryType.AUTOMOTIVE);

		object[] ids  = new object[3]{ 1, 2, 3 };

	 
		Merchant[] result = proxy.Execute(task, ids);

	}

	public void executeDistributedTaskAsync(){
		MerchantByCategoryTask task = new MerchantByCategoryTask(
			ECategoryType.AUTOMOTIVE);
			
		IAsyncResult<List<Merchant>> asyncResult = proxy.BeginExecute(task, null /*callback*/, null /*state object*/);
		//	...
		//This will block until the result execution has arrived
		asyncResult.AsyncWaitHandle.WaitOne();
		//Gets the actual result of the async execution
		List<Merchant> result = proxy.EndExecute(asyncResult);
	}

}
