namespace MailChimpApiClient
{
    using CommandLine;
    using CommandLine.Text;

    /// <summary>
    /// Command line options specification.
    /// </summary>
    class CommandLineOptions
    {
        public CommandLineOptions()
        {
            this.List = new ListOptions();
        }

        [VerbOption("list",
                    HelpText = "List all lists.")]
        public ListOptions List { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }
}
