using Kidz.DatabaseConnection;
using Kidz.Models;

namespace Kidz.Queries
{
    public class HitHistoryQuery
    {
        private DatabaseContex _databaseContex;

        public HitHistoryQuery(DatabaseContex databaseContex)
        {
            this._databaseContex = databaseContex;
        }

        public bool insertHistory(HitHistoryModel modelHitHistory)
        {
            _databaseContex.entityHitHistory.Add(modelHitHistory);
            return _databaseContex.SaveChanges() > 0;
        }
    }
}