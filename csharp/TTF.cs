using System;
using System.Drawing.Text;
using System.IO;

namespace odttf2ttf
{
    public static class TTF
    {
        public static string GetFontName(byte[] fontData)
        {
            unsafe
            {
                fixed (byte* ptr = fontData)
                {
                    PrivateFontCollection fontCol = new PrivateFontCollection();
                    fontCol.AddMemoryFont((IntPtr)ptr, fontData.Length);

                    string fontName = fontCol.Families[0].Name;

                    fontCol.Dispose();

                    return fontName;
                }
            }
        }

        public static void Save(byte[] fontData)
        {
            string fontName = GetFontName(fontData);
            string outputFileName = fontName + ".ttf";

            using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                fs.Write(fontData, 0, fontData.Length);
            }
            
            Console.WriteLine("Decrypted font written to: " + outputFileName);
        }
    }
}