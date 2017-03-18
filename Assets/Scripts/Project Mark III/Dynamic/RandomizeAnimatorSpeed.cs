using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomizeAnimatorSpeed : MonoBehaviour
{

    public float baseSpeed = 1f;
    public float variation;

    void Awake()
    {
        GetComponent<Animator>().speed = baseSpeed.Variation(variation);
        Destroy(this);
    }
}
