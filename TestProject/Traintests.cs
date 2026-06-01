using Lab7_Rework.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Lab7_Rework.Tests
{
    [TestFixture]
    public class TrainTests
    {
        private Time _testTime;

        [SetUp]
        public void SetUp()
        {
            _testTime = new Time(12, 0);
        }

        [Test]
        public void Constructor_ValidData_InitializesProperties()
        {
            var train = new Train(1, "123A", "Москва", _testTime, Train.TrainType.Express, 100, 50);
            Assert.Multiple(() =>
            {
                Assert.That(train.Id, Is.EqualTo(1));
                Assert.That(train.Number, Is.EqualTo("123A"));
                Assert.That(train.Destination, Is.EqualTo("Москва"));
                Assert.That(train.Departure, Is.SameAs(_testTime));
                Assert.That(train.Type, Is.EqualTo(Train.TrainType.Express));
                Assert.That(train.TotalSeats, Is.EqualTo(100));
                Assert.That(train.SeatsAvailable, Is.EqualTo(50));
            });
        }

        [Test]
        public void Number_NullOrWhitespace_ThrowsArgumentException()
        {
            var train = new Train(1, "valid", "Москва", _testTime, Train.TrainType.Express, 100, 50);
            Assert.That(() => train.Number = "", Throws.ArgumentException);
            Assert.That(() => train.Number = "   ", Throws.ArgumentException);
        }

        [Test]
        public void TotalSeats_NegativeValue_ThrowsArgumentException()
        {
            var train = new Train(1, "123A", "Москва", _testTime, Train.TrainType.Express, 100, 50);
            Assert.That(() => train.TotalSeats = -5, Throws.ArgumentException);
        }

        [Test]
        public void SeatsAvailable_NegativeOrGreaterThanTotal_ThrowsArgumentException()
        {
            var train = new Train(1, "123A", "Москва", _testTime, Train.TrainType.Express, 100, 50);
            Assert.That(() => train.SeatsAvailable = -10, Throws.ArgumentException);
            Assert.That(() => train.SeatsAvailable = 150, Throws.ArgumentException);
        }

        [Test]
        public void RandomTrain_ReturnsNonNullTrainWithValidData()
        {
            var train = Train.RandomTrain(42);
            Assert.Multiple(() =>
            {
                Assert.That(train.Id, Is.EqualTo(42));
                Assert.That(train.Number, Is.Not.Empty);
                Assert.That(Train.s_CityBank, Contains.Item(train.Destination));
                Assert.That(train.Departure, Is.Not.Null);
                Assert.That(Enum.IsDefined(typeof(Train.TrainType), train.Type));
                Assert.That(train.TotalSeats, Is.InRange(500, 1499));
                Assert.That(train.SeatsAvailable, Is.InRange(0, train.TotalSeats));
            });
        }

        [Test]
        public void GetTrainTypeName_ReturnsCorrectName()
        {
            Assert.That(Train.GetTrainTypeName(Train.TrainType.Passanger), Is.EqualTo("Пассажирский"));
            Assert.That(Train.GetTrainTypeName(Train.TrainType.HighSpeed), Is.EqualTo("Скоростной"));
        }

        [Test]
        public void GetTrainTypeEnum_ValidName_ReturnsEnum()
        {
            var type = Train.GetTrainTypeEnum("Экспресс");
            Assert.That(type, Is.EqualTo(Train.TrainType.Express));
        }

        [Test]
        public void GetTrainTypeEnum_InvalidName_ReturnsNull()
        {
            var type = Train.GetTrainTypeEnum("Несуществующий");
            Assert.That(type, Is.Null);
        }

        [Test]
        public void ToString_ReturnsExpectedFormat()
        {
            var train = new Train(5, "999K", "Казань", new Time(23, 45), Train.TrainType.Freight, 1200, 800);
            Assert.That(train.ToString(), Does.Contain("#5").And.Contains("999K").And.Contains("Казань").And.Contains("Грузовой"));
        }
    }
}