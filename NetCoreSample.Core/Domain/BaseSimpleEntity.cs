using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreSample.Core.Domain
{
    public abstract class BaseSimpleEntity
    {
        public Guid Id { get; set; }
    }
}
