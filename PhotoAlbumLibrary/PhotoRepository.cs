using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;

namespace PhotoAlbumLibrary
{
    public class PhotoRepository : IPhotoRepository
    {
        private const string BASE_URL = "https://jsonplaceholder.typicode.com/photos";
        private const string PARM_FORMAT = "?albumId={0}";

        public IEnumerable<Photo> GetAlbum(int albumId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{BASE_URL}{string.Format(PARM_FORMAT, albumId)}";

            var response = client.GetAsync(url).Result;
            if(response.IsSuccessStatusCode)
            {
                var objects = response.Content.ReadAsAsync<IEnumerable<Photo>>().Result;
                return objects;
            }
            else
            {
                throw new Exception($"---HTTP Response Error---\nStatus: {response.StatusCode}\nReason: {response.ReasonPhrase}");
            }
        }
    }
}
