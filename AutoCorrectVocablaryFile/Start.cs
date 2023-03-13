using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoCorrectVocablaryFile
{
    internal class Start
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var dllFilePath = Assembly.GetExecutingAssembly().CodeBase;
            var projFolderPath = FindProjectFolder(dllFilePath);
            var vocabularyCsvFilePath = $"{projFolderPath}\\..\\Brain\\Brain\\Vocabulary.csv";

            string content = File.ReadAllText(vocabularyCsvFilePath);

            ParseContext parseContext = new ParseContext();
            parseContext.CurrentParseState = new ParseStateColumnStarted();

            foreach(char c in content)
            {
                parseContext.CurrentParseState.ProcessCharacter(parseContext,c);
            }

            File.WriteAllText(vocabularyCsvFilePath, parseContext.NewContent.ToString(),Encoding.UTF8);
        }

        private static string? FindProjectFolder(string? path)
        {
            if (path == null)
            {
                return null;
            }

            if (Path.GetFileName(path) == "AutoCorrectVocablaryFile")
            {
                var result = path.Replace("file:\\","");
                return result;
            }

            return FindProjectFolder(Path.GetDirectoryName(path));
        }
    }
}
