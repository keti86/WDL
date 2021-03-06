using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using wdl.sdk.common.models.v01;
using wdl.sdk.common.serialization;

namespace wdl.sdk.core.services.v01
{
    /// <summary>
    /// https://api.welldatalabs.com/docs/v1/fluidsandchemicals
    /// </summary>
    public class FluidsAndChemicalsService
    {
        private const string _endpoint = "fluidsandchemicals";
        private const string _version = "1";
        private string _apiKey;
        private Uri _baseUri;


        /// <summary>
        /// Initialize FluidsAndChemicalsService with api authorization token.
        /// </summary>
        /// <param name="apiKey">API Key provided by Well Data Labls.</param>
        /// <param name="baseUri">Optional baseUri</param>
        /// <remarks>See https://api.welldatalabs.com/docs/home/authentication </remarks>
        public FluidsAndChemicalsService(string apiKey, string baseUri = null)
        {
            _apiKey = apiKey;
            _baseUri = string.IsNullOrWhiteSpace(baseUri) ? HttpHelper.BaseUri : new Uri(baseUri);
        }


        /// <summary>
        /// Get The fluids and chemicals for the first 10 jobs.
        /// </summary>
        /// <returns>Collection of FluidsAndChemicals objects.</returns>
        public IEnumerable<FluidsAndChemicals> GetAll()
        {
            try
            {
                return GetAllAsync().Result;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
                throw ae.InnerExceptions.First();
            }
        }


        /// <summary>
        /// Get The fluids and chemicals for the first 10 jobs.
        /// </summary>
        /// <returns>Collection of FluidsAndChemicals objects.</returns>
        public async Task<IEnumerable<FluidsAndChemicals>> GetAllAsync()
        {
            string requestUri = HttpHelper.GetRequestUri(_baseUri, _endpoint);
            var fluidsAndChemicalsJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return fluidsAndChemicalsJson.DeserializeJson<IEnumerable<FluidsAndChemicals>>();
        }


        /// <summary>
        /// Get fluids and chemicals data for a specific job id, well id or well API number.  A job id will only return a
        /// single result in the collection.  A well API Number may return multiple results.
        /// </summary>
        /// <param name="id">Job Id, Well Id or well API Number</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of FluidsAndChemicals objects.</returns>
        public IEnumerable<FluidsAndChemicals> Get(string id, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            try
            {
                return GetAsync(id, fromStageNumber, toStageNumber).Result;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
                throw ae.InnerExceptions.First();
            }
        }


        /// <summary>
        /// Get fluids and chemicals data for a specific job id, well id or well API number.  A job id will only return a
        /// single result in the collection.  A well API Number may return multiple results.
        /// </summary>
        /// <param name="id">Job Id, Well Id or well API Number</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of FluidsAndChemicals objects.</returns>
        public async Task<IEnumerable<FluidsAndChemicals>> GetAsync(string id, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"fromStageNumber", fromStageNumber.ToString()},
                {"toStageNumber", toStageNumber.ToString()}
            };
            string requestUri = HttpHelper.GetRequestUri(_baseUri, string.Format("{0}/{1}", _endpoint, id), parameters);
            var fluidsAndChemicalsJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return fluidsAndChemicalsJson.DeserializeJson<IEnumerable<FluidsAndChemicals>>();
        }


        /// <summary>
		/// Synchronous version of the method that gets fluids and chemicals data for jobs that have changed 
        /// within a given UTC range.  If toChangeUtc is null, will return date for all jobs 
        /// modified since fromChangeUtc.  If fromChangeUtc is null, will return data for all
        /// jobs modified before toChangeUtc. 
        /// </summary>
        /// <param name="fromChangeUtc">Optional from datetime in UTC format</param>
        /// <param name="toChangeUtc">Optional to datetime in UTC format</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of FluidsAndChemicals objects.</returns>
        public IEnumerable<FluidsAndChemicals> GetByChangeUtc(DateTime? fromChangeUtc, DateTime? toChangeUtc, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            try
            {
                return GetByChangeUtcAsync(fromChangeUtc, toChangeUtc, fromStageNumber, toStageNumber).Result;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
                throw ae.InnerExceptions.First();
            }
        }


        /// <summary>
        /// Asynchronous version of the method that gets fluids and chemicals data for jobs that have changed 
        /// within a given UTC range.  If toChangeUtc is null, will return date for all jobs 
        /// modified since fromChangeUtc.  If fromChangeUtc is null, will return data for all
        /// jobs modified before toChangeUtc. 
        /// </summary>
        /// <param name="fromChangeUtc">Optional from datetime in UTC format</param>
        /// <param name="toChangeUtc">Optional to datetime in UTC format</param>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of FluidsAndChemicals objects.</returns>
        public async Task<IEnumerable<FluidsAndChemicals>> GetByChangeUtcAsync(DateTime? fromChangeUtc, DateTime? toChangeUtc, decimal? fromStageNumber = null, decimal? toStageNumber = null)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"fromChangeUtc", fromChangeUtc.ToString()},
                {"toChangeUtc", toChangeUtc.ToString()},
                {"fromStageNumber", fromStageNumber.ToString()},
                {"toStageNumber", toStageNumber.ToString()}
            };
            string requestUri = HttpHelper.GetRequestUri(_baseUri, _endpoint, parameters);
            var fluidsAndChemicalsJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return fluidsAndChemicalsJson.DeserializeJson<IEnumerable<FluidsAndChemicals>>();
        }


        /// <summary>
        /// Synchronous version of the method that gets fluids and chemicals data for jobs within Stage Number 
        /// range.  If toStageNumber is null, will return date for all jobs greater than or equal to 
        /// fromStageNumber.  If fromStageNumber is null, will return data for all jobs less than or 
        /// equal to toStageNumber. 
        /// </summary>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of FluidsAndChemicals objects.</returns>
        public IEnumerable<FluidsAndChemicals> GetByStageNumber(decimal? fromStageNumber, decimal? toStageNumber)
        {
            try
            {
                return GetByStageNumberAsync(fromStageNumber, toStageNumber).Result;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
                throw ae.InnerExceptions.First();
            }
        }


        /// <summary>
        /// Asynchronous version of the method that gets fluids and chemicals data for jobs within Stage Number 
        /// range.  If toStageNumber is null, will return date for all jobs greater than or equal to 
        /// fromStageNumber.  If fromStageNumber is null, will return data for all jobs less than or 
        /// equal to toStageNumber.  
        /// </summary>
        /// <param name="fromStageNumber">Optional from stage number in decimal format</param>
        /// <param name="toStageNumber">Optional to stage number in decimal format</param>
        /// <returns>Collection of FluidsAndChemicals objects.</returns>
        public async Task<IEnumerable<FluidsAndChemicals>> GetByStageNumberAsync(decimal? fromStageNumber, decimal? toStageNumber)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"fromStageNumber", fromStageNumber.ToString()},
                {"toStageNumber", toStageNumber.ToString()}
            };
            string requestUri = HttpHelper.GetRequestUri(_baseUri, _endpoint, parameters);
            var fluidsAndChemicalsJson = await HttpHelper.GetAsync(requestUri, _apiKey, _version);
            return fluidsAndChemicalsJson.DeserializeJson<IEnumerable<FluidsAndChemicals>>();
        }

    }
}
