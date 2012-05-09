using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FunctionalDotNet;

namespace tests
{
    /// <summary>
    /// The tests in this file might not be too rigorous, since after all the functions they are testing are 
    /// just pass-throught wrappers to existing extension methods on IEnumerable...
    /// </summary>
    [TestClass]
    public class TestAliases
    {
        [TestMethod]
        public void TestMap()
        {
            Func<int, int> square = x => x*x;
            var nums = new[] {1, 2, 3, 6, 5, 4, 12};
            var res = nums.Map(square);

            Assert.IsTrue(res.SequenceEqual(new[] {1, 4, 9, 36, 25, 16, 144}));
        }

        [TestMethod]
        public void TestMapi()
        {
            Func<double, int, double> firstThreeZeroRestHalved = (d, i) => i < 3 ? 0 : d/2d;
            var nums = new[] { 1.0, 2, 3, 6, 5, 4, 12 };
            var res = nums.Mapi(firstThreeZeroRestHalved);

            Assert.IsTrue(res.SequenceEqual(new[] { 0.0, 0, 0, 3, 2.5, 2, 6 }));
        }

        [TestMethod]
        public void TestMap2()
        {
            Func<int, int, int> mult = (x, y) => x * y;
            var first = new[] { 1, 2, 3, 6, 5, 4, 12 };
            var second = new[] { 2, 6, 3, 4, 5, 6, 2 };
            var res = first.Map2(second, mult);

            Assert.IsTrue(res.SequenceEqual(new[] {2, 12, 9, 24, 25, 24, 24}));
        }
    }
}
