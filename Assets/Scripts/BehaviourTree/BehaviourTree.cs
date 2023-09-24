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
        public Node start;
        public Dictionary<BlackboardVariable, object> Blackboard;

        public static BehaviourTree Instance()
        {
            Assert.IsNotNull(instance);
            return instance;
        }

        public BehaviourTree()
        {
            instance = this;
            start = new Node();
            Blackboard = new Dictionary<BlackboardVariable, object>();
        }

        public void Compute()
        {
            start.Evaluate();
        }
    }
}
