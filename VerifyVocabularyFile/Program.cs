// See https://aka.ms/new-console-template for more information

using DownloadToBrain.Infrastructure;

Console.WriteLine("Hello, World!");

var userRepository = new UserRepository();

var words = await userRepository.LoadAllWords();

int a = 0;