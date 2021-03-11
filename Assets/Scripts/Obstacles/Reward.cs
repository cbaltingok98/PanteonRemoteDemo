using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private float reward;

    public float GetReward()
    {
        return reward;
    }
}
