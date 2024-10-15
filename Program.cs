using System.Text.Json;
using static System.Console;

internal class Program
{
  static int PlayerWins = 0;
  static int ComputerWins = 0;

  private static void Main(string[] args)
  {
    WriteLine("Rock Paper Scissors!");

    try
    {
      LoadGame();
      WriteLine("File Found!");
    }
    catch (Exception)
    {
      WriteLine("No File Found!");
    }

    // NOTE Check win conditions | replaced the alternative
    var outcomes = new Dictionary<string, Dictionary<string, string>>()
        {
            { "Rock", new Dictionary<string, string>
                {
                    { "Rock", "Tie" },
                    { "Paper", "Lose" },
                    { "Scissors", "Win" }
                }
            },
            { "Paper", new Dictionary<string, string>
                {
                    { "Rock", "Win" },
                    { "Paper", "Tie" },
                    { "Scissors", "Lose" }
                }
            },
            { "Scissors", new Dictionary<string, string>
                {
                    { "Rock", "Lose" },
                    { "Paper", "Win" },
                    { "Scissors", "Tie" }
                }
            }
        };

    while (true)
    {
      Clear();
      string userHand = ChooseHand();
      string computerHand = ComputerHand();
      WriteLine($"You chose: {userHand}");
      WriteLine($"Computer Chose: {computerHand}");

      string result = outcomes[userHand][computerHand];

      if (result == "Win")
      {
        WriteLine("Player WINS!");
        ++PlayerWins;
      }
      else if (result == "Lose")
      {
        WriteLine("Computer WINS!");
        ++ComputerWins;
      }
      else
      {
        WriteLine("It's a TIE!");
      }

      // NOTE Check win conditions if / else | Alternative
      // if (userHand == "Rock" && computerHand == "Scissors" || userHand == "Paper" && computerHand == "Rock" || userHand == "Scissors" && computerHand == "Paper")
      // {
      //   WriteLine("Player WINS!");
      //   ++PlayerWins;
      // }
      // else if (computerHand == "Rock" && userHand == "Scissors" || computerHand == "Paper" && userHand == "Rock" || computerHand == "Scissors" && userHand == "Paper")
      // {
      //   WriteLine("Computer WINS!");
      //   ++ComputerWins;
      // }
      // else
      // {
      //   WriteLine("It's a TIE!");
      // }

      Write("Play again? (Y/N): ");
      char decider = ReadKey().KeyChar;
      decider = char.ToUpper(decider);
      if (decider == 'Y')
      {
        WriteLine("\nAlrighty!");
      }
      else
      {
        WriteLine("\nFarewell");
        WriteLine($"Stats\nPlayer: {PlayerWins} | Computer: {ComputerWins}");
        SaveGame();
        Environment.Exit(0);
      }
    }
  }

  static string ChooseHand()
  {
    while (true)
    {
      Write("Choose a hand: [Rock, Paper, Scissors, Lizard, Spock]: ");
      string userInput = ReadLine();
      // If first letter is lowercase then we uppercase it
      userInput = string.Concat(userInput[0].ToString().ToUpper(), userInput.AsSpan(1));
      switch (userInput)
      {
        case "Rock":
        case "Paper":
        case "Scissors":
        case "Lizard":
        case "Spock":
          return userInput;
        default:
          WriteLine("Invalid! \nValid Options: [Rock, Paper, Scissors, Lizard, Spock]");
          continue;
      }
    }
  }

  static string ComputerHand()
  {
    string[] hand = ["Rock", "Paper", "Scissors", "Lizard", "Spock"];
    int randomInt = new Random().Next(0, 5);
    string selectedHand = hand[randomInt];
    return selectedHand;
  }

  static void SaveGame()
  {
    SaveData save = new SaveData(PlayerWins, ComputerWins);
    string saveData = JsonSerializer.Serialize(save);
    File.WriteAllText("saveGame.json", saveData);
  }

  static void LoadGame()
  {
    string jsonString = File.ReadAllText("saveGame.json");
    SaveData data = JsonSerializer.Deserialize<SaveData>(jsonString);
    if (data != null)
    {
      PlayerWins = data.PlayerWins;
      ComputerWins = data.ComputerWins;
    }
  }
}

internal class SaveData
{
  public int PlayerWins { get; set; }
  public int ComputerWins { get; set; }

  public SaveData(int playerWins, int computerWins)
  {
    PlayerWins = playerWins;
    ComputerWins = computerWins;
  }
}