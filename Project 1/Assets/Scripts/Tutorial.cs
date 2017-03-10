using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public PickUp pickupScript;
    public ColliderDetector InSuitcaseColliderDetector;

    public Image backgroundPanel;
    public float fadeInSpeed;
    public float fadeOutSpeed;
    public float alphaMax;

    private void Start()
    {
        StartCoroutine(BeginTutorial());
    }

    private IEnumerator BeginTutorial()
    {
        StartCoroutine(FadeInBackground());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MessageBox.ShowMessageBox("You're going to miss your plane! Pack up as much as you can within the time limit!");
        yield return MessageBox.WaitForSubmit();

        MessageBox.HideMessageBox();
        StartCoroutine(FadeOutBackground());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        while(!pickupScript.HoldingObject)
        {
            yield return 0;
        }

        StartCoroutine(FadeInBackground());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MessageBox.ShowMessageBox("You've picked up an item! Drop the item in the suitcase to pack it.");
        yield return MessageBox.WaitForSubmit();

        MessageBox.HideMessageBox();
        StartCoroutine(FadeOutBackground());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        while (InSuitcaseColliderDetector.ItemCount == 0 || pickupScript.HoldingObject)
        {
            yield return 0;
        }

        StartCoroutine(FadeInBackground());
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MessageBox.ShowMessageBox("One down! Pack up the rest of the items as quickly as possible!");
        yield return MessageBox.WaitForSubmit();

        MessageBox.HideMessageBox();
        StartCoroutine(FadeOutBackground());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        Color backgroundColor = new Color(backgroundPanel.color.r, backgroundPanel.color.g, backgroundPanel.color.b, alphaMax);

        backgroundPanel.color = backgroundColor;
        yield return 0;

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
