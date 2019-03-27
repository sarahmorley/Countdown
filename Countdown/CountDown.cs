using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Countdown
{
    class Program
    {
        const int Rounds = 8;
        static void Main(string[] args)
        {
            var currentRound = 0;
            var playing = true;
            var turn = 0;
            var letters = new List<char>();

            var playerOne = new Player();
            playerOne = CreatePlayer(playerOne);
            var playerTwo = new Player();
            playerTwo = CreatePlayer(playerTwo);


            while (playing && currentRound <= Rounds)
            {
                if (turn % 2 == 0)
                {
                    letters = PickLetters(playerOne);
                }
                else
                {
                    letters = PickLetters(playerTwo);
                }
                turn++;

                //Show timer
                Console.Write("Timer Running: ");
                for (int a = 30; a >= 0; a--)
                {
                    Console.CursorLeft = 22;
                    Console.Write("{0}", a);
                    Thread.Sleep(1000);
                }

                Console.WriteLine(Environment.NewLine);

                //Get the players to enter their word
                Console.WriteLine("{0} Please Enter your word: ", playerOne.Name);
                var wordOne = Console.ReadLine().ToUpper();
                Console.WriteLine("{0} Please Enter you word: ", playerTwo.Name);
                var wordTwo = Console.ReadLine().ToUpper();

                Console.WriteLine("The two words for this round are {0} and {1}", wordOne, wordTwo);

                CheckWordIsValid(letters, wordOne, playerOne);
                CheckWordIsValid(letters, wordTwo, playerTwo);

                //Display the latest score
                Console.WriteLine("{0} your score is: {1}", playerOne.Name, playerOne.Score);
                Console.WriteLine("{0} your score is: {1}", playerTwo.Name, playerTwo.Score);

                currentRound++;
            }

            CalculateWinner(playerOne, playerTwo);
            Console.ReadLine();
            
        }

        public static void CheckWordIsValid(List<char> letters, string word, Player player)
        {
            bool valid = true;
            foreach (var letter in word)
            {
                if (!letters.Contains(letter))
                    valid = false;
            }

            if (valid)
                ScoreWord(word, player);
            else
                Console.WriteLine("{0} is not a valid word. {1} you get no score for this round", word, player.Name);
        }

        public static void CalculateWinner(Player playerOne, Player playerTwo)
        {
            if (playerOne.Score > playerTwo.Score)
                Console.WriteLine("Congratulations {0} You are the winner", playerOne.Name);
            else
                Console.WriteLine("Congratulations {0} You are the winner", playerTwo.Name);
        }

        public static int ScoreWord(string word, Player player)
        {
            var score = word.Count();
            player.Score += score;
            return player.Score;
        }

        public static List<char> PickLetters(Player player)
        {
            List<char> letters = new List<char>();
            char x;
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("{0} its your turn: Please pick a Vowel(v) or Consonant(c)", player.Name);
                var type = Console.ReadLine();
                while (type != "v" && type != "c")
                {
                    Console.WriteLine("Please select either v or c");
                    type = Console.ReadLine();
                }
                if (type == "v")
                {
                    x = char.Parse(PickRandomVorC(1));
                    Console.WriteLine(x);
                    letters.Add(x);
                }
                else if (type == "c")
                {
                    x = char.Parse(PickRandomVorC(2));
                    Console.WriteLine(x);
                    letters.Add(x);
                }
            }
            Console.WriteLine(string.Format("The letters for this round are: " + Environment.NewLine + " {0}", string.Join(" ", letters)));
            return letters; 
        }

        public static string PickRandomVorC(int type)
        {
            char[] vowels = "AEIOU".ToCharArray();
            char[] cons = "BCDFGHJKLMNPQRSTVWXYZ".ToCharArray();
            var sb = new StringBuilder();
            var random = new Random();

            switch(type)
            {
                case 1:
                    int num = random.Next(0, vowels.Length);
                    sb.Append(vowels[num]);
                    break;
                case 2:
                    int num2 = random.Next(0, cons.Length);
                    sb.Append(cons[num2]);
                    break;
            }
            return sb.ToString();
        }

        public static Player CreatePlayer(Player player)
        {
            Console.WriteLine("Please Enter Your Name: ");
            player.Name = Console.ReadLine();
            Console.WriteLine("Welcome to the Game {0}.", player.Name);
            Console.WriteLine("\n");
            return player;
        }
    }
}
