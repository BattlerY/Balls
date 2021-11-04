using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private Image Img;

    private BallCreator BallCreator;
    private int Points, Damage;
    public int GetDamage => Damage;
    public int GetPoints => Points;
    public Vector4 GetColor => Img.color;

    public void Instantiate(Vector4 color, float velosity, Vector2 position, int points, int damage, BallCreator ballCreator)
    {
        transform.localPosition = position;
        Rb.velocity = new Vector2(0, -velosity);
        Img.color = color;
        Points = points;
        Damage = damage;
        BallCreator = ballCreator;
    }
    private void OnMouseDown()
    {
        if (GameMan.GameMode)
        {
            GameMan.ChangePoints(GetPoints);
            BallCreator.ToPool(this);
        }
    }

}
