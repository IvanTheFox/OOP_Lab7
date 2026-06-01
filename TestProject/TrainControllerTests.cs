using Lab7_Rework.Controller;
using Lab7_Rework.Model;
using NUnit.Framework;
using System;
using System.Linq;

namespace Lab7_Rework.Tests
{
    [TestFixture]
    public class TrainControllerTests
    {
        private TrainController _controller;
        private TrainModel _model;

        [SetUp]
        public void SetUp()
        {
            _controller = TrainController.Instance;
            _model = TrainModel.Instance;

            // Удаляем все тестовые поезда, которые могли остаться от предыдущих тестов
            var all = _model.GetAll().ToList();
            foreach (var t in all)
            {
                if (t.Destination.StartsWith("Тестовый"))
                    _model.RemoveById(t.Id);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Удаляем добавленные во время теста поезда
            var all = _model.GetAll().ToList();
            foreach (var t in all)
            {
                if (t.Destination.StartsWith("Тестовый"))
                    _model.RemoveById(t.Id);
            }
        }

        [Test]
        public void GetAll_ReturnsAllTrainsFromModel()
        {
            var all = _controller.GetAll();
            Assert.That(all, Is.EqualTo(_model.GetAll()));
        }

        [Test]
        public void GetById_ExistingId_ReturnsTrain()
        {
            var first = _model.GetAll().FirstOrDefault();
            if (first == null) Assert.Ignore("Модель пуста");
            var train = _controller.GetById(first.Id);
            Assert.That(train, Is.EqualTo(first));
        }

        [Test]
        public void Add_ValidData_AddsTrain()
        {
            int initialCount = _controller.GetAll().Count();
            _controller.Add("TEST123", "Тестовый город", new Time(12, 0), Train.TrainType.Express, 200, 100);
            Assert.That(_controller.GetAll().Count(), Is.EqualTo(initialCount + 1));
        }

        [Test]
        public void Add_InvalidSeatsAvailable_ThrowsArgumentException()
        {
            Assert.That(() => _controller.Add("T", "D", new Time(12, 0), Train.TrainType.Passanger, 100, 150),
                Throws.ArgumentException);
        }

        [Test]
        public void Delete_ExistingId_RemovesTrain()
        {
            // Добавим тестовый поезд
            var testTrain = new Train(0, "DEL", "Тестовый удаляемый", new Time(1, 1), Train.TrainType.Passanger, 10, 5);
            _model.AddTrain(testTrain);
            int id = testTrain.Id;

            _controller.Delete(id);
            Assert.That(_controller.GetById(id), Is.Null);
        }

        [Test]
        public void Delete_NonExistingId_DoesNothing()
        {
            var allBefore = _controller.GetAll().ToList();
            _controller.Delete(999999);
            var allAfter = _controller.GetAll().ToList();
            Assert.That(allAfter.Count, Is.EqualTo(allBefore.Count));
        }

        [Test]
        public void Search_ReturnsMatchingTrains()
        {
            var results = _controller.Search("Москва");
            Assert.That(results.All(t => t.Destination.Contains("Москва")), Is.True);
        }
    }
}