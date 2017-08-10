using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    public class UserCollection : CollectionBase
    {
        public User this[int index]
        {
            get
            {
                return (User)this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(User user)
        {
            return List.Add(user);
        }

        public void Remove(User user)
        {
            List.Remove(user);
        }

        public void Insert(int index, User user)
        {
            List.Insert(index, user);
        }

        public int IndexOf(User user)
        {
            return List.IndexOf(user);
        }

        public bool Contains(User user)
        {
            return List.Contains(user);
        }
    }
}
