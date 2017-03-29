using UnityEngine;
using Managers;

public class SwitchAnimatorBoolIfNoGravity : MonoBehaviour
{

    public string parameterName;

    [Space(10)]
    public Animator animator;

    [Space(10)]
    public bool reverse;

    private LevelManager levelManager;

    void Start()
    {
        if (!animator)
            animator = GetComponent<Animator>();
        levelManager = GameObject.FindGameObjectWithTag("GAMEOBJECTS").GetComponent<LevelManager>();
    }

    void Update()
    {
        animator.SetBool(parameterName, levelManager.noGravity == !reverse);
    }
}
