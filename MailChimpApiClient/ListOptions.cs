namespace MailChimpApiClient
{
    using CommandLine;

    /// <summary>
    /// List option specification.
    /// </summary>
    class ListOptions
    {
        public ListOptions()
        {
            this.Key = string.Empty;
        }

        [Option('k', "key", Required = true, HelpText = "Mail Chimp API key.")]
        public string Key { get; set; }
    }
}