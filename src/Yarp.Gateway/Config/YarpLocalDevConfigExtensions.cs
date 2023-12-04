using System.ComponentModel;
using Yarp.Gateway.Services;
using Yarp.ReverseProxy.Configuration;

namespace Yarp.Gateway.Config;

public static class YarpLocalDevConfigExtensions
{
    private const string webAppLegacyClusterId = "webAppLegacy";

    private const string webAppLegacyApiClusterId = "webAppLegacyApi";

    private const string angularClusterId = "angularServePath";

    private const string angularAssetsClusterId = "assets";

    private static IReverseProxyBuilder LoadFromMemory(this IReverseProxyBuilder builder,
        IReadOnlyList<RouteConfig> routes,
        IReadOnlyList<ClusterConfig> clusters)
    {
        builder.Services.AddSingleton<IProxyConfigProvider>(new YarpInMemoryConfiguration(routes, clusters));
        return builder;
    }

    public static void AddSpaYarp(this IServiceCollection services,
        IConfiguration configuration)
    {
        ReverseProxySetting? config = configuration.GetSection(ReverseProxySetting.Key).Get<ReverseProxySetting>();

        string webAppLegacyAddress = config?.WebAppLegacyServeAddress ?? throw new ArgumentNullException(nameof(ReverseProxySetting.WebAppLegacyServeAddress),
            $@"Missing config {ReverseProxySetting.Key} for WebAppLegacy");

        string angularServeAddress = config?.AngularServeAddress ?? throw new ArgumentNullException(nameof(ReverseProxySetting.AngularServeAddress),
            $@"Missing config {ReverseProxySetting.Key} for Angular serve");

        var webRoutes = new List<RouteConfig>
        {
                // Route for Legacy WebApp Information
                new()
                {
                    RouteId = "webAppLegacyServePath",
                    ClusterId = webAppLegacyClusterId,
                    Match = new RouteMatch
                    {
                        Path = "/legacywebapp/{**catch-all}",
                    },
                },

                // Route for Legacy WebApp
                new()
                {
                    RouteId = "webAppLegacyApiServePath",
                    ClusterId = webAppLegacyApiClusterId,
                    Match = new RouteMatch
                    {
                        Path = "/api/{**catch-all}",
                    },
                },
        };

        var webClusters = new List<ClusterConfig>
        {
            new()
            {
                ClusterId = webAppLegacyClusterId,
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    {"webAppLegacyServePath", new DestinationConfig{ Address = webAppLegacyAddress } }
                }
            },

            new()
            {
                ClusterId = webAppLegacyApiClusterId,
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    {"webAppLegacyApiServePath", new DestinationConfig{ Address = webAppLegacyAddress } }
                }
            },
        };

        switch (config.AngularServeModeEnum)
        {
            case ServeMode.Artifact:

                // No additional configuration needed for "Artifact" mode
                break;

            case ServeMode.Proxy:

                //Add routes
                webRoutes.InsertRange(0, GetWebRoutesConfigForProxy());

                // Add Clusters
                webClusters.AddRange(GetWebClusterConfigForProxy(angularServeAddress));

                break;

            default:
                throw new InvalidEnumArgumentException(nameof(ServeMode), (int)config.AngularServeModeEnum, typeof(ServeMode));
        }

        services
            .AddReverseProxy()
            .LoadFromMemory(
                webRoutes.ToList(),
                webClusters.ToList());
    }

    private static RouteConfig[] GetWebRoutesConfigForProxy()
    {
        return new RouteConfig[]
        {
                // Route for Angular
                new RouteConfig
                {
                    RouteId = "angularUIServePath",
                    ClusterId = angularClusterId,
                    Match = new RouteMatch
                    {
                        Path = "/{**catch-all}",
                    },
                }
        };

    }

    private static ClusterConfig[] GetWebClusterConfigForProxy(string angularServeAddress)
    {
        return new ClusterConfig[]
        {
                new ClusterConfig
                {
                    ClusterId= angularAssetsClusterId,
                    Destinations = new Dictionary<string, DestinationConfig>
                    {
                        { "angularAssetsPath", new DestinationConfig { Address = angularServeAddress } }
                    },

                },

                new ClusterConfig
                {
                    ClusterId= angularClusterId,
                    Destinations = new Dictionary<string, DestinationConfig>
                    {
                        { "angularUIServePath", new DestinationConfig { Address =  angularServeAddress } }
                    }
                },
        };
    }
}

