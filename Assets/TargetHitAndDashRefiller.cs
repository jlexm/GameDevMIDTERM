using System.Collections;
using UnityEngine;


public class TargetHitAndDashRefiller : MonoBehaviour
{
    private Animator anim;
    private bool isHitInProgress = false;

    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void HandleHitAndRefillDash()
    {
        if (!isHitInProgress)
        {
            isHitInProgress = true;
            anim.SetBool("isHit", true);
            StartCoroutine(DestroyAfterAnimation());
            FindObjectOfType<PlayerController>().RefillDashCharges();
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(1f);
        isHitInProgress = false;
        Destroy(gameObject);
    }
}