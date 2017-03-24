using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageBox : MonoBehaviour
{
    private static MessageBox Instance = null; // The singleton instance. Singletons establish a single instance and prevent that instance reference from being changed.

    private GameObject messageBox; // The canvas component that should be toggled to show/hide the message box.
    private Text message; // The text component in the message box.
    private Image messageBoxEndIcon; // The image that the textbox should enable when the message has finished.

    private int fontSize; // The size of the message font.
    private Color fontColor; // The color of the message.
    private Font font; // The font of the message.
    private FontStyle fontStyle; // The font style of the message (bold, italic, etc.).

    public float textSpeed = 30.0f; // The speed at which the message appears (Cannot be 0).
    public float fastTextSpeed = 120.0f; // The speed at which the message appears when the player is holding the confirm button (Cannot be 0).
    private float currentTextSpeed; // The current speed of the message.

    private int characterCount = 0; // The number od characters currently in the message.
    private bool messageFinished = false; // Set to true when the message has reached the end.

	/// <summary>
	/// The font size of the message text.
	/// </summary>
    public static int FontSize
    {
        get { return Instance.fontSize; }
        set { Instance.fontSize = value; }
    }

	/// <summary>
	/// The color of the message text.
	/// </summary>
    public static Color FontColor
    {
        get { return Instance.fontColor; }
        set { Instance.fontColor = value; }
    }

	/// <summary>
	/// The font of the message text.
	/// </summary>
    public static Font Font
    {
        get { return Instance.font; }
        set { Instance.font = value; }
    }

	/// <summary>
	/// The font style of the message text.
	/// </summary>
    public static FontStyle FontStyle
    {
        get { return Instance.fontStyle; }
        set { Instance.fontStyle = value; }
    }

	/// <summary>
	/// Whether or not the message box canvas is enabled.
	/// </summary>
    public static bool IsActive
    {
        get { return Instance.messageBox.activeSelf; }
    }

    private void Awake()
    {
        // If there is no other existing instance, set the Instance variable to this instance.
        // This is important because we can access the MessageBox public properties and methods
        // without having a reference to the MessageBox instance.
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called when the MessageBox game object is loaded.
    private void Start()
    {
		messageBox = transform.FindChild("Textbox").gameObject; // Assigns the Canvas component of this game object to the messageBox variable.
        message = GetComponentInChildren<Text>(); // Assigns the first instance of the Text component from the children of this game object to the message variable.
        messageBoxEndIcon = GameObject.FindGameObjectWithTag("Textbox End Icon").GetComponent<Image>(); // Saves the Image component of the game object with the tag "Textbox End Icon".

        // Sets all the font settings to the same as the text component.
        fontSize = message.fontSize;
        fontColor = message.color;
        font = message.font;
        fontStyle = message.fontStyle;
        
		currentTextSpeed = textSpeed; // Assigns the current text speed to the normal text speed by default.

        // Hides the message box by default
        Instance.messageBox.SetActive(false);
    }
    
    // Update is called once every frame.
    private void Update()
    {
        if (GameSettings.STATE == GameSettings.GAME_STATE.FROZEN) return;

		// If the submit button is being pressed down, set the current text speed to the fast text speed.
        if(Input.GetButtonDown("Submit"))
        {
			currentTextSpeed = fastTextSpeed;
        }
        
		// If the submit button is released, set the current text speed back to the regular text speed.
        if(Input.GetButtonUp("Submit"))
        {
			currentTextSpeed = textSpeed;
        }
    }
    
	/// <summary>
	/// Displays the message box with the given message and the given text attributes.
	/// </summary>
    public static void ShowMessageBox(string text, int fontSize, Color fontColor, Font font, FontStyle style)
    {
        GameSettings.STATE = GameSettings.GAME_STATE.PAUSED;
        Instance.messageBox.SetActive(true); // Shows the message box.
        Instance.messageBoxEndIcon.enabled = false; // Disables the message box end icon.

        // Sets all of the parameters to their appropriate instance variables.
        Instance.message.fontSize = fontSize;
        Instance.message.color = fontColor;
        if (font != null) // Only assign the font if a font is passed in.
        {
            Instance.message.font = font;
        }
        Instance.message.fontStyle = style;

		// Starts a coroutine that plays the message.
        Instance.StartCoroutine(Instance.PlayMessage(text));
    }

	/// <summary>
	/// Displays the message box with the given message and the default text attributes.
	/// </summary>
    public static void ShowMessageBox(string text)
    {
        ShowMessageBox(text, Instance.fontSize, Instance.fontColor, Instance.font, Instance.fontStyle);
    }

	/// <summary>
	/// Hides the message box from view.
	/// </summary>
    public static void HideMessageBox()
    {
        Instance.characterCount = 0;
        Instance.messageBoxEndIcon.enabled = false; // Hides the message box end icon.
        Instance.messageBox.SetActive(false); // Hides the message box.
        GameSettings.STATE = GameSettings.GAME_STATE.PLAYING;
        
    }

	/// <summary>
    /// Forces the message box to wait until the text is finished displaying and the player has pressed the submit button before advancing.
    /// </summary>
    public static IEnumerator WaitForSubmit()
    {
		// Loops while the message hasn't finished or while the submit button hasn't been pressed.
        while(!Input.GetButtonDown("Submit") || !Instance.messageFinished)
        {
			// If this method should continue looping, finish the frame and come back the next frame.
            yield return 0;
        }
    }

	/// <summary>
    /// Forces the message box to wait until the message has completely finished being displayed before advancing. This does not require the player to press anything to advance.
    /// </summary>
    public static IEnumerator WaitForMessageFinished()
    {
		// Loops while the message hasn't finished yet.
        while (!Instance.messageFinished)
        {
			// If this method should continue looping, finish the frame and come back the next frame.
            yield return 0;
        }
    }

    // Forces the message box to wait until the player presses the submit button when the message box has filled.
    // This is different from when the message has completely finished, this returns simply when the message box is full.
    // This is private because only the PlayMessage() method should call this.
    private static IEnumerator WaitForSubmitWhenFilled()
    {
        // Loops while the submit button hasn't been pressed.
        while (!Input.GetButtonDown("Submit"))
        {
            // If this method should continue looping, finish the frame and come back the next frame.
            yield return 0;
        }
    }

	// This method is responsible for displaying the message letter by letter to the message box.
    private IEnumerator PlayMessage(string msg)
    {
        message.text = ""; // Clears the text from the message box.
        characterCount = 0;

        // States that the message has not finished.
        messageFinished = false;

        int lastCharIndex = 0; // Defines the index of the previous character in the string.
        
		string[] words = msg.Split (' '); // Defines an array of words from the message passed in, using a space as the delimiter.

		// Loops through each word in the words array.
        foreach(string word in words)
        {
            while(GameSettings.STATE == GameSettings.GAME_STATE.FROZEN)
            {
                yield return null;
            }

			// Adds a word and a space to the message box text simply to see if the word will fit in the box.
            message.text += word + " ";

            // Adds to the character count (including spaces).
            characterCount += word.Length + 1;

            // "Refreshes" the text box to include the new characters in characterCountVisible
            Vector2 extents = message.rectTransform.rect.size;
            TextGenerationSettings settings = message.GetGenerationSettings(extents);
            message.cachedTextGenerator.Populate(message.text, settings);

            // Checks if there are characters visible in the message box and that there are more characters than currently visible.
            // Effectively checks if the message box needs to be refreshed to show the rest of the message.
            if (characterCount > message.cachedTextGenerator.characterCountVisible && message.cachedTextGenerator.characterCountVisible > 0)
			{
                message.text = message.text.Remove(lastCharIndex); // Removes the newly added word.

                Instance.messageBoxEndIcon.enabled = true; // Enables the message end icon.

                // Starts the WaitForSubmitWhenFilled coroutine, forcing the player to press the submit button before advancing with the rest of the message.
                yield return StartCoroutine(WaitForSubmitWhenFilled());
				
				// Once the player has pressed the submit button, display the rest of the message that didn't fit before.
                ShowMessageBox(msg.Substring(lastCharIndex));
				
				// End this coroutine.
                yield break;
			}
			else
			{
                message.text = message.text.Remove(lastCharIndex); // Removes the newly added word.
                characterCount -= word.Length + 1; // Removes the characters from the character count.

				// Loops through each character in the word to add the word character by character.
				foreach (char letter in word)
				{
					// The amount of time that the method must wait before adding the character to the text.
                    float txtSpeed = Time.realtimeSinceStartup + 1 / currentTextSpeed;
                    
					// Loops until the time required to wait is surpassed.
                    while (Time.realtimeSinceStartup < txtSpeed)
                    {
						// If this method should continue looping, finish the frame and come back the next frame.
                        yield return 0;
                    }
					
					// Adds the letter to the message
					message.text += letter;
                    characterCount++; // Increments the character count
				}
				
				// Adds a space to the end of the word.
				message.text += " ";
                characterCount++; // Increments the character count
            }

			// Sets the last character index in the message to the length of the message (This is not set to length - 1 because string.Remove
			// includes the starting index when removing, so to keep that character we effectively start at one character ahead).
            lastCharIndex = message.text.Length;
		}

		// The message has now been completely displayed.
        messageFinished = true;

        Instance.messageBoxEndIcon.enabled = true;
    }
}
