using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ZoneManager : MonoBehaviour
{
    public TextMeshProUGUI zoneText;
    public TextMeshProUGUI descriptionText;
    public GameObject descriptionPanel;
    public TextMeshProUGUI promptText; // Add prompt text field
    public GameObject promptPanel;      // Add prompt panel container

    private Dictionary<string, string> zoneDescriptions = new Dictionary<string, string>()
    {
        { "ControlRoom", "The control room serves as the central nervous system of the substation.  Here, operators utilize sophisticated SCADA (Supervisory Control and Data Acquisition) systems and control panels to continuously monitor the flow of electricity, manage equipment status, and respond to any alarms or abnormal conditions.  Communication with other substations and the grid control center is also managed from here." },
        { "CircuitBreaker", "Circuit breakers are critical safety devices designed to automatically interrupt the flow of electricity in the event of a fault, such as a short circuit or overload.  This rapid disconnection protects valuable equipment from damage and helps prevent electrical fires. Circuit Breaker 1 safeguards a specific section of the substation." },
        { "CircuitBreaker2", "Similar to Circuit Breaker 1, this breaker provides essential protection for a different segment of the substation's electrical network.  The presence of multiple circuit breakers allows for selective isolation of faulted areas, minimizing disruption to the overall power supply." },
        { "CurrentTransformer", "Current transformers (CTs) play a vital role in measuring the current flowing through high-voltage conductors.  They provide a scaled-down, proportional representation of the current, which is safe for use by metering instruments, protection relays, and control systems.  Accurate current measurement is essential for billing, fault detection, and overall system monitoring." },
        { "DisconnectSwitch", "Disconnect switches, also known as isolators, are manually operated switches used to completely isolate sections of the substation for maintenance, repair, or testing.  They provide a visible break in the circuit, ensuring that workers are safe from electrical hazards when working on de-energized equipment. They are *not* designed to interrupt current flow under load." },
        { "LightningArrester", "Lightning arresters are crucial protective devices that shield the substation from the damaging effects of lightning strikes.  When a high-voltage surge occurs, the arrester diverts the excess current to the ground, preventing it from reaching and damaging sensitive equipment like transformers, circuit breakers, and control systems.  This helps maintain the reliability of the power supply." },
        { "OutgoingLines", "These are the high-voltage power lines that carry electricity away from the substation to the distribution network.  They represent the final stage of the transmission process, delivering power to local distribution substations, which then further reduce the voltage for use in homes, businesses, and industries." },
        { "Transformer", "The power transformer is a core component of the substation, responsible for reducing the high voltage of electricity received from transmission lines to a lower, safer voltage suitable for distribution to consumers.  This step-down process involves large, oil-filled transformers with complex cooling systems to dissipate the heat generated during operation.  It's a critical link between the high-voltage transmission grid and the lower-voltage distribution network." }
    };

    private Dictionary<string, string> zoneTitles = new Dictionary<string, string>()
    {
        { "ControlRoom", "Control Room" },
        { "CircuitBreaker", "Circuit Breaker 1" },
        { "CircuitBreaker2", "Circuit Breaker 2" },
        { "CurrentTransformer", "Current Transformer" },
        { "DisconnectSwitch", "Disconnect Switch" },
        { "LightningArrester", "Lightning Arrester" },
        { "OutgoingLines", "Outgoing Lines" },
        { "Transformer", "Transformer" }
    };

    private static string currentZone = ""; // Make currentZone static
    private bool showingDescription = false; // Track if description is showing

    private void Start()
    {
        descriptionPanel.SetActive(false); // Ensure description panel is hidden initially
        promptPanel.SetActive(false);      // Ensure prompt panel is hidden initially
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // No need to remove "Zone_" here, we'll do it in Update
            currentZone = gameObject.name;
            string zoneName = currentZone.Replace("Zone_", "");
            
            if (zoneTitles.ContainsKey(zoneName))
            {
                zoneText.text = zoneTitles[zoneName];
                // Show the prompt panel with instructions
                promptText.text = zoneTitles[zoneName];
                promptPanel.SetActive(true);
                showingDescription = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentZone = ""; // Reset current zone
            zoneText.text = "";
            descriptionText.text = "";
            descriptionPanel.SetActive(false);
            promptPanel.SetActive(false);
            showingDescription = false;
        }
    }

    private void Update()
    {
        // Only process if player is in a zone
        if (!string.IsNullOrEmpty(currentZone))
        {
            string zoneName = currentZone.Replace("Zone_", "");
            
            // When Enter is pressed, switch from prompt to description
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!showingDescription && zoneDescriptions.ContainsKey(zoneName))
                {
                    // Hide prompt, show description
                    promptPanel.SetActive(false);
                    descriptionText.text = zoneDescriptions[zoneName];
                    descriptionPanel.SetActive(true);
                    showingDescription = true;
                }
                else if (showingDescription)
                {
                    // Hide description, show prompt again
                    descriptionPanel.SetActive(false);
                    promptPanel.SetActive(true);
                    showingDescription = false;
                }
            }
        }
        else
        {
            // No zone, hide everything
            descriptionText.text = "";
            descriptionPanel.SetActive(false);
            promptPanel.SetActive(false);
            showingDescription = false;
        }
    }
}