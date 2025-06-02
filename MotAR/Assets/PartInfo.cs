using UnityEngine;
using UnityEngine.UI;

public class PartInfo : MonoBehaviour {
    public string partDescription;
    public GameObject infoPanel;
    public Text infoText;

    void OnMouseDown() {
        infoPanel.SetActive(true);
        infoText.text = "Part: " + partDescription;
    }
}
