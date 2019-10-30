using System;
using System.Collections.Generic;
using System.Text;

namespace SeratGraphic.DomainModels.Entities
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
