using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModelSwitcher : MonoBehaviour
{
    public TMP_Dropdown dropdown;  // Use TMP Dropdown
    public GameObject[] models;    // Array for models
    public TMP_Text infoText;      // Single TMP text element
    public string[] textValues;    // Array of text descriptions
    public GameObject[] buttons;   // Array for the two buttons

    void Start()
    {
        dropdown.onValueChanged.AddListener(SwitchModel);
        SwitchModel(dropdown.value); // Initialize with the selected model, text, and buttons
    }

    void SwitchModel(int index)
    {
        // Activate only the selected model
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(i == index);
        }

        // Update the text based on the selected option
        infoText.text = textValues[index];

        // Show buttons only for the first option (index 0), hide otherwise
        bool showButtons = (index == 0);
        foreach (GameObject button in buttons)
        {
            button.SetActive(showButtons);
        }
    }
}
