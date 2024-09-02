using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading;

namespace ProblematicProblem
{
    class Program
    {
        static bool cont = true;

        static List<string> activities = new List<string>() 
        { "Movies", "Paintball", "Bowling",
            "Lazer Tag", "LAN Party", "Hiking",
            "Axe Throwing", "Wine Tasting" 
        };
        static void Main(string[] args)
        {
            Random rng = new Random();

            bool cont = false;
            while (true)
            {
                Console.Write("Hello, welcome to the random activity generator! \nWould you like to generate a random activity? yes/no: ");
                string userResponse = Console.ReadLine().Trim().ToLower();

                if (userResponse == "yes")
                {
                    cont = true;
                    break;
                }
                else if (userResponse == "no")
                {
                    Console.WriteLine("Okay, Goodbye!");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid unput. Please enter a 'yes' or a 'no'.");
                }
            }
            Console.WriteLine();

            Console.Write("We are going to need your information first! What is your name? ");
            string userName = Console.ReadLine();
            Console.WriteLine();

            Console.Write("What is your age? ");
            string ageInput = Console.ReadLine();
            int userAge;

            if (!int.TryParse(ageInput, out userAge))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                return;
            }
            Console.WriteLine($"Your age is {userAge}");
            Console.WriteLine();

            Console.Write("Would you like to see the current list of activities? Sure/No: ");
            var input = Console.ReadLine().Trim().ToLower();
            bool seeList;

            if (input == "sure")
            {
                seeList = true;
            }
            else if (input == "no")
            {
                seeList= false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'sure' or 'no'.");
                seeList = false;
            }

            if (seeList)
            {
                List<string> displayActivities = new List<string>(activities);

                if (userAge < 21)
                {
                    displayActivities.Remove("Wine Tasting");
                }

                for (int i = 0; i < displayActivities.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {displayActivities[i]}");
                    Thread.Sleep(250);
                }
                Console.WriteLine();

                Console.Write("Would you like to add any activities from the list before we generate one? yes/no: ");
                input = Console.ReadLine().Trim().ToLower();
                bool addToList = (input == "yes");

                if (!addToList && input != "no")
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }

                Console.WriteLine();

                while (addToList)
                {
                    Console.Write("Please select the activity you would like to add by entering the corresponding number. ");

                    for (int i = 0; i < displayActivities.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {displayActivities[i]}");
                        Thread.Sleep(250);
                    }
                    Console.WriteLine();

                    if (int.TryParse(Console.ReadLine(), out int selectedNumber) && selectedNumber > 0 && selectedNumber <= displayActivities.Count)
                    {
                        string selectedActivity = displayActivities[selectedNumber - 1];

                        if (userAge < 21 && selectedActivity == "Wine Tasting")
                        {
                            Console.WriteLine("You cannot add 'Wine Tasting' to your list.");                          
                        }
                        else
                        {
                            activities.Add(selectedActivity);
                            Console.WriteLine($"{selectedActivity} has been added to your list!");
                        }

                        Console.WriteLine("Would you like to add more? yes/no: ");
                        input = Console.ReadLine().Trim().ToLower();

                        addToList = (input == "yes");

                        if (input != "yes" && input != "no")
                        {
                            Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                            addToList = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please enter a valid number.");
                    }                 
                }
            }

            while (cont)
            {
                Console.Write("Connecting to the database");

                for (int i = 0; i < 10; i++)
                {
                    Console.Write(". ");
                    Thread.Sleep(500);
                }
                Console.WriteLine();

                Console.Write("Choosing your random activity");
                for (int i = 0; i < 9; i++)
                {
                    Console.Write(". ");
                    Thread.Sleep(500);
                }
                Console.WriteLine();

                int randomNumber = rng.Next(activities.Count);
                string randomActivity = activities[randomNumber];

                if (userAge < 21 && randomActivity == "Wine Tasting")
                {
                   Console.WriteLine($"Oh no! Looks like you are too young to do {randomActivity}");
                   Console.WriteLine("Pick something else!");

                   activities.Remove(randomActivity);
                   randomNumber = rng.Next(activities.Count);
                   randomActivity = activities[randomNumber];
                }
                Console.Write($"Ah got it! {userName}, your random activity is: {randomActivity}! Is this ok or do you want to grab another activity? Keep/Redo: ");
                Console.WriteLine();

                string userChoice = Console.ReadLine().Trim().ToLower();

                if (userChoice == "keep") 
                {
                    Console.WriteLine($"Great! Enjoy your {randomActivity}, {userName}!");
                    cont = false;
                }
                else if (userChoice == "redo")
                {
                    Console.WriteLine($"No problem, {userName}. Let's pick another activity.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'Keep' or 'Redo.");
                }
            }
        }

    }
}
