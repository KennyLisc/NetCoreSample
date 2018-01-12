using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreSample.Core.Domain
{
    public class UserScore: BaseEntity
    {
        public string UserName { get; set; }
        public int Score { get; set; }
    }
}
