using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class JoystickModelRotator : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform joystickBackground;
    [SerializeField] private RectTransform joystickHandle;
    [SerializeField] private List<GameObject> targetModels; // List to hold multiple models
    [SerializeField] private float rotationSpeed = 5f;
    
    private Vector2 inputVector;
    private bool isDragging = false;
    private Canvas parentCanvas;
    
    private void Start()
    {
        // Get reference to the parent canvas
        parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas == null)
        {
            Debug.LogError("JoystickModelRotator: No Canvas found in parents!");
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        isDragging = true;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (joystickBackground == null || joystickHandle == null || parentCanvas == null)
            return;
        
        Vector2 position = RectTransformUtility.WorldToScreenPoint(null, joystickBackground.position);
        Vector2 radius = joystickBackground.sizeDelta / 2;
        
        // Use the instance reference to the canvas
        inputVector = (eventData.position - position) / (radius * parentCanvas.scaleFactor);
        
        // Clamp input to circle
        if (inputVector.magnitude > 1)
            inputVector = inputVector.normalized;
        
        // Move joystick handle
        joystickHandle.anchoredPosition = inputVector * radius;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset joystick position and input vector
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
        isDragging = false;
    }
    
    private void Update()
    {
        if (targetModels != null && targetModels.Count > 0 && isDragging)
        {
            float horizontalRotation = inputVector.x * rotationSpeed;
            float verticalRotation = inputVector.y * rotationSpeed;
            
            foreach (GameObject model in targetModels)
            {
                if (model != null)
                {
                    model.transform.Rotate(Vector3.up, -horizontalRotation, Space.World);
                    model.transform.Rotate(Vector3.right, verticalRotation, Space.World);
                }
            }
        }
    }
}
