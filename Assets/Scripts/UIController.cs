using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class UIController : MonoBehaviour
{
    [Header("Base Dia")]
    [SerializeField] Transform gemparent;
    [SerializeField] Transform target;
    [SerializeField] TextMeshProUGUI diaText;
    [SerializeField] GameObject gemprefab;
    Queue<GameObject> gemqueue = new Queue<GameObject>();
    private int totalGem;
    [Header("On Complete")]
    [SerializeField] CanvasGroup canvasGroupOnComplete;
    [SerializeField] GameObject scrolingText;
    [SerializeField] GameObject scaleText;
    [SerializeField] GameObject againbtn;
    [SerializeField] TextMeshProUGUI multiperGem;
    [SerializeField] TextMeshProUGUI TotalGem;

    [Header("On GameOver")]
    [SerializeField] CanvasGroup GameOverCanvasGroup;
    [SerializeField] GameObject GameOverscaleText;
    [SerializeField] GameObject GameOveragainbtn;
    [Header("Game Start")]
    [SerializeField] GameObject GameStartPanel;
    [SerializeField] RectTransform GameStartCursor;
    private void Awake()
    {
        totalGem = 0;
        diaText.SetText(totalGem.ToString());
        for (int i = 0; i < 20; i++)
        {
            GameObject gem = Instantiate(gemprefab, gemparent);
            gem.transform.localPosition = new Vector3(0, 0, 0);
            gem.SetActive(false);
            gemqueue.Enqueue(gem);
        }
        GameStartCursor.DOAnchorPosX(-150, 2f).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnEnable()
    {
        Events.GemCollect += Events_GemCollect;
        Events.complateGame += Events_complateGame;
        Events.GameOver += Events_GameOver;
        Events.GameStart += Events_GameStart;
    }

   
    private void OnDisable()
    {
        Events.GemCollect -= Events_GemCollect;
        Events.complateGame -= Events_complateGame;
        Events.GameOver -= Events_GameOver;
        Events.GameStart -= Events_GameStart;
    }

    private void Events_GameStart()
    {
        GameStartPanel.SetActive(false);
    }

    private void Events_GameOver()
    {
        GameOverCanvasGroup.transform.gameObject.SetActive(true);
        
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1f);
        seq.Append(GameOverCanvasGroup.DOFade(1, 2f));
        seq.Append(GameOverscaleText.transform.DOScale(Vector3.one, 2f)).SetEase(Ease.OutCirc);
        seq.Append(GameOveragainbtn.transform.DOScale(Vector3.one, 2f));
    }

    private void Events_complateGame()
    {
        canvasGroupOnComplete.transform.gameObject.SetActive(true);
        multiperGem.text = "X"+GamaManager.Instance.gemMultipier.ToString();
        TotalGem.text = (totalGem * GamaManager.Instance.gemMultipier).ToString();
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1f);
        seq.Append(canvasGroupOnComplete.DOFade(1, 2f));
        seq.Append(scrolingText.transform.DOLocalMoveY(0, 2f));

        seq.Append(scaleText.transform.DOScale(Vector3.one, 2f));
        seq.Append(againbtn.transform.DOScale(Vector3.one, 2f));
    }

   

    private void Events_GemCollect(GemsArg obj)
    {
        GameObject gem = gemqueue.Dequeue();
        gem.SetActive(true);
        gem.transform.position = Camera.main.WorldToScreenPoint(obj.pos);
        gem.transform.DOMove(target.position, 1f).OnComplete(() => { gem.SetActive(false); gemqueue.Enqueue(gem); totalGem++; diaText.SetText(totalGem.ToString()); });

    }




}
