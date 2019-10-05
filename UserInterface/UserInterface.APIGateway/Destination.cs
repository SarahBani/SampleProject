using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway
{
    public class Destination
    {

        #region Properties

        public string Uri { get; set; }

        public bool RequiresAuthentication { get; set; }

        private static HttpClient client = new HttpClient();

        #endregion /Properties

        #region Constructors

        private Destination()
        {
            Uri = "/";
            RequiresAuthentication = false;
        }

        public Destination(string uri)
            : this(uri, false)
        {
        }

        public Destination(string uri, bool requiresAuthentication)
        {
            Uri = uri;
            RequiresAuthentication = requiresAuthentication;
        }

        #endregion /Constructors

        #region Methods

        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            string requestContent;
            using (var receiveStream = request.Body)
            {
                using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    requestContent = readStream.ReadToEnd();
                }
            }

            using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request)))
            {
                newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
                using (var response = await client.SendAsync(newRequest))
                {
                    return response;
                }
            }
        }

        private string CreateDestinationUri(HttpRequest request)
        {
            string requestPath = request.Path.ToString();
            string queryString = request.QueryString.ToString();

            string endpoint = string.Empty;
            string[] endpointSplit = requestPath.Substring(1).Split('/');

            if (endpointSplit.Length > 1)
            {
                endpoint = endpointSplit[1];
            }

            return Uri + endpoint + queryString;
        }

        #endregion /Methods

    }
}
