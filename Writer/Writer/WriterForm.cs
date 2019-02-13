using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: remove 'magic numbers' in squares. Set constant square side length
// TODO: Make old entries readonly
// TODO: Stop saving whatever entry is currently displayed as the day's current entry...

namespace Writer
{
    public partial class WriterForm : Form
    {
        private List<EntrySquare> entrySquares;

        private const int WordCount = 750;
        private const int MonthsInPanel = 5; // the number of months to display in the entry record

        private int numWords;
        private bool reachedWordCount;
        private string fileName;

        public WriterForm()
        {
            numWords = 0;
            reachedWordCount = false;

            entrySquares = new List<EntrySquare>();

            InitializeComponent();

            // Create a directory to hold the entries if none exists
            Directory.CreateDirectory("Entries");
            // Get the date
            DateTime today = DateTime.Now.Date;
            // Convert the date to the prospective filename
            fileName = today.ToString("d-M-yyyy", CultureInfo.InvariantCulture);
            
            DisplayFile(fileName, true);
            
        }

        private bool GetIsTodaysEntry(string fileName)
        {
            return fileName.Equals(DateTime.Today.ToString("d-M-yyyy", CultureInfo.InvariantCulture));
        }

        private void DisplayFile(string fileName, bool isTodaysEntry)
        {
            //Open or create the file
            FileStream inputStream = new FileStream($@"Entries\{fileName}.txt", FileMode.OpenOrCreate);
            // Read whatever is written in the file
            using (StreamReader reader = new StreamReader(inputStream))
            {
                string fileText = reader.ReadToEnd();
                // Print whatever is written to the textbox
                writingRichTextBox.Text = fileText;
                // Remove the highlight from the added text
                writingRichTextBox.SelectionStart = writingRichTextBox.TextLength;
                writingRichTextBox.SelectionLength = 0;
                // Paint the progress circle
                numWords = writingRichTextBox.Text.Split(' ').Count();
                progressPanel.Refresh();
            }
            // Set the word count checker
            reachedWordCount = writingRichTextBox.Text.Split(' ').Count() >= WordCount;

            writingRichTextBox.ReadOnly = !isTodaysEntry;
        }

        private void SaveFile()
        {
            using (StreamWriter writer = new StreamWriter(
                new FileStream($@"Entries\{fileName}.txt", FileMode.Truncate)))
            {
                string text = writingRichTextBox.Text;
                writer.Write(text);
                writer.Flush();
            }
        }

        private void writingTextBox_TextChanged(object sender, EventArgs e)
        {
            notificationLabel.Text = "";
            numWords = writingRichTextBox.Text.Split(' ').Count();
            if (!reachedWordCount)
            {
                progressPanel.Refresh();
            }
        }

        private void progressPanel_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("Updating PP");

            using (Graphics g = progressPanel.CreateGraphics())
            {
                float wordCount = (float)WordCount;
                float degrees = 360;
                int pieProgress = (int)(numWords / (wordCount / degrees));
                
                int green = numWords <= WordCount ? numWords / 3 : 255;

                Color progressColor = Color.FromArgb(128, 0, green, 0);
                g.FillPie(new SolidBrush(progressColor), 0, 0, progressPanel.Width - 1, 
                    progressPanel.Height - 1, -90, pieProgress);
                if (numWords >= WordCount)
                {
                    g.DrawImage(Properties.Resources.tick, 12, 12);
                    reachedWordCount = true;
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFile();
            notificationLabel.Text = "Entry saved";
        }

        private void monthPanel_Paint(object sender, PaintEventArgs e)
        {
            // Holds the list of filenames as they are generated.
            // Used to initialise the values in entry squares
            // Kind of awkward...
            List<string> fileNames = new List<string>();

            // Get the length of the past X months
            // TODO: Make the ordering a bit more logical?
            int numMonthsToInclude = MonthsInPanel;
            int[] monthArray = new int[numMonthsToInclude];
            for (int i = monthArray.Length - 1; i >= 0; i--)
            {
                int month = DateTime.Now.Month - i;
                int year = DateTime.Now.Year;

                if (month <= 0) // go back a year
                {
                    year--;
                    month += 12;
                }

                monthArray[(numMonthsToInclude - i) - 1] = DateTime.DaysInMonth(year, month);
            }

            // Build a jagged array showing whether or not entries have been written for each day
            int[][] entryStatusArray = new int[numMonthsToInclude][];

            // Build a DateTime object to keep track of the days
            int tMonth = DateTime.Now.Month - (numMonthsToInclude - 1);
            int tYear = DateTime.Now.Year;
            if (tMonth <= 0) // go back a year
            {
                tYear--;
                tMonth += 12;
            }
            DateTime tracker = new DateTime(tYear, tMonth, 1);

            for (int i = 0; i < entryStatusArray.Length; i++)
            {
                entryStatusArray[i] = new int[monthArray[i]];
                // Initialise the values for each month
                // 1 = Some entry exists
                // 2 = Entry >= 250 words (implementation tbd)
                // 3 = Entry >= 750 words (implementation tbd)
                for (int j = 0; j < entryStatusArray[i].Length; j++)
                {
                    string fName = tracker.ToString("d-M-yyyy", CultureInfo.InvariantCulture);

                    fileNames.Add(fName);

                    Console.WriteLine(fName);
                    if (File.Exists($@"Entries\{fName}.txt"))
                    {
                        entryStatusArray[i][j] = 1;
                        using (StreamReader reader = new StreamReader($@"Entries\{fName}.txt"))
                        {
                            int words = reader.ReadToEnd().Split(' ').Length;
                            if (words >= 750)
                            {
                                entryStatusArray[i][j] = 3;
                            }
                            else if (words >= 250)
                            {
                                entryStatusArray[i][j] = 2;
                            }
                        }
                    }
                    tracker = tracker.AddDays(1);
                    
                }
            }


            // Draw gray squares corresponding to the number of days in each of the past four months
            using (Graphics g = monthPanel.CreateGraphics())
            {
                Brush noEntryBrush = new SolidBrush(Color.FromArgb(231, 231, 231));
                Brush someEntryBrush = new SolidBrush(Color.FromArgb(200, 234, 190));
                Brush substanialEntryBrush = new SolidBrush(Color.FromArgb(140, 196, 123));
                Brush fullEntryBrush = new SolidBrush(Color.FromArgb(79, 132, 63));
                Brush brush;

                // Iterate through the filenames stored above. Pretty awkward...
                int fileNameCounter = -1;

                for (int y = 0; y < monthArray.Length; y++)
                {
                    for (int x = 0; x < monthArray[y]; x++)
                    {
                        if (entryStatusArray[y][x] == 1)
                        {
                            brush = someEntryBrush;
                        }
                        else if (entryStatusArray[y][x] == 2)
                        {
                            brush = substanialEntryBrush;
                        }
                        else if (entryStatusArray[y][x] == 3)
                        {
                            brush = fullEntryBrush;
                        }
                        else
                        {
                            brush = noEntryBrush;
                        }
                        g.FillRectangle(brush, x * 20, y * 20, 15, 15);
                        // Add square representation to list of squares
                        entrySquares.Add(new EntrySquare(x * 20, y * 20, 
                            entryStatusArray[y][x] != 0, 
                            fileNames[++fileNameCounter]));
                    }
                }
            }
        }

        // When the user clicks an old entry in the month panel, load that entry
        private void monthPanel_MouseClick(object sender, MouseEventArgs e)
        {
            bool clickedOnSquare = false;
            bool entExists = false;
            string entFileName = "";

            if (e.Button == MouseButtons.Left)
            {
                foreach (EntrySquare entrySquare in entrySquares)
                {
                    if (e.X >= entrySquare.XPos && e.X <= entrySquare.XPos + 14
                        && e.Y >= entrySquare.YPos && e.Y <= entrySquare.YPos + 14)
                    {
                        clickedOnSquare = true;
                        if (entrySquare.EntryExists)
                        {
                            entExists = true;
                            entFileName = entrySquare.FileName;
                        }
                        break;
                    }
                }

                if (clickedOnSquare && entExists)
                {
                    if (fileName.Equals(DateTime.Today.ToString("d-M-yyyy", CultureInfo.InvariantCulture)))
                    {
                        SaveFile();
                    }
                    fileName = entFileName;
                    DisplayFile(fileName, GetIsTodaysEntry(fileName));
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            // Save the current entry before loading a new one
            SaveFile();

            // Bring up the open file dialog. Show only text files.
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Text Files|*.txt"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // We only want the name part of the file, not the directory or extension
                string fileToLoad = Path.GetFileNameWithoutExtension(ofd.FileName);
                DisplayFile(fileToLoad, GetIsTodaysEntry(fileToLoad));
            } 
        }

        private void writingRichTextBox_TextChanged(object sender, EventArgs e)
        {
            notificationLabel.Text = "";
            numWords = writingRichTextBox.Text.Split(' ').Count();
            if (!reachedWordCount)
            {
                progressPanel.Refresh();
            }
        }

        // Save the current entry before closing
        private void WriterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFile();
        }
        
    }
}
