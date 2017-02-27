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

    private TutorialState tutorialState;
    private int tutorialProgress;

    private void Start()
    {
        tutorialState = TutorialState.TEXTBOX;
        tutorialProgress = 0;

        StartCoroutine(BeginTutorial());
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

    private IEnumerator BeginTutorial()
    {
        MessageBox.ShowMessageBox("You're going to miss your plane! Pack up as much as you can within the time limit! You're going to miss your plane! Pack up as much as you can within the time limit! You're going to miss your plane! Pack up as much as you can within the time limit!");
        yield return MessageBox.WaitForSubmit();

        MessageBox.HideMessageBox();
    }

    private void Advance()
    {
        tutorialProgress++;
    }
}
