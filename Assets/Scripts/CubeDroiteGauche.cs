using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeDroiteGauche : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _distance;
        
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime, transform.position.y, transform.position.z);

        if (transform.position.x > _distance || transform.position.x < -_distance || Input.GetKeyDown(KeyCode.Space)) _speed = -_speed;
        
    }
}
