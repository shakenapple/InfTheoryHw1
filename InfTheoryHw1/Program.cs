using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfTheoryHw1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var analizer = new StatisticsAnalyzer(@"C:\PROGL"))
            {
                var a = analizer.GetSeqFrequences(1);
            }
        }
    }

    class StatisticsAnalyzer : IDisposable
    {
        private FileStream _reader;

        public StatisticsAnalyzer(string path)
        {
            _reader = File.OpenRead(path);
        }

        public Dictionary<string, int> GetSeqFrequences(int seqLength)
        {
            byte[] symbs = new byte[seqLength];
            var lastPos = 0;
            var result = new Dictionary<string, int>();
            while (_reader.CanRead)
            {
                _reader.Read(symbs, lastPos, seqLength);
                var str = Encoding.Default.GetString(symbs);
                if (!result.ContainsKey(str))
                {
                    result.Add(str,0);
                }
                result[symbs.ToString()] ++;
                lastPos += seqLength;
            }
            return result;
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
