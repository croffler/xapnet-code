using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using GigaSpaces.Core;

using samples.model;

namespace samples.query
{
    class IteratorExample
    {

        public void getAllUsers()
        {
            var spaceProxy = GigaSpacesFactory.FindSpace("/./NivSpace");
            var query = new SqlQuery<User>("Name='Niv'");

            SpaceIteratorConfig config = new SpaceIteratorConfig
            {
                IteratorScope = IteratorScope.ExistingAndFutureEntries,
                BufferSize = 1000
            };

            foreach (var user in spaceProxy.GetSpaceIterator<User>(query, config))
            {
                Console.WriteLine("Got a User: " + user);
            }
        }
    }
}
