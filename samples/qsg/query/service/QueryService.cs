using System;

using GigaSpaces.Core; 
using GigaSpaces.Core.Metadata;
using GigaSpaces.Core.Document;
using GigaSpaces.Core.Linq;


using samples.model;

public class QueryService {

	private ISpaceProxy proxy;


	public QueryService(ISpaceProxy proxy)
	{
		this.proxy = proxy;
	}

	public User findUserById() {
		return proxy.ReadById<User>(1L);
	}

	public User[] findUsersByIds() {
		object[] ids  = new object[3]{ 1L, 2L, 3L };

		IReadByIdsResult<User> result = proxy.ReadByIds<User>(ids);
		return result.ResultsArray;
	}

	public User findUserByTemplate() {
		User user = new User();
		user.Name="John Dow";
		return proxy.Read<User>(user);
	}

	public User[] findUsersByTemplate() {
		User user = new User();
		user.Status=EAccountStatus.ACTIVE;
		return proxy.ReadMultiple<User>(user);
	}

	public User[] sqlFindUsersByName() {
		SqlQuery<User> query = new SqlQuery<User>("Name = 'John Dow'");
		return proxy.ReadMultiple<User>(query);
	}

	public User[] sqlFindUsersByNameAndCreditLimit() {
		SqlQuery<User> query = new SqlQuery<User>("Name = 'John Dow' AND CreditLimit > 1000");
		return proxy.ReadMultiple<User>(query);
	}

	public User[] sqlFindUsersByNameAndIds() {
		SqlQuery<User> query = new SqlQuery<User>("Name = 'John Dow' AND Id IN(1,3,5)");
		return proxy.ReadMultiple<User>(query);
	}

	public User[] sqlParameterFindUsersByName() {
		SqlQuery<User> query = new SqlQuery<User>("Name = ?")
			.SetParameter(1, "John Dow");
		return proxy.ReadMultiple<User>(query);
	}

	public User[] sqlParameterFindUsersByNameAndCreditLimit() {
		SqlQuery<User> query = new SqlQuery<User>("Name = ? AND CreditLimit > ?");
		query.SetParameter(1, "John Dow");
		query.SetParameter(2, 1000.00);
		return proxy.ReadMultiple<User>(query);
	}

	public User[] sqlFindUsersByZipCode() {
		SqlQuery<User> query = new SqlQuery<User>("Address.ZipCode = 12345");
		return proxy.ReadMultiple<User>(query);
	}

	public User[] sqlFindUsersByPhoneNumber() {
		SqlQuery<User> query = new SqlQuery<User>( "Contacts.HOME = '770-123-5555'");
		return proxy.ReadMultiple<User>(query);
	}

	public User[] findUsersByNameAndProjection() {
		SqlQuery<User> query = new SqlQuery<User>( "Name = ?"){Projections = new []{"Name"}};
		query.SetParameter (1, "John Dow");

		return proxy.ReadMultiple<User>(query); 
	}

	public User[] findUsersByRating() {
		SqlQuery<User> sqlQuery = new SqlQuery<User>("Ratings[*] = 1");
		return proxy.ReadMultiple<User>(sqlQuery);
	}

	public User[] findUsersByGroup() {
		SqlQuery<User> sqlQuery = new SqlQuery<User>("Groups[*].Id = 1L");
		return proxy.ReadMultiple<User>(sqlQuery);
	}

	public User[] findUsersByComment() {
		return proxy.ReadMultiple<User>(new SqlQuery<User>( "Comment[*]='existing'"));
	}

	public User[] findUsersBySQLExpression() {
		SqlQuery<User> query = new SqlQuery<User>( "Name like 'J%'");
		return proxy.ReadMultiple<User>(query);
	}

	public User[] findUsersByRegularExpression() {
		// Match all entries of type User that have a name that starts with J or
		// R:
		SqlQuery<User> query = new SqlQuery<User>( "Name rlike '(J|R).*'");
		return proxy.ReadMultiple<User>(query);
	}

	public User[] findUsersByEnum() {
		SqlQuery<User> query = new SqlQuery<User>(  "Status = ?");
		query.SetParameter(1, EAccountStatus.BLOCKED);
		return proxy.ReadMultiple<User>(query);
	}

	public SpaceDocument readProductById() {
        return proxy.ReadById(new IdQuery<SpaceDocument>("Product", "av-9876")); 
	}

	public SpaceDocument readProductByTemplate() {
		SpaceDocument template = new SpaceDocument("Product");
		template["Name"]= "Jet Propelled Pogo Stick";
        return proxy.Read<SpaceDocument>(template);
	}

	public SpaceDocument[] readProductsBySQL() {
		SqlQuery<SpaceDocument> query = new SqlQuery<SpaceDocument>("Product","Price > ?");
		query.SetParameter(1, 15.0);
		return proxy.ReadMultiple<SpaceDocument>(query);
	}


	public Account getAccount()
	{
		//var query = from p in proxy.Query<Account> ()
		//		where p.number == "1234"
		//     select p; 
	 
		//foreach (var Account in query) { 
			// ... 
		//}
		return null;
	}


	public void readWithIterator()
	{
		SqlQuery<Account> query1 = new SqlQuery<Account>( "fName like 'f%'");
		SqlQuery<Account> query2 = new SqlQuery<Account>( "lName like 'l%'");

		//	ISpaceIterator iterator = new ISpaceIterator(

		//	.addTemplate(query1)
		//	.addTemplate(query2)
		//	.bufferSize(100) // Limit of the number of objects to store for each iteration.
		//	.iteratorScope(IteratorScope.CURRENT_AND_FUTURE) ;

	}

	public void readWithTransaction()
	{
		ITransactionManager mgr = GigaSpacesFactory.CreateDistributedTransactionManager ();

		// Create a transaction using the transaction manager:
		using (ITransaction txn = mgr.Create ()) {
			try {
				// ...
				SqlQuery<User> query = new SqlQuery<User> ("Contacts.HOME = '770-123-5555'");
				User user = proxy.Read<User> (query, txn, long.MaxValue, ReadModifiers.ExclusiveReadLock);
				// ....
				txn.Commit ();
			} catch (Exception ex) {

                System.Console.WriteLine(ex.ToString());
				// rollback the transaction
				txn.Abort ();
			}
		}
	}

	public void queryExamples()
	{
		SpaceTypeDescriptorBuilder descriptorBuilder = new SpaceTypeDescriptorBuilder("WithCompoundIndex");
		descriptorBuilder.AddFixedProperty("IntProp", typeof(int));
		descriptorBuilder.AddFixedProperty("StringProp", typeof(String));
		descriptorBuilder.AddFixedProperty("LongProp", typeof(long));
		descriptorBuilder.AddCompoundIndex(new []{ "IntProp", "StringProp" });
		descriptorBuilder.AddCompoundIndex(new []{ "LongProp", "StringProp" });
	
		// Option 1 - Use the fluent setParameter(int index, Object value) method:
		SqlQuery<Account> query1 = new SqlQuery<Account>("num > ? or num < ? and name = ?");
		query1.SetParameter(1, 2);
		query1.SetParameter(2, 3);
		query1.SetParameter(3, "smith");

		query1.Routing = 1;

		// Option 2 - Use the setParameters(Object... parameters) method:
		SqlQuery<Account> query2 = new SqlQuery<Account>("num > ? or num < ? and name = ?");
		//	query2.SetParameter(2, 3, "smith");

		// Option 3: Use the constructor to pass the parameters:
		//SqlQuery<Account> query3 = new SqlQuery<Account>("num > ? or num < ? and name = ?", 2, 3, "smith");
	}
}
