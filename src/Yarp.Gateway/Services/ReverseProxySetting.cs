namespace Yarp.Gateway.Services;

public enum ServeMode
{
    Proxy,
    Artifact,
}

internal static class ServeModeString
{
    public const string Proxy = "proxy";

    public const string Artifact = "artifact";
}

public class ReverseProxySetting
{
    public const string Key = "ReverseProxy";

    public string WebAppLegacyServeAddress { get; set; } = string.Empty;

    public string AngularServeAddress { get; set; } = string.Empty;

    public string AngularServeMode { get; set; } = string.Empty;

    public ServeMode AngularServeModeEnum => AngularServeMode.ToLowerInvariant() switch
    {
        ServeModeString.Artifact => ServeMode.Artifact,
        ServeModeString.Proxy => ServeMode.Proxy,
        _ => ServeMode.Proxy,
    };
}
