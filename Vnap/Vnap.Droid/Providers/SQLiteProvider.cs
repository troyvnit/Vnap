using System.IO;
using SQLite;
using Vnap.Droid.Providers;
using Vnap.Repository;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteProvider))]
namespace Vnap.Droid.Providers
{
    public class SQLiteProvider : ISQLiteProvider
    {
        public SQLiteAsyncConnection GetSQLiteAsyncConnection(string databaseName)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, databaseName);
            var connection = new SQLiteAsyncConnection(path);
            return connection;
        }
    }
}