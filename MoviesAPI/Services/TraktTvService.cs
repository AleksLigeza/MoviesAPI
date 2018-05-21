using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoviesAPI.Services
{
    public class TraktTvService : ITraktTVService
    {
        public async Task<List<Movie>> GetPopularMovies()
        {
            var url = "https://api.trakt.tv/movies/popular";
            var response = await DownloadResponseForHttpGet(url);

            var traktList = (List<TraktTvMovie>)JsonConvert.DeserializeObject(response, typeof(List<TraktTvMovie>));
            var list = AutoMapper.Mapper.Map<List<Movie>>(traktList).ToList();

            return list;
        }


        #region Helpers

        private async Task<string> DownloadResponseForHttpGet(string url)
        {
            string result = "";

            using(HttpClient client = new HttpClient())
            using(HttpResponseMessage response = await client.GetAsync(url))
            using(HttpContent content = response.Content)
            {
                result = await content.ReadAsStringAsync();
            }

            return result;
        }

        #endregion
    }
}
