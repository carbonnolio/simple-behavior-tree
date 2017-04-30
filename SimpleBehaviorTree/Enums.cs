namespace SimpleBehaviorTree
{
    public enum TNode
    {
        Branch,
        Leaf
    }

    public enum TOperation
    {
        Error,
        Success,
        Failure
    }

    public enum TBranch
    {
        Sequence,
        Selector
    }
}
