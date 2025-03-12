using System;
using System.IO;
using System.Xml.Linq;
using Avalonia.Animation;
using Avalonia.Markup.Xaml;
using Avalonia.Rendering;

namespace ImageEditor
{
    class BitGrid
    {
        private string _filename;
        public string FileName
        {
            get { return _filename; }
            set
            {
                _filename = value;
                Save(value);
            }
        }
        public int Width { get; set; }
        public int Height { get; set; }

        public Cell[,] bitmap { get; set; }

        public string bits { get; set; }

        //constructor
        public BitGrid(int width, int height)
        {
            // size of bitmap
            Width = width;
            Height = height;
        }

        public Cell[,] Load(string filepath)
        {
            try
            {
                StreamReader sr = new StreamReader(filepath);
                string[] bit_params = sr.ReadLine().Split(' ');

                try
                {
                    Height = int.Parse(bit_params[0]);
                    Width = int.Parse(bit_params[1]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error, file corrupted");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }

                bits = sr.ReadLine();
                bitmap = PopulateGrid();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return bitmap;
        }

        public void Save(string filepath)
        {
            try
            {
                string[] lines = { $"{Height} {Width}", grid2bit() };

                using (StreamWriter outputFile = new StreamWriter(filepath))
                {
                    foreach (string line in lines)
                    {
                        outputFile.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private Cell[,] PopulateGrid()
        {
            bitmap = new Cell[Height, Width];
            
            // filling 2D array with individual cells
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    int index = i * Width + j; // Calculate the index in the binary string
                    bitmap[i, j] = new Cell(i, j);
                    bitmap[i, j].IsColored = bits[index]; // Convert '0' or '1' to integer
                    Console.Write(bitmap[i, j].IsColored);
                }
                Console.Write('\n');
            }
            return bitmap;
        }

        private string grid2bit()
        {
            string bit = "";
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    bit += bitmap[i, j].IsColored; // Convert '0' or '1' to integer
                }
            }
            return bit;
        }
    }
}
