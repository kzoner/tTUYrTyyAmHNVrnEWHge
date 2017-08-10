using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    public class ResourceTypeCollection : CollectionBase
    {
        public ResourceType this[int index]
        {
            get
            {
                return (ResourceType)this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(ResourceType resourceType)
        {
            return List.Add(resourceType);
        }

        public void Remove(ResourceType resourceType)
        {
            List.Remove(resourceType);
        }

        public void Insert(int index, ResourceType resourceType)
        {
            List.Insert(index, resourceType);
        }

        public int IndexOf(ResourceType resourceType)
        {
            return List.IndexOf(resourceType);
        }

        public bool Contains(ResourceType resourceType)
        {
            return List.Contains(resourceType);
        }
    }
}
