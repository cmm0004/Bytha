using Bytha.Models;
using Bytha.Repositories;
using HotChocolate;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bytha.Query
{
    [ExtendObjectType(Name = "Query")]
    public class RoomQueries
    {
        public Task<List<Room>> GetRooms([Service] IRoomRepository roomRepo)
        {
            return roomRepo.GetRooms();
        }
    }
}
