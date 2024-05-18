using NUnit.Framework;
using ConsoleApp2;
using NUnit.Framework.Legacy;

namespace ConsoleApp2Tests
{
    [TestFixture]
    public class PolishNotationTests
    {
        [Test]
        public void Simple()
        {
            string input = "3 + 4";
            string expected = "3 4 +";
            string actual = Program.ConvertToPolishNotation(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Multiplication()
        {
            string input = "3 + 4 * 2";
            string expected = "3 4 2 * +";
            string actual = Program.ConvertToPolishNotation(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Parentheses()
        {
            string input = "3 + 4 * (2 - 1)";
            string expected = "3 4 2 1 - * +";
            string actual = Program.ConvertToPolishNotation(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NestedParentheses()
        {
            string input = "3 * (4 + (2 - 1))";
            string expected = "3 4 2 1 - + *";
            string actual = Program.ConvertToPolishNotation(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void InvalidParentheses()
        {
            string input = "3 + (4 * 2";
            Assert.Throws<ArgumentException>(() => Program.ConvertToPolishNotation(input));
        }

        [Test]
        public void InvalidCharacters()
        {
            string input = "3 + 4 * 2a";
            Assert.Throws<ArgumentException>(() => Program.ConvertToPolishNotation(input));
        }

        [Test]
        public void EmptyString()
        {
            string input = "";
            Assert.Throws<ArgumentException>(() => Program.ConvertToPolishNotation(input));
        }

        [Test]
        public void SingleNumber()
        {
            string input = "42";
            string expected = "42";
            string actual = Program.ConvertToPolishNotation(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Complex()
        {
            string input = "3 + 4 * 2 / (1 - 5) + 6";
            string expected = "3 4 2 * 1 5 - / + 6 +";
            string actual = Program.ConvertToPolishNotation(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void AllOperators()
        {
            string input = "3 - 4 + 5 * 6 / 7";
            string expected = "3 4 - 5 6 * 7 / +";
            string actual = Program.ConvertToPolishNotation(input);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
