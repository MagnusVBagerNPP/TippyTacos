namespace ImageEditor
{
    class Cell
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public char IsColored { get; set; }

        public Cell(int x, int y)
        {
            RowNumber = x;
            ColumnNumber = y;
        }
    }
}
