using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Domain.Common
{
    public interface IBaseEntity
    {
    }

    public interface IBaseEntity<out TKey> : IBaseEntity where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }
        DateTime CreatedAt { get; set; }
    }
}
