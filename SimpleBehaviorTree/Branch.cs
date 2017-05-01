using SimpleBehaviorTree.Interfaces;
using System;
using System.Collections.Generic;

namespace SimpleBehaviorTree
{
    public class Branch : ITreeNode
    {
        private readonly TBranch _branchType;
        private readonly Queue<ITreeNode> _children = new Queue<ITreeNode>();

        public TBranch BranchType => _branchType;

        public TNode NodeType => TNode.Branch;

        public Queue<ITreeNode> Children => _children;

        public Branch(TBranch branchType)
        {
            _branchType = branchType;
        }

        public Branch AddNode(ITreeNode node)
        {
            if (node == null) throw new ArgumentNullException("Node cannot be null.");

            _children.Enqueue(node);

            return this;
        }
    }
}
