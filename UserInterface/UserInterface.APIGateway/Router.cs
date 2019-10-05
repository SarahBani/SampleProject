using Core.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIGateway
{
    public class Router
    {

        #region Properties

        public List<Route> Routes { get; set; }

        public Destination AuthenticationService { get; set; }

        #endregion /Properties

        #region Constructors

        public Router(string routeConfigFilePath)
        {
            dynamic router = JsonLoader.LoadFromFile<dynamic>(routeConfigFilePath);
            this.Routes = JsonLoader.Deserialize<List<Route>>(Convert.ToString(router.routes));
            this.AuthenticationService = JsonLoader.Deserialize<Destination>(Convert.ToString(router.authenticationService));
        }


        #endregion /Constructors

        #region Methods

        public async Task<HttpResponseMessage> RouteRequest(HttpRequest request)
        {
            string path = request.Path.ToString();
            string basePath = '/' + path.Split('/')[1];

            Destination destination;
            try
            {
                destination = Routes.First(r => r.Endpoint.Equals(basePath)).Destination;
            }
            catch
            {
                return ConstructErrorMessage(Constant.Exception_PathNotFound);
            }

            if (destination.RequiresAuthentication)
            {
                string token = request.Headers["token"];
                request.Query.Append(new KeyValuePair<string, StringValues>("token", new StringValues(token)));
                HttpResponseMessage authResponse = await AuthenticationService.SendRequest(request);
                if (!authResponse.IsSuccessStatusCode)
                {
                    return ConstructErrorMessage(Constant.Exception_FailedAuthentication);
                }
            }

            return await destination.SendRequest(request);
        }

        private HttpResponseMessage ConstructErrorMessage(string error)
        {
            var errorMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(error)
            };
            return errorMessage;
        }

        #endregion /Methods

    }
}
