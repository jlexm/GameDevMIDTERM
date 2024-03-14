using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    private bool canDestroyOffScreen = false;
    public AudioClip impact;
    AudioSource audioSource;

    private Animator anim;

     private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))      
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null && playerController.isDashing)
            {
                anim.SetBool("isHit", true);
                StartCoroutine(DestroyAfterAnimation());
            }
            else
            {
                anim.SetBool("isHit", true);
                StartCoroutine(DestroyAfterAnimation());
                GameController.isGameOver = true;
            }
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        audioSource.PlayOneShot(impact);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!canDestroyOffScreen && IsInCameraView())
        {
            canDestroyOffScreen = true;
        }
        if (canDestroyOffScreen && !IsInCameraView())
        {
           StartCoroutine(WaitForSound());
        }

    }   

    private bool IsInCameraView()
    {
        Bounds bounds = GetComponent<Renderer>().bounds;
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), bounds);
    }
}