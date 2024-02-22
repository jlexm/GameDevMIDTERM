using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool canDestroyOffScreen = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            GameController.isGameOver = true;
            
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
