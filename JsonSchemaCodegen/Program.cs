using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
namespace JsonSchemaCodegen
{
    class Program
    {
        private static void HandleParsed(Options options)
        {
            var filePaths = options.FilePaths.ToList<string>();
            if (filePaths.Count > 0)
            {
                CodeGenerator.GenerateJsonSchema(
                    filePaths,
                    options.OutputDir,
                    options.Namespace, 
                    !options.KeepDefaultName);
            }
            else if (options.Directory != null)
            {
                try
                {
                    CodeGenerator.GenerateJsonSchema(
                        Directory.GetFiles(options.Directory).ToList<string>(),
                        options.OutputDir,
                        options.Namespace,
                        !options.KeepDefaultName);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unable to list files in directory {options.Directory}");
                }
            }
            else
            {
                Console.WriteLine("No file paths or search directory provided");
            }
        }

        private static void HandleErrors(IEnumerable<Error> errors)
        {
            Console.WriteLine("There were some error with processing application arguments");
            errors
                .ToList<Error>()
                .ForEach((error) => Console.WriteLine(error.ToString()));
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(options => HandleParsed(options))
                .WithNotParsed<Options>(errors => HandleErrors(errors));
        }
    }
}
