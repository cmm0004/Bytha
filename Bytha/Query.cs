using Bytha.Models;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bytha
{
    public class Query 
    {
        public IEnumerable<Room> GetRooms() => new Room[] { new Room { Name = "hello", Id = Guid.NewGuid() } };
    }
}
