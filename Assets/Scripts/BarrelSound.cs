using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSound : MonoBehaviour
{
    AudioSource mySource;
    [SerializeField] AudioClip mySound;
    void Start()
    {
        mySource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        mySource.PlayOneShot(mySound);
    }
}