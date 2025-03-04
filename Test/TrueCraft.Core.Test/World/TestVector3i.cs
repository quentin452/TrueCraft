using System;
using TrueCraft.Core.World;
using NUnit.Framework;

namespace TrueCraft.Core.Test.World
{
    [TestFixture]
    public class TestVector3i
    {
        [TestCase(1, 2, 3)]
        public void Vector3i_Constructor(int x, int y, int z)
        {
            Vector3i actual = new Vector3i(x, y, z);

            Assert.AreEqual(x, actual.X);
            Assert.AreEqual(y, actual.Y);
            Assert.AreEqual(z, actual.Z);
        }

        [Test]
        public void Vector3i_Equality()
        {
            Vector3i a, b;

            a = new Vector3i(1, 2, 3);
            b = new Vector3i(3, 2, 1);

            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);

            b = new Vector3i(1, 2, 3);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [TestCase(2, 4, 6, 1, 0, 0, 3, 4, 6)]
        [TestCase(2, 4, 6, 0, 1, 0, 2, 5, 6)]
        [TestCase(2, 4, 6, 0, 0, 1, 2, 4, 7)]
        public void Vector3i_Add(int x1, int y1, int z1, int x2, int y2, int z2,
                  int expectedX, int expectedY, int expectedZ)
        {
            Vector3i a = new Vector3i(x1, y1, z1);
            Vector3i b = new Vector3i(x2, y2, z2);
            Vector3i expected = new Vector3i(expectedX, expectedY, expectedZ);

            Vector3i actual = a + b;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Vector3i_ToString()
        {
            Vector3i a = new Vector3i(2, 3, 5);

            string actual = a.ToString();

            Assert.AreEqual("<2,3,5>", actual);
        }

        [TestCase(1, 2, 3)]
        [TestCase(-3, -2, -1)]
        public void Vector3i_NegationOperator(int x, int y, int z)
        {
            Vector3i t = new Vector3i(x, y, z);
            Vector3i expected = new Vector3i(-x, -y, -z);

            Vector3i actual = -t;

            Assert.AreEqual(expected.X, actual.X);
            Assert.AreEqual(expected.Y, actual.Y);
            Assert.AreEqual(expected.Z, actual.Z);
        }

        [TestCase(1, 2, 3, 5)]
        public void Vector3i_MultiplyOperator(int x, int y, int z, int m)
        {
            Vector3i v = new Vector3i(x, y, z);

            Vector3i actual = m * v;

            Assert.AreEqual(actual.X, m * x);
            Assert.AreEqual(actual.Y, m * y);
            Assert.AreEqual(actual.Z, m * z);

            actual = v * m;

            Assert.AreEqual(actual.X, m * x);
            Assert.AreEqual(actual.Y, m * y);
            Assert.AreEqual(actual.Z, m * z);
        }
    }
}
