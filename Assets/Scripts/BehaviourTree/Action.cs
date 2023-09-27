using UnityEngine;

namespace BehaviorTree
{
    // Exemple de comportement "Patrouiller"
    public class Patrol : Node
    {
        public override NodeState Evaluate()
        {
            // Logique pour patrouiller
            Debug.Log("Patrouiller");
            return NodeState.SUCCESS; // Indique que le comportement a réussi
        }
    }

    // Exemple de comportement "Attaquer"
    public class Attack : Node
    {
        public override NodeState Evaluate()
        {
            // Logique pour attaquer
            Debug.Log("Attaquer");
            return NodeState.SUCCESS; // Indique que le comportement a réussi
        }
    }
}