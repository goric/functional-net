using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunctionalDotNet;

namespace tests
{
    [TestClass]
    public class TestFSharpReImplementations
    {
        [TestMethod]
        public void TestForall()
        {
            var shortStrings = new[] {"these", "are", "all", "less", "than", "eight", "chars"};
            var res = shortStrings.Forall(p => p.Length < 8);
            Assert.IsTrue(res);

            var res2 = shortStrings.Forall(p => p.Contains("r"));
            Assert.IsFalse(res2);
        }

        [TestMethod]
        public void TestForall2()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestExists()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestExists2()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestIter()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestIter2()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestNth()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestTruncate()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestPairwise()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestZip()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestZip3()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestSingleton()
        {
            var def = default(DateTime);
            var singleton = FSharpReImplementations.Singleton<DateTime>();
            Assert.AreEqual(1, singleton.Count());
            Assert.AreEqual(def, singleton.First());

            var def2 = default(List<int>);
            var singleton2 = FSharpReImplementations.Singleton<List<int>>();
            Assert.AreEqual(1, singleton2.Count());
            Assert.AreEqual(def2, singleton2.First());
        }

        [TestMethod]
        public void TestUnfold()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestInitializeInfinite()
        {
            Assert.Fail("not implemented");
        }

        [TestMethod]
        public void TestCollect()
        {
            Assert.Fail("not implemented");
        }
    }
}
