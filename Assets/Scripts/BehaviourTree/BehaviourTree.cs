using System.Collections.Generic;

namespace BehaviourTree
{
    public enum BlackboardVariable
    {
        myPlayerPosition,
        myPlayerId,
        targetPosition,
        targetIsEnemy,
        enemyProximityLimit
    }

    public class Tree
    {
        private readonly Node root;
        private readonly Dictionary<BlackboardVariable, object> blackboard;

        public void ClearBlackboard()
        {
            blackboard.Clear();
        }

        public object GetVariable(BlackboardVariable key)
        {
            if (blackboard.TryGetValue(key, out object value))
                return value;
            return null;
        }

        public void SetVariable(BlackboardVariable key, object value)
        {
            blackboard.Add(key, value);
        }

        public Tree()
        {
            root = new();
            blackboard = new Dictionary<BlackboardVariable, object>();
        }

        public Tree(Node node)
        {
            root = node;
            blackboard = new Dictionary<BlackboardVariable, object>();
        }

        public void StartEvaluation()
        {
            root.Evaluate();
        }
    }
}
