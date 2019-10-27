using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameEngine
{
    public class CSVReader
    {
        public CSVReader() { }

        public static string[,] ReadFile(string fileName, out int width, out int height, string path = "./")
        {
            string[,] file;
            width = 0;
            height = 0;
            string[] stringData;

            using (var sr = new StreamReader(@"Content" + path + fileName))
            {
                var data = sr.ReadLine();
                data += ",";
                width = data.Length / 2;
                height++;
                while (!sr.EndOfStream)
                {
                    
                    data += sr.ReadLine();
                    Console.WriteLine(data);
                    data += ",";
                    Console.WriteLine(data);
                    height++;
                }
                stringData = new string[data.Length];
                stringData = data.Split(',');
            }

            file = new string[width, height];         
            for(int i = 0; i < height; i++)
            {
                for(int k = 0; k < width; k++)
                {
                    file[k, i] = stringData[k + width * i];
                }
            }

            return file;
            

        }
    }
}
