using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameEngine
{
    public class CSVReader//CSVファイルを読み込む
    {
        private List<string[]> stringData;
        public CSVReader()
        {
            stringData = new List<string[]>();
        }
        public void Read(string fileName, string path = "")
        {
            Initialize();
            try
            {

                using (var sr = new System.IO.StreamReader(@"Content/" + path + fileName))
                {
                    do
                    {
                        var line = sr.ReadLine();
                        var values = line.Split(',');
                        stringData.Add(values);
#if DEBUG
                        for (int i = 0; i < stringData.Count; i++)
                        {
                            for (int j = 0; j < stringData[0].Length; j++)
                            {
                                Console.Write(stringData[i][j]);
                            }
                            Console.WriteLine();
                        }
#endif
                    } while (
                        !sr.EndOfStream);
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                return;
            }
        }

        public void Initialize()
        {
            stringData.Clear();
        }
        public string[][] GetArrayData()
        {
            return stringData.ToArray();
        }
        public List<string[]> GetData()
        {
            return stringData;
        }
        public int[][] GetIntData()
        {
            var data = GetArrayData();
            int row = data.Count();
            int[][] intdata = new int[row][];
            for (int i = 0; i < row; i++)
            {
                int col = data[i].Count();
                intdata[i] = new int[col];
            }
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < intdata[y].Count(); x++)
                {
                    intdata[y][x] = int.Parse(data[y][x]);
                }
            }
            return intdata;
        }
        public string[,] GetStringMatrix()
        {
            var data = GetArrayData();
            int row = data.Count();
            int col = data[0].Count();

            string[,] result = new string[row, col];

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    result[y, x] = data[y][x];
                }
            }
            return result;
        }
        public int[,] GetIntMatrix()
        {
            var data = GetIntData();
            int row = data.Count();
            int col = data[0].Count();

            int[,] result = new int[row, col];
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    result[y, x] = data[y][x];
                }
            }
            return result;
        }

    }
}
