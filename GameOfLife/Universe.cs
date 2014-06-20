using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Universe
    {
        private HashSet<Cell> _livingCells = new HashSet<Cell>();

        public List<Cell> GetAllLivingCells()
        {
            return _livingCells.ToList();
        }

        public void Add(Cell cell)
        {
            _livingCells.Add(cell);
        }

        public Cell GetCell(Cell cell)
        {
            return _livingCells.Contains(cell) ? cell : null;
        }

        public IEnumerable<Cell> GetNeighbors(Cell cell)
        {
            var addresses = GetNeighborAddress(cell);
            return addresses.Where(_livingCells.Contains);
        }

        private IEnumerable<Cell> GetNeighborAddress(Cell address)
        {
            for (int row = address.Row - 1; row <= address.Row + 1; row++)
            {
                for (int collum = address.Collumn - 1; collum <= address.Collumn + 1; collum++)
                {
                    if (row != address.Row || collum != address.Collumn)
                    {
                        yield return new Cell(row, collum);
                    }
                }
            }
        }

        public void Tick()
        {
            var dyingCells = _livingCells.Where(IsDyingCell).ToList();
            var neighborDeadCells = _livingCells.SelectMany(GetNeighborAddress).Distinct().Where(x => !_livingCells.Contains(x));
            var becomeAliveCells = neighborDeadCells.Where(IsLivingCell).ToList();
            dyingCells.ForEach((x) => _livingCells.Remove(x));
            becomeAliveCells.ForEach(Add);
        }

        private bool IsDyingCell(Cell address)
        {
            int countNeighbor = GetNeighbors(address).Count();
            return countNeighbor < 2 || countNeighbor > 3;
        }

        private bool IsLivingCell(Cell address)
        {
            return GetNeighbors(address).Count() == 3;
        }
    }
}
