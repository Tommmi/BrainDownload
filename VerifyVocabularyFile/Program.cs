// See https://aka.ms/new-console-template for more information

using DownloadToBrain.Infrastructure;

Console.WriteLine("verifying Brain\\Brain\\Vocabulary.csv ...");

var userRepository = new UserRepository(validate:true);

try
{
    var words = await userRepository.LoadAllWords();
    if(words.GroupBy(w=>w.Id).Any(g=>g.Count()!=1))
    {
        foreach(var word in words.GroupBy(w => w.Id).First(g => g.Count() != 1))
        {
            Console.WriteLine($"##############\nERROR\n##############\nin id:{word.Id}");
            return;
        }
    }
    Console.WriteLine("succeeded");
}
catch (Exception e)
{
    Console.WriteLine("##############\nERROR\n##############\nin line: " + e.Message);
}
