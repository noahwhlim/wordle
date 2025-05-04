using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        var wordList = LoadWordsFromCsv("C:\\Users\\Noah\\Documents\\projects\\wordle\\testwords.csv");

        if (wordList.Count == 0)
        {
            Console.WriteLine("Word list is empty or could not be loaded.");
            return;
        }

        var random = new Random();
        string targetWord = wordList[random.Next(wordList.Count)];
        int attempts = 6;

        Console.WriteLine("Welcome to Wordle (C# CLI version)!");
        Console.WriteLine("Guess the 5-letter word. You have 6 attempts.\n");

        while (attempts > 0)
        {
            Console.Write($"Enter guess ({attempts} left): ");
            string guess = Console.ReadLine()?.Trim().ToLower();

            if (guess == null || guess.Length != 5)
            {
                Console.WriteLine("❌ Invalid guess.");
                continue;
            }

            if (guess == targetWord)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n🎉 Correct! You win!");
                Console.ResetColor();
                return;
            }

            PrintFeedback(guess, targetWord);
            attempts--;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n💀 Out of attempts! The word was: {targetWord}");
        Console.ResetColor();
    }

    static List<string> LoadWordsFromCsv(string path)
    {
        var words = new List<string>();

        try
        {
            foreach (var line in File.ReadLines(path))
            {
                var word = line.Trim().ToLower();

                if (word.Length == 5 && !string.IsNullOrWhiteSpace(word))
                {
                    words.Add(word);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading word file: {e.Message}");
        }

        return words;
    }

    static void PrintFeedback(string guess, string target)
    {
        for (int i = 0; i < 5; i++)
        {
            char letter = guess[i];

            if (letter == target[i])
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (target.Contains(letter))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

            Console.Write(letter);
        }

        Console.ResetColor();
        Console.WriteLine();
    }
}
