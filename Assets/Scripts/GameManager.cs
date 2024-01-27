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
        GoToSleep
    }

    public Animator lightingAnimator;
    public Animator potatoAnimator;

    private StoryState currentStoryState;

    private float stateStartTime;
        

    // Start is called before the first frame update
    void Start()
    {
        StartBeginningState();
        stateStartTime = Time.time;
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
                    if (Input.GetKey(KeyCode.Alpha1))
                    {
                        StartWalkAroundState();
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);

                    }
                    else if (Input.GetKey(KeyCode.Alpha2))
                    {
                        StartTurnOnLightState();
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }
                    else if (Input.GetKey(KeyCode.Alpha3))
                    {
                        StartTurnOffLightState();
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }
                    else if (Input.GetKey(KeyCode.Alpha4))
                    {
                        currentStoryState = StoryState.GoToSleep;
                        stateStartTime = Time.time;
                        Debug.Log("Transitioning to " + currentStoryState);
                    }

                    break;
                }
                
            case StoryState.WalkAround:
                {
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
                    float stateDurationSeconds = 5;

                    if (elapsedSecondsInState >= stateDurationSeconds)
                    {
                        StartBeginningState();
                        stateStartTime = Time.time;
                    }

                    break;
                }

            case StoryState.GoToSleep:
                {
                    float stateDurationSeconds = 5;

                    if (elapsedSecondsInState >= stateDurationSeconds)
                    {
                        StartBeginningState();
                        stateStartTime = Time.time;
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
        Debug.Log("Press 1 for walk around, 2 for lights on, 3 for lights off, 4 for sleep");
        currentStoryState = StoryState.Beginning;
    }

    void StartTurnOnLightState()
    {
        lightingAnimator.SetTrigger("playTurnOnLights");
        currentStoryState = StoryState.TurnOnLight;
    }

    void StartTurnOffLightState()
    {
        lightingAnimator.SetTrigger("playTurnOffLights");
        currentStoryState = StoryState.TurnOffLight;
    }

    void StartWalkAroundState()
    {
        potatoAnimator.SetTrigger("playPotatoWalkAround");
        currentStoryState = StoryState.WalkAround;
    }
}
