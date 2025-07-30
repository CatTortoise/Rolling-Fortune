using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDispenser : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Rigidbody _rigidbody;

    void Update()
    {
        gameObject.transform.position = _target.transform.position;
        Vector3 velocity = _rigidbody.linearVelocity;
        Vector3 direction = velocity.normalized;
        Quaternion rotateTo = Quaternion.LookRotation(-direction, Vector3.up);
        gameObject.transform.rotation = rotateTo;
    }
}
