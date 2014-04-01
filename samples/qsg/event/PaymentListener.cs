using System;

using GigaSpaces.Core.Events;
using GigaSpaces.XAP.Events.Notify;  
using GigaSpaces.XAP.Events;

using samples.model;

[NotifyEventDriven(NotifyType = DataEventType.Write | DataEventType.Update)]
[TransactionalEvent(TransactionType = TransactionType.Distributed)]
public class PaymentListener {
	[EventTemplate]
	Payment unprocessedData() {
		Payment template = new Payment();
		template.Status=ETransactionStatus.CANCELLED;
		return template;
	}

	[DataEventHandler]
	public Payment eventListener(Payment ev) {
		// process Payment
		Console.WriteLine("Notifier Received a payment");
		return null;
	}
}