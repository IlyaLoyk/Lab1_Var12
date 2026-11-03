using System;
using System.IO;
using System.Text;

namespace Lab3.FileStorage
{
        public class TextFileStorage : ITextStorage
    {
        private readonly string path;

        public TextFileStorage(string path)
        {
            this.path = path;
        }

        public string[] ReadLines()
        {
            string[] lines = new string[0];
            if (!File.Exists(path))
            {
                return lines;
            }

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Array.Resize(ref lines, lines.Length + 1);
                    lines[lines.Length - 1] = line;
                }
            }
            return lines;
        }

        public void WriteLines(string[] lines)
        {
            using (StreamWriter writer = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
