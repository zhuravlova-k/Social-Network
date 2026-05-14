using System;

namespace SocialTopology
{
    public abstract class BaseEntity : IDisplayable
    {
        public Guid Id { get; set; }
        
        public abstract string GetInfo()
        {
            return $"Entity ID: {Id}";
        }
    }
}