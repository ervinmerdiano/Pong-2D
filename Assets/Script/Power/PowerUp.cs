using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerType { ChangeDirection, DoubleScore, SpeedUp }
    public PowerType powerType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            BallController ballCtrl = collision.GetComponent<BallController>();
            if (ballCtrl == null) return;

            switch (powerType)
            {
                case PowerType.ChangeDirection:
                    ballCtrl.ReverseDirection();
                    Destroy(gameObject);
                    break;

                case PowerType.DoubleScore:
                    ballCtrl.doubleScoreActive = true;
                    Destroy(gameObject);
                    break;

                case PowerType.SpeedUp:
                    ballCtrl.SpeedUp();
                    Destroy(gameObject);
                    break;
            }

            Debug.Log("Power-Up collected: " + powerType);
        }
    }
}
