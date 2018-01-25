using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSchemaCodegen
{
    internal static class CodeGenerator
    {
        public static void GenerateJsonSchema(List<string> filePaths, string outputPath, string nmespace, bool overwriteRootObjectName)
        {
            filePaths.ForEach(path =>
            {
                Console.WriteLine($"Generating c# classes from path: ${path}");
                try
                {
                    var task = JsonSchema4.FromFileAsync(path);
                    task.Wait();

                    var fileName = Path.GetFileNameWithoutExtension(path);

                    var schema = task.Result;
                    var settings =
                    new CSharpGeneratorSettings
                    {
                        GenerateDataAnnotations = false,
                        ClassStyle = CSharpClassStyle.Poco,
                        Namespace = nmespace
                    };

                    var generator = new CSharpGenerator(schema, settings);
                    var file = generator.GenerateFile();
                    if(overwriteRootObjectName)
                    {
                        file = file.Replace("Jschema", fileName);
                    }

                    var outputFilePath = Path.Combine(outputPath, $"{fileName}.cs");

                    File.WriteAllText(outputFilePath, file);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"There were some error when parsing file from path: ${path}");
                    Console.WriteLine(e.StackTrace);
                }

            });
        }
    }
}
