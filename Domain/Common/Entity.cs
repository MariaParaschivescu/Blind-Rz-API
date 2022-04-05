using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Common
{
    public abstract class Entity<T>: IEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
