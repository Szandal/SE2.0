using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace SE
{
    class Payment
    {

        public async Task<string> payAsync()
        {
            string token = "";
            string token_type = "";
            var ip = "127.0.0.1";
            var baseAddress1 = new Uri("https://secure.snd.payu.com/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress1 })
            {
                using (var content = new StringContent("grant_type=client_credentials&client_id=300746&client_secret=2ee86a66e5d97e3fadc400c9f19b065d" , System.Text.Encoding.Default, "application/x-www-form-urlencoded"))
                {
                    using (var response = await httpClient.PostAsync("/pl/standard/user/oauth/authorize", content))
                    {

                        string responseData = await response.Content.ReadAsStringAsync();
                        dynamic obj = JsonConvert.DeserializeObject<dynamic>(responseData);
                        token = obj["access_token"];
                        token_type = obj["token_type"];
                    }
                }
            }
            string order = "{\r\n\r\n    \"notifyUrl\":\"https:\\/\\/your.eshop.com\\/notify\",\r\n    \"customerIp\":\"127.0.0.1\",\r\n    \"merchantPosId\":\"300746\",\r\n    \"description\":\"RTV market\",\r\n    \"currencyCode\":\"PLN\",\r\n    \"totalAmount\":\"21000\",\r\n    \"buyer\":{},\r\n    \"settings\":{\r\n        \"invoiceDisabled\":\"true\"\r\n    },\r\n    \"products\":[\r\n        {\r\n            \"name\":\"Wireless Mouse for Laptop\",\r\n            \"unitPrice\":\"15000\",\r\n            \"quantity\":\"1\"\r\n        },\r\n        {\r\n            \"name\":\"HDMI cable\",\r\n            \"unitPrice\":\"6000\",\r\n            \"quantity\":\"1\"\r\n        }\r\n    ]\r\n\r\n}";

            string JsonOrder = JsonConvert.SerializeObject(order, Formatting.Indented);

            var baseAddress = new Uri("https://secure.snd.payu.com/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Bearer " + token);

                using (var content = new StringContent(JsonOrder, System.Text.Encoding.Default, "application/json"))
                {
                    using (var response = await httpClient.PostAsync("api/v2_1/orders", content))
                    {

                        try
                        {
                            return response.RequestMessage.RequestUri.AbsoluteUri;
                        }
                        catch
                        {

                        }

                        return "";
                        }

                    }
                }
            }

        }
 }

