namespace BehaviorTree
{
    public class DecoratorForceFailure : Node
    {
        public override NodeState Evaluate()
        {
            foreach (Node child in children) child.Evaluate();
            return NodeState.FAILURE;
        }
    }

    public class DecoratorForceSuccess : Node
    {
        public override NodeState Evaluate()
        {
            foreach (Node child in children) child.Evaluate();
            return NodeState.SUCCESS;
        }
    }

    public class DecoratorInverter : Node
    {
        public override NodeState Evaluate()
        {
            foreach (Node child in children) state = child.Evaluate();
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
            foreach (Node child in children) state = child.Evaluate();

            uint i = 0;
            if (state == NodeState.FAILURE)
            {
                while (i < nbRetry && state != NodeState.SUCCESS)
                {
                    foreach (Node child in children)
                        state = child.Evaluate();
                    i++;
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
            for (int i = 0; i < nbRepeat; i++)
            {
                foreach (Node child in children)
                    state = child.Evaluate();
            }

            return state;
        }
    }
}