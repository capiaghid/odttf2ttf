using System;
using System.IO;

namespace odttf2ttf
{
    public static class ODTTF
    {
        public static byte[] Deobfuscate(string odttfFilePath)
        {
            byte[] fontData;

            using (FileStream fs = new FileStream(odttfFilePath, FileMode.Open))
            {
                fontData = new byte[fs.Length];
                int dataRead = fs.Read(fontData, 0, fontData.Length);

                if(dataRead < fontData.Length)
                {
                    throw new ApplicationException($"Failure in conversion: Only {dataRead} bytes were converted "
                        + $"ant not the expected {fontData.Length} bytes!");
                }
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(odttfFilePath);

            string guid = fileNameWithoutExtension.Replace("-", "");

            Console.WriteLine("Key found: " + guid);

            byte[] key = new byte[16];
            for (int i = 0; i < key.Length; i++)
            {
                key[key.Length - 1 - i] = Convert.ToByte(guid.Substring(i * 2, 2), 16);
            }

            for (int i = 0; i < 32; i++)
            {
                fontData[i] ^= key[i % key.Length];
            }

            Console.WriteLine("Encryption complete");
            return fontData;
        }
    }
}