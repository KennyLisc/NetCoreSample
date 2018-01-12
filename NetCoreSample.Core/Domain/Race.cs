using System;

namespace NetCoreSample.Core.Domain
{
    public class Race: BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}
