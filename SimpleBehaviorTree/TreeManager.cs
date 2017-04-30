using SimpleBehaviorTree.Extensions;
using SimpleBehaviorTree.Interfaces;
using System;
using System.Linq;

namespace SimpleBehaviorTree
{
    public class TreeManager : ITreeManager
    {
        private Branch _rootNode;

        public TreeManager(ITreeNode node)
        {
            if (node == null) throw new ArgumentNullException();

            node.OnNodeType(() =>
            {
                _rootNode = node as Branch;
            }, () =>
            {
                _rootNode = CreateRootBranch(node as Leaf);
            });
        }

        public TreeManager(Leaf leaf, TBranch branchType)
        {
           _rootNode = CreateRootBranch(leaf, branchType);
        }

        public void Execute()
        {
            ProcessBranch(_rootNode);
        }

        internal static TOperation ProcessBranch(Branch branch)
        {
            if (!branch.IsValid()) throw new ArgumentException();

            var result = default(TOperation);

            while (branch.Children.Any())
            {
                var currentNode = branch.Children.Dequeue();

                result = ProcessNode(currentNode);

                if (branch.IsFailedSequence(result) || branch.IsSucceededSelector(result) || result == TOperation.Error)
                {
                    break;
                }
            }

            return result;
        }

        internal static TOperation ProcessNode(ITreeNode node)
        {
            if (node == null) throw new ArgumentNullException();

            var result = default(TOperation);

            node.OnNodeType(() =>
            {
                result = ProcessBranch(node as Branch);
            }, () => 
            {
                result = (node as Leaf).Operation.Invoke();
            });

            return result;
        }

        // Root branch will default to selector type if its not provided.
        private static Branch CreateRootBranch(Leaf leaf, TBranch branchType = TBranch.Selector)
        {
            if (leaf == null) throw new ArgumentNullException();

            Branch root = new Branch(branchType);
            root.Children.Enqueue(leaf);

            return root;
        }
    }
}
