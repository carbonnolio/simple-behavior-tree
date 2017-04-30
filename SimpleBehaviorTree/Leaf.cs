using SimpleBehaviorTree.Interfaces;
using System;

namespace SimpleBehaviorTree
{
    public class Leaf : ITreeNode
    {
        private readonly Func<TOperation> _operation;

        public Func<TOperation> Operation => _operation;

        public TNode NodeType => TNode.Leaf;

        public Leaf(Func<TOperation> operation)
        {
            _operation = operation ?? throw new ArgumentNullException();
        }
    }
}
