using Bytha.Models;
using Bytha.Repositories;
using HotChocolate.DataLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bytha.Dataloaders
{
    public class RoomByIdDataloader : BatchDataLoader<Guid, Room>
    {
        private readonly IRoomRepository _roomRepository;
        public RoomByIdDataloader(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        protected override Task<IReadOnlyDictionary<Guid, Room>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
           return _roomRepository.GetRoomByIdsAsync(keys, cancellationToken);
        }
    }
}
