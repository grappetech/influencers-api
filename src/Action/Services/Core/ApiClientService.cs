using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using WatsonServices.Services.ApiClient.Core.Models;

namespace WatsonServices.Services.ApiClient.Core
{
    public abstract class AApiClientService
    {
        public async Task<T> Get<T>(Uri uri, string resource, bool isSsl = true, ApiAuthorization authorization = null,
            Dictionary<string, string> queryStrings = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                if (authorization != null)
                    client.DefaultRequestHeaders.Authorization = authorization.GetAuthorizationValue();


                var path = queryStrings == null ? $"{resource}" : $"{resource}{queryStrings.ToUrlParams()}";

                try
                {
                    using (var response = await client.GetAsync(path))
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            if (json.StartsWith("<"))
                            {
                                XmlDocument doc = new XmlDocument();
                                doc.LoadXml(json);
                                json = JsonConvert.SerializeXmlNode(doc);
                            }

                            return JsonConvert.DeserializeObject<T>(json);
                        }

                        throw new Exception(json);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }
        }

        public async Task<T> Post<T>(Uri uri, string resource, object content, bool isSsl = true,
            ApiAuthorization authorization = null,
            Dictionary<string, string> queryStrings = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                if (authorization != null)
                    client.DefaultRequestHeaders.Authorization = authorization.GetAuthorizationValue();

                var path = queryStrings == null ? $"{resource}" : $"{resource}{queryStrings.ToUrlParams()}";

                try
                {
                    using (var response = await client.PostAsync(path,
                        new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")))
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            return JsonConvert.DeserializeObject<T>(json);
                        }

                        throw new Exception(json);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }
        }

        public async Task<T> Delete<T>(Uri uri, string resource, bool isSsl = true,
            ApiAuthorization authorization = null,
            Dictionary<string, string> queryStrings = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                if (authorization != null)
                    client.DefaultRequestHeaders.Authorization = authorization.GetAuthorizationValue();

                var path = queryStrings == null ? $"{resource}" : $"{resource}{queryStrings.ToUrlParams()}";

                try
                {
                    using (var response = await client.DeleteAsync(path))
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            return JsonConvert.DeserializeObject<T>(json);
                        }

                        throw new Exception(json);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }
        }

        public async Task<T> Put<T>(Uri uri, string resource, dynamic content, bool isSsl = true,
            ApiAuthorization authorization = null,
            Dictionary<string, string> queryStrings = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                if (authorization != null)
                    client.DefaultRequestHeaders.Authorization = authorization.GetAuthorizationValue();


                try
                {
                    using (var response = await client.PostAsync(resource, content))
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            return JsonConvert.DeserializeObject<T>(json);
                        }

                        throw new Exception(json);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }
        }
    }
}