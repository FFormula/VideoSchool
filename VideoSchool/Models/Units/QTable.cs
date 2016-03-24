using System.Data;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public class QTable
    {
        Shared shared;

        // Those queries contain filter, but not contain neither sorts nor limits.
        string queryCount; // a Query to get count of lines in table
        // ex: SELECT COUNT(*) FROM user WHERE status = 1

        string queryLines; // a Query to get limited amount of lines
        // ex: SELECT id, name, email FROM user WHERE status = 1

        public int lines { get; private set; }
        public int limit = 50; // how many rows are shown

        // result table to show
        public DataTable table { get; private set; }

        public QTable (Shared shared)
        {
            this.shared = shared;
            lines = 0;
            table = null;
        }

        public void Init (string queryCount, string queryLines)
        {
            this.queryCount = queryCount;
            this.queryLines = queryLines;
            Select();
        }

        public void Select ()
        {
            lines = shared.db.ScalarInt(queryCount);
            table = shared.db.Select   (queryLines);
        }
    }
}