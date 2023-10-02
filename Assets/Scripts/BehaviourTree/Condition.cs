namespace BehaviourTree
{
    public class Condition : Node
    {
        public Del evaluateCondition;
        public delegate NodeState Del();

        public Condition() : base() { }

        public void AssignCondition(Del conditionFunction)
        {
            evaluateCondition = conditionFunction;
        }

        public override NodeState Evaluate()
        {
            state = evaluateCondition();
            if (state == NodeState.SUCCESS)
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
                            continue;
                        case NodeState.RUNNING:
                            state = NodeState.RUNNING;
                            return NodeState.RUNNING;
                    }
                }
            }
            return state;
        }
    }
}