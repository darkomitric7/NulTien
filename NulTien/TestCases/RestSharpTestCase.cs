using Newtonsoft.Json;
using NulTien.Extensions;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Net;

namespace NulTien.TestCases
{
    class RestSharpTestCase
    {

        private static string baseUrl;
        private readonly string city = "Belgrade";
        private readonly string stateCode = "RS";
        private readonly string apiKey = "972f472747fa6e6a460b75cd1ef37d5b";
        private readonly string mode = "xml";
        private readonly string units = "metric";
        private readonly string lang = "sr";

        [SetUp]
        public void SetUp()
        {
            baseUrl = "http://api.openweathermap.org/data/2.5/";
        }

        [Test]
        public void TestValidRequestRequiredParameters()
        {
            string expectedCityName = city;
            string expectedCountry = stateCode;
            int expectedCityId = 792680;
            double expectedLon = 20.47;
            double expectedLat = 44.8;

            // Creating client connection
            var client = new RestClient(baseUrl);

            // Creating a valid request
            var request = new RestRequest("weather", DataFormat.Json);
            request.AddParameter("q", city);
            request.AddParameter("appid", apiKey);

            // Executing GET method
            IRestResponse response = client.Get(request);

            // Deserializing JSON response using Newtonsoft JSON.NET deserialize library
            Root weatherForecast = JsonConvert.DeserializeObject<Root>(response.Content);

            // Validate JSON response
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ContentType, Is.EqualTo("application/json; charset=utf-8"));
            Assert.That(weatherForecast.Name, Is.EqualTo(expectedCityName));
            Assert.That(weatherForecast.Id, Is.EqualTo(expectedCityId));
            Assert.That(weatherForecast.Sys.Country, Is.EqualTo(expectedCountry));
            Assert.That(weatherForecast.Coord.Lon, Is.EqualTo(expectedLon));
            Assert.That(weatherForecast.Coord.Lat, Is.EqualTo(expectedLat));

        }

        [Test]
        public void TestValidRequestRequiredParameters1()
        {
            string expectedName = city;
            string expectedCountry = stateCode;

            var client = new RestClient(baseUrl);

            var request = new RestRequest("weather", DataFormat.Json);
            request.AddParameter("q", city + "," + stateCode);
            request.AddParameter("appid", apiKey);

            IRestResponse response = client.Get(request);

            Root weatherForecast = JsonConvert.DeserializeObject<Root>(response.Content);

            string name = weatherForecast.Name;
            string country = weatherForecast.Sys.Country;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(name, Is.EqualTo(expectedName));
            Assert.That(country, Is.EqualTo(expectedCountry));
        }

        [Test]
        public void TestValidRequestAllParameters()
        {
            string expectedName = city;
            string expectedCountry = stateCode;

            var client = new RestClient(baseUrl);

            var request = new RestRequest("weather", DataFormat.Json);
            request.AddParameter("q", city);
            request.AddParameter("appid", apiKey);
            request.AddParameter("mode", mode);
            request.AddParameter("units", units);
            request.AddParameter("lang", lang);

            IRestResponse response = client.Get(request);
            string xmlResponse = response.Content;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ContentType, Is.EqualTo("application/xml; charset=utf-8"));
            Assert.That(xmlResponse.ToString, Does.Contain("Beograd"));
            Assert.That(xmlResponse.ToString, Does.Contain("RS"));
        }

        [Test]
        public void TestMissingRequiredParameterAppId()
        {
            string expectedCityName = city;
            string expectedCountry = stateCode;
            string expectedErrorMessage = "Invalid API key. Please see http://openweathermap.org/faq#error401 for more info.";
            string expectedHTTPCode = "401";

            var client = new RestClient(baseUrl);

            var request = new RestRequest("weather", DataFormat.Json);
            request.AddParameter("q", city);
            //request.AddParameter("appid", apiKey);

            IRestResponse response = client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.That(response.ContentType, Is.EqualTo("application/json; charset=utf-8"));
            Assert.That(response.Content, Does.Contain(expectedErrorMessage));
            Assert.That(response.Content, Does.Contain(expectedHTTPCode));

        }

        [Test]
        public void TestMissingRequiredParameterCity()
        {
            string expectedCityName = city;
            string expectedCountry = stateCode;
            string expectedErrorMessage = "Nothing to geocode";
            string expectedHTTPCode = "400";

            var client = new RestClient(baseUrl);

            var request = new RestRequest("weather", DataFormat.Json);
            //request.AddParameter("q", city);
            request.AddParameter("appid", apiKey);

            IRestResponse response = client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.ContentType, Is.EqualTo("application/json; charset=utf-8"));
            Assert.That(response.Content, Does.Contain(expectedErrorMessage));
            Assert.That(response.Content, Does.Contain(expectedHTTPCode));

        }

        [Test]
        public void TestMethodNotAllowed()
        {

            var client = new RestClient(baseUrl);

            var request = new RestRequest("weather", DataFormat.Json);
            request.AddParameter("q", city);
            request.AddParameter("appid", apiKey);

            IRestResponse response = client.Delete(request);

            Root weatherForecastRoot = JsonConvert.DeserializeObject<Root>(response.Content);
            Sys weatherForecastSys = JsonConvert.DeserializeObject<Sys>(response.Content);


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));

        }
    }
}
