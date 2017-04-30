using NUnit.Framework;
using SimpleBehaviorTree.Extensions;

namespace SimpleBehaviorTree.Test.Extensions
{
    [TestFixture]
    public class BranchExtensionsTest
    {
        Branch branchSequence, branchSelector;

        [OneTimeSetUp]
        public void TestSetup()
        {
            branchSequence = new Branch(TBranch.Sequence);
            branchSelector = new Branch(TBranch.Selector);
        }

        [Test]
        public void ShouldValidateBranch()
        {
            branchSelector.AddNode(new Leaf(() => { return TOperation.Success; }));
            var result = branchSelector.IsValid();
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldConfirmFailedSequence()
        {
            var result = branchSequence.IsFailedSequence(TOperation.Failure);
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldConfirmSucceededSelector()
        {
            var result = branchSelector.IsSucceededSelector(TOperation.Success);
            Assert.IsTrue(result);
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            branchSequence = null;
            branchSelector = null;
        }
    }
}
