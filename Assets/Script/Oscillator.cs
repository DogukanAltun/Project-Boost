using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] private float Period = 2f;
    [SerializeField] private Vector3 MovementVector;
    private float MoveDistance;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Period == 0f)
        {
            return;
        }
        float cycles = Time.time / Period;
        const float Tau = Mathf.PI * 2f;
        MoveDistance = (Mathf.Sin(Tau * cycles)+1)/2;
        Vector3 offset = MovementVector * MoveDistance;
        transform.position = offset + startPosition;
    }
}
