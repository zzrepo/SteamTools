using Android.Content;
using Android.Net;
using System.Application.Services;
using System.Application.Services.Implementation;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using XEPlatform = Xamarin.Essentials.Platform;

namespace System.Application.Services.Implementation
{
    internal sealed class DnsAnalysisServiceImpl : IDnsAnalysisService
    {
        /// <summary>
        /// https://developer.android.google.cn/reference/android/net/ConnectivityManager?hl=zh-cn
        /// </summary>
        static ConnectivityManager ConnectivityManager
        {
            get
            {
                var connectivityManager = XEPlatform.CurrentActivity.GetSystemService<ConnectivityManager>(Context.ConnectivityService);
                return connectivityManager;
            }
        }

        public static List<IPEndPoint> GetDnsServers()
        {
            List<IPEndPoint> endPoints = new();
            var connectivityManager = ConnectivityManager;

            var activeConnection = connectivityManager.ActiveNetwork;
            var linkProperties = connectivityManager.GetLinkProperties(activeConnection);

            if (linkProperties != null)
            {
                foreach (var currentAddress in linkProperties.DnsServers)
                {
                    var endPoint = new IPEndPoint(IPAddress.Parse(currentAddress.HostAddress), 53);
                    endPoints.Add(endPoint);
                }
            }

            return endPoints;
        }

        public Task<IPAddress[]?> AnalysisDomainIp(string url, bool isIPv6 = false)
        {
            throw new NotImplementedException();
        }

        public Task<IPAddress[]?> AnalysisDomainIpBy114Dns(string url, bool isIPv6 = false)
        {
            throw new NotImplementedException();
        }

        public Task<IPAddress[]?> AnalysisDomainIpByAliDns(string url, bool isIPv6 = false)
        {
            throw new NotImplementedException();
        }

        public Task<IPAddress[]?> AnalysisDomainIpByCloudflare(string url, bool isIPv6 = false)
        {
            throw new NotImplementedException();
        }

        public Task<IPAddress[]?> AnalysisDomainIpByCustomDns(string url, IPAddress[]? dnsServers = null, bool isIPv6 = false)
        {
            throw new NotImplementedException();
        }

        public Task<IPAddress[]?> AnalysisDomainIpByDnspod(string url, bool isIPv6 = false)
        {
            throw new NotImplementedException();
        }

        public Task<IPAddress[]?> AnalysisDomainIpByGoogleDns(string url, bool isIPv6 = false)
        {
            throw new NotImplementedException();
        }

        public int AnalysisHostnameTime(string url)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetHostByIPAddress(IPAddress ip)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetIsIpv6Support()
        {
            throw new NotImplementedException();
        }

        public Task<long> PingHostname(string url)
        {
            throw new NotImplementedException();
        }
    }
}

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDnsAnalysisService(this IServiceCollection services)
        {
            services.AddSingleton<IDnsAnalysisService, DnsAnalysisServiceImpl>();
            return services;
        }
    }
}