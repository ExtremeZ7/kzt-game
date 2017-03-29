using UnityEngine;
using Controllers;

public class AddGemToCollectionWhenAllowed : MonoBehaviour
{

    public Animator animator;
    public TriggerSwitch trigger;
    public SpriteRenderer spriteRenderer;

    [Space(10)]
    public GUIStyle textStyle;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (trigger == null)
            trigger = GetComponent<TriggerSwitch>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameController.Instance.items.crystalsShownInGUI < GameController.Instance.items.totalCrystalsInLevel)
        {
            animator.SetBool("Unlocked", false);
        }
        else
        {
            animator.SetBool("Unlocked", true);
            if (trigger.IsActivated)
            {
                GameController.Instance.items.hasGem = true;
                spriteRenderer.sortingLayerName = "Collected Items";
                animator.SetTrigger("Collected");
            }	
        }
    }
}