using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    public class OperationCollection : CollectionBase
    {
        public Operation this[int index]
        {
            get
            {
                return (Operation)this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(Operation operation)
        {
            return List.Add(operation);
        }

        public void Remove(Operation operation)
        {
            List.Remove(operation);
        }

        public void Insert(int index, Operation operation)
        {
            List.Insert(index, operation);
        }

        public int IndexOf(Operation operation)
        {
            return List.IndexOf(operation);
        }

        public bool Contains(Operation operation)
        {
            return List.Contains(operation);
        }
    }
}
