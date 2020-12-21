﻿using System;
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


            string templateFilePath = System.Environment.GetEnvironmentVariable("INPUT_TEMPLATE_FILE") ?? @"SolutionVersion.template.txt";
            string outputFilePath = System.Environment.GetEnvironmentVariable("INPUT_OUTPUT_FILE") ?? @"SolutionVersion.cs";
            string version = System.Environment.GetEnvironmentVariable("INPUT_VERSION") ?? "0.0.0.0";

            context.Version= version;
           
            if (!string.IsNullOrEmpty(context.Version))
            {
                context.BuildDate = DateTime.Now;
                var file = System.IO.File.ReadAllText(templateFilePath);
                var results = Stubble.Core.StaticStubbleRenderer.Render(file, context);
                Console.WriteLine(results);
            }
            else
            {
                Environment.ExitCode = -1;
            }
        }
    }
}
