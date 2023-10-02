using System.Collections.Generic;

namespace BehaviourTree
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
            int count = children.Count;
            for (int i = 0; i < count; ++i)
            {
                Node child = children[i];
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate()
        {
            int count = children.Count;
            for (int i = 0; i < count; ++i)
            {
                Node child = children[i];
                switch (child.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        break;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        break;
                }
            }
            return state;
        }
    }
}