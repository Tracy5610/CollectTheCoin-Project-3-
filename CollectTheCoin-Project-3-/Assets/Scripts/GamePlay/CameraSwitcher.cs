using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Array to hold different cameras
    private int currentCameraIndex = 0;

    public Camera firstPersonCamera; // The first-person camera
    private bool isFirstPersonView = false; // Track if first-person view is active

    private PlayerMovement playerMovement; // Reference to the player movement script

    void Start()
    {
        // Ensure only the first camera is enabled initially
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = (i == currentCameraIndex);
        }

        // Ensure first-person camera is disabled initially
        if (firstPersonCamera != null)
        {
            firstPersonCamera.enabled = false;
        }

        // Get the player movement script from the player object
        playerMovement = FindObjectOfType<PlayerMovement>();

        // Set the main camera in the player movement script
        if (playerMovement != null && cameras.Length > 0)
        {
            playerMovement.mainCamera = cameras[currentCameraIndex];
        }
    }

    void Update()
    {
        // Check for Shift key press to switch between cameras
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            SwitchCamera();
        }

        // Check for Tab key press to toggle first-person view
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleFirstPersonView();
        }
    }

    void SwitchCamera()
    {
        // Toggle between cameras
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Disable all cameras except the current one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = (i == currentCameraIndex);
        }

        // Disable first-person camera if it was active
        if (firstPersonCamera != null)
        {
            firstPersonCamera.enabled = false;
            isFirstPersonView = false;
        }

        // Update the main camera reference in the player movement script
        if (playerMovement != null)
        {
            playerMovement.mainCamera = cameras[currentCameraIndex];
        }
    }

    void ToggleFirstPersonView()
    {
        if (firstPersonCamera != null)
        {
            // Toggle first-person view
            isFirstPersonView = !isFirstPersonView;

            // Enable or disable the first-person camera based on the current mode
            firstPersonCamera.enabled = isFirstPersonView;

            // Disable other cameras if first-person view is active
            if (isFirstPersonView)
            {
                for (int i = 0; i < cameras.Length; i++)
                {
                    cameras[i].enabled = false;
                }

                // Ensure the first-person camera is properly positioned
                firstPersonCamera.transform.SetParent(playerMovement.firstPersonCameraTransform);
                firstPersonCamera.transform.localPosition = Vector3.zero;
                firstPersonCamera.transform.localRotation = Quaternion.identity;

                // Update the main camera reference in the player movement script
                if (playerMovement != null)
                {
                    playerMovement.mainCamera = firstPersonCamera;
                }
            }
            else
            {
                // Re-enable the current camera if first-person view is turned off
                cameras[currentCameraIndex].enabled = true;

                // Detach the first-person camera from the player
                firstPersonCamera.transform.SetParent(null);

                // Update the main camera reference in the player movement script
                if (playerMovement != null)
                {
                    playerMovement.mainCamera = cameras[currentCameraIndex];
                }
            }
        }
    }
}
