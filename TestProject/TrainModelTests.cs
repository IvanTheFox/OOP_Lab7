using Lab7_Rework.Model;
using NUnit.Framework;
using System.Linq;

namespace Lab7_Rework.Tests
{
    [TestFixture]
    public class TrainModelTests
    {
        private TrainModel _model;
        private Train _testTrain;

        [SetUp]
        public void SetUp()
        {
            _model = new TrainModel();
            _testTrain = new Train(0, "T001", "Тестовый город", new Time(10, 0), Train.TrainType.Passanger, 200, 100);
        }

        [Test]
        public void AddTrain_AssignsUniqueIdAndRaisesEvent()
        {
            bool eventRaised = false;
            _model.TrainAdded += (t) => eventRaised = true;

            int id = _model.AddTrain(_testTrain);

            Assert.Multiple(() =>
            {
                Assert.That(id, Is.EqualTo(0)); // первый добавленный получает Id = 0
                Assert.That(_testTrain.Id, Is.EqualTo(0));
                Assert.That(_model.GetAll().Count(), Is.EqualTo(1));
                Assert.That(eventRaised, Is.True);
            });
        }

        [Test]
        public void AddTrain_IncrementsIdSequentially()
        {
            var t1 = new Train(0, "A", "City1", new Time(1, 0), Train.TrainType.Passanger, 100, 50);
            var t2 = new Train(0, "B", "City2", new Time(2, 0), Train.TrainType.Passanger, 100, 50);
            _model.AddTrain(t1);
            _model.AddTrain(t2);
            Assert.Multiple(() =>
            {
                Assert.That(t1.Id, Is.EqualTo(0));
                Assert.That(t2.Id, Is.EqualTo(1));
            });
        }

        [Test]
        public void GetAll_ReturnsReadOnlyCopy()
        {
            _model.AddTrain(_testTrain);
            var all = _model.GetAll();
            Assert.That(all, Is.Not.SameAs(_model.GetAll())); // новая коллекция
            Assert.That(all.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetById_ReturnsCorrectTrain()
        {
            _model.AddTrain(_testTrain);
            var found = _model.GetById(_testTrain.Id);
            Assert.That(found, Is.SameAs(_testTrain));
        }

        [Test]
        public void GetById_NotFound_ReturnsNull()
        {
            var found = _model.GetById(999);
            Assert.That(found, Is.Null);
        }

        [Test]
        public void RemoveById_ExistingId_RemovesAndRaisesEvent()
        {
            _model.AddTrain(_testTrain);
            bool eventRaised = false;
            _model.TrainRemoved += (t) => eventRaised = true;

            bool removed = _model.RemoveById(_testTrain.Id);

            Assert.Multiple(() =>
            {
                Assert.That(removed, Is.True);
                Assert.That(_model.GetAll().Any(t => t.Id == _testTrain.Id), Is.False);
                Assert.That(eventRaised, Is.True);
            });
        }

        [Test]
        public void RemoveById_NonExistingId_ReturnsFalse()
        {
            bool removed = _model.RemoveById(999);
            Assert.That(removed, Is.False);
        }

        [Test]
        public void ModifyTrain_UpdatesFieldsAndRaisesEvent()
        {
            _model.AddTrain(_testTrain);
            var modifiedTrain = new Train(_testTrain.Id, "NEW", "Новый город", new Time(23, 59), Train.TrainType.Express, 500, 250);
            bool eventRaised = false;
            _model.TrainModified += (t) => eventRaised = true;

            bool result = _model.ModifyTrain(modifiedTrain);

            var updated = _model.GetById(_testTrain.Id);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(updated.Number, Is.EqualTo("NEW"));
                Assert.That(updated.Destination, Is.EqualTo("Новый город"));
                Assert.That(eventRaised, Is.True);
            });
        }

        [Test]
        public void ModifyTrain_NonExistingId_ReturnsFalse()
        {
            var fake = new Train(999, "X", "X", new Time(0, 0), Train.TrainType.Passanger, 10, 5);
            bool result = _model.ModifyTrain(fake);
            Assert.That(result, Is.False);
        }

        [Test]
        public void Search_EmptyQuery_ReturnsAll()
        {
            _model.AddTrain(_testTrain);
            var results = _model.Search("");
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [TestCase("T001")]
        [TestCase("тестовый город")]
        [TestCase("пассажирский")]
        [TestCase("0")]
        public void Search_ValidQuery_ReturnsMatches(string query)
        {
            _model.AddTrain(_testTrain);
            var results = _model.Search(query);
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Search_NoMatches_ReturnsEmpty()
        {
            _model.AddTrain(_testTrain);
            var results = _model.Search("несуществующее");
            Assert.That(results, Is.Empty);
        }
    }
}