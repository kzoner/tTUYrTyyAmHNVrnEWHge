using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    public class PermissionCollection : CollectionBase
    {
        public Permission this[int index]
        {
            get
            {
                return (Permission)this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(Permission permission)
        {
            return List.Add(permission);
        }

        public void Remove(Permission permission)
        {
            List.Remove(permission);
        }

        public void Insert(int index, Permission permission)
        {
            List.Insert(index, permission);
        }

        public int IndexOf(Permission permission)
        {
            return List.IndexOf(permission);
        }

        public bool Contains(Permission permission)
        {
            return List.Contains(permission);
        }
    }
}
