using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
   [SerializeField] private BallCreator BallCreator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Ball>(out Ball ball))
        {
            GameMan.ChangeHp(-ball.GetDamage);
            BallCreator.ToPool(ball);
        }
    }



}
