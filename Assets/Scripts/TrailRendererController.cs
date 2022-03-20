using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererController : MonoBehaviour
{
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] LayerMask whatIsGround;

    RaycastHit raycastHit;
    private void FixedUpdate()
    {

        if (Physics.Raycast(transform.position+Vector3.up*0.2f, Vector3.down, out raycastHit, 0.5f, whatIsGround))
       
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }

    }
}
