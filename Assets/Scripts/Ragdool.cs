using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdool : MonoBehaviour
{
    [SerializeField] Rigidbody hips;
    Rigidbody[] rigibodys;
    Animator animator;
    Rigidbody rigibody;
    // Start is called before the first frame update
    void Awake()
    {
        rigibodys = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody>();
        DeActivateRagDoll();
    }
    private void OnEnable()
    {
        Events.GameOver += Events_GameOver;
        Events.complateGame += Events_CallComplateGame;

    }

    private void Events_CallComplateGame()
    {
        animator.SetTrigger("Dance");
    }

    private void OnDisable()
    {
        Events.GameOver -= Events_GameOver;
    }
    private void Events_GameOver()
    {
        ActivateRagDoll();
    }

    public void DeActivateRagDoll()
    {
        RigiBodyClosed();
        animator.enabled = true;
    }

    public void RigiBodyClosed()
    {
        foreach (var item in rigibodys)
        {
            if (item != rigibody)
            {
                item.GetComponent<Collider>().isTrigger = true;
                item.useGravity = false;
                item.isKinematic = true;
                item.freezeRotation = true;
                item.constraints = RigidbodyConstraints.FreezeAll;
            }

        }
    }

    public void ActivateRagDoll()
    {
        RigiBodyOpen();

        animator.enabled = false;
    }

    public void RigiBodyOpen()
    {
        foreach (var item in rigibodys)
        {
            if (item != rigibody)
            {
                item.useGravity = true;
                item.isKinematic = false;
                item.constraints = RigidbodyConstraints.None;
                item.GetComponent<Collider>().isTrigger = false;
                item.useGravity = true;
            }
        }
        GetComponent<Collider>().enabled = false;
        Destroy(rigibody);
        transform.SetParent(null);
    }

    public void ApplyForce(Vector3 force)
    {
        //var rigibody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();

        hips.AddForce(force, ForceMode.VelocityChange);


    }
}
