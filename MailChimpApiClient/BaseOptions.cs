namespace MailChimpApiClient
{
    using CommandLine;

    /// <summary>
    /// Base options for all options.
    /// </summary>
    class BaseOptions
    {
        public BaseOptions()
        {
            this.Key = string.Empty;
        }

        [Option('k', "key", Required = true, HelpText = "Mail Chimp API key.")]
        public string Key { get; set; }
    }
}