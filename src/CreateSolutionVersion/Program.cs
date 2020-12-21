using System;
using System.IO;
using System.Linq;


namespace CreateSolutionVersion
{
    class Program
    {
        public class Context
        {
            public string Version { get; set; }
            public DateTime BuildDate { get; set; }
        }

        static void Main(string[] args)
        {
            var context = new Context();


            string templateFilePath = System.Environment.GetEnvironmentVariable("INPUT_TEMPLATE_FILE");
            string outputFilePath = System.Environment.GetEnvironmentVariable("INPUT_OUTPUT_FILE");
            if (string.IsNullOrEmpty(outputFilePath)) outputFilePath = @"SolutionVersion.cs";
            string version = System.Environment.GetEnvironmentVariable("INPUT_VERSION");
            if (string.IsNullOrEmpty(version)) version = "0.0.0.0";

            context.Version = version;

            if (!string.IsNullOrEmpty(context.Version))
            {
                context.BuildDate = DateTime.Now;
                var template = GetDefaultTemplate();
                if (!string.IsNullOrEmpty(templateFilePath))
                {
                    template = System.IO.File.ReadAllText(templateFilePath);
                }

                var results = Stubble.Core.StaticStubbleRenderer.Render(template, context);
                Console.WriteLine(results);
                System.IO.File.WriteAllText(outputFilePath, results);
            }
            else
            {
                Environment.ExitCode = -1;
            }
        }

        public static string GetDefaultTemplate()
        {
            var stream = System.Reflection.Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("CreateSolutionVersion.SolutionVersion.template.txt");
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
