using Kidz.DatabaseConnection;
using Kidz.Models;

namespace Kidz.Queries
{
    public class RequestHistoryQuery
    {
        private DatabaseContex _databaseContex;

        public RequestHistoryQuery(DatabaseContex databaseContex)
        {
            this._databaseContex = databaseContex;
        }

        public bool insertRequestHistory(RequestHistoryModel modelRequestHistory)
        {
            _databaseContex.entityRequestHistory.Add(modelRequestHistory);
            return _databaseContex.SaveChanges() > 0;
        }
    }
}