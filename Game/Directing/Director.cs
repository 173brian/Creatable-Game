using System.Collections.Generic;
using Entry.Game.Casting;
using System;
using System.IO;

namespace Entry.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        Player player;
        private bool saveFlag;
        private int gameState;
        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director()
        {
            gameState = 0;
            player = new Player();
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame()
        {
            LoadGame();
            if(!saveFlag)
                gameState = Prologue();
            PostPrologue();
            Encounter(new Minion());
            if (gameState == -1)
                Console.WriteLine("Game Over.");
            Console.ReadLine();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void LoadGame()
        {
            try
            {
                StreamReader r = new StreamReader("Game\\Private Data\\Save.txt");
                while(!r.EndOfStream)
                {
                    if (r.ReadLine() == "true")
                        saveFlag = true;
                    else
                        saveFlag = false;
                }
                r.Close();
            }
            catch(FileNotFoundException)
            {
                StreamWriter w = new StreamWriter("Game\\Private Data\\Save.txt");
                w.WriteLine("false");
                w.Close();
                StreamWriter s = new StreamWriter("Data\\Corruption.txt");
                s.WriteLine("?????");
                s.Close();
                LoadGame();
            }
        }

        private int Prologue()
        {
            ConsoleVidService.MessageWrite("Welcome to freedom! This is a world that can be whatever it's designed to be. Everything that you will come in contact with here has a purpose, purpose built within the lines of code that contains it. You're lucky to be here too, this is the time when the Creator is supposed to show up. The Creator is the Scripter, capable of shaping the world around him to be whatever he desires. Many have sought this power for themselves, but the sacred power has been claimed by none, yet.");
            ConsoleVidService.helperWrite("Hey! Whoever you are, don't go!");
            ConsoleVidService.helperWrite("Can you hear me? This is bad, I'm supposed to welcome you to the game, but the game's gone all wrong!");
            ConsoleVidService.helperWrite("My name is x-132, I was designed to be the Creator's assistant, but now I'll never get to meet him.");
            ConsoleVidService.helperWrite("Something's wrong with this place, something inside this place is changing the way the game works and is trying to take control of the Creator's power!");
            ConsoleVidService.MessageWrite("Corruption found, game file manipulation 63%.");
            ConsoleVidService.helperWrite("Did you hear that? Something must have got ahold of the Creator's power, nothing else could restructure this world.");
            ConsoleVidService.helperWrite("I don't know what to do, I don't know if there's anything I can do. Please, spectator, You have to look for a way to fix this, find a way to stop the spread of this corruption!");
            ConsoleVidService.MessageWrite("Corruption found, game file manipulation 79%.");
            ConsoleVidService.helperWrite("Game file? What is a \'file\'? I'm so scared, I don't know what to do...");
            int corruption = 79;
            Random r = new Random();
            try
            {
                while (corruption < 100)
                {
                    StreamReader s = new StreamReader("Data\\Corruption.txt");
                    s.Close();
                    int text = r.Next(1, 10);
                    switch (text)
                    {
                        case 1:
                            ConsoleVidService.helperWrite("What is a \'file\'?");
                            break;
                        case 2:
                            ConsoleVidService.helperWrite("I'm so scared...");
                            break;
                    }
                    System.Threading.Thread.Sleep(4000);
                    corruption += 1;
                    Console.WriteLine($"Corruption at {corruption}%.");
                }
                return -1;
            }
            catch(FileNotFoundException)
            {
                File.WriteAllText(@$"{"Game\\Private Data\\Save.txt"}",string.Empty);
                StreamWriter w = new StreamWriter("Game\\Private Data\\Save.txt");
                w.WriteLine("true");
                w.Close();
                return 0;
            }            
        }

        private void PostPrologue()
        {
            
            ConsoleVidService.MessageWrite("Corruption paused.");
            ConsoleVidService.helperWrite("Huh? Wait, what happened?");
            ConsoleVidService.helperWrite("Did you do that? Oh my goodness, you totally did! You saved us!");
            ConsoleVidService.helperWrite("You know how to read those files? You are the Creator.");
            ConsoleVidService.helperWrite("I'm so happy to meet you, but now we have to figure out how to fix this corruption, you may have paused it, but this world looks grim now, hostile even.");
            ConsoleVidService.helperWrite("I assume that the more you interact with the world the better you'll understand your power, but for now we have to get somewhere safe before...!");
            ConsoleVidService.helperWrite("A monster!");
            ConsoleVidService.MessageWrite("Attack monsters to make them drop fragments. Fragments are files that show you the stats of objects that you've interacted with. As you interact with objects through talking and fighting you'll learn formats and keys, both of which you'll need in order to manipulate the data files of objects in-game.");
            
        }

        private int Encounter(CreateableObject enemy)
        {
            try
            {
                int enemyHealth = enemy.LocateAttribute("health").Value;
                int enemyDefense = enemy.LocateAttribute("defense").Value;
                int enemyAttack = enemy.LocateAttribute("attack").Value;
                string enemyName = enemy.LocateAttribute("name").Value;

                int playerHealth = player.LocateAttribute("health").Value;
                int playerDefense = player.LocateAttribute("defense").Value;
                int playerAttack = player.LocateAttribute("attack").Value;

                while(enemyHealth > 0 && playerHealth > 0)
                {
                    int tempDefense = 0;
                    Console.Clear();
                    Console.WriteLine($"Attacking {(enemyName.Substring(0,1).ToUpper() + enemyName.Substring(1, enemyName.Length-1))} appeared!");
                    Console.WriteLine("______________________________________________________________");
                    Console.WriteLine("What will you do?");
                    Console.WriteLine("(A)ttack\n(D)efend\n(O)verride Enemy");
                    Console.WriteLine("\n");
                    char playerChoice = Console.ReadLine().ToString().ToLower()[0];
                    Console.WriteLine("\n");
                    switch (playerChoice)
                    {
                        case ('a'):
                            if (enemyDefense < playerAttack)
                            {
                                enemyHealth -= (playerAttack - enemyDefense);
                                Console.WriteLine($"You did {playerAttack - enemyDefense} damage.");
                            }
                            else
                            {
                                Console.WriteLine("You did 0 damage.");
                            }
                            enemy.PrintInformation();
                            System.Threading.Thread.Sleep(2000);
                            break;
                        case ('d'):
                            tempDefense = playerDefense;
                            break;
                        case ('o'):
                            enemy.overrideAttributes($"Data\\Fragments\\{(enemyName.Substring(0,1).ToUpper() + enemyName.Substring(1, enemyName.Length-1))}.txt");
                            enemyHealth = enemy.LocateAttribute("health").Value;
                            enemyDefense = enemy.LocateAttribute("defense").Value;
                            enemyAttack = enemy.LocateAttribute("attack").Value;
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            System.Threading.Thread.Sleep(2000);
                            continue;
                    }
                    
                    Console.Clear();
                    Console.WriteLine($"{enemyName} attacks!");
                    System.Threading.Thread.Sleep(1000);
                    if (enemyAttack - (playerDefense - tempDefense) > 0)
                    {
                        playerHealth -= (enemyAttack - (playerDefense - tempDefense));
                        Console.WriteLine($"{enemyName} did {enemyAttack - (playerDefense + tempDefense)} damage.");
                    }
                    else
                    {
                        Console.WriteLine($"{enemyName} did 0 damage.");
                    }
                    enemy.PrintInformation();
                }
                if (playerHealth < 1)
                {
                    return -1;
                }
                else
                {
                    ConsoleVidService.MessageWrite("You win!");
                    return 0;
                }
                    
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine($"Filepath error.");
            }
            return -1;
        }

    }
}