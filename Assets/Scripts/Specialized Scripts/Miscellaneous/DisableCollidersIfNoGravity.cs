using UnityEngine;
using Managers;

public class DisableCollidersIfNoGravity : MonoBehaviour
{

    public Collider2D collider2d;

    [Space(10)]
    public bool reverse;

    private LevelManager levelManager;

    void Start()
    {
        if (!collider2d)
            collider2d = GetComponent<Collider2D>();
			
        levelManager = GameObject.FindGameObjectWithTag("GAMEOBJECTS").GetComponent<LevelManager>();
    }

    void Update()
    {
        collider2d.enabled = !levelManager.noGravity == !reverse;
    }
}
