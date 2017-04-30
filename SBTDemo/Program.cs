using SimpleBehaviorTree;
using System;

namespace SBTDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting BT");

            Run();

            Console.ReadLine();
        }

        public static void Run()
        {
            var i = 0;
            var j = 7;
            
            var rootBranch = new Branch(TBranch.Sequence);

            for (int k = 0; k < 10; k++)
            {
                rootBranch.AddNode(new Branch(TBranch.Selector).AddNode(new Leaf(() => {

                    var result = i > j ? TOperation.Success : TOperation.Failure;
                    Console.WriteLine($"i is {i}, check completed, result: {result}");
                    return result;

                })).AddNode(new Leaf(() => {

                    i++;
                    Console.WriteLine($"Now i is {i}");
                    return TOperation.Success;

                }))).AddNode(new Leaf(() => {

                    var result = i < j ? TOperation.Success : TOperation.Failure;
                    Console.WriteLine($"i less then {j}, result: {result}");
                    return result;

                }));
            }

            var btManager = new TreeManager(rootBranch);

            btManager.Execute();
        }
    }
}
