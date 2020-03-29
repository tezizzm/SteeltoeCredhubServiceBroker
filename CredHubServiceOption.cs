using Steeltoe.Extensions.Configuration.CloudFoundry;

public class CredHubServiceOption : AbstractServiceOptions
{
    public CredHubServiceOption() {}
    public CredhubCredentials Credentials { get; set; }
}

public class CredhubCredentials
{
    public string Username { get; set; }
    public string Password { get; set; }
}