using NUnit.Framework;
using System;

namespace SimpleBehaviorTree.Test
{
    [TestFixture]
    public class BranchTest
    {
        [Test]
        public void ShouldAddNode()
        {
            Branch branch = new Branch(TBranch.Sequence);

            var result = branch.AddNode(new Leaf(() => { return TOperation.Success; }));

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Children.Count);
        }

        [Test]
        public void ShouldThrowExceptionOnAddNode()
        {
            Branch branch = new Branch(TBranch.Sequence);

            Assert.Throws<ArgumentNullException>(() => { branch.AddNode(null); });
        }
    }
}
