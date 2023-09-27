using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class UsingTree : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private void Start()
    {
        // Créer les nœuds de comportement
        Patrol patrolNode = new();
        Attack attackNode = new();

        // Créer la séquence (Patrouiller puis Attaquer)
        List<Node> sequenceNodes = new() { patrolNode, attackNode };
        Sequence sequence = new(sequenceNodes);

        // Créer le sélecteur (Effectuer la séquence ou Attaquer)
        List<Node> selectorNodes = new() { sequence, attackNode };
        Selector selector = new(selectorNodes);

        // Config WorldGameData
        BehaviourTree BT = new(selector);
        BT.SetVariable(BlackboardVariable.myPlayerPosition, player.transform);
        BT.SetVariable(BlackboardVariable.targetPosition, enemy.transform);

        // Compute
        BT.StartEvaluation();
    }
}
