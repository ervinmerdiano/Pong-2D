using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public bool isPlayer1 = true;
    public float speed = 8f;

    [Header("Movement Limits")]
    public Transform topLimit;
    public Transform bottomLimit;

    Rigidbody2D rb;
    float input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.gameStarted)
        {
            input = 0f;
            return;
        }

        if (isPlayer1)
        {
            input = 0f;
            if (Input.GetKey(KeyCode.W)) input = 1f;
            else if (Input.GetKey(KeyCode.S)) input = -1f;
        }
        else
        {
            if (!GameManager.Instance.IsMultiplayer())
            {
                input = 0f;
            }
            else
            {
                input = 0f;
                if (Input.GetKey(KeyCode.UpArrow)) input = 1f;
                else if (Input.GetKey(KeyCode.DownArrow)) input = -1f;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(0, input * speed);

        if (topLimit != null && bottomLimit != null)
        {
            float minY = bottomLimit.position.y;
            float maxY = topLimit.position.y;

            Vector3 pos = transform.position;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            transform.position = pos;
        }
    }
}
