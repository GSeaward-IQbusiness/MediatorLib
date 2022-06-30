using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multigate.Titanic.MediatR.Shared.Entities
{
    public class CommandResponse<TValue> : CqrsResponse
    {
        public TValue Value { get; set; }
    }
}
