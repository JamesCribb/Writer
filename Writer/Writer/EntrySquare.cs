// An entry to be represented as a square in the monthPanel

namespace Writer
{
    class EntrySquare
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public bool EntryExists { get; set; }
        public string FileName { get; set; }    // what the filename would be if the entry exists

        public EntrySquare(int xPos, int yPos, bool exists, string fileName)
        {
            XPos = xPos;
            YPos = yPos;
            EntryExists = exists;
            FileName = fileName;
        }
    }
}
