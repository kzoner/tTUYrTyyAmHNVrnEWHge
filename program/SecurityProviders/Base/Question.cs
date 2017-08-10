using System;

namespace Inside.SecurityProviders
{
    public class Question
    {
        #region Member variables

        private int m_QuestionID = -1;
        private string m_Description = string.Empty;

        #endregion

        #region Constructors

        public Question() { }

        public Question(int questionID, string description)
        {
            m_QuestionID = questionID;
            m_Description = description;
        }

        #endregion

        #region Properties

        public int QuestionID
        {
            get
            {
                return m_QuestionID;
            }
        }

        public string Description
        {
            get
            {
                return m_Description;
            }

            set
            {
                m_Description = value;
            }
        }

        #endregion
    }
}
