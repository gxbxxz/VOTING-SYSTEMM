using System;
using System.Collections.Generic;

namespace VotingSystem
{
    class Program
    {
        static Dictionary<string, int> candidates = new Dictionary<string, int>();
        static HashSet<string> voterIDs = new HashSet<string>();

        static void AddCandidate(string name)
        {
            if (!candidates.ContainsKey(name))
            {
                candidates[name] = 0;
                Console.WriteLine($"Candidate '{name}' added successfully!");
            }
            else
            {
                Console.WriteLine("Candidate already exists.");
            }
        }

        static void CastVote(string voterID, string candidateName)
        {
            if (voterIDs.Contains(voterID))
            {
                Console.WriteLine("You have already voted. Each user is allowed only one vote.");
                return;
            }

            if (candidates.ContainsKey(candidateName))
            {
                candidates[candidateName]++;
                voterIDs.Add(voterID);
                Console.WriteLine($"Vote casted successfully for {candidateName} by voter ID {voterID}.");
            }
            else
            {
                Console.WriteLine("Candidate not found.");
            }
        }

        static void DisplayResults()
        {
            Console.WriteLine("\nVoting Results:");
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"{candidate.Key}: {candidate.Value} vote(s)");
            }
        }

        static void DeclareWinner()
        {
            int maxVotes = -1;
            List<string> winners = new List<string>();

            foreach (var candidate in candidates)
            {
                if (candidate.Value > maxVotes)
                {
                    maxVotes = candidate.Value;
                    winners.Clear();
                    winners.Add(candidate.Key);
                }
                else if (candidate.Value == maxVotes)
                {
                    winners.Add(candidate.Key);
                }
            }

            Console.WriteLine("\nElection Result:");
            if (winners.Count == 1)
            {
                Console.WriteLine($"The winner is {winners[0]} with {maxVotes} vote(s)!");
            }
            else
            {
                Console.WriteLine("It's a tie between the following candidates:");
                foreach (var winner in winners)
                {
                    Console.WriteLine($"{winner} with {maxVotes} vote(s)");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Voting System!");

            AddCandidate("Sahil");
            AddCandidate("Gaurav");
            AddCandidate("Abhay");

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Cast a vote");
                Console.WriteLine("2. Display results");
                Console.WriteLine("3. Add a candidate");
                Console.WriteLine("4. Declare winner and exit");

                bool isValidInput = int.TryParse(Console.ReadLine(), out int choice);
                if (!isValidInput)
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter your voter ID: ");
                        string voterID = Console.ReadLine();

                        Console.Write("Enter the candidate's name: ");
                        string candidateName = Console.ReadLine();

                        CastVote(voterID, candidateName);
                        break;

                    case 2:
                        DisplayResults();
                        break;

                    case 3:
                        Console.Write("Enter the new candidate's name: ");
                        string newCandidate = Console.ReadLine();
                        AddCandidate(newCandidate);
                        break;

                    case 4:
                        DeclareWinner();
                        Console.WriteLine("Thank you for using the voting system. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
