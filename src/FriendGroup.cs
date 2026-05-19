using System;
using System.Collections.Generic;

namespace SocialTopology
{
    public class FriendGroup : BaseEntity
    {
        public string GroupName { get; set; }
        
        // юзери існують незалежно від групи
        public List<User> Members { get; set; }

        public FriendGroup() 
        { 
            GroupName = ""; 
            Members = new List<User>(); 
        }

        public FriendGroup(string name)
        {
            Id = Guid.NewGuid();
            GroupName = name;
            Members = new List<User>();
        }

        public void AddMember(User user)
        {
            if (!Members.Contains(user))
            {
                Members.Add(user);
            }
        }

        public void RemoveMember(User user)
        {
            Members.Remove(user);
        }

        public override string GetInfo()
        {
            return $"[Group] {GroupName} (members: {Members.Count})";
        }
    }
}