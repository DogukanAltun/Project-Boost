using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    [SerializeField] private ParticleSystem Burning;
    [SerializeField] private ParticleSystem LeftBurning;
    [SerializeField] private ParticleSystem RightBurning;
    private Vector3 vector3;
    private float rotationthurst = 100f;
    [SerializeField] private float force = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Burning.Play();
            rb.AddRelativeForce(Vector3.up * force * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationthurst);
            RightBurning.Play();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationthurst);
            LeftBurning.Play();
        }
    }

    void ApplyRotation(float rotationthurst)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationthurst * Time.deltaTime);
        rb.freezeRotation = false;
    }
}

