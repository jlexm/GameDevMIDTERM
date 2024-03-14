using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public float spawnInterval = 2f;
    public float targetDuration = 3f;
    public float spawnAreaWidth = 10f;
    public float spawnAreaHeight = 10f;

    private float timer;

    public AudioClip impact;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnTarget();
            timer = spawnInterval;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null && hit.collider.CompareTag("Target"))
            {
                audioSource.PlayOneShot(impact);
                TargetHitAndDashRefiller hitAndRefiller = hit.collider.gameObject.GetComponent<TargetHitAndDashRefiller>();
                if (hitAndRefiller != null)
                {
                    hitAndRefiller.HandleHitAndRefillDash();
                }
            }
        }
    }

    private void SpawnTarget()
    {
        Vector2 spawnAreaBounds = new Vector2(spawnAreaWidth / 2f, spawnAreaHeight / 2f);
        float randomX = Random.Range(-spawnAreaBounds.x, spawnAreaBounds.x);
        float randomY = Random.Range(-spawnAreaBounds.y, spawnAreaBounds.y);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0f) + transform.position;
        GameObject newTarget = Instantiate(targetPrefab, randomPosition, Quaternion.identity);
        Destroy(newTarget, targetDuration);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaWidth, spawnAreaHeight, 0f));
    }
}
