using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class CSVWriter
    {
        int[,] data;
        public CSVWriter()
        {

        }
        public void Write(string fileName,int[,]writeData)
        {
            try
            {
                using(var sw=new System.IO.StreamWriter(@"Content/"+fileName))
                    {
                    string output="";
                    for(int i = 0; i < writeData.GetLength(0); i++)
                    {
                        for(int j = 0; j < writeData.GetLength(1)-1; j++)
                        {
                            output += writeData[i, j]+ ",";
                        }
                        output += writeData[i, writeData.GetLength(1) - 1]+sw.NewLine;
                        
                    }
                    sw.Write(output);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
