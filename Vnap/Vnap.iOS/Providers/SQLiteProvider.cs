using System;
using System.IO;
using SQLite;
using Vnap.iOS.Providers;
using Vnap.Repository;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteProvider))]

namespace Vnap.iOS.Providers
{
    public class SQLiteProvider : ISQLiteProvider
    {
        public SQLiteAsyncConnection GetSQLiteAsyncConnection(string databaseName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library"); 
            var path = Path.Combine(libraryPath, databaseName);
            var connection = new SQLiteAsyncConnection(path);
            return connection;
        }
    }
}
