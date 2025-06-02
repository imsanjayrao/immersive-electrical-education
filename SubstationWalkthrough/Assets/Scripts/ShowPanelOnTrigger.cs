using UnityEngine;

public class ShowPanelOnTrigger : MonoBehaviour
{
    public GameObject panel; // Assign the UI Panel in Inspector

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false); // Start hidden
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false);
        }
    }
}
