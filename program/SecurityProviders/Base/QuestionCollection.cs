using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    public class QuestionCollection  :CollectionBase
    {
        public Question this[int index]
        {
            get
            {
                return (Question)this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public int Add(Question question)
        {
            return List.Add(question);
        }

        public void Remove(Question question)
        {
            List.Remove(question);
        }

        public void Insert(int index, Question question)
        {
            List.Insert(index, question);
        }

        public int IndexOf(Question question)
        {
            return List.IndexOf(question);
        }

        public bool Contains(Question question)
        {
            return List.Contains(question);
        }
    }
}
