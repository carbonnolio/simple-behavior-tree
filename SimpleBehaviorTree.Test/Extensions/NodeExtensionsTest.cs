using NUnit.Framework;
using SimpleBehaviorTree.Extensions;
using SimpleBehaviorTree.Interfaces;

namespace SimpleBehaviorTree.Test.Extensions
{
    [TestFixture]
    public class NodeExtensionsTest
    {
        [Test]
        public void ShouldRunBranchAction()
        {
            ITreeNode branch = new Branch(TBranch.Selector);
            var result = false;

            branch.OnNodeType(() => { result = true; }, () => {});

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldRunLeafAction()
        {
            ITreeNode leaf = new Leaf(() => { return TOperation.Success; });
            var result = false;

            leaf.OnNodeType(() => {}, () => { result = true; });

            Assert.IsTrue(result);
        }
    }
}
