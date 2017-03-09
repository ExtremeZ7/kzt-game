using UnityEngine;
using AssemblyCSharp;

public class clonePrefabAsChildAtStart : MonoBehaviour
{

    public GameObject prefabToClone;
    public float startDelay = 0f;

    [Space(10)]
    public bool replaceSelfInstead;

    SpriteRenderer spriteRenderer;

    /*[ContextMenu("Revalidate")]
    void OnValidate()
    {
        try
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            try
            {
                if (prefabToClone != null)
                    spriteRenderer.sprite = prefabToClone.GetComponent<SpriteRenderer>().sprite;
            }
            catch (MissingComponentException ex2)
            {
                Debug.LogError(ex2.ToString());
            }
        }
        catch (MissingComponentException ex)
        {
            Debug.LogError(ex.ToString());
            gameObject.AddComponent<SpriteRenderer>();
            OnValidate();
        }
    }*/

    void Update()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }

        if (Help.UseAsTimer(ref startDelay))
        {
            string name = prefabToClone.name;
            GameObject newChild = Instantiate(prefabToClone, transform.position, Quaternion.identity) as GameObject;
            newChild.name = name;
            if (!replaceSelfInstead)
            {
                newChild.transform.parent = transform;
                this.enabled = false;
            }
            else
            {
                newChild.transform.parent = transform.parent;
                Object.Destroy(gameObject);
            }
        }
    }
}
