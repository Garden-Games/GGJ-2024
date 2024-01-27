using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] GameObject ui = null;
    private UIController controller = null;

    private void OnEnable()
    {
        controller = ui.GetComponent<UIController>();
    }


    public IEnumerator sendDelayedDialogue(string text)
    {
        yield return new WaitForSeconds(1);
        controller.updateDialogue(text);
    }


    private void Update()
    {
        List<string> options = new List<string> { "OYSTERS", "POTATOS", "ICE CREAM CONES" };
        if (Input.GetKey(KeyCode.Alpha1))
        {
            sendDelayedDialogue("FART IN MY SANDWICH");
            //controller.sendOptions(options);
        }
    }

}
