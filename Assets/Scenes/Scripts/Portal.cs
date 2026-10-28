using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject endPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            //if (!other.GetComponentInChildren<Grab>().isGrabbingSomething)
            //{
            //    other.gameObject.transform.position = endPoint.transform.position;
            //}      
            CharacterController charC = other.GetComponent<CharacterController>();

            if (other.GetComponentInChildren<Grab>() != null)
            {
                other.GetComponentInChildren<Grab>().DropObjectOnTeleport();
            }            

            charC.enabled = false;
            other.gameObject.transform.position = endPoint.transform.position;
            charC.enabled = true;
        }
        else if (other.gameObject.CompareTag("Grabbable"))
        {
            //other.gameObject.GetComponent<ObjectGrabbable>().Drop();
            //other.gameObject.transform.position = endPoint.transform.position;
        }
        else
        {
            //other.gameObject.transform.position = endPoint.transform.position;
        }
    }
}
