using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }

        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            return state;
        }
    }
}