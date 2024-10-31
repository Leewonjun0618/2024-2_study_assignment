using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;
            // ---------- TODO ----------
            for (int i = 2; i <5; i++)
            {
                double sum = 0;
                for (int j = 1; j < 6; j++) 
                {
                    sum += double.Parse(data[j, i]);
                }
                double ave = sum / stdCount;
                Console.WriteLine($"{data[0, i]} Average = {ave}");
            }
            
            for (int i=2;i<5;i++)
            {
                double maxscore = 0;
                double minscore = 100;
                for (int j=1;j<6;j++)
                {
                    if (double.Parse(data[j,i]) > maxscore){maxscore = double.Parse(data[j,i]);}
                    if (double.Parse(data[j,i]) < minscore){minscore = double.Parse(data[j,i]);}
                }
                Console.WriteLine($"{data[0, i]} Maximum = {maxscore}\n{data[0, i]} Minimum = {minscore}");
            }
            
            var studentscores = new (string Name, double Totalscore)[stdCount];
            for (int j=1;j<6;j++)
            {
                double total = 0;
                for (int i=2;i<5;i++) 
                {
                    total += double.Parse(data[j, i]);
                }
                studentscores[j - 1] = (data[j, 1], total);
            }
            var scoreorder = studentscores.OrderByDescending(s => s.Totalscore).ToArray();
            for (int i=0;i<scoreorder.Length;i++)   
            {
                Console.WriteLine($"{scoreorder[i].Name}: {i+1}th");
            }
            // --------------------
        }
    }
}

/* example output

Average Scores: 
Math: 84.40
Science: 86.80
English: 86.20

Max and min Scores: 
Math: (94, 72)
Science: (95, 76)
English: (92, 78)

Students rank by total scores:
Alice: 4th
Bob: 1st
Charlie: 5th
David: 2nd
Eve: 3rd

*/
