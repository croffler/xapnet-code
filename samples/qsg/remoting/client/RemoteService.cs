using System;

using GigaSpaces.Core;
using GigaSpaces.XAP.Remoting;

using samples.model;

public class RemoteService {

	private ISpaceProxy proxy;


	public RemoteService(ISpaceProxy proxy)
	{
		this.proxy = proxy;
	}

//	private IPaymentProcessor dataProcessor;

	public void executePaymentService() {
	//	Payment payment = dataProcessor.processPayment(new Payment());
	//	Console.WriteLine(payment);
	}
}
