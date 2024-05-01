// See https://aka.ms/new-console-template for more information

using DownloadToBrain.Infrastructure;

Console.WriteLine("verifying Brain\\Brain\\Vocabulary.csv ...");

var userRepository = new UserRepository(validate:true);

try
{
    var words = await userRepository.LoadAllWords();
    Console.WriteLine("succeeded");
}
catch (Exception e)
{
    Console.WriteLine("##############\nERROR\n##############\nin line: " + e.Message);
}
