namespace MailChimpApiClient
{
    using CommandLine;

    /// <summary>
    /// Campaign command specification.
    /// </summary>
    class CampaignOptions : BaseOptions
    {
        public CampaignOptions()
        {
            this.ListId = string.Empty;
        }

        [Option('i', "id", Required = true, HelpText = "Mail Chimp List Id.")]
        public string ListId { get; set; }
    }
}