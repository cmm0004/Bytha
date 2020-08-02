using Bytha.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Neo4jClient;

namespace Bytha.Controllers
{
    [ApiController]
    [Route("rooms")]
    public class RoomsController : ControllerBase
    {
        private readonly ILogger<RoomsController> _logger;
        private readonly IAsyncSession _graphClient;
        //private readonly IGraphClient _graphClient;

        public RoomsController(ILogger<RoomsController> logger,
            IDriver graphClient)
            //IGraphClient graphClient)
        {
            _logger = logger;
            _graphClient = graphClient.AsyncSession(o => o.WithDatabase("neo4j"));
               // graphClient;
        }

        
        [HttpGet]
        public async Task<List<Room>> GetRooms()
        {
            var rooms = new List<Room>();

            var cursor = await _graphClient.RunAsync("MATCH (a:Room) RETURN a");
            var records = await cursor.ToListAsync();
            foreach (var record in records)
            {
                var nodeProps = JsonSerializer.Serialize(record[0].As<INode>().Properties);
                rooms.Add(JsonSerializer.Deserialize<Room>(nodeProps));
            }
            return rooms;
            //        return _graphClient.Cypher
            //.Match("(r:Room)")
            //.Return(r => r.As<Room>())
            //.Results;
        }
    }
}
