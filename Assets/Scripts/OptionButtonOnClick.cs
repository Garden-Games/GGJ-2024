using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionButtonOnClick : MonoBehaviour
{
    Button thisButton;
    private bool isClicked = false;
    private GameObject gameManager;


    private void OnEnable()
    {
        thisButton = gameObject.GetComponent<UIDocument>().rootVisualElement.Query<Button>("button");
        thisButton.clicked += OnClick;
        gameManager = GameObject.Find("GameManager");

    }


    void OnClick()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.Find("GameManager");
        }

        if(!isClicked)
        {
        Debug.Log($"{thisButton.text} button has been pressed");
        gameManager.SendMessage("handleOption",thisButton.text);
        isClicked = true;
        }
    }

}
