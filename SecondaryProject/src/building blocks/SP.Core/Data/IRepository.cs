using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Core.DomainObjects;

namespace SP.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {



    }

}
