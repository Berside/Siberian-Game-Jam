using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothPlayerFollow : MonoBehaviour
{
    [SerializeField]
    protected Vector3 offset;

    [SerializeField]
    protected float damping;

    private Transform target;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}
