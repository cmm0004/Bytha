using Bytha.Models;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Bytha.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetRooms();
        Task<IReadOnlyDictionary<Guid, Room>> GetRoomByIdsAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken);
    }
    public class RoomRepository : IRoomRepository
    {
        private readonly ILogger<RoomRepository> _logger;
        private readonly IAsyncSession _graphClient;

        public RoomRepository(ILogger<RoomRepository> logger,
            IDriver graphClient)
        {
            _logger = logger;
            _graphClient = graphClient.AsyncSession(o => o.WithDatabase("neo4j"));
          
        }
        public async Task<List<Room>> GetRooms()
        {
            _logger.LogInformation("starting query");
            var rooms = new List<Room>();

            var cursor = await _graphClient.RunAsync("MATCH (a:Room) RETURN a");
            var records = await cursor.ToListAsync();
            foreach (var record in records)
            {
                var nodeProps = JsonSerializer.Serialize(record[0].As<INode>().Properties);
                rooms.Add(JsonSerializer.Deserialize<Room>(nodeProps));
            }
            return rooms;
        }

        public async Task<IReadOnlyDictionary<Guid, Room>> GetRoomByIdsAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var rooms = new List<Room>();
            _logger.LogInformation($"Match (a:Room) Where (a.Id in [{string.Join(", ", keys.Select(x => $"\"{x}\""))}] ) Return (a)");
            var cursor = await _graphClient.RunAsync($"Match (a:Room) Where (a.Id in [{string.Join(", ", keys.Select(x => $"\"{x}\""))}] ) Return (a)");
            var records = await cursor.ToListAsync();
            foreach (var record in records)
            {
                var nodeProps = JsonSerializer.Serialize(record[0].As<INode>().Properties);
                rooms.Add(JsonSerializer.Deserialize<Room>(nodeProps));
            }

           return rooms.ToDictionary(x => x.Id, x => x);
        }
    }
}
