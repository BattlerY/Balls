using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreator : MonoBehaviour
{
    #region ForInspector
    [Header("Obj")]
    [SerializeField] private GameObject BallPrefab;
    [Header("Transform")]
    [SerializeField] private Transform PoolTransform;
    [SerializeField] private Transform BallsTransform;
    [Header("Offset")]
    [SerializeField] private float LeftOffset;
    [SerializeField] private float RightOffset;
    [SerializeField] private float TopOffset;
    [Header("Time")]
    [SerializeField] private float MinTime;
    [SerializeField] private float MaxTime;
    [Header("Points")]
    [SerializeField] private int MinPoints;
    [SerializeField] private int MaxPoints;
    [Header("Damage")]
    [SerializeField] private int MinDamage;
    [SerializeField] private int MaxDamage;
    [Header("Velosity")]
    [SerializeField] private float MinVelosity;
    [SerializeField] private float MaxVelosity;
    [Header("Acceleration")]
    [SerializeField] private int PointsForAcceleration;
    [SerializeField] private float Acceleration;
    [Header("Particle")]
    [SerializeField] private ParticleSystem ParticleSystem;
    #endregion

    private Stack<GameObject> BallPool;
    private RectTransform RT;
    private float LeftX;
    private float RightX;
    private float TopY;

    private void Start()
    {
        RT = GetComponent<RectTransform>();
        LeftX = RT.rect.xMin + LeftOffset;
        RightX = RT.rect.xMax + RightOffset;
        TopY = RT.rect.yMax + TopOffset;
        BallPool = new Stack<GameObject>();
        NewStart();
    }
    private IEnumerator Create()
    {
        float speed = Acceleration * (int)(GameMan.CurentResult / PointsForAcceleration);

        int damage = Random.Range(MinDamage, MaxDamage);
        int points = Random.Range(MinPoints, MaxPoints);
        float velosity = Random.Range(MinVelosity + speed, MaxVelosity + speed);
        Vector2 position = new Vector2(Random.Range(LeftX, RightX), TopY);
        Vector4 color = new Vector4(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 1);

        GameObject go;
        if (BallPool.Count > 0)
        {
            go = BallPool.Pop();
            go.SetActive(true);
            go.transform.parent = BallsTransform;
        }
        else
            go = Instantiate(BallPrefab, BallsTransform);
        
        go.GetComponent<Ball>().Instantiate(color, velosity, position, points, damage, this);

        yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));
        StartCoroutine(Create());
    }
    public void ToPool(Ball ball)
    {
        var a  = Instantiate(ParticleSystem, this.transform);
        a.transform.position = ball.transform.position;
        a.startColor = ball.GetColor;

        ball.transform.parent = PoolTransform;
        ball.gameObject.SetActive(false);
        BallPool.Push(ball.gameObject);
    }
    public void NewStart()
    {
        GameMan.Recalculate();

        for (int i = BallsTransform.childCount-1; i>=0; i--)
        {
            if (BallsTransform.GetChild(i).TryGetComponent<Ball>(out Ball ball))
            {
                ToPool(ball);
            }
        }

        StopAllCoroutines();
        StartCoroutine(Create());
    }
        
}
