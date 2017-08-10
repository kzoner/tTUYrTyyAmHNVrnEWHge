using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    public class RoleCollection : CollectionBase
    {
        public Role this[int index]
        {
            get
            {
                return (Role)this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(Role role)
        {
            return List.Add(role);
        }

        public void Remove(Role role)
        {
            List.Remove(role);
        }

        public void Insert(int index, Role role)
        {
            List.Insert(index, role);
        }

        public int IndexOf(Role role)
        {
            return List.IndexOf(role);
        }

        public bool Contains(Role role)
        {
            return List.Contains(role);
        }
    }
}
