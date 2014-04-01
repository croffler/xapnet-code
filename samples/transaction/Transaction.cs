using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GigaSpaces.Core;
using GigaSpaces.Core.Exceptions;

using samples.model;

namespace samples.transaction
{
    class Transaction
    {
        private ISpaceProxy proxy;

        //http://docs.gigaspaces.com/xap97net/transactions.html
        public void readWithTransaction()
        {
            ITransactionManager mgr = GigaSpacesFactory.CreateDistributedTransactionManager();

            // Create a transaction using the transaction manager:
            ITransaction trn = mgr.Create();

            try
            {
                // ...
                SqlQuery<User> query = new SqlQuery<User>("Contacts.Home = '770-123-5555'");
                User user = proxy.Read<User>(query, trn, 0, ReadModifiers.RepeatableRead);
                // ....
                trn.Commit();
            }
            catch (Exception e)
            {
                // rollback the transaction
                trn.Abort();
            }
        }

        //   http://docs.gigaspaces.com/xap97net/transaction-read-modifiers.html
        public void exclusiveReadLock()
        {
            // this will allow all read operations with this proxy to use Exclusive Read Lock mode
            proxy.ReadModifiers = ReadModifiers.ExclusiveReadLock;
           
            Lock lok = new Lock();
            lok.key = 1;
            lok.data = "my data";
            proxy.Write(lok, null, long.MaxValue);

            ITransactionManager mgr = GigaSpacesFactory.CreateDistributedTransactionManager ();
            ITransaction txn1 = mgr.Create();

            Lock lock_template1 = new Lock();
            lock_template1.key = 1;

            Lock lock1 = proxy.Read<Lock>(lock_template1, txn1, 10000);

            if (lock1 != null)
            {
                Console.WriteLine("Transaction " + txn1 + " Got exclusive Read Lock on Entry:"
                    + lock1.key);
            }
        }


        //http://docs.gigaspaces.com/xap97net/transaction-pessimistic-locking.html
        public void executeUsers(long?[] userIDs)
        {
            ITransactionManager mgr = GigaSpacesFactory.CreateDistributedTransactionManager();
            ITransaction txn = mgr.Create();

            User[] users = new User[userIDs.Length];

            for (int i = 0; i < userIDs.Length; i++)
            {
                users[i] = proxy.ReadById<User>(userIDs[i], userIDs[i], txn, 5000, ReadModifiers.ExclusiveReadLock);

                if (users[i] != null)
                    users[i].Status = EAccountStatus.ACTIVE;
            }

            Object[] rets = proxy.WriteMultiple(users, txn, WriteModifiers.UpdateOnly);

            for (int i = 0; i < rets.Length; i++)
            {
                if (rets[i] == null)
                {
                    throw (new Exception("can't update object " + users[i]));
                }

                if ( rets[i].GetType().IsSubclassOf(typeof(Exception)))
                {

                }
                /*           if (rets[i] instanceof Exception)
                           {
                               if (rets[i] instanceof EntryNotInSpaceException)
                               {
                                   throw (EntryNotInSpaceException)rets[i];
                               }
                               else if (rets[i] instanceof OperationTimeoutException )
                               {
                                   throw (OperationTimeoutException)rets[i];
                               }
                               else
                               { 
                                   throw (Exception)rets[i];
                               }
                           }
                     */
            }
        }
    }
}
