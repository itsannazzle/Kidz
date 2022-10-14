using Kidz.DatabaseConnection;
using Kidz.Models;

namespace Kidz.Queries
{
    public class ResponseHistoryQuery
    {
        private DatabaseContex _databaseContex;

        public ResponseHistoryQuery(DatabaseContex databaseContex)
        {
            this._databaseContex = databaseContex;
        }

        public bool insertResponseHistory(ResponseHistoryModel modelResponseHistory)
        {
            _databaseContex.entityResponseHistory.Add(modelResponseHistory);
            return _databaseContex.SaveChanges() > 0;
        }
    }
}