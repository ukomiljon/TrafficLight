using System.Collections.Generic;
using System.Threading.Tasks;
using TrafficLightCentralSystem.Model.DTO;

namespace TrafficLightCentralSystem.Repositories
{
    public interface IEventRepository
    {
        public Task Create(TrafficLightIntersection trafficBound);
        public Task<TrafficLightIntersection> Get(string IntersectionName);
        public Task<IEnumerable<TrafficLightIntersection>> GetAll();
    }
}
