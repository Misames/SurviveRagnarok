using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeState
    {
        SUCCESS,
        FAILURE,
        RUNNING
    }

    public class Node
    {
        protected NodeState state;
        protected Node parent;
        protected List<Node> children = new();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                Attach(child);
        }

        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate()
        {
            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        continue;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        break;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            return state;
        }
    }
}