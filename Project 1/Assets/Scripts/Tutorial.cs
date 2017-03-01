using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image backgroundPanel;
    public float fadeInSpeed;
    public float fadeOutSpeed;
    public float alphaMax;

    private enum TutorialState
    {
        TEXTBOX, // This is the state until the user left clicks to close the text box
        PLAYING // This is the state while there is no textbox on the screen
    }

    private TutorialState tutorialState;
    private int tutorialProgress;

    private void Start()
    {
        StartCoroutine(FadeInBackground());

        tutorialState = TutorialState.TEXTBOX;
        tutorialProgress = 0;

        StartCoroutine(BeginTutorial());
    }

    private void Update()
    {
    }

    private IEnumerator BeginTutorial()
    {
        MessageBox.ShowMessageBox("You're going to miss your plane! Pack up as much as you can within the time limit!");
        yield return MessageBox.WaitForSubmit();

        MessageBox.HideMessageBox();
        Advance();

        tutorialState = TutorialState.PLAYING;
        StartCoroutine(FadeOutBackground());
    }

    private void Advance()
    {
        tutorialProgress++;
    }

    private IEnumerator FadeInBackground()
    {
        Color backgroundColor = new Color(backgroundPanel.color.r, backgroundPanel.color.g, backgroundPanel.color.b, 0);

        backgroundPanel.color = backgroundColor;
        yield return 0;

        while(backgroundColor.a < alphaMax)
        {
            backgroundColor.a += Time.deltaTime * fadeInSpeed;
            backgroundPanel.color = backgroundColor;
            yield return 0;
        }

        backgroundColor.a = alphaMax;
        backgroundPanel.color = backgroundColor;
    }

    private IEnumerator FadeOutBackground()
    {
        Debug.Log("Coroutine started");
        Color backgroundColor = new Color(backgroundPanel.color.r, backgroundPanel.color.g, backgroundPanel.color.b, alphaMax);

        backgroundPanel.color = backgroundColor;
        yield return 0;

        Debug.Log(backgroundColor.a);
        while (backgroundColor.a > 0)
        {
            backgroundColor.a -= Time.deltaTime * fadeOutSpeed;
            backgroundPanel.color = backgroundColor;
            yield return 0;
        }

        backgroundColor.a = 0;
        backgroundPanel.color = backgroundColor;
    }
}
