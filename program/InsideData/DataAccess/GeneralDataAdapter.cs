using Inside.DataProviders;

namespace Inside.InsideData.DataAccess
{
    public class GeneralDataAdapter
    {
        private SQLDatabase m_db = null;

        protected SQLDatabase Database
        {
            get
            {
                if (m_db == null)
                {
                    m_db = new SQLDatabase(ConfigurationHelper.ReadKey(Constants.STR_ISD_DATA_CONN_KEY));
                }
                return m_db;
            }
        }
    }
}
