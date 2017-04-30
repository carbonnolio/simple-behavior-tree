using SimpleBehaviorTree.Interfaces;
using System;

namespace SimpleBehaviorTree.Extensions
{
    internal static class NodeExtensions
    {
        internal static void OnNodeType(this ITreeNode node, Action branch, Action leaf)
        {
            if (node.NodeType == TNode.Leaf)
            {
                leaf.Invoke();
            }

            if (node.NodeType == TNode.Branch)
            {
                branch.Invoke();
            }
        }
    }
}
