using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private enum TutorialState
    {
        TEXTBOX, // This is the state until the user left clicks to close the text box
        PLAYING // This is the state while there is no textbox on the screen
    }

    private GameObject textbox;
    private TutorialState tutorialState;
    private int tutorialProgress;

    private void Start()
    {
        textbox = transform.FindChild("Textbox").gameObject;

        tutorialState = TutorialState.TEXTBOX;
        tutorialProgress = 0;
    }

    private void Update()
    {
        switch(tutorialState)
        {
            case TutorialState.TEXTBOX:
                if (Input.GetMouseButtonDown(0))
                {
                    // Close textbox
                    Advance();
                }
                break;

            case TutorialState.PLAYING:
                break;

            default:
                break;
        }
    }

    private void Advance()
    {
        tutorialProgress++;
    }
}
