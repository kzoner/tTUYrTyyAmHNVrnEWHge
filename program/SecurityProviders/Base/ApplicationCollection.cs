using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    public class ApplicationCollection : CollectionBase
    {
        public Application this[int index]
        {
            get
            {
                return (Application)this[index]; 
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(Application application)
        {
            return List.Add(application);
        }

        public void Remove(Application application)
        {
            List.Remove(application);
        }
        
        public void Insert(int index, Application application)
        {
            List.Insert(index, application);
        }

        public int IndexOf(Application application)
        {
            return List.IndexOf(application);
        }

        public bool Contains(Application application)
        {
            return List.Contains(application);
        }
    }
}
