using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficLightCentralSystem.Model.DTO;

namespace TrafficLightCentralSystem.Repositories
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly ConcurrentDictionary<string, TrafficLightIntersection> _trafficBounds = new ConcurrentDictionary<string, TrafficLightIntersection>();

        public Task Create(TrafficLightIntersection trafficBound)
        {
            _trafficBounds.TryAdd(trafficBound.IntersectionName, @trafficBound);
            return Task.Run(() => true);
        }

        public Task<TrafficLightIntersection> Get(string IntersectionName)
        {
            if (_trafficBounds.TryGetValue(IntersectionName, out var trafficBound))
            {
                return Task.Run(() => trafficBound);
            }

            throw new KeyNotFoundException();
        }

        public Task<IEnumerable<TrafficLightIntersection>> GetAll()
        {
            return Task.Run(() => _trafficBounds.Select(item => item.Value));
        }

    }
}
