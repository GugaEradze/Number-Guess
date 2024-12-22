using System;
using System.Collections.Generic;
using System.IO;

internal class Program
{
    private static string filePath = "records.txt";

    private static void Main(string[] args)
    {
        bool isCorrectGuess = false;
        Random random = new Random();
        int randomNumber = random.Next(1, 101);
        int attempts = 0;

        List<int> records = LoadRecords();

        Console.WriteLine("========================================");
        Console.WriteLine("          Number Guessing Game          ");
        Console.WriteLine("========================================");
        Console.WriteLine("A Number Between 1 And 100 Will Be Generated.");
        Console.WriteLine("If You Guess The Correct Number, You Win!");
        Console.WriteLine("Press any key to start...");
        Console.ReadKey(intercept: true);
        Console.Clear();

        while (!isCorrectGuess)
        {
            Console.WriteLine("Please Enter Your Guess:");
            string input = Console.ReadLine();
            int guess;

            if (!int.TryParse(input, out guess))
            {
                Console.WriteLine("Invalid input! Please enter a number.");
                continue;
            }

            attempts++;

            if (guess > randomNumber)
            {
                Console.WriteLine("Your Guess Is Too High!");
            }
            else if (guess < randomNumber)
            {
                Console.WriteLine("Your Guess Is Too Low!");
            }
            else
            {
                Console.WriteLine("Correct!");
                isCorrectGuess = true;
            }
        }

        Console.WriteLine($"Congratulations! You guessed the number in {attempts} attempts.");
        records.Add(attempts);

        SaveRecords(records);

        int bestScore = int.MaxValue;
        int worstScore = int.MinValue;

        foreach (int record in records)
        {
            if (record < bestScore)
            {
                bestScore = record;
            }

            if (record > worstScore)
            {
                worstScore = record;
            }
        }

        Console.WriteLine("========================================");
        Console.WriteLine("              Game Records               ");
        Console.WriteLine("========================================");
        Console.WriteLine($"Best Score: {bestScore} attempts");
        Console.WriteLine($"Worst Score: {worstScore} attempts");
        Console.WriteLine("========================================");
        Console.ReadKey(intercept:true);
    }

    private static List<int> LoadRecords()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            List<int> records = new List<int>();
            foreach (string line in lines)
            {
                if (int.TryParse(line, out int record))
                {
                    records.Add(record);
                }
            }
            return records;
        }
        return new List<int>();
    }

    private static void SaveRecords(List<int> records)
    {
        File.WriteAllLines(filePath, records.ConvertAll(r => r.ToString()));
    }
}
