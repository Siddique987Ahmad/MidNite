using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MidNite.Core.Events
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        public string EventPicturePath { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Host { get; set; }
        public string City { get; set; }
        public int AmountOfGuest { get; set; }
        public float AverageAge { get; set; }
        public string MaleFemaleRatio { get; set; }
    }
}
