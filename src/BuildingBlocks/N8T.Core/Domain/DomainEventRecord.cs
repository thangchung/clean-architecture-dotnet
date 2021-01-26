using System;
using System.Collections.Generic;

namespace N8T.Core.Domain
{
    public sealed class DomainEventRecord
    {
        public string EventType { get; set; }
        public List<KeyValuePair<string, string>> MetaData { get; set; }
        public string CorrelationId { get; set; }
        public DateTime Created { get; set; }
    }
}
