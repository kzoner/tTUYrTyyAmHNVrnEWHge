using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    public class OperationCategoryCollection : CollectionBase
    {
        public OperationCategory this[int index]
        {
            get
            {
                return (OperationCategory)this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(OperationCategory operationCategory)
        {
            return List.Add(operationCategory);
        }

        public void Remove(OperationCategory operationCategory)
        {
            List.Remove(operationCategory);
        }

        public void Insert(int index, OperationCategory operationCategory)
        {
            List.Insert(index, operationCategory);
        }

        public int IndexOf(OperationCategory operationCategory)
        {
            return List.IndexOf(operationCategory);
        }

        public bool Contains(OperationCategory operationCategory)
        {
            return List.Contains(operationCategory);
        }
    }
}
