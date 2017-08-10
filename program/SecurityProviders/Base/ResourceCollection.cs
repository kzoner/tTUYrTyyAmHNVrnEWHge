using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    public class ResourceCollection : CollectionBase
    {
        public Resource this[int index]
        {
            get
            {
                return (Resource)this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(Resource resource)
        {
            return List.Add(resource);
        }

        public void Remove(Resource resource)
        {
            List.Remove(resource);
        }

        public void Insert(int index, Resource resource)
        {
            List.Insert(index, resource);
        }

        public int IndexOf(Resource resource)
        {
            return List.IndexOf(resource);
        }

        public bool Contains(Resource resource)
        {
            return List.Contains(resource);
        }
    }
}
