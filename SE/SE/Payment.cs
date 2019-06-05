using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace SE
{
    class Payment
    {

        public async Task payAsync()
        {
            string token = "";
            string token_type = "";
            var ip = "127.0.0.1";
            var baseAddress1 = new Uri("https://secure.payu.com/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress1 })
            {
                using (var content = new StringContent("grant_type=client_credentials&client_id=145227&client_secret=12f071174cb7eb79d4aac5bc2f07563f", System.Text.Encoding.Default, "application/x-www-form-urlencoded"))
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
            string order = "{  \"notifyUrl\": \"https://your.eshop.com/notify\",  \"customerIp\": \"127.0.0.1\",  \"merchantPosId\": \"145227\",  \"description\": \"Sprzedam duszę\",  \"currencyCode\": \"PLN\",  \"totalAmount\": \"999900\",  \"products\": [    {      \"name\": \"Wireless mouse\",      \"unitPrice\": \"15000\",      \"quantity\": \"1\"    },    {      \"name\": \"HDMI cable\",      \"unitPrice\": \"6000\",      \"quantity\": \"1\"    }  ]}";
           

                string JsonOrder = JsonConvert.SerializeObject(order, Formatting.Indented);

            var baseAddress = new Uri("https://secure.payu.com/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Bearer " + token);

                using (var content = new StringContent(order, System.Text.Encoding.Default, "application/json"))
                {
                    using (var response = await httpClient.PostAsync("api/v2_1/orders", content))
                    {

                        try
                        {
                            Process.Start(response.RequestMessage.RequestUri.AbsoluteUri);
                        }
                        catch
                        {

                        }

                     
                        }

                    }
                }
            }

        }
 }

