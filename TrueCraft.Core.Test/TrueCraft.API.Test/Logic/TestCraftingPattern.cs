using System;
using Moq;
using TrueCraft.API.Logic;
using TrueCraft.API.Windows;
using NUnit.Framework;

namespace TrueCraft.API.Test.Logic
{
    [TestFixture]
    public class TestCraftingPattern
    {
        private static Mock<ICraftingArea> GetCraftingArea(short[] grid)
        {
            if (grid.Length != 9 && grid.Length != 4)
                throw new ArgumentException(nameof(grid));

            int sz = (grid.Length == 9 ? 3 : 2);

            Mock<ICraftingArea> area = new Mock<ICraftingArea>(MockBehavior.Strict);
            area.Setup(a => a.Width).Returns(sz);
            area.Setup(a => a.Height).Returns(sz);
            area.Setup(a => a.GetItemStack(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((x, y) => grid[y * sz + x] != 0 ? new ItemStack(grid[y * sz + x]) : ItemStack.EmptyStack);

            return area;
        }

        [Test]
        public void ctor_ItemStacks_Empty_Grid_Gets_Null()
        {
            // test 2x2 case
            ItemStack[,] items = new[,]
            {
                { ItemStack.EmptyStack, ItemStack.EmptyStack },
                { ItemStack.EmptyStack, ItemStack.EmptyStack }
            };

            CraftingPattern actual = CraftingPattern.GetCraftingPattern(items);
            Assert.Null(actual);

            // 3x3 case
            items = new[,]
            {
                { ItemStack.EmptyStack, ItemStack.EmptyStack, ItemStack.EmptyStack },
                { ItemStack.EmptyStack, ItemStack.EmptyStack, ItemStack.EmptyStack },
                { ItemStack.EmptyStack, ItemStack.EmptyStack, ItemStack.EmptyStack }
            };

            actual = CraftingPattern.GetCraftingPattern(items);
            Assert.Null(actual);

        }

        [Test]
        public void ctor_Area_Empty_Grid_Gets_Null()
        {
            Mock<ICraftingArea> grid1 = GetCraftingArea(new short[] { 0, 0, 0, 0 });
            Mock<ICraftingArea> grid2 = GetCraftingArea(new short[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            CraftingPattern actual = CraftingPattern.GetCraftingPattern(grid1.Object);
            Assert.Null(actual);

            actual = CraftingPattern.GetCraftingPattern(grid2.Object);
            Assert.Null(actual);
        }

        [TestCase(3, new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 })]
        [TestCase(2, new short[] { 5, 5, 5, 5 })]
        [TestCase(2, new short[] { 0, 0, 0, 0, 1, 1, 0, 1, 2 })]
        [TestCase(1, new short[] { 0, 2, 0, 0, 1, 0, 0, 1, 0 })]
        [TestCase(1, new short[] { 0, 0, 0, 0, 3, 0, 0, 0, 0 })]
        public void Width(int expectedWidth, short[] grid)
        {
            Mock<ICraftingArea> area = GetCraftingArea(grid);

            CraftingPattern actual = CraftingPattern.GetCraftingPattern(area.Object);

            Assert.AreEqual(expectedWidth, actual.Width);
        }

        [TestCase(3, new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 })]
        [TestCase(2, new short[] { 5, 5, 5, 5 })]
        [TestCase(2, new short[] { 0, 0, 0, 0, 1, 1, 0, 1, 2 })]
        [TestCase(1, new short[] { 0, 0, 0, 2, 1, 1, 0, 0, 0 })]
        [TestCase(1, new short[] { 0, 0, 0, 0, 3, 0, 0, 0, 0 })]
        public void Height(int expectedHeight, short[] grid)
        {
            Mock<ICraftingArea> area = GetCraftingArea(grid);

            CraftingPattern actual = CraftingPattern.GetCraftingPattern(area.Object);

            Assert.AreEqual(expectedHeight, actual.Height);
        }

        [TestCase(true, new short[] { 1, 1, 1, 0, 2, 0, 0, 2, 0 }, new short[] { 1, 1, 1, 0, 2, 0, 0, 2, 0 })]
        [TestCase(true, new short[] { 1, 0, 0, 0, 0, 0, 0, 0, 0 }, new short[] { 0, 1, 0, 0 })]
        [TestCase(true, new short[] { 0, 0, 0, 0, 1, 1, 0, 1, 2 }, new short[] { 1, 1, 1, 2 })]
        [TestCase(false, new short[] { 1, 1, 1, 0, 2, 0, 0, 2, 0 }, new short[] { 1, 2, 1, 0, 2, 0, 0, 2, 0 })]
        [TestCase(false, new short[] { 1, 0, 0, 0, 0, 0, 0, 0, 0}, new short[] { 0, 0, 1, 1 })]
        [TestCase(false, new short[] { 1, 0, 0, 0, 0, 0, 0, 0, 0 }, new short[] { 2, 0, 0, 0, 0, 0, 0, 0, 0 })]
        public void Test_Equality(bool expected, short[] grid1, short[] grid2)
        {
            Mock<ICraftingArea> area1 = GetCraftingArea(grid1);
            Mock<ICraftingArea> area2 = GetCraftingArea(grid2);

            CraftingPattern a = CraftingPattern.GetCraftingPattern(area1.Object);
            CraftingPattern b = CraftingPattern.GetCraftingPattern(area2.Object);

            Assert.False(object.ReferenceEquals(a, b));
            Assert.AreEqual(expected, a.Equals(b));
            Assert.AreEqual(expected, b.Equals(a));

            Assert.False(a.Equals(null));
            Assert.False(a.Equals("wrong type of object"));

            Assert.AreEqual(expected, a == b);
            Assert.AreEqual(expected, b == a);

            Assert.AreNotEqual(expected, a != b);
            Assert.AreNotEqual(expected, b != a);

            Assert.False(a == null);
            Assert.False(null == a);
        }

    }
}
