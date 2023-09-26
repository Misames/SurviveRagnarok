using System.Collections.Generic;
using UnityEngine.Assertions;

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

    class BehaviourTree
    {
        private static BehaviourTree instance;
        public Node root;
        public Dictionary<BlackboardVariable, object> blackboard;

        public static BehaviourTree Instance()
        {
            Assert.IsNotNull(instance);
            return instance;
        }

        public BehaviourTree()
        {
            instance = this;
            root = new Node();
            blackboard = new Dictionary<BlackboardVariable, object>();
        }

        public void Compute()
        {
            root.Evaluate();
        }
    }
}
