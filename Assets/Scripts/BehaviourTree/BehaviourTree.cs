using System.Collections.Generic;

namespace BehaviorTree
{
    public enum BlackboardVariable
    {
        myPlayerPosition,
        myPlayerId,
        targetPosition,
        targetIsEnemy,
        enemyProximityLimit
    }

    public class BehaviourTree
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

        public BehaviourTree()
        {
            root = new Node();
            blackboard = new Dictionary<BlackboardVariable, object>();
        }

        public BehaviourTree(Node root)
        {
            this.root = root;
            blackboard = new Dictionary<BlackboardVariable, object>();
        }

        public void StartEvaluation()
        {
            root.Evaluate();
        }
    }
}
