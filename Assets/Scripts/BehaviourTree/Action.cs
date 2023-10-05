using UnityEngine;

namespace BehaviourTree
{
    public class Patrol : Node
    {
        public override NodeState Evaluate()
        {
            Debug.Log("Patrouiller");
            return NodeState.SUCCESS;
        }
    }

    public class GoToTarget : Node
    {
        private readonly Transform AITransform;

        public GoToTarget(Transform transform)
        {
            AITransform = transform;
        }

        public override NodeState Evaluate()
        {
            Rusher.blackboard.TryGetValue("target", out object value);
            Transform targetTranform = (Transform)value;

            if (Vector3.Distance(AITransform.position, targetTranform.position) > 0.01f)
            {
                AITransform.position = Vector3.MoveTowards(
                    AITransform.position, targetTranform.position, Rusher.speed * Time.deltaTime);
                AITransform.LookAt(targetTranform.position);
            }

            // à supprimer
            Debug.Log("GoToPosition : " + targetTranform.position);

            state = NodeState.RUNNING;
            return state;
        }
    }

    public class CheckTargetRange : Node
    {
        public override NodeState Evaluate()
        {
            Debug.Log("Vérifie la portée");
            return NodeState.SUCCESS;
        }
    }

    public class Attack : Node
    {
        public override NodeState Evaluate()
        {
            Debug.Log("Attaquer");
            return NodeState.SUCCESS;
        }
    }
}