using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum StoryState
    {
        Beginning,
        WalkAround,
        TurnOnLight,
        TurnOffLight,
        EndingNothing,
        OpenDoor,
        DefineAntagonist,
        NavigateAntagonist,
        EndingEaten,
        Counter
    }

    public Animator lightingAnimator;
    public Animator potatoAnimator;
    public UIController controller;


    private StoryState currentStoryState;

    private float stateStartTime;
    private bool lightsOn;
    private string antagonist;
    private string option;
        

    // Start is called before the first frame update
    void Start()
    {
        StartBeginningState();
        stateStartTime = Time.time;
        lightsOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        TransitionStoryState();
    }

    void TransitionStoryState()
    {
        float elapsedSecondsInState = Time.time - stateStartTime;

        switch (currentStoryState)
        {
            case StoryState.Beginning:
                {
                    
                    // TODO: Use UI inputs instead of key presses
                    if (lightsOn)
                    {
                        controller.updateDialogue("Eyeing the lit room, McCringle opts to _______ .");

                        if (this.option == "Walk Around")
                        {
                            clearOptions();
                            StartWalkAroundState();
                            stateStartTime = Time.time;
                            

                            Debug.Log("Transitioning to " + currentStoryState);
                        }
                        else if (option == "Turn Off Lights")
                        {
                            clearOptions();
                            StartTurnOffLightState();
                            stateStartTime = Time.time;
                            
                            Debug.Log("Transitioning to " + currentStoryState);
                        }
                        else if (option == "Open Door")
                        {
                            clearOptions();
                            StartOpenDoorState();
                            stateStartTime = Time.time;
                            
                            Debug.Log("Transitioning to " + currentStoryState);
                        }

                        break;
                    }

                    else // lightsOn == false
                    {
                        controller.updateDialogue("Waiting in darkness, Julius \"Spud\" McCringle decides to _______ .");

                        if (option == "Walk Around")
                        {
                            clearOptions();
                            StartWalkAroundState();
                            stateStartTime = Time.time;
                            Debug.Log("Transitioning to " + currentStoryState);
                        }
                        else if (option == "Turn On Lights")
                        {
                            clearOptions();
                            StartTurnOnLightState();
                            stateStartTime = Time.time;
                            
                            Debug.Log("Transitioning to " + currentStoryState);
                        }
                        else if (option == "Go to sleep")
                        {
                            clearOptions();
                            currentStoryState = StoryState.EndingNothing;
                            stateStartTime = Time.time;
                            
                            Debug.Log("Transitioning to " + currentStoryState);
                        }
                        else
                        {
                            //Debug.Log("Got unexpected option " + option);
                        }

                        break;
                    }
                }
                
            case StoryState.WalkAround:
                {
                    controller.updateDialogue("");

                    float stateDurationSeconds = 5;

                    if (elapsedSecondsInState >= stateDurationSeconds)
                    {
                        StartBeginningState();
                        stateStartTime = Time.time;
                    }

                    break;
                }

            case StoryState.TurnOnLight:
                {
                    controller.updateDialogue("");

                    float stateDurationSeconds = 5;

                    if (elapsedSecondsInState >= stateDurationSeconds)
                    {
                        StartBeginningState();
                        stateStartTime = Time.time;
                    }

                    break;
                }
            case StoryState.TurnOffLight:
                {
                    controller.updateDialogue("");

                    float stateDurationSeconds = 5;

                    if (elapsedSecondsInState >= stateDurationSeconds)
                    {
                        StartBeginningState();
                        stateStartTime = Time.time;
                    }

                    break;
                }

            // Triggered when the user chooses to go to sleep
            case StoryState.EndingNothing:
                {
                    controller.updateDialogue("McCringle succumbs to a deep and dreamless slumber...");
                    // TODO: Trigger ending and credits sequence
                    break;
                }

            case StoryState.OpenDoor:
                {
                    controller.updateDialogue("McCringle finds a tremendous _______ as the pantry door swings open...");

                    float stateDurationSeconds = 5;

                    if (elapsedSecondsInState >= stateDurationSeconds)
                    {
                        StartDefineAntagonistState();
                        stateStartTime = Time.time;
                    }

                    break;
                }

            case StoryState.DefineAntagonist:
                {
                    // TODO: Use UI inputs instead of key presses
                    if (Input.GetKey(KeyCode.Alpha1))
                    {
                        LoadAntagonist("Dog");
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }
                    else if (Input.GetKey(KeyCode.Alpha2))
                    {
                        LoadAntagonist("Hippo");
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }
                    else if (Input.GetKey(KeyCode.Alpha3))
                    {
                        LoadAntagonist("Kraken");
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }

                    break;
                }

            case StoryState.NavigateAntagonist:
                {
                    // TODO: Use UI inputs instead of key presses
                    if (Input.GetKey(KeyCode.Alpha1))
                    {
                        currentStoryState = StoryState.EndingEaten;
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }
                    else if (Input.GetKey(KeyCode.Alpha2))
                    {
                        currentStoryState = StoryState.Counter;
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }

                    break;
                }

            case StoryState.EndingEaten:
                {
                    // TODO: Trigger ending and credits sequence
                    break;
                }

            case StoryState.Counter:
                {
                    // TODO: Load counter scene, continue story
                    break;
                }

            default:
                {
                    Debug.LogWarning("Unknown current state " + currentStoryState);
                    break;
                }
        }
    }

    void StartBeginningState()
    {
    
        if (lightsOn)
        {
            controller.sendOptions(new List<string> { "Walk Around", "Turn Off Lights", "Open Door" });
            Debug.Log("Press 1 for walk around, 2 for lights off, 3 to open door");
        }
        else
        {
            controller.sendOptions(new List<string> { "Walk Around", "Turn On Lights", "Go to sleep" });
            Debug.Log("Press 1 for walk around, 2 for lights on, 3 to go to sleep");
        }
        currentStoryState = StoryState.Beginning;
    }

    void StartTurnOnLightState()
    {
        lightingAnimator.SetTrigger("playTurnOnLights");
        currentStoryState = StoryState.TurnOnLight;
        lightsOn = true;
        
    }

    void StartTurnOffLightState()
    {
        lightingAnimator.SetTrigger("playTurnOffLights");
        currentStoryState = StoryState.TurnOffLight;
        lightsOn = false;
    }

    void StartWalkAroundState()
    {
        potatoAnimator.SetTrigger("playPotatoWalkAround");
        currentStoryState = StoryState.WalkAround;
    }

    void StartOpenDoorState()
    {
        // TODO: Trigger audio of door opening
        currentStoryState = StoryState.OpenDoor;
    }

    void StartDefineAntagonistState()
    {
        Debug.Log("Press 1 for dog, 2 for hippo, 3 for kraken");
        currentStoryState = StoryState.DefineAntagonist;
    }

    void LoadAntagonist(string name)
    {
        switch(name)
        {
            case "Dog":
                Debug.Log("Loading dog");
                // TODO: Trigger loading dog
                break;
            case "Hippo":
                Debug.Log("Loading hippo");
                // TODO: Trigger loading hippo
                break;
            case "Kraken":
                Debug.Log("Loading kraken");
                // TODO: Trigger loading kraken
                break;
        }
        antagonist = name;

        controller.updateDialogue("Surprised by the ferocious beast, Spud quickly _______ the " + name + ".");

        currentStoryState = StoryState.NavigateAntagonist;
        Debug.Log("Press 1 for approach, 2 for avoid");
    }

    void handleOption(string option)
    {
        Debug.Log("Hello from game manager " + option);
        this.option = option;
    } 

    void clearOptions()
    {
        option = null;
        controller.sendOptions(new List<string>());
    }
   

}
