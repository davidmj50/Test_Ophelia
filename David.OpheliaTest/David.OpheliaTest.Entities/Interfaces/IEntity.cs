using System;
using System.Collections.Generic;
using System.Text;

namespace David.OpheliaTest.Entities.Interfaces
{
    public interface IEntity
    {
        public int Id { get; set; }
        DateTime LastUpdate { get; set; }
        bool Active { get; set; }
    }
}
