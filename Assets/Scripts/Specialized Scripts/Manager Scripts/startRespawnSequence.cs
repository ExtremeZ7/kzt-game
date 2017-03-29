using UnityEngine;
using Managers;

public class startRespawnSequence : MonoBehaviour
{

    void Awake()
    {
        FindObjectOfType<LevelManager>().StartRespawnSequence();
    }
}
