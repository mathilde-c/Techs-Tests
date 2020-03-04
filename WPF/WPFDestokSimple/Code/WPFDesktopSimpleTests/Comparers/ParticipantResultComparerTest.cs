using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopSimple.Comparers;
using WPFDesktopSimple.Models;
using ThirdPartySimulator;

namespace WPFDesktopSimpleTests.Comparers
{
    [TestClass()]
    public class ParticipantResultComparerTest
    {
        [TestMethod()]
        public void CompareFinalistOnTime_WithSmallerTimeBeingSmallest()
        {
            ParticipantResultComparer comparer = new ParticipantResultComparer();

            Participant p1 = new Participant() { Id = 0, PersonInfo = new Person() { Country = "C", Id = 0, Name = "p1" } };
            ParticipantResult pr1 = new ParticipantResult(p1, EndingEventType.None, 1.7);
            Participant p2 = new Participant() { Id = 1, PersonInfo = new Person() { Country = "C", Id = 1, Name = "p2" } };
            ParticipantResult pr2 = new ParticipantResult(p2, EndingEventType.None, 1.8);

            int comparisonResult = comparer.Compare(pr1, pr2);

            Assert.AreEqual(-1, comparisonResult);
        }

        [TestMethod()]
        public void CompareFinalistWithSameTimeOnBib_WithSmallerBibBeingSmallest()
        {
            ParticipantResultComparer comparer = new ParticipantResultComparer();

            Participant p1 = new Participant() { Id = 6, PersonInfo = new Person() { Country = "C", Id = 4, Name = "p1" } };
            ParticipantResult pr1 = new ParticipantResult(p1, EndingEventType.None, 1.7);
            Participant p2 = new Participant() { Id = 1, PersonInfo = new Person() { Country = "C", Id = 1, Name = "p2" } };
            ParticipantResult pr2 = new ParticipantResult(p2, EndingEventType.None, 1.7);

            int comparisonResult = comparer.Compare(pr1, pr2);

            Assert.AreEqual(1, comparisonResult);
        }

        [TestMethod()]
        public void CompareCompetitor_OnIrmType()
        {
            ParticipantResultComparer comparer = new ParticipantResultComparer();

            Participant p = new Participant() { Id = 0, PersonInfo = new Person() { Country = "C", Id = 4, Name = "p" } };
            ParticipantResult prFinished = new ParticipantResult(p, EndingEventType.None, 1.5);
            ParticipantResult prAbandon = new ParticipantResult(p, EndingEventType.Abandon);
            ParticipantResult prFault = new ParticipantResult(p, EndingEventType.Fault);
            ParticipantResult prWithdraw = new ParticipantResult(p, EndingEventType.Withdrawn);

            int comparisonResult = comparer.Compare(prFinished, prAbandon);
            Assert.AreEqual(-1, comparisonResult);
            comparisonResult = comparer.Compare(prFinished, prFault);
            Assert.AreEqual(-1, comparisonResult);
            comparisonResult = comparer.Compare(prFinished, prWithdraw);
            Assert.AreEqual(-1, comparisonResult);

            comparisonResult = comparer.Compare(prAbandon, prFault);
            Assert.AreEqual(-1, comparisonResult);

            comparisonResult = comparer.Compare(prAbandon, prWithdraw);
            Assert.AreEqual(-1, comparisonResult);

            comparisonResult = comparer.Compare(prFault, prWithdraw);
            Assert.AreEqual(-1, comparisonResult);
        }

        [TestMethod()]
        public void CompareNonFinalistWithSameIRMOnBib_WithSmallerBibBeingSmallest()
        {
            ParticipantResultComparer comparer = new ParticipantResultComparer();

            Participant p1 = new Participant() { Id = 0, PersonInfo = new Person() { Country = "C", Id = 1, Name = "p1" } };
            Participant p2 = new Participant() { Id = 5, PersonInfo = new Person() { Country = "C", Id = 4, Name = "p2" } };
            ParticipantResult prAbandon1 = new ParticipantResult(p1, EndingEventType.Abandon);
            ParticipantResult prFault1 = new ParticipantResult(p1, EndingEventType.Fault);
            ParticipantResult prWithdraw1 = new ParticipantResult(p1, EndingEventType.Withdrawn);
            ParticipantResult prAbandon2 = new ParticipantResult(p2, EndingEventType.Abandon);
            ParticipantResult prFault2 = new ParticipantResult(p2, EndingEventType.Fault);
            ParticipantResult prWithdraw2 = new ParticipantResult(p2, EndingEventType.Withdrawn);

            int comparisonResult = comparer.Compare(prAbandon1, prAbandon2);
            Assert.AreEqual(-1, comparisonResult);

            comparisonResult = comparer.Compare(prFault1, prFault2);
            Assert.AreEqual(-1, comparisonResult);

            comparisonResult = comparer.Compare(prWithdraw1, prWithdraw2);
            Assert.AreEqual(-1, comparisonResult);
        }
    }
}