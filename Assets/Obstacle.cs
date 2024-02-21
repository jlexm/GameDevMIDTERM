using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool canDestroyOffScreen = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Slow down the player directly from PlayerController script
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SlowDownPlayer();
            }

            // Destroy the obstacle GameObject
            Destroy(transform.parent.gameObject); // Destroy the parent GameObject
        }
    }

    private void Update()
    {
        if (!canDestroyOffScreen && IsInCameraView())
        {
            canDestroyOffScreen = true;
        }
        if (canDestroyOffScreen && !IsInCameraView())
        {
            Destroy(gameObject);
        }
    }

    private bool IsInCameraView()
    {
        Bounds bounds = GetComponent<Renderer>().bounds;
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), bounds);
    }
}
