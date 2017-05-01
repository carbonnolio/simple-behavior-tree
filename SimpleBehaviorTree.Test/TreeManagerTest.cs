using NUnit.Framework;
using System;

namespace SimpleBehaviorTree.Test
{
    [TestFixture]
    public class TreeManagerTest
    {
        [Test]
        public void ShouldProcessNodeAsBranch()
        {
            var branch = new Branch(TBranch.Sequence)
                .AddNode(new Leaf(() => { return TOperation.Success; }))
                .AddNode(new Leaf(() => { return TOperation.Success; }));

            var result = TreeManager.ProcessNode(branch);
            Assert.AreEqual(TOperation.Success, result);
        }

        [Test]
        public void ShouldProcessNodeAsLeaf()
        {
            var result = TreeManager.ProcessNode(new Leaf(() => { return TOperation.Failure; }));
            Assert.AreEqual(TOperation.Failure, result);
        }

        [Test]
        public void ShouldThrowExceptionOnProcessNode()
        {
            Assert.Throws<ArgumentNullException>(() => { TreeManager.ProcessNode(null); });
        }

        [Test]
        public void ShouldProcessBranchAsSequence()
        {
            var rootBranch = new Branch(TBranch.Sequence)
                .AddNode(new Leaf(() => { return TOperation.Success; }))
                .AddNode(new Leaf(() => { return TOperation.Success; }))
                .AddNode(new Leaf(() => { return TOperation.Success; }));

            var result = TreeManager.ProcessBranch(rootBranch);
            Assert.AreEqual(TOperation.Success, result);
            Assert.AreEqual(0, rootBranch.Children.Count);
        }

        [Test]
        public void ShouldExitBranchAsSequence()
        {
            var rootBranch = new Branch(TBranch.Sequence)
                .AddNode(new Leaf(() => { return TOperation.Success; }))
                .AddNode(new Leaf(() => { return TOperation.Failure; }))
                .AddNode(new Leaf(() => { return TOperation.Success; }));

            var result = TreeManager.ProcessBranch(rootBranch);
            Assert.AreEqual(TOperation.Failure, result);
            Assert.AreEqual(1, rootBranch.Children.Count);
        }

        [Test]
        public void ShouldProcessBranchAsSelector()
        {
            var rootBranch = new Branch(TBranch.Selector)
                .AddNode(new Leaf(() => { return TOperation.Failure; }))
                .AddNode(new Leaf(() => { return TOperation.Failure; }))
                .AddNode(new Leaf(() => { return TOperation.Success; }));

            var result = TreeManager.ProcessBranch(rootBranch);
            Assert.AreEqual(TOperation.Success, result);
            Assert.AreEqual(0, rootBranch.Children.Count);
        }

        [Test]
        public void ShouldExitBranchAsSelector()
        {
            var rootBranch = new Branch(TBranch.Selector)
                .AddNode(new Leaf(() => { return TOperation.Success; }))
                .AddNode(new Leaf(() => { return TOperation.Failure; }))
                .AddNode(new Leaf(() => { return TOperation.Failure; }));

            var result = TreeManager.ProcessBranch(rootBranch);
            Assert.AreEqual(TOperation.Success, result);
            Assert.AreEqual(2, rootBranch.Children.Count);
        }

        [Test]
        public void ShouldThrowExceptionOnInvalidBranch()
        {
            Assert.Throws<ArgumentException>(() => { TreeManager.ProcessBranch(new Branch(TBranch.Selector)); });
            Assert.Throws<ArgumentException>(() => { TreeManager.ProcessBranch(null); });
        }
    }
}
