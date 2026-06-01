using Lab7_Rework.Model;
using NUnit.Framework;
using System;

namespace Lab7_Rework.Tests
{
    [TestFixture]
    public class TimeTests
    {
        [Test]
        public void Constructor_ValidValues_SetsProperties()
        {
            var time = new Time(14, 30);
            Assert.Multiple(() =>
            {
                Assert.That(time.Hour, Is.EqualTo(14));
                Assert.That(time.Minute, Is.EqualTo(30));
            });
        }

        [Test]
        public void Constructor_InvalidValues_ThrowsArgumentException()
        {
            // Проверка сеттеров свойств
            var time = new Time(12, 30);
            Assert.That(() => time.Hour = 25, Throws.ArgumentException);
            Assert.That(() => time.Hour = -1, Throws.ArgumentException);
            Assert.That(() => time.Minute = 60, Throws.ArgumentException);
            Assert.That(() => time.Minute = -5, Throws.ArgumentException);
        }

        [Test]
        public void ToString_ReturnsFormattedString()
        {
            var time = new Time(8, 5);
            Assert.That(time.ToString(), Is.EqualTo("08:05"));
        }

        [TestCase("14:30", 14, 30)]
        [TestCase("9:05", 9, 5)]
        public void FromString_ValidInput_ReturnsTimeObject(string input, int expectedHour, int expectedMinute)
        {
            var time = Time.FromString(input);
            Assert.Multiple(() =>
            {
                Assert.That(time.Hour, Is.EqualTo(expectedHour));
                Assert.That(time.Minute, Is.EqualTo(expectedMinute));
            });
        }

        [TestCase("")]
        [TestCase("14:5")]
        [TestCase("25:00")]
        [TestCase("14:60")]
        [TestCase("14-30")]
        public void FromString_InvalidInput_ThrowsArgumentException(string input)
        {
            Assert.That(() => Time.FromString(input), Throws.ArgumentException);
        }
    }
}