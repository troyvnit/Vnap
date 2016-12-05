using System.Threading.Tasks;
using Vnap.Entity;
using Xamarin.Forms;
using Image = Vnap.Entity.Image;

namespace Vnap.Repository
{
    public static class DatabaseHelper
    {
        public static async Task InitialDatabase()
        {
            var db = DependencyService.Get<ISQLiteProvider>().GetSQLiteAsyncConnection("Vnap.db3");

            await db.CreateTableAsync<Plant>();
            await db.CreateTableAsync<PlantDisease>();
            await db.CreateTableAsync<ArticleEntity>();
            await db.CreateTableAsync<AdvisoryMessageEntity>();
            await db.CreateTableAsync<Image>();
        }
    }
}
