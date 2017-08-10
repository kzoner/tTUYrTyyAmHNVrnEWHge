using System;
using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders
{
    public class QuestionManager
    {

        private QuestionAdapter m_questionAdapter = null;

        private QuestionAdapter Adapter
        {
            get
            {
                if (m_questionAdapter == null)
                {
                    m_questionAdapter = new QuestionAdapter();
                }
                return m_questionAdapter;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public QuestionCollection GetAllQuestions()
        {
            try
            {
                return Adapter.GetAllQuestions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
