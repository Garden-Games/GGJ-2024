using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionButtonOnClick : MonoBehaviour
{
    Button thisButton;
    private bool isClicked = false;
    private GameObject gameManager;
    private VisualElementStyleSheetSet style;


    private void OnEnable()
    {
        thisButton = gameObject.GetComponent<UIDocument>().rootVisualElement.Query<Button>("option-button");
        thisButton.clicked += OnClick;
        style = thisButton.styleSheets;
        gameManager = GameObject.Find("GameManager");

    }

    void OnClick()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.Find("GameManager");
        }

        Debug.Log($"{thisButton.text} button has been pressed");
        if(!isClicked)
        {
        gameManager.SendMessage("handleOption",thisButton.text);
        isClicked = true;
        }
    }

}
