using System.Linq;

namespace SimpleBehaviorTree.Extensions
{
    internal static class BranchExtensions
    {
        internal static bool IsValid(this Branch branch) => branch != null && branch.Children.Any();

        internal static bool IsFailedSequence(this Branch branch, TOperation result) => branch != null && branch.BranchType == TBranch.Sequence && result == TOperation.Failure;

        internal static bool IsSucceededSelector(this Branch branch, TOperation result) => branch != null && branch.BranchType == TBranch.Selector && result == TOperation.Success;
    }
}
