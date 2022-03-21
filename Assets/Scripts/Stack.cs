using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stack : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] int closeTime;
    // Start is called before the first frame update
    void Start()
    {
        if (Canvas.activeSelf)
        {
            Canvas.transform.localScale = Vector3.zero;
            Canvas.transform.DOScale(Vector3.one, closeTime).SetEase(Ease.OutBounce).OnComplete(()=>Canvas.SetActive(false));
        }
    }

    //private IEnumerator CanvasClose()
    //{
    //    yield return new WaitForSeconds(closeTime);
    //    Canvas.SetActive(false);
    //}

    private void OnDisable()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            this.transform.SetParent(null);
            Events.CallObstacleHitEvent(this.gameObject);
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Water"))
        {
            this.transform.SetParent(null);
            Events.CallObstacleHitEvent(this.gameObject);
        }
        if (other.CompareTag("Gem"))
        {
            Events.CallScoreChangedEvent(new GemsArg() { pos = transform.position, gem = other.gameObject });
            other.gameObject.SetActive(false);
        }
    }
}
