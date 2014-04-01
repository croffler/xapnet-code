using System;
using GigaSpaces.Core;  
using GigaSpaces.XAP.Events.Polling;  
using GigaSpaces.XAP.Events.Notify; 
using GigaSpaces.XAP.Events;

using GigaSpaces.Core.Metadata;
 
public class EventService {

	private ISpaceProxy spaceProxy;

    public EventService(ISpaceProxy spaceProxy)
    {
        this.spaceProxy = spaceProxy;
    }

	public void registerNotifierListener() {
	
		NotifyEventListenerContainer<PaymentListener> notifyEventListenerContainer = 
			new NotifyEventListenerContainer<PaymentListener>(spaceProxy);
			
		notifyEventListenerContainer.Template = new PaymentListener();
		notifyEventListenerContainer.Start ();

		// when needed dispose of the container
		notifyEventListenerContainer.Dispose();
	}

	public void registerNotifierListenerOnlyUpdate() {
		//		SimpleNotifyEventListenerContainer notifyEventListenerContainer = new SimpleNotifyContainerConfigurer(
		//	proxy).eventListenerAnnotation(new PaymentListener())
		//		.notifyUpdate(true).notifyWrite(false).notifyTake(false)
		//		.notifyContainer();
		//notifyEventListenerContainer.start();
	}

	public void registerPollingListener() {
		PollingEventListenerContainer<AuditListener> pollingEventListenerContainer = 
			new PollingEventListenerContainer<AuditListener> (spaceProxy);
			                                                                          
		pollingEventListenerContainer.Template = new AuditListener ();
		pollingEventListenerContainer.Start(); 
	}

}
