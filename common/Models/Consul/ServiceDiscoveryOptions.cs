public class ServiceDiscoveryOptions
{
    public string ServiceAddress { get; set; }
    
    public string ServiceName { get; set; }
    
    public int ServicePort { get; set; }

    public ConsulOptions Consul { get; set; }

    public string HealthCheckEndPoint { get; set; }
  
    public int RequestRetries { get; set; }

    public string ServiceAbsoluteUri => $"http://{ServiceAddress}:{ServicePort}";
}
