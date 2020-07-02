using System;

namespace Promotion.Engine.Domain.SeedWork
{
    public abstract class Entity
    {
        Guid _id;

        public virtual Guid Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }

        //Followed by domain event implementations as per DDD concepts
    }
}
