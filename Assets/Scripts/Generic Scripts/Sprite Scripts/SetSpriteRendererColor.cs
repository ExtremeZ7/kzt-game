using UnityEngine;

[ExecuteInEditMode]
public class SetSpriteRendererColor : MonoBehaviour
{
    public Color color = new Color(1f, 1f, 1f, 1f);

    void Update()
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}