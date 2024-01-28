using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class UIController : MonoBehaviour
{
    [SerializeField] UIDocument doc;
    [SerializeField] GameObject buttonTemplate;
    private Label dialogueBox;
    private VisualElement choicebox;
    private List<GameObject> optionButtons = new List<GameObject>();
    private List<string> options = new List<string>();

    private void OnEnable()
    {
        VisualElement ROOT = doc.rootVisualElement;
        VisualElement mainBox = ROOT.Query("main-dialogue");
        choicebox = ROOT.Query("choice-box");
        dialogueBox = mainBox.Query<Label>("main-label");
        dialogueBox.text = "Potato";
    }


    public void updateDialogue(string text)
    {
        print(text);
        dialogueBox.text = text;        
    }

    public void sendOptions(List<string> options)
    {
        clearButtons();
        this.options = options;
        this.options.ForEach((string option) => {
            GameObject button = Instantiate(buttonTemplate);
            optionButtons.Add(button);
            UIDocument buttonUI = button.GetComponent<UIDocument>();
            VisualElement buttonRoot = buttonUI.rootVisualElement;
            Button buttonElement = buttonRoot.Query<Button>("button");
            buttonElement.text = option;
            choicebox.Add(buttonElement);
        });
    }

   

    private void Update()
    {
        List<string> options = new List<string> { "OYSTERS", "POTATOS", "ICE CREAM CONES" };
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("Hello from button press");
            sendOptions(options);
        }
    }

    private void clearButtons()
    {
        choicebox.Clear();
        this.options.Clear();
        this.optionButtons.ForEach((optionButton) => Destroy(optionButton));
        this.optionButtons.Clear();
    }
}


