using IdeaCipher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Idea_Example
{
    class Program
    {
        static readonly String tempInputFilename = "tempPlainText.txt";
        static readonly String tempOutputFilename = "tempEncryptedData.dat";
        static void Main(string[] args)
        {
            //  sed123459x921234sed123459x921234sed123459x921234sed123459x921234sed123459x921234sed123459x921234sed123459x921234sed123459x921234
            BigInteger inputKey = new BigInteger(2);
            inputKey = BigInteger.Pow(inputKey, 127);
            inputKey -= 123456789;

            string encryptedText, decryptedText = string.Empty;
            StringBuilder plainText = new StringBuilder();
            using (StreamReader reader = new StreamReader(tempInputFilename))
            {
                plainText.Append(reader.ReadToEnd());
            }
            File.WriteAllText(tempInputFilename, plainText.ToString());
            IdeaCrypt.СryptFile(tempInputFilename, tempOutputFilename, inputKey.ToString(), true);
            encryptedText = String.Join(" ", File.ReadAllBytes(tempOutputFilename));

            using (StreamWriter writer = new StreamWriter("tempEncryptedText.txt"))
            {
                writer.WriteLine(encryptedText);
            }
            
            string[] symbols = encryptedText.Split(' ');
            byte[] bytes = new byte[symbols.Length];
            for (int i = 0; i < symbols.Length; ++i)
            {
                Console.WriteLine($"{i + 1}.\t{symbols[i]}");
                bytes[i] = byte.Parse(symbols[i]);
            }

            Console.WriteLine("Encrypted text: {0}", encryptedText);

            Console.ReadKey();
        }
    }
}
