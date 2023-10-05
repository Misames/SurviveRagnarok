using UnityEngine;

public class InitBlackboard : MonoBehaviour
{
    public GameObject basePlayer;

    private void Start()
    {
        Rusher.blackboard.Add("target", basePlayer.transform);
    }
}
