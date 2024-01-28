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
        Counter,
        CloseCurtains,
        OpenCurtains
    }

    public Animator lightingAnimator;
    public Animator potatoAnimator;
    public Animator curtainsAnimator;
    public UIController controller;


    private StoryState currentStoryState;

    private float stateStartTime;
    private bool lightsOn;
    private string antagonist;
    private string option;
    private bool doorOpened;
        

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
                    
                    StartCloseCurtainState();
                    stateStartTime = Time.time;
                    Debug.Log("Transitioning to " + currentStoryState);

                    break;
                }

            case StoryState.DefineAntagonist:
                {
                    controller.updateDialogue("Define antagonist");
                    // TODO: Use UI inputs instead of key presses
                    if (option != null)
                    {
                        LoadAntagonist(option);
                        antagonist = option;
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }

                    break;
                }

            case StoryState.NavigateAntagonist:
                {
                    
                    controller.updateDialogue("Surprised by the ferocious beast, Spud quickly _______ the " + option + ".");

                    // TODO: Use UI inputs instead of key presses
                    if (option == "Approach")
                    {
                        currentStoryState = StoryState.EndingEaten;
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }
                    else if (option == "Avoid")
                    {
                        StartCloseCurtainState();
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }

                    break;
                }

            case StoryState.EndingEaten:
                {
                    controller.updateDialogue("YOU DIED");
                    // TODO: Trigger ending and credits sequence
                    break;
                }

            case StoryState.Counter:
                {
                    // TODO: Load counter scene, continue story
                    break;
                }

            case StoryState.CloseCurtains:
                {
                    float stateDurationSeconds = 5;
                    if (elapsedSecondsInState < stateDurationSeconds)
                    {
                        break;
                    }

                    // Define antagonist after opening the door
                    if (doorOpened)
                    {
                        DefineAntagonist();
                    }

                    break;
                }

            case StoryState.OpenCurtains:
                {
                    float stateDurationSeconds = 5;
                    if (elapsedSecondsInState >= stateDurationSeconds)
                    {
                        break;
                    }

                    // Turn off door-open sequence after curtains re-open
                    if (doorOpened)
                    {
                        StartNavigateAntagonist();
                        stateStartTime = Time.time;
                        doorOpened = false;
                    }

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
        }
        else
        {
            controller.sendOptions(new List<string> { "Walk Around", "Turn On Lights", "Go to sleep" });
        }
        currentStoryState = StoryState.Beginning;
    }

    void StartTurnOnLightState()
    {
        lightingAnimator.SetTrigger("playTurnOnTopLeftLight");
        currentStoryState = StoryState.TurnOnLight;
        lightsOn = true;
    }

    void StartTurnOffLightState()
    {
        lightingAnimator.SetTrigger("playTurnOffTopLeftLight");
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
        doorOpened = true;
    }

    void DefineAntagonist()
    {
        controller.updateDialogue("McCringle finds a tremendous _______ as the pantry door swings open...");
        controller.sendOptions(new List<string> { "Dog", "Hippo", "Kraken" });
        LoadAntagonist(option);
        antagonist = option;
        stateStartTime = Time.time;
        if (antagonist != null)
        {
            StartOpenCurtainState();
            currentStoryState = StoryState.OpenCurtains;
        }
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

        
    }

    void StartNavigateAntagonist()
    {
        controller.sendOptions(new List<string> { "Approach", "Avoid" });
        currentStoryState = StoryState.NavigateAntagonist;
    }

    void handleOption(string option)
    {
        this.option = option;
    } 

    void clearOptions()
    {
        option = null;
        controller.sendOptions(new List<string>());
    }
   
    void StartCloseCurtainState()
    {
        Debug.Log("Closing curtains. Press 5 to open curtains");
        curtainsAnimator.SetTrigger("playCloseCurtains");
        lightingAnimator.SetTrigger("playTurnOnFrontLights");
        currentStoryState = StoryState.CloseCurtains;
    }

    void StartOpenCurtainState()
    {
        curtainsAnimator.SetTrigger("playOpenCurtains");
        lightingAnimator.SetTrigger("playTurnOffFrontLights");
        currentStoryState = StoryState.OpenCurtains;
    }
}
