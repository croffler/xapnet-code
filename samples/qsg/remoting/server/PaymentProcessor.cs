using System;

using GigaSpaces.Core;
using GigaSpaces.XAP.Remoting;

using samples.model;

[SpaceRemotingService]
public class PaymentProcessor : IPaymentProcessor {

	public Payment processPayment(Payment payment) {
		Console.WriteLine("Processing payment ");
		return payment;
	}
}
