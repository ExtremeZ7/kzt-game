using UnityEngine;
using AssemblyCSharp;

public class CreateNewObjectIfOldObjectLeft : MonoBehaviour
{

    public GameObject objectToCreate;
    public GameObject startingObject;

    [Space(10)]
    public float watchRange;
    public float delay;

    private GameObject currentObject;
    [SerializeField]
    private float timer;

    void Start()
    {
        if (startingObject == null)
            currentObject = Instantiate(objectToCreate, transform.position, Quaternion.identity) as GameObject;
        else
            currentObject = startingObject;
    }

    void Update()
    {
        if (currentObject != null && Vector3.Distance(currentObject.transform.position, transform.position) > watchRange)
        {
            currentObject = null;
        }

        if (currentObject == null)
        {
            if (timer == 0f)
                timer = delay;

            if (Help.UseAsTimer(ref timer))
                currentObject = Instantiate(objectToCreate, transform.position, Quaternion.identity) as GameObject;
        }

    }
}
