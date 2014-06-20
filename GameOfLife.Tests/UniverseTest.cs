using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Tests
{
    [TestClass]
    public class UniverseTest
    {
        [TestMethod]
        public void UniverseGetAllLivingCells()
        {
            Universe universe = new Universe();
            universe.Add(new Cell(1, 1));
            universe.Add(new Cell(2, 1));
            List<Cell> cells = universe.GetAllLivingCells();
            Assert.IsTrue(cells.Count == 2);
        }

        [TestMethod]
        public void UniverseAddDuplicatedLivingCells()
        {
            Universe universe = new Universe();
            universe.Add(new Cell(1, 1));
            universe.Add(new Cell(1, 1));
            List<Cell> cells = universe.GetAllLivingCells();
            Assert.IsTrue(cells.Count == 1);
        }

        [TestMethod]
        public void GetEmptyCellInUniverse()
        {
            Universe universe = new Universe();
            Cell expected = universe.GetCell(new Cell(1, 2));
            Assert.IsTrue(expected == null);
        }

        [TestMethod]
        public void GetCellInUniverse()
        {
            Universe universe = new Universe();
            Cell cell = new Cell(1, 1);
            universe.Add(cell);
            Cell expected = universe.GetCell(cell);
            Assert.IsTrue(expected.Equals(cell));
        }

        [TestMethod]
        public void TestGetEmptyNeighbor()
        {
            Universe universe = new Universe();
            Cell cell = new Cell(1, 1);
            universe.Add(cell);
            IEnumerable<Cell> expected = universe.GetNeighbors(cell);
            Assert.IsTrue(!expected.Any());
        }

        [TestMethod]
        public void TestGetManyNeiborghs()
        {
            Universe universe = new Universe();
            universe.Add(new Cell(1, 1));
            universe.Add(new Cell(2, 1));
            universe.Add(new Cell(1, 2));
            IEnumerable<Cell> expected = universe.GetNeighbors(new Cell(1, 1));
            Assert.IsTrue(expected.Count() == 2);
        }

        [TestMethod]
        public void TestLivingCellDie()
        {
            Universe universe = new Universe();
            Cell cell = new Cell(1, 1);
            universe.Add(cell);
            universe.Tick();
            Cell expected = universe.GetCell(cell);
            Assert.IsTrue(expected == null);
        }

        [TestMethod]
        public void TestDyingCellBecomeALive()
        {
            Universe universe = new Universe();
            universe.Add(new Cell(1, 1));
            universe.Add(new Cell(2, 1));
            universe.Add(new Cell(1, 2));
            universe.Tick();
            Cell expected = universe.GetCell(new Cell(2, 2));
            Assert.IsTrue(expected != null);
        }
    }
}