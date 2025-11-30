using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Speed Settings")]
    public float initialSpeed = 8f;
    public float speedIncrease = 0.5f;
    public float maxSpeed = 15f;

    [Header("Power-Up Settings")]
    public float speedUpMultiplier = 2f;
    public float speedUpDuration = 5f;
    [HideInInspector] public bool doubleScoreActive = false;

    [Header("Collision Control")] //supaya tidak bug spam pantulan bola
    public float hitCooldown = 0.05f;
    private float lastHitTime = 0f;

    public Rigidbody2D rb;
    private Vector2 currentDirection;

    void Start()
    {
        rb.velocity = Vector2.zero;
    }

    public void OnGameStart()
    {
        LaunchBall();
    }

    public void ReverseDirection()
    {
        rb.velocity = -rb.velocity;
    }

    public void LaunchBall()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(-1f, 1f);

        currentDirection = new Vector2(x, y).normalized;

        rb.velocity = currentDirection * initialSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (Time.time - lastHitTime < hitCooldown) return;
            lastHitTime = Time.time;

            float dirX = collision.transform.position.x < transform.position.x ? 1 : -1;

            float relativeY = transform.position.y - collision.transform.position.y;
            float maxAngle = 0.8f;
            float dirY = Mathf.Clamp(relativeY, -maxAngle, maxAngle);

            Vector2 dir = new Vector2(dirX, dirY).normalized;

            Vector2 newVelocity = dir * (rb.velocity.magnitude + speedIncrease);

            if (newVelocity.magnitude > maxSpeed)
                newVelocity = newVelocity.normalized * maxSpeed;

            rb.velocity = newVelocity;
        }

        if (collision.collider.CompareTag("Goal_Left"))
        {
            GameManager.Instance.AddScoreRight();
            GameManager.Instance.GoalScored();
        }

        else if (collision.collider.CompareTag("Goal_Right"))
        {
            GameManager.Instance.AddScoreLeft();
            GameManager.Instance.GoalScored();
        }
    }

    public void SpeedUp()
    {
        StopAllCoroutines();
        StartCoroutine(SpeedUpRoutine());
    }

    IEnumerator SpeedUpRoutine()
    {
        rb.velocity *= speedUpMultiplier;

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;

        yield return new WaitForSeconds(speedUpDuration);

        rb.velocity /= speedUpMultiplier;

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
