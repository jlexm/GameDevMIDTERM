using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;
    public float jumpVelocity = 20;
    public float maxXVelocity = 100;
    public float groundHeight = -5;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;

    public int maxDashCharges = 3;
    public int currentDashCharges;
    public float dashSpeed = 50f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;
    public bool isDashing = false;
    private bool canDash = true;

    public float jumpGroundThreshold = 1;
    private Animator anim;
    private Spawner spawner;

    private void Start()
    {
        currentDashCharges = maxDashCharges;
        anim = GetComponent<Animator>();
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("isJumping", true);
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && currentDashCharges > 0)
        {
            StartCoroutine(Dash());
            currentDashCharges--;
        }

        if (Mathf.FloorToInt(distance) % 200 == 0 && Mathf.FloorToInt(distance) > 0)
        {
            spawner.UpdateObstacleSpawnTime(distance);
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        distance += velocity.x * Time.fixedDeltaTime;
        if (distance % 10 == 0)
        {
            maxXVelocity += 10;
        }

        if (isGrounded)
        {
            anim.SetBool("isJumping", false);
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }
        }

        transform.position = pos;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        Vector2 savedVelocity = velocity;
        velocity.x = dashSpeed;
        anim.SetBool("isDashing", true);
        yield return new WaitForSeconds(dashDuration);
        anim.SetBool("isDashing", false);   
        velocity = savedVelocity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void RefillDashCharges()
    {
        if (currentDashCharges < maxDashCharges)
        {
            currentDashCharges++;
        }
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }
}