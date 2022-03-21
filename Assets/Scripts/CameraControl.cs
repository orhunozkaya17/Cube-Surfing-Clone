using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraControl : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] Transform cameraBodyFollow;
    private bool isRotating;

    private void OnEnable()
    {
        Events.complateGame += Events_complateGame;
    }

    private void Events_complateGame()
    {
        isRotating = true;
    }

    void Update()
    {
        if (isRotating)
        {
            cameraBodyFollow.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }



}
