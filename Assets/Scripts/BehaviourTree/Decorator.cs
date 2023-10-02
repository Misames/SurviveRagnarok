namespace BehaviourTree
{
    public class DecoratorForceFailure : Node
    {
        public override NodeState Evaluate()
        {
            int count = children.Count;
            for (int i = 0; i < count; i++)
            {
                Node child = children[i];
                child.Evaluate();
            }
            return NodeState.FAILURE;
        }
    }

    public class DecoratorForceSuccess : Node
    {
        public override NodeState Evaluate()
        {
            int count = children.Count;
            for (int i = 0; i < count; ++i)
            {
                Node child = children[i];
                child.Evaluate();
            }
            return NodeState.SUCCESS;
        }
    }

    public class DecoratorInverter : Node
    {
        public override NodeState Evaluate()
        {
            int count = children.Count;
            for (int i = 0; i < count; ++i)
            {
                Node child = children[i];
                state = child.Evaluate();
            }
            return state == NodeState.SUCCESS ? NodeState.FAILURE : NodeState.SUCCESS;
        }
    }

    public class DecoratorRetry : Node
    {
        private readonly int nbRetry;

        public DecoratorRetry(int nbRetry = 1) : base()
        {
            this.nbRetry = nbRetry;
        }

        public override NodeState Evaluate()
        {
            int count = children.Count;
            for (int i = 0; i < count; ++i)
            {
                Node child = children[i];
                state = child.Evaluate();
            }

            int j = 0;
            if (state == NodeState.FAILURE)
            {
                while (j < nbRetry && state != NodeState.SUCCESS)
                {
                    for (int k = 0; k < children.Count; ++k)
                    {
                        Node child = children[k];
                        state = child.Evaluate();
                    }
                    ++j;
                }
            }
            return state;
        }
    }

    public class DecoratorRepeat : Node
    {
        private readonly int nbRepeat;

        public DecoratorRepeat(int nbRepeat = 1) : base()
        {
            this.nbRepeat = nbRepeat;
        }

        public override NodeState Evaluate()
        {
            for (int i = 0; i < nbRepeat; ++i)
            {
                for (int j = 0; j < children.Count; ++j)
                {
                    Node child = children[j];
                    state = child.Evaluate();
                }
            }
            return state;
        }
    }
}