using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_game
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    public class FileManager
    {
        private string filename;                //string variable to store the file name

        public FileManager(string filename)
        {
            this.filename = filename;
        }

        public List<string[]> ReadFileHiscore()
        {
            try
            {
                //reads text files and returns an array with the text file content (high score table)
                List<string[]> lines = new List<string[]>();
                using (StreamReader file = new StreamReader(filename))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] wordsInLine = line.Split();
                        lines.Add(wordsInLine);
                    }
                }
                return lines;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public List<string> ReadFile()
        {
            List<string> result = new List<string>();
            try
            {
                using (StreamReader file = new StreamReader(filename))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        result.Add(line.Trim());  // Trim removes trailing newline characters
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File '{filename}' not found.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading file: {e}");
            }
            return result;
        }

        public void WriteFile(List<string> scoreList)
        {
            try
            {   //writes the high score in the text file
                using (StreamWriter file = new StreamWriter(filename))
                {
                    foreach (var item in scoreList)
                    {
                        file.WriteLine(item);
                    }
                }
                Console.WriteLine($"Sorted array saved to {filename}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error saving to file: {e}");
            }
        }

        public void AddNewScore(string newScore)
        {
            List<string> scoreList = ReadFile();                            //add text file content to the list
            // Add the new string to the array
            scoreList.Add(newScore);                                        

            // Define a custom sorting function based on the points
            int GetPoints(string item) => int.Parse(item.Split().Last());

            // Use the custom sorting function to sort the updated array
            List<string> sortedList = scoreList.OrderByDescending(GetPoints).Take(5).ToList();
            WriteFile(sortedList);
        }
    }

}
