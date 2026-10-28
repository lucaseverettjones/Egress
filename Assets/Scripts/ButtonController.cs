using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    Animator anim;
    [SerializeField] Animator[] connectedAnims;
    AudioSource playerSource;
    [SerializeField] string activated;
    [SerializeField] string deactivated;
    [SerializeField] AudioClip sink;
    [SerializeField] AudioClip rise;
    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;
    bool isWaiting;
    AudioSource audio;
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        playerSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if(!isWaiting && other.gameObject.CompareTag("Grabbable") && !anim.GetCurrentAnimatorStateInfo(0).IsName("PillarSink"))
        {
            other.transform.parent = transform;
            StartCoroutine(PlayAnim("PillarSink",activated,sink,open));
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (!isWaiting && other.gameObject.CompareTag("Grabbable") && !anim.GetCurrentAnimatorStateInfo(0).IsName("PillarRise"))
        {
            other.transform.SetParent(other.transform);
            StartCoroutine(PlayAnim("PillarRise",deactivated,rise,close));
        }
    }
    IEnumerator PlayAnim(string toPlay, string toTrigger, AudioClip sound, AudioClip connectedSound)
    {
        isWaiting = true;
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        anim.Play(toPlay);
        audio.PlayOneShot(sound);
        playerSource.PlayOneShot(connectedSound);
        if(connectedAnims.Length > 0)
        {
            for (int i = 0; i < connectedAnims.Length; i++)
            {
                connectedAnims[i].Play(toTrigger);
            }
        }
        isWaiting = false;
    }
}
