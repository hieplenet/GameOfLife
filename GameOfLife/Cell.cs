namespace GameOfLife
{
    public class Cell
    {
        public Cell(int row, int col)
        {
            Row = row;
            Collumn = col;
        }

        public int Row { get; set; }

        public int Collumn { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Cell)
            {
                return Row == (obj as Cell).Row && Collumn == (obj as Cell).Collumn;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Row + "," + Collumn;
        }
    }
}
