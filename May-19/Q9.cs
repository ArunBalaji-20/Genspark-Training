using System;
using System.Collections.Generic;

//Has a predefined secret word (e.g., "GAME").

//Accepts user input as a 4-letter word guess.

//Compares the guess to the secret word and outputs:

//X Bulls: number of letters in the correct position.

//Y Cows: number of correct letters in the wrong position.

//Continues until the user gets 4 Bulls (i.e., correct guess).

//Displays the number of attempts.

//Bull = Correct letter in correct position.

//Cow = Correct letter in wrong position.

//Secret Word	User Guess	Output	Explanation
//GAME	GAME	4 Bulls, 0 Cows	Exact match
//GAME	MAGE	2 Bull, 2 Cows	A,E in correct position, MG misplaced
//GAME	GUYS	1 Bull, 0 Cows	G in correct place, rest wrong
//GAME	AMGE	2 Bulls, 2 Cows	A, E right; M, G misplaced
//NOTE	TONE	2 Bulls, 2 Cows	O, E right; T, N misplaced

namespace may19
{
    internal class Class8
    {

        static void CheckWord(string secret, string word)
        {
            int bulls = 0;
            int cows = 0;
            if (secret == word)
            {
                Console.WriteLine($"{secret} {word} 4 Bulls, 0 Cows Exact match");
            }
            else
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == secret[i])
                    {
                        bulls++;
                    }
                    else
                    {
                        if (secret.Contains(word[i]))
                        {
                            cows++;
                        }
                    }
                }
                Console.WriteLine($"{secret} {word} {bulls} Bulls, {cows} Cows Exact match");


            }


        }


        public static void Main(string[] args)
        {
            String secret = "GAME";


            string word;
            while (true)
            {
                Console.Write("Please submit your Guess word (4 letters): ");
                word = (Console.ReadLine() ?? "").ToUpper();

                if (word.Length == 4 && word.All(char.IsLetter))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter exactly 4 alphabetic letters.");
                }
            }
            CheckWord(secret, word);

        }
    }
}
