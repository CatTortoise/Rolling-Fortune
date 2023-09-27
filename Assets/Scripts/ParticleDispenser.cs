using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDispenser : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Rigidbody _rigidbody;

    void Update()
    {
        gameObject.transform.position = _player.transform.position;
        Vector3 velocity = _rigidbody.velocity;
        Vector3 direction = velocity.normalized;
        Quaternion rotateTo = Quaternion.LookRotation(-direction, Vector3.up);
        gameObject.transform.rotation = rotateTo;
    }
}
