using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothPlayerFollow : MonoBehaviour
{
    [SerializeField]
    protected Transform target;

    [SerializeField]
    protected Vector3 offset;

    [SerializeField]
    protected float damping;

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}
