using System.Linq;
using NUnit.Framework;
using SkbLib;

namespace Tests
{
    [TestFixture]
    public class SearchEngineTests
    {
        private SearchEngine _searchEngine;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _searchEngine = SearchEngine.GenerateFromDictionary(TestDictionary.Dictionary);
        }

        [Test]
        public void Test1()
        {
            var correctAnswer = new[]
            {
                "wasadsfasdf",
                "wasasdf",
                "wascvbnrfgh",
                "wasder",
                "wasdsfdf",
                "wasfer",
                "wasfgdsf",
                "wasfsdfg",
                "wasgft",
                "washglui"
            };

            var answer = _searchEngine.PrefixSearch("was");

            Assert.IsTrue(correctAnswer.SequenceEqual(answer));
        }

        [Test]
        public void Test2()
        {
            var correctAnswer = new[]
            {
                "retvcnd",
                "retdjjtuju",
                "retgj",
                "retth",
                "retdfhs",
                "retxcvn",
                "retsduj",
                "retbz",
                "retjhgfj",
                "retqwer"
            };

            var answer = _searchEngine.PrefixSearch("ret");

            Assert.IsTrue(correctAnswer.SequenceEqual(answer));
        }

        [Test]
        public void Test3()
        {
            var correctAnswer = new[]
            {
                "kanojo",
                "kare",
                "korosu",
                "karetachi"
            };

            var answer = _searchEngine.PrefixSearch("k");

            Assert.IsTrue(correctAnswer.SequenceEqual(answer));
        }

        [Test]
        public void Test4()
        {
            var correctAnswer = new[]
            {
                "kanojo",
                "kare",
                "karetachi"
            };

            var answer = _searchEngine.PrefixSearch("ka");

            Assert.IsTrue(correctAnswer.SequenceEqual(answer));
        }

        [Test]
        public void Test5()
        {
            var correctAnswer = new[]
            {
                "kare",
                "karetachi"
            };

            var answer = _searchEngine.PrefixSearch("kar");

            Assert.IsTrue(correctAnswer.SequenceEqual(answer));
        }

        [Test]
        public void Test6()
        {
            var correctAnswer = new[]
            {
                "sakurb",
                "sakurc",
                "sakura"
            };

            var answer = _searchEngine.PrefixSearch("sak");

            Assert.IsTrue(correctAnswer.SequenceEqual(answer));
        }
    }
}