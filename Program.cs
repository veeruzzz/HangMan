using System;



public class Hangman
{
    private string[] words = { "house", "mirror", "floor" };
    private string selectedWord;
    private bool[] guessedLetters;
    private int guessCount;
    private string[] gallows =
    {
        "_____   \n|   |   \n|       \n|       \n|       \n|______ ",
        "_____   \n|   |   \n|   O   \n|       \n|       \n|______ ",
        "_____   \n|   |   \n|   O   \n|   |   \n|       \n|______ ",
        "_____   \n|   |   \n|   O   \n|  /|   \n|       \n|______ ",
        "_____   \n|   |   \n|   O   \n|  /|\\ \n|       \n|______ ",
        "_____   \n|   |   \n|   O   \n|  /|\\ \n|  /    \n|______ ",
        "_____   \n|   |   \n|   O   \n|  /|\\ \n|  / \\ \n|______ "
    };

    public Hangman()
    {
        Init();
    }

    public void Init()
    {
        Random rng = new Random();
        selectedWord = words[rng.Next(words.Length)];
        guessedLetters = new bool[selectedWord.Length];
        guessCount = 0;
    }

    public void ShowGameStartScreen()
    {
        Console.WriteLine(@"
   _    _   _____   __  __            _   _ 
  | |  | | / ____| |  \/  |    /\    | \ | |
  | |__| || |  __  | \  / |   /  \   |  \| |
  |  __  || | |_ | | |\/| |  / /\ \  | . ` |
  | |  | || |__| | | |  | | / ____ \ | |\  |
  |_|  |_| \_____| |_|  |_|/_/    \_\|_| \_|
                                            
                                            ");
        Console.WriteLine("Welcome to H@NG MAN!");
         
    }

    public void ShowBoard()
    {
        Console.WriteLine(gallows[guessCount]);
        for (int i = 0; i < guessedLetters.Length; i++)
        {
            Console.Write(guessedLetters[i] ? selectedWord[i] + " " : "_ ");
        }
        Console.WriteLine($"\nYou have {6 - guessCount} tries left.");
    }

    public void ShowInputOptions()
    {
        Console.Write("Enter any letter: ");
    }

    public string GetInput()
    {
        return Console.ReadLine().Trim().ToLower();
    }

    public bool IsValidInput(string input)
    {
        if (input.Length != 1 || !char.IsLetter(input[0]))
        {
            Console.WriteLine("Invalid input. Enter a single lowercase letter.");
            return false;
        }
        return true;
    }

    public void ProcessInput(string input)
    {
        bool found = false;
        for (int i = 0; i < selectedWord.Length; i++)
        {
            if (selectedWord[i] == input[0])
            {
                guessedLetters[i] = true;
                found = true;
            }
        }
        if (!found) guessCount++;
        Console.Clear();
    }

    public bool IsGameOver()
    {
        return guessCount >= 6 || Array.TrueForAll(guessedLetters, x => x);
    }

    public void ShowGameOverScreen()
    {
        ShowBoard();
        if (Array.TrueForAll(guessedLetters, x => x))
        {
            Console.WriteLine("Congratulations! You won!");
        }
        else
        {
            Console.WriteLine($"Game Over! You lost. The word was \"{selectedWord}\".");
        }
    }

    public void Start()
    {
        ShowGameStartScreen();
        do
        {
            ShowBoard();
            string input;
            do
            {
                ShowInputOptions();
                input = GetInput();
            } while (!IsValidInput(input));
            ProcessInput(input);
        } while (!IsGameOver());
        ShowGameOverScreen();
    }

    public static void Main()
    {
        Hangman game = new Hangman();
        game.Start();
    }
}