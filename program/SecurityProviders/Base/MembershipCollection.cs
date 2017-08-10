using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    class MembershipCollection : CollectionBase
    {
        public Membership this[int index]
        {
            get
            {
                return (Membership)this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(Membership membership)
        {
            return List.Add(membership);
        }

        public void Remove(Membership membership)
        {
            List.Remove(membership);
        }

        public void Insert(int index, Membership membership)
        {
            List.Insert(index, membership);
        }

        public int IndexOf(Membership membership)
        {
            return List.IndexOf(membership);
        }

        public bool Contains(Membership membership)
        {
            return List.Contains(membership);
        }
    }
}
