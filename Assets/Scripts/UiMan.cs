using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMan : MonoBehaviour
{
    [SerializeField] private Button Stop, Restart;
    [SerializeField] private Transform Menu;
    [SerializeField] private Text CurentHp, CurentPoints;
    [SerializeField] private Text Last, Best, Curent;
    [SerializeField] private BallCreator BallCreator;
    [SerializeField] private Canvas Canvas;

    private void Awake()
    {
        Stop.onClick.AddListener(() => {
          
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            Menu.gameObject.SetActive(!Menu.gameObject.activeSelf);
            Last.text = GameMan.LastResult.ToString();
            Best.text = GameMan.BestResult.ToString();
            Curent.text = GameMan.CurentResult.ToString();
            Canvas.sortingOrder = Canvas.sortingOrder == -1 ? 0 : -1;
        });

        Restart.onClick.AddListener(() => { 
            BallCreator.NewStart();
            Time.timeScale = 1;
            Menu.gameObject.SetActive(false);
            Canvas.sortingOrder = -1;
            Stop.interactable = true;
        });
    }
    private void OnEnable()
    {
        GameMan.HPChanger += HpShower;
        GameMan.ResultChanger += ResultShower;
        GameMan.Death += DeathShower;
    }
    private void OnDisable()
    {
        GameMan.HPChanger -= HpShower;
        GameMan.ResultChanger -= ResultShower;
        GameMan.Death -= DeathShower;

    }
    private void HpShower()=> CurentHp.text=GameMan.CurentHP.ToString();
    private void ResultShower() => CurentPoints.text = GameMan.CurentResult.ToString();
    private void DeathShower()
    {
        Stop.onClick.Invoke();
        Stop.interactable = false;
    }
}
