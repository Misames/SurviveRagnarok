using System.Collections.Generic;

namespace BehaviourTree
{
    public class Selector : Node
    {
        public Selector() : base() { }

        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            int count = children.Count;
            for (int i = 0; i < count; ++i)
            {
                Node node = children[i];
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                }
            }
            return NodeState.FAILURE;
        }
    }
}