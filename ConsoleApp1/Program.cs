using System;
using System.Runtime.Serialization;
using DeltaWare.SDK.Web;
using DeltaWare.SDK.Web.Interfaces;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IApiHandler<CountryDto, SiteApiVersion> apiHandler = new BaseApi<CountryDto, SiteApiVersion>();

            CountryDto country = new CountryDto
            {
                Name = "FUCK YOU"
            };

            IApiResponse<CountryDto> response = apiHandler.CreateAsync(SiteApiVersion.V1, country).Result;



            var countries = apiHandler.GetAsync(SiteApiVersion.V1).Result;
        }

        [DataContract]
        public class CountryDto
        {
            [DataMember] public string Name { get; set; }

            [DataMember] public Guid Identity { get; set; }
        }

        public class SiteApiVersion : IApiVersion
        {
            public string VersionString { get; }

            private SiteApiVersion(string version)
            {
                VersionString = version;
            }

            public static SiteApiVersion V1 => new SiteApiVersion("1.0");
        }

        public class BaseApi<TEntity, TVersion> : BaseApiHandler<TEntity, TVersion> where TEntity : class where TVersion : IApiVersion
        {
            public override string BaseEndPoint { get; } = "Countries";
            public override Uri BaseUri { get; } = new Uri("http://localhost:55001/api/");
        }
    }
}
