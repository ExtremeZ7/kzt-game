using UnityEngine;
using Managers;

public class DisableRigidbodyIfNoGravity : MonoBehaviour
{

    public Rigidbody2D rb2d;

    [Space(10)]
    public bool reverse;

    private LevelManager levelManager;

    void Start()
    {
        if (!rb2d)
            rb2d = GetComponent<Rigidbody2D>();
        levelManager = GameObject.FindGameObjectWithTag("GAMEOBJECTS").GetComponent<LevelManager>();
    }

    void Update()
    {
        if (levelManager.noGravity == !reverse)
            rb2d.Sleep();
        else
            rb2d.WakeUp();
    }
}
