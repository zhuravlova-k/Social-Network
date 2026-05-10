using System;

namespace SocialTopology
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        
        public virtual string GetInfo()
        {
            return $"Entity ID: {Id}";
        }
    }
}