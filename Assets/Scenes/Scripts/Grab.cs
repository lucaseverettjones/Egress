using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform objectGrabPointTransform;

    private ObjectGrabbable objectGrabbable;

    private float grabLength = 10.0f;

    public bool isGrabbingSomething = false;
    private bool isGrabbingTurret = false;

    private GameObject turret;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootRay();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if ((objectGrabbable != null))
            {
                objectGrabbable.Drop();
                isGrabbingSomething = false;
                isGrabbingTurret = false;
            }
        }

        if(isGrabbingTurret == true)
        {
            StartCoroutine(shootTurret());
        }
    }

    void ShootRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabLength))
        {
            if (hit.transform.gameObject.CompareTag("Grabbable") && hit.transform.TryGetComponent(out objectGrabbable))
            {
                objectGrabbable.Grab(objectGrabPointTransform);
                isGrabbingSomething = true;

                if(hit.transform.gameObject.name == "Turret")
                {
                    turret = hit.transform.gameObject;
                    isGrabbingTurret = true;
                }
            }
        }
    }

    public void DropObjectOnTeleport()
    {
        objectGrabbable.Drop();
    }

    IEnumerator shootTurret()
    {
        if (turret.GetComponentInChildren<Turret>().shotProjectile == false)
        {
            turret.GetComponentInChildren<Turret>().shotProjectile = true;
            StartCoroutine(turret.GetComponentInChildren<Turret>().Shoot());
        }
        yield return null;
    }
}
