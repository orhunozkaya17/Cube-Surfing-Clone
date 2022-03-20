using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [SerializeField] Transform gemparent;
    [SerializeField] Transform target;
    [SerializeField] TextMeshProUGUI diaText;
    [SerializeField] GameObject gemprefab;
    Queue<GameObject> gemqueue = new Queue<GameObject>();

    private int totalGem;
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
    }
    private void OnEnable()
    {
        Events.GemCollect += Events_GemCollect;
    }
    private void OnDisable()
    {
        Events.GemCollect -= Events_GemCollect;
    }

    private void Events_GemCollect(GemsArg obj)
    {
        GameObject gem = gemqueue.Dequeue();
        gem.SetActive(true);
        gem.transform.position = Camera.main.WorldToScreenPoint(obj.pos);
        gem.transform.DOMove(target.position, 1f).OnComplete(() => { gem.SetActive(false); gemqueue.Enqueue(gem);totalGem++; diaText.SetText(totalGem.ToString()); });
    }




}
