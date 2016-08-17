using System.Threading.Tasks;
using Vnap.Entity;
using Xamarin.Forms;

namespace Vnap.Repository
{
    public static class DatabaseHelper
    {
        public static async Task InitialDatabase()
        {
            var db = DependencyService.Get<ISQLiteProvider>().GetSQLiteAsyncConnection("Vnap.db3");

            await db.CreateTableAsync<Plant>();
            await db.CreateTableAsync<PlantDisease>();
        }
    }
}
