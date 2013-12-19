using System.Collections.Generic;
using MailChimp.Campaigns;

namespace MailChimpApiClient
{
    using System;
    using CommandLine;

    /// <summary>
    /// Mail Chimp API client program.
    /// </summary>
    class Program
    {
        private const int SubjectColumnWidth = 20;
        private const int IdColumnWidth = 20;
        private const int NameColumnWidth = 35;

        /// <summary>
        /// Entry point for the Mail Chimp API client.
        /// </summary>
        /// <param name="args">Command line options.</param>
        static int Main(string[] args)
        {
            var invokedVerb = "";
            object invokedVerbInstance = null;

            var options = new CommandLineOptions();
            if (!Parser.Default.ParseArguments(args, options,
                                               delegate(string verb, object subOptions)
                                               {
                                                   invokedVerb = verb;
                                                   invokedVerbInstance = subOptions;
                                               }))
            {
                if (invokedVerb != "help")
                    Console.Error.WriteLine("Error: Missing command.");

                return Parser.DefaultExitCodeFail;
            }

            switch (invokedVerb)
            {
                case "list":
                    return RunListCommand((ListOptions) invokedVerbInstance);

                case "campaign":
                    return RunCampaignCommand((CampaignOptions) invokedVerbInstance);

                default:
                    return Parser.DefaultExitCodeFail;
            }
        }

        private static int RunListCommand(ListOptions listOptions)
        {
            var manager = new MailChimp.MailChimpManager(listOptions.Key);
            var listResult = manager.GetLists();
            Console.Out.WriteLine("{0}{1}",
                                  "Id".PadRight(IdColumnWidth),
                                  "Name".PadRight(NameColumnWidth));
            foreach (var listInfo in listResult.Data)
            {
                Console.Out.WriteLine("{0}{1}",
                                      listInfo.Id.PadRight(IdColumnWidth),
                                      listInfo.Name.PadRight(NameColumnWidth));
            }

            return 0;
        }

        private static int RunCampaignCommand(CampaignOptions campaignOptions)
        {
            var manager = new MailChimp.MailChimpManager(campaignOptions.Key);
            var campaignListResult = manager.GetCampaigns(CreateCampaignFilters(campaignOptions));

            Console.Out.WriteLine("{0}{1}",
                                  "Id".PadRight(IdColumnWidth),
                                  "Subject".PadRight(SubjectColumnWidth));
            foreach (var campaign in campaignListResult.Data)
            {
                Console.Out.WriteLine("{0}{1}",
                                      campaign.Id.PadRight(IdColumnWidth),
                                      campaign.Subject.PadRight(SubjectColumnWidth));
            }

            return 0;
        }

        private static List<CampaignFilter> CreateCampaignFilters(CampaignOptions campaignOptions)
        {
            return new List<CampaignFilter>{new CampaignFilter{ListId = campaignOptions.ListId}};
        }
    }
}
