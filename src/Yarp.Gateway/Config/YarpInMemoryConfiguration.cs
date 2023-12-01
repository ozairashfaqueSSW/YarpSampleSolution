using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace Yarp.Gateway.Config;

public class YarpInMemoryConfiguration : IProxyConfigProvider
{
    private volatile InMemoryConfig _config;

    public YarpInMemoryConfiguration(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
    {
        _config = new InMemoryConfig(routes, clusters);
    }
    public IProxyConfig GetConfig() => _config;

    private class InMemoryConfig : IProxyConfig
    {
        private readonly CancellationTokenSource _cts = new();

        public InMemoryConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
        {
            Routes = routes;
            Clusters = clusters;
            ChangeToken = new CancellationChangeToken(_cts.Token);
        }

        public IReadOnlyList<RouteConfig> Routes { get; }

        public IReadOnlyList<ClusterConfig> Clusters { get; }

        public IChangeToken ChangeToken { get; }

        internal void SignalChange() => _cts.Cancel();
    }
}