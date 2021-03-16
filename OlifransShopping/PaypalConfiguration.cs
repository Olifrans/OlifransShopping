using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace OlifransShopping
{
    public class PaypalConfiguration
    {
        public readonly static string clienteId;
        public readonly static string clienteSecret;


        static PaypalConfiguration()
        {
            var config = getconfig();
            clienteId = "Digite o código API fornecido pela operadora PayPal";
            clienteSecret = "Digite o código API fornecido pela operadora PayPal";
        }
               

        private static Dictionary<string, string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clienteId, clienteSecret, getconfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = getconfig();
            return apicontext;
        }
    }
}