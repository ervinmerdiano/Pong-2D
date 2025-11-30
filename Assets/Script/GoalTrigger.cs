using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public bool isLeftGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (isLeftGoal)
            {
                GameManager.Instance.AddScoreRight();
            }
            else
            {
                GameManager.Instance.AddScoreLeft();
            }
        }
    }
}
