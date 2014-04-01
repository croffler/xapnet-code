using System;
using GigaSpaces.Core.Document; 

public class AuditRecord : SpaceDocument {

	private const String TYPE_NAME = "AuditRecord";
	private const String ID = "id";
	private const String TIMESTAMP = "timeStamp";
	private const String USERNAME = "userName";
	private const String APPLICATION = "application";
	private const String CONTENT = "content";

	public AuditRecord() : base(TYPE_NAME)
	{
	}

	public long? getId() {
		return (long?)this[ID];
	}

	public void setId(long? id) {
		this[ID]= id;
	}

	public int? getTimeStamp() {
		return (int?)this[TIMESTAMP];
	}

	public void setTimeStamp(int? timeStamp) {
		this[TIMESTAMP]= timeStamp;
	}

	public String getApplication() {
		return (String)this[APPLICATION];
	}

	public void setApplication(String application) {
		this[APPLICATION] =  application;
	}

	public String getAuditContent() {
		return (String)this[CONTENT];
	}

	public void setAuditContent(String auditContent) {
		this[CONTENT] =  auditContent;
	}

	public String getUserName() {
		return (String)this[USERNAME];
	}

	public void setUserName(String userName) {
		this[USERNAME] =  userName;
	}

}
