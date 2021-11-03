
using UnityEngine.Events;

public class GameMan
{
    public const int MaxHP = 100;

    public static int BestResult;
    public static int LastResult;
    public static int CurentResult;
    public static int CurentHP;

    public static bool GameMode;

    public static UnityAction HPChanger;
    public static UnityAction ResultChanger;
    public static UnityAction Death;
    public static void ChangePoints(int i)
    {
        CurentResult += i;
        ResultChanger();
    }
    public static void ChangeHp(int i)
    {  
        CurentHP += i;
        if (CurentHP <= 0)
        {
            CurentHP = 0;
            HPChanger();
            Death();
        }
        else
            HPChanger();
    }
    public static void Recalculate()
    {
        CurentHP = MaxHP;
        if (CurentResult > BestResult) 
            BestResult = CurentResult;
        LastResult = CurentResult;
        CurentResult = 0;
        ResultChanger();
        HPChanger();
    }
}
