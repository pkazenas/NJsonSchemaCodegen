using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
namespace JsonSchemaCodegen
{
    internal class Options
    {
        [Option('f', "files", HelpText = "files with containing json schema")]
        public IEnumerable<string> FilePaths { get; set; }

        [Option('d', "dirs", HelpText = "directory with json schema files only")]
        public string Directory { get; set; }

        [Option('o', "output", HelpText = "output directory")]
        public string OutputDir { get; set; } = ".";

        [Option('n', "namespace", Required = true, HelpText = "output directory")]
        public string Namespace { get; set; }

        [Option("overwrite", HelpText = "If set then default root class name will be changed to json schema file name")]
        public bool Overwrite { get; set; } = true;
    }
}
