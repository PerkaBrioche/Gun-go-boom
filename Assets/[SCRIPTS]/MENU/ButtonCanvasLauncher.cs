using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonCanvasLauncher : MonoBehaviour
{
    [System.Serializable]
    public class ButtonData
    {
        public Button button;
        public string buttonText;
        public string sceneToLoad;

        public bool QuitGame;

        public bool InputField;
        public TMP_InputField inputField;
    }

    [Header("List of Buttons")]
    public List<ButtonData> buttons = new List<ButtonData>();

    private Dictionary<Button, string> inputValues = new Dictionary<Button, string>();

    void Start()
    {
        foreach (ButtonData buttonData in buttons)
        {
            TextMeshProUGUI tmpText = buttonData.button.GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null)
            {
                tmpText.text = buttonData.buttonText;
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the button’s children.");
            }

            buttonData.button.onClick.RemoveAllListeners();

            if (buttonData.InputField && buttonData.inputField != null)
            {
                buttonData.button.onClick.AddListener(() =>
                {
                    string input = buttonData.inputField.text;

                    if (string.IsNullOrEmpty(input))
                    {
                        Debug.LogWarning("enter a valid number.");
                        return;
                    }

                    if (!int.TryParse(input, out int numericValue))
                    {
                        Debug.LogWarning("Invalid number");
                        return;
                    }

                    inputValues[buttonData.button] = input;

                    if (!string.IsNullOrEmpty(buttonData.sceneToLoad))
                    {
                        LoadScene(buttonData.sceneToLoad);
                    }
                });
            }
            else if (!string.IsNullOrEmpty(buttonData.sceneToLoad))
            {
                string sceneName = buttonData.sceneToLoad;
                buttonData.button.onClick.AddListener(() => LoadScene(sceneName));
            }

            if (buttonData.QuitGame)
            {
                buttonData.button.onClick.AddListener(() => Application.Quit());
            }
        }
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public string GetInputValue(Button button)
    {
        if (inputValues.TryGetValue(button, out string value))
        {
            return value;
        }
        return null;
    }
}
