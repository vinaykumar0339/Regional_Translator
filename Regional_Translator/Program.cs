using System.Text.Json;

namespace Regional_Translator
{
    class MainClass
    {
        public static List<DictionaryWords>? dictionaryWords = LoadDictionary("Resources/dictionary.json");
        public static void Main(string[] args)
        {
            if (dictionaryWords is null)
            {
                Console.WriteLine("Failed to load dictionary.");
                return;
            }

            AskWord(dictionaryWords);
        }

        public static void AskWord(List<DictionaryWords> dictionaryWords) {
            Console.WriteLine("Hello! Happy to help!!!");
            Console.Write("Please enter a word: ");
            string teluguWord = Console.ReadLine() ?? string.Empty;
            string? englishWord = GetEnglishWord(teluguWord, dictionaryWords);
            if (englishWord is null) {
                Console.WriteLine($"Sorry, I don't know the English word for \"{teluguWord}\".");
            } else {
                Console.WriteLine($"The English word for \"{teluguWord}\" is \"{englishWord}\"");
            }
            Console.Write("Are you looking for more words? [y/n] ");
            string response = Console.ReadLine() ?? string.Empty;
            if (response == "y")
            {
                AskWord(dictionaryWords);
            }
            else
            {
                Console.WriteLine("Thanks for using Happy to help 😊 Bye!");
            }
        }

        public static string? GetEnglishWord(string teluguWord, List<DictionaryWords> dictionaryWords) {
            foreach (DictionaryWords word in dictionaryWords)
            {
                if (word.telugu == teluguWord)
                {
                    return word.english;
                }
            }
            return null;
        }

        public static List<DictionaryWords>? LoadDictionary(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file was not found.", filePath);
            }

            string jsonContent = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<DictionaryWords>>(jsonContent);
        }
    }
}
