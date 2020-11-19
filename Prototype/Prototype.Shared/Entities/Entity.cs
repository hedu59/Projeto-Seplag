using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Shared.Entities
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            RegisterDate = DateTime.UtcNow;
            Active = true;
        }

        public Guid Id { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public bool Active { get; private set; }

        public void Disable()
        {
            Active = false;
        }
    }
}
