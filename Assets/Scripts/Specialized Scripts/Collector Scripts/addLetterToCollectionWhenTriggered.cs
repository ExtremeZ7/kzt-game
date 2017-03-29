using UnityEngine;
using Controllers;

public class addLetterToCollectionWhenTriggered : MonoBehaviour
{

    public enum CollectibleLetter
    {
        K,
        Z,
        T}

    ;

    public CollectibleLetter letterID;

    [Space(10)]
    public TriggerSwitch binarySwitch;

    void Start()
    {
        if (binarySwitch == null)
            binarySwitch = GetComponent<TriggerSwitch>();
        if (GameController.Instance.items.lettersCollected[(int)letterID])
            Object.Destroy(gameObject);
    }

    void Update()
    {
        if (binarySwitch.IsActivated)
        {
            GameController.Instance.items.lettersCollected[(int)letterID] = true;
        }
    }
}