using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveInput : MonoBehaviour
{
    private float _LastFrameFingerPostionX;
    private float _movefactor;

    
    public float MoveFactor
    {
        get { return _movefactor; }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _LastFrameFingerPostionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _movefactor = Input.mousePosition.x - _LastFrameFingerPostionX;
            _LastFrameFingerPostionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _movefactor = 0f;
        }
        if (GamaManager.Instance.gameState==GameState.None)
        {
            if (_movefactor !=0)
            {
                Events.CallGameStart();
            }
        }
    }
}
