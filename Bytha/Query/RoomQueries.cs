using Bytha.Dataloaders;
using Bytha.Models;
using Bytha.Repositories;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bytha.Query
{
    [ExtendObjectType(Name = "Query")]
    public class RoomQueries
    {
        public Task<List<Room>> GetRoomsAsync([Service] IRoomRepository roomRepo)
        {
            return roomRepo.GetRooms();
        }

        public Task<Room> GetRoomByIdAsync(
            Guid id,
            RoomByIdDataloader roomById,
            CancellationToken cancellationToken)
        {
            return roomById.LoadAsync(id, cancellationToken);
        }
    }
}

