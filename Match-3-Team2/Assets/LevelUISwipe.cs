using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUISwipe : MonoBehaviour
{
    //Its a 3D mesh element with some buttons that need to swipe up and down to see the next level buttons
    // its done for mobile devices to save screen space and make it more interactive

    public float swipeSpeed = 5f; // Speed of the swipe
    private Vector3 initialPosition; // Initial position of the UI element
    private Vector3 targetPosition; // Target position for the swipe
    private bool isSwiping; // Flag to indicate if the UI element is currently swiping
    
    
    void Start()
    {
        // Store the initial position of the UI element
        initialPosition = transform.position;
        targetPosition = initialPosition; // Start with the target position as the initial position
        isSwiping = true; 
    }

    void Update()
    {
        //it drags with the mouse/finger
        //also moves with the mouse direction, so if you swipe up it moves up and if you swipe down it moves down
        if (isSwiping)
        {
            if (Input.GetMouseButton(0)) // Check for mouse button down (or touch input)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10f; // Set a fixed distance from the camera
                targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            }

            // Smoothly move the UI element towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, swipeSpeed * Time.deltaTime);
        }
    }
}
