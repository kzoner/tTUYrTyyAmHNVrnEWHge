using System;
using System.Data;
using System.Data.SqlClient;

using Inside.DataProviders;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders.DataAccess
{
    internal class QuestionAdapter
    {
        private SQLDatabase m_db = null;

        protected SQLDatabase Database
        {
            get
            {
                if (m_db == null)
                {
                    m_db = new SQLDatabase(ConfigurationHelper.ReadKey(Constants.STR_AUTHDB_CONN_APPSETTING_KEY));
                }
                return m_db;
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
                QuestionCollection collection = new QuestionCollection();

                using (IDataReader dr = Database.ExecuteReader("UspGetAllQuestions", CommandType.StoredProcedure))
                {
                    while (dr.Read())
                    {
                        Question question = Populate(dr);

                        collection.Add(question);
                    }
                }

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Question Populate(IDataReader dr)
        {
            try
            {
                int questionID = dr.GetInt32(dr.GetOrdinal("QuestionID"));
                string description = dr["Description"].ToString();

                Question question = new Question(questionID, description);

                return question;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
