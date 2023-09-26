using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Action : Node
    {
        public Del doAction;
        public delegate NodeState Del();

        public Action() : base() { }

        public void AssignAction(Del actionFunction)
        {
            doAction = actionFunction;
        }

        /*
        public NodeState ActionShoot()
        {
            Data data = BehaviourTree.Instance().data;
            Vector3 target = (Vector3)data.Blackboard[BlackboardVariable.targetPosition];

            if (target == null)
                return NodeState.FAILURE;

            AIActionLookAtPosition actionLookAt = new AIActionLookAtPosition(target);
            BehaviourTree.Instance().computeAction.Add(actionLookAt);
            BehaviourTree.Instance().computeAction.Add(new AIActionFire());

            return NodeState.SUCCESS;
        }

        public NodeState ActionFindTarget()
        {
            Data data = BehaviourTree.Instance().data;
            List<PlayerInformations> playerInfos = data.GameWorld.GetPlayerInfosList();

            PlayerInformations target = null;

            float minDistance = 100000;

            foreach (PlayerInformations playerInfo in playerInfos)
            {
                if (playerInfo.PlayerId != (int)data.Blackboard[BlackboardVariable.myPlayerId])
                {
                    float distance = Vector3.Distance((Vector3)data.Blackboard[BlackboardVariable.myPlayerPosition], playerInfo.Transform.Position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = playerInfo;
                    }
                }
            }

            if (target == null)
                return NodeState.FAILURE;

            data.Blackboard[BlackboardVariable.targetPosition] = target.Transform.Position;
            return NodeState.SUCCESS;
        }
        */

        /*
        public NodeState ActionMoveToTarget()
        {
            Data data = BehaviourTree.Instance().data;

            Vector3 target = (Vector3)data.Blackboard[BlackboardVariable.targetPosition];
            if (target == null)
                return NodeState.FAILURE;

            float distance = Vector3.Distance((Vector3)data.Blackboard[BlackboardVariable.myPlayerPosition], target);
            if (distance < 1)
                return NodeState.SUCCESS;

            AIActionMoveToDestination newAction = new AIActionMoveToDestination(target);
            BehaviourTree.Instance().computeAction.Add(newAction);
            return NodeState.RUNNING;
        }
        */

        public override NodeState Evaluate()
        {
            state = doAction();
            if (state == NodeState.RUNNING)
            {
                foreach (Node child in children)
                {
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
                        default:
                            continue;
                    }
                }
            }

            return state;
        }
    }
}