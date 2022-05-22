using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidNite.Service.Events.Dto
{
    public class CreateEventDto
    {
        public string EventPicture { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Host { get; set; }
        public string City { get; set; }
        public int AmountOfGuest { get; set; }
        public Double AverageAge { get; set; }
        public Double MaleFemaleRatio { get; set; }
    }
}
