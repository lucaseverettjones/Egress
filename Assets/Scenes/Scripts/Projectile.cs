using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AudioSource sfx;
    [SerializeField] private AudioClip fire;
    [SerializeField] private AudioClip explosion;
    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        sfx.clip = fire;
        sfx.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(destroyFireball());
    }

    IEnumerator destroyFireball()
    {
        sfx.clip = explosion;
        sfx.Play();
        yield return new WaitForSeconds(1.536f);
        Destroy(this.gameObject);
    }
}
