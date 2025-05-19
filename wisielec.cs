using System;
using System.Collections.Generic;

class WordBank
{
    private List<string> words = new List<string> { "komputer", "programowanie", "wisielec", "klasa", "python" };
    private Random random = new Random();

    public string GetRandomWord()
    {
        int index = random.Next(words.Count);
        return words[index];
    }
}

class Player
{
    public HashSet<char> GuessedLetters { get; private set; } = new HashSet<char>();

    public char GuessLetter()
    {
        Console.Write("Podaj literę: ");
        string input = Console.ReadLine().ToLower();

        if (!string.IsNullOrEmpty(input) && char.IsLetter(input[0]))
            return input[0];
        else
            return GuessLetter();
    }
}

class Game
{
    private WordBank wordBank = new WordBank();
    private Player player = new Player();
    private string word;
    private HashSet<char> correctLetters = new HashSet<char>();
    private int errors = 0;
    private int maxErrors = 7;

    public void Play()
    {
        word = wordBank.GetRandomWord();

        Console.WriteLine("=== Gra w Wisielca ===");

        while (true)
        {
            DisplayWord();
            Console.WriteLine($"Błędy: {errors}/{maxErrors}");
            Console.WriteLine($"Odgadnięte litery: {string.Join(", ", player.GuessedLetters)}");

            char guess = player.GuessLetter();

            if (player.GuessedLetters.Contains(guess))
            {
                Console.WriteLine("Już próbowałeś tej litery.");
                continue;
            }

            player.GuessedLetters.Add(guess);

            if (word.Contains(guess))
            {
                correctLetters.Add(guess);
                Console.WriteLine("Dobrze!");
            }
            else
            {
                errors++;
                Console.WriteLine("Źle!");
            }

            if (CheckGameStatus()) break;
        }

        Console.WriteLine("Koniec gry.");
    }

    private void DisplayWord()
    {
        foreach (char c in word)
        {
            if (correctLetters.Contains(c))
                Console.Write(c + " ");
            else
                Console.Write("_ ");
        }
        Console.WriteLine();
    }

    private bool CheckGameStatus()
    {
        if (errors >= maxErrors)
        {
            Console.WriteLine($"Przegrałeś! Słowo to: {word}");
            return true;
        }
        else if (new HashSet<char>(word).IsSubsetOf(correctLetters))
        {
            Console.WriteLine($"Gratulacje! Odgadłeś słowo: {word}");
            return true;
        }

        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Play();
    }
}
