using SQLite;

namespace Vnap.Repository
{
    public interface ISQLiteProvider
    {
        SQLiteAsyncConnection GetSQLiteAsyncConnection(string databaseName);
    }
}
