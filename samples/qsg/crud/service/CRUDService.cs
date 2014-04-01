using System;

using GigaSpaces.Core;  
using GigaSpaces.Core.Document;
using GigaSpaces.Core.Metadata;

using samples.model;

public class CRUDService {

	private ISpaceProxy proxy;

	public CRUDService(ISpaceProxy proxy)
	{
		this.proxy = proxy;
	}

	public void writeUser(){ 

		User user = new User();
		user.Id=1L;
		user.Name="John Smith";
	//	user.setComment(new String[] {"This", "is","a","comment"});
	//	user.Balance=10.5;
	//	user.CreditLimit=1000.00;
	//	user.Status=EAccountStatus.ACTIVE;

		// Write the user to the space
		proxy.Write(user);
	}

	public void writeUsers() {
		User[] users = new User[2];

		users[0] = new User();
		users[0].Id=1;
		users[0].Name="John Dow";
		users[0].Status=EAccountStatus.ACTIVE;

		users[1] = new User();
		users[1].Id=2;
		users[1].Name="John Dow";
		users[1].Status=EAccountStatus.ACTIVE;

		proxy.WriteMultiple(users);
	}

	public void writeOnlyWithLease() {
		User user = new User();
		user.Id= 1;
		user.Name="John Smith";
		user.Status=EAccountStatus.ACTIVE;

		proxy.Write(user,null,long.MaxValue,0, WriteModifiers.WriteOnly);
	}

	public void partialUpdate() {
		User user = new User();
		user.Id =1;
		user.Name="John Dow";
		user.Status=EAccountStatus.ACTIVE;
		proxy.Write(user);

		// Update the User
		user.Status=EAccountStatus.BLOCKED;
		proxy.Write(user, null, 0, long.MaxValue, WriteModifiers.PartialUpdate);
	}

	public void ChangeSet() {
		User user = new User();
		user.Id=1L;
		user.Name="John Dow";
		user.Status=EAccountStatus.ACTIVE;
		proxy.Write(user);

		IdQuery<User> idQuery = new IdQuery<User>(1L);
		IChangeResult<User> changeResult = 
			proxy.Change<User>(idQuery,
        	new ChangeSet().Set("Name", "Testing"));

		if (changeResult.NumberOfChangedEntries == 0) {
			Console.WriteLine("Entry does not exist");
		}
    }

	public SpaceDocument createDocumemt() {
		DocumentProperties properties = new DocumentProperties ();

		properties ["CatalogNumber"] = "av-9876";
		properties["Category"]= "Aviation";
		properties["Name"] = "Jet Propelled Pogo Stick";
		properties["Price"]= 19.99;
		properties["Tags"]= new String[4] {"New", "Cool", "Pogo", "Jet"};

		DocumentProperties p2 = new DocumentProperties();
		p2["Manufacturer"]="Acme";
		p2["RequiresAssembly"]=true;
		p2["NumberOfParts"]= 42;
		properties["Features"]=p2;
						 
		SpaceDocument document = new SpaceDocument("Product", properties);
		proxy.Write(document);

		return document;
	}

	public void registerProductType() {
		// Create type Document descriptor:
		SpaceTypeDescriptorBuilder typeBuilder = new SpaceTypeDescriptorBuilder("Product");
		typeBuilder.SetIdProperty("CatalogNumber");
		typeBuilder.SetRoutingProperty("Catagory");
		typeBuilder.AddPathIndex("Name");
		typeBuilder.AddPathIndex("Price", SpaceIndexType.Extended);
		ISpaceTypeDescriptor typeDescriptor = typeBuilder.Create();

		// Register type:
		proxy.TypeManager.RegisterTypeDescriptor(typeDescriptor);
	}


    // ROffler long
	public User takeUserById() {
		return proxy.TakeById<User>(1L);
	}

	public User takeUserByTemplate() {
		User template = new User();
		template.Name="John Dow";

        return  proxy.Take<User>(template);
	}

	public User takeUserBySQL() {
		SqlQuery<User> query = new SqlQuery<User>("Status = ?");
		query.SetParameter(1, EAccountStatus.BLOCKED);

		return proxy.Take<User>(query);	 
	}

	public void clearUserByTemplate() {
		User template = new User();
		proxy.Clear(template);
	}

	public void clearUserBySQL() {
		SqlQuery<User> query = new SqlQuery<User>("Name = ?");
		query.SetParameter(1, "John Dow");
		proxy.Clear(query);
	}

	public void clearAllObjectInSpace() {
		proxy.Clear(null);
	}

	// Transactions
	public void createPayment() {
	
		ITransactionManager mgr = GigaSpacesFactory.CreateDistributedTransactionManager();

		// Create a transaction using the transaction manager:
		ITransaction trn = mgr.Create();

		try{
			Payment payment = new Payment();
			payment.CreatedDate=DateTime.Today;
			payment.Status=ETransactionStatus.PROCESSED;

			proxy.Write(payment);

			// Commit the transaction
			trn.Commit();
		}
		catch(Exception ex) {
            System.Console.WriteLine(ex.ToString());
			// rollback the transaction
			trn.Abort ();
		}
	}


	public void test()
	{
		proxy.ReadModifiers = ReadModifiers.ExclusiveReadLock;
	}

}
