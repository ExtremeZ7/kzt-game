using UnityEngine;

public class ShowHintOnTrigger : TriggerListener
{

    [Space(10)]
    [TextArea(1, 3)]
    public string hintText;

    public override void ManagedUpdate()
    {
        if (Listener.IsActivated && !hintText.Equals(""))
        {
            // If hint text already exists, remove it
            //
            if (Object.FindObjectOfType<drawHelpfulHint>() != null)
            {
                Destroy(Object.FindObjectOfType<drawHelpfulHint>());
            }

            // Instantiate a new hint drawer (temporary)
            //
            GameObject newHint = 
                Instantiate(Resources.Load<GameObject>("Prefabs/HintBox"));

            // Modify the drawer text (temporary)
            //
            drawHelpfulHint dhh = newHint.GetComponent<drawHelpfulHint>();
            dhh.stringToDraw = hintText;

            //Finally, remove the behaviour
            //
            Destroy(this);
        }
    }

}