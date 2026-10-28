using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadManager : MonoBehaviour
{
    [SerializeField] Animator[] activatedAnims;
    [SerializeField] Animator[] deactivatedAnims;
    [SerializeField] Animator finalAnim;
    AudioSource playerSource;
    [SerializeField] AudioClip sound;
    void Start()
    {
        playerSource = GameObject.Find("Player").GetComponent<AudioSource>();
        StartCoroutine("Check");
    }
    IEnumerator Check()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            bool correct = true;
            foreach(Animator anim in activatedAnims)
            {
                if(!anim.GetCurrentAnimatorStateInfo(0).IsName("PillarSink"))
                {
                    correct = false;
                }
            }
            foreach (Animator anim in deactivatedAnims)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("PillarSink"))
                {
                    correct = false;
                }
            }
            if(correct)
            {
                if(finalAnim.GetCurrentAnimatorStateInfo(0).IsName("Nothing"))
                {
                    finalAnim.Play("Open");
                    playerSource.PlayOneShot(sound);
                }
            }
        }
    }
}
