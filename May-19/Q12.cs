using System;
using System.Collections.Generic;
using System.ComponentModel;

//12) Write a program that:

//Takes a message string as input (only lowercase letters, no spaces or symbols).

//Encrypts it by shifting each character forward by 3 places in the alphabet.

//Decrypts it back to the original message by shifting backward by 3.

//Handles wrap-around, e.g., 'z' becomes 'c'.

//Examples
//Input:     hello
//Encrypted: khoor
//Decrypted: hello
//------------ -
//Input:     xyz
//Encrypted: abc
//Test cases
//| Input | Shift | Encrypted | Decrypted |
//| ----- | ----- | --------- | --------- |
//| hello | 3     | khoor     | hello     |
//| world | 3     | zruog     | world     |
//| xyz   | 3     | abc       | xyz       |
//| apple | 1     | bqqmf     | apple     |

namespace may19
{
    internal class Encryption
    {
        string? word;
        int shift;
        string? encryptedWord;
        string? decryptedWord;
        void getInput()
        {
            while (true)
            {
                Console.WriteLine("Enter the string :");
                word = Console.ReadLine();

                if (!string.IsNullOrEmpty(word))
                {
                    word = word.ToLower().Trim();
                    if (word.All(char.IsLetter))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(" Numbers and symbols are not allowed.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }

            Console.WriteLine("Enter shift value(1-26):");
            while (!int.TryParse(Console.ReadLine(), out shift) || shift < 1 || shift > 26)
                Console.WriteLine("Invalid input. Please try again \n Enter shift value(1-26):;");
        }

        void Encrypt()
        {
            for (int i = 0; i < word.Length; i++)
            {
                int asciiCode = Convert.ToInt32(word[i]);
                int code = (((asciiCode - 97) + shift) % 26) + 97;
                encryptedWord = encryptedWord + (char)code;
            }
            Console.WriteLine($"Encrypted string: {encryptedWord}");

        }

        void decrypt()
        {
            for (int i = 0; i < word.Length; i++)
            {
                int asciiCode = Convert.ToInt32(word[i]);
                int code = ((asciiCode - 97 - shift + 26) % 26) + 97;
                decryptedWord = decryptedWord + (char)code;
            }
            Console.WriteLine($"decrypted string: {decryptedWord}");
        }
        public static void Main(string[] args)
        {
            Encryption obj = new Encryption();
            Console.WriteLine("1.Encrypt a string\n2.Decrypt a string\n3.exit");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    obj.getInput();
                    obj.Encrypt();
                    break;

                case 2:
                    obj.getInput();
                    obj.decrypt();
                    break;

                case 3:
                    break;
                default:
                    Console.WriteLine("Enter a valid choice!!");
                    break;
            }

        }
    }
}
