using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class UsingTree : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private void Start()
    {
        Patrol patrolNode = new();
        Attack attackNode = new();

        List<Node> sequenceNodes = new() { patrolNode, attackNode };
        Sequence sequence = new(sequenceNodes);

        List<Node> selectorNodes = new() { sequence };
        Selector selector = new(selectorNodes);

        BehaviourTree.Tree BT = new(selector);
        BT.SetVariable(BlackboardVariable.myPlayerPosition, player.transform);
        BT.SetVariable(BlackboardVariable.targetPosition, enemy.transform);
        BT.StartEvaluation();
    }
}
