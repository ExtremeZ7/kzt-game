using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomizeAnimatorSpeed : MonoBehaviour
{

    public float baseSpeed = 1f;
    public float variation;

    void Start()
    {
        GetComponent<Animator>().speed = baseSpeed.Variation(variation);
        Destroy(this);
    }
}
