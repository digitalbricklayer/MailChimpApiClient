namespace MailChimpApiClient
{
    using System;
    using CommandLine;

    /// <summary>
    /// Mail Chimp API client program.
    /// </summary>
    class Program
    {
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
    }
}
