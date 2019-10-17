using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel.Entities;
using Core.DomainService.Models;
using Core.DomainService.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace UserInterface.WebApplication.Controllers
{
    public class BankController : Controller
    {

        #region Properties

        private readonly MVCAppSettings _appSettings;

        private string _authToken;

        #endregion /Properties

        #region Constructors

        public BankController(IOptions<MVCAppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
            this._authToken = GetJWTAuthenticationToken().Result;
        }

        #endregion /Constructors

        #region Public Methods

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            IList<Bank> banks = null;
            var uri = new Uri(this._appSettings.BankAPIUrl);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._authToken);
                var responseMessage = await client.GetAsync(uri);
                banks = await responseMessage.Content.ReadAsAsync<IList<Bank>>();
            }
            return View(banks);
        }

        public async Task<IActionResult> _GetInfo(int id)
        {
            Bank bank = null;
            var uri = new Uri(this._appSettings.BankAPIUrl + "/" + id);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._authToken);
                var responseMessage = await client.GetAsync(uri);
                bank = await responseMessage.Content.ReadAsAsync<Bank>();
            }
            return View(bank);
        }

        public async Task<IActionResult> _Delete(int id)
        {
            var uri = new Uri(this._appSettings.BankAPIUrl + "/" + id);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._authToken);
                var responseMessage = await client.DeleteAsync(uri);
            }
            return View();
        }

        #endregion /Public Methods

        #region Private Methods

        private async Task<string> GetJWTAuthenticationToken()
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(this._appSettings.APIAuthenticationUrl);
                var responseMessage = await client.PostAsync(uri, GetAuthenticationContent());
                return await responseMessage.Content.ReadAsStringAsync();
            }
        }

        private StringContent GetAuthenticationContent()
        {
            var tokenRequest = new UserCredential()
            {
                Username = "Admin",
                Password = "123"
            };
            string serializedContent = JsonConvert.SerializeObject(tokenRequest);
            return new StringContent(serializedContent, Encoding.UTF8, "application/json");
        }

        #endregion /Private Methods

    }
}
