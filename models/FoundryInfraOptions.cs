namespace MyTerraformStack
{
    public class FoundryInfraOptions
    {
        public const string Name = "Config";
        public string DigitalOceanToken { get; set; } = "";
        public string DigitalOceanSpacesAccessId { get; set; } = "";
        public string DigitalOceanSpacesSecretKey { get; set; } = "";
        public string VpcName { get; set; } = "";
        public string VpcRegion { get; set; } = "";
    }
}
