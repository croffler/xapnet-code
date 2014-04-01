using System;

using GigaSpaces.Core; 
using GigaSpaces.XAP.Events.Notify;  
using GigaSpaces.XAP.Events;

using samples.model;

[NotifyEventDriven] 
public class PaymentQueryListener {

	[EventTemplate]
	SqlQuery<Payment> unprocessedData() {
		SqlQuery<Payment> template = new SqlQuery<Payment>( "status = ?");
		template.SetParameter(1, ETransactionStatus.CANCELLED);
		return template;
	}

	[DataEventHandler]
	public Payment eventListener(Payment ev) {
		return null;
	}
}