using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public float speed = 6f; 
    public Transform ball;
    public Transform topLimit;
    public Transform bottomLimit;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (ball == null) return;

        float targetY = ball.position.y;
        float newY = Mathf.Lerp(transform.position.y, targetY, speed * Time.fixedDeltaTime);

        Vector3 pos = transform.position;
        pos.y = newY;

        pos.y = Mathf.Clamp(pos.y, bottomLimit.position.y, topLimit.position.y);

        transform.position = pos;
    }
}
