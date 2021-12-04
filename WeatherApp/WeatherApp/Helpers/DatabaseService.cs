using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Helpers
{
    public class DatabaseService
    {
        FirebaseClient client;

        public DatabaseService()
        {
            client = new FirebaseClient("https://weatherapp-9a17d-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<List<Key>> GetKeysAsync()
        {
            var keys = (await client.Child("Keys").OnceAsync<Key>()).Select(k => new Key
            {
                APIkey = k.Object.APIkey
            }).ToList();

            return keys;
        }
    }
}
