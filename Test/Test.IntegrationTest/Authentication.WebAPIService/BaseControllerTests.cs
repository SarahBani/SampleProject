using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Test.IntegrationTest.Authentication.WebAPIService
{  
    public abstract class BaseControllerTests
    {

        #region Properties

        protected AuthWebAPIApplicationFactory Factory;

        protected HttpClient Client;

        #endregion /Properties

        #region Constructors

        public BaseControllerTests()
            : base()
        {
            this.Factory = new AuthWebAPIApplicationFactory();
            this.Client = this.Factory.CreateClient();
        }

        #endregion /Constructors

        #region Methods

        [OneTimeSetUp]
        public abstract void Setup();

        [OneTimeTearDown]
        public void TearDown()
        {
            this.Client.Dispose();
            this.Factory.Dispose();
        }

        protected async Task<T> GetDeserializedContent<T>(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            if (typeof(T) == typeof(string))
            {
                return (T)(object)content;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        protected StringContent GetSerializedContent(object content)
        {
            string serializedContent = string.Empty;
            if (content != null)
            {
                serializedContent = JsonConvert.SerializeObject(content);
            }
            else
            {
                serializedContent = "{ null }";
            }
            return new StringContent(serializedContent, Encoding.UTF8, "application/json");
        }

        protected string GetModelStateContent(ModelStateDictionary modelState)
        {
            var dict = modelState.ToDictionary(q =>
                q.Key,
                q => q.Value.Errors.Select(e => e.ErrorMessage).ToArray());
            return JsonConvert.SerializeObject(dict);
        }

        #endregion /Methods

    }
}
