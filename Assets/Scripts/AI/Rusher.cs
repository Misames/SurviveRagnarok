using System.Collections.Generic;
using BehaviourTree;

public class Rusher : Tree
{
    public static float speed = 5f;
    public static uint attackRange = 1;
    public static uint dammage = 5;
    public static Dictionary<string, object> blackboard = new();

    protected override Node SetupTree()
    {
        GoToTarget goToTargetNode = new(transform);
        Attack attackNode = new();
        Attack attackNode2 = new();

        List<Node> sequenceNodes = new() { goToTargetNode, attackNode };
        Sequence sequence = new(sequenceNodes);

        List<Node> selectorNodes = new() { sequence, attackNode2 };
        Selector selector = new(selectorNodes);

        return selector;
    }
}
