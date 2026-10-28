using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets.Characters.FirstPerson;

public class Turret : MonoBehaviour
{
    private float speed = .05f;
    private Transform player;
    private float maxDis = 8f;
    public LineRenderer beam;
    private Transform turretHead;

    Color alphaRed = new Color(1, 0, 0, 0.2f);
    Color alphaGreen = new Color(0, 1, 0, .7f);

    bool isRotatingRight = true;
    bool isRotatingLeft = false;
    float maxRot = 45;
    float minRot = -45;

    [SerializeField] private GameObject fireBall;
    public bool shotProjectile = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        turretHead = this.gameObject.transform;
        beam.SetPosition(1, new Vector3 (0, 0, 3.5f));
        maxRot = turretHead.rotation.eulerAngles.y + 45f;
        minRot = turretHead.rotation.eulerAngles.y - 45;



    }

    // Update is called once per frame
    void Update()
    {
        TurretRotation();
        playerDetection();
    }

    private void TurretRotation()
    {
        float currentYRotation = turretHead.rotation.eulerAngles.y;
        currentYRotation = NormalizeAngle(currentYRotation);

        beam.startColor = alphaGreen;
        beam.endColor = alphaGreen;

        if (currentYRotation > maxRot)
        {
            isRotatingRight = false;
            isRotatingLeft = true;
            Debug.Log("Turning Left");
        }
        else if (currentYRotation < minRot)
        {
            isRotatingRight = true;
            isRotatingLeft = false;
            Debug.Log("Turning Right");
        }

        if (isRotatingRight == true)
        {
            turretHead.Rotate(Vector3.up * speed);
        }
        else if(isRotatingLeft == true)
        {
            turretHead.Rotate(-Vector3.up * speed);
        }
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 180)
            angle -= 360;
        while (angle < -180)
            angle += 360;
        return angle;
    }

    public IEnumerator Shoot()
    {
        yield return new WaitForSeconds(3);
        GameObject clone = Instantiate(fireBall, turretHead.transform.position,  this.gameObject.transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
        shotProjectile = false;
    }

    private void playerDetection()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, maxDis))
        {
            if (hit.collider.tag == "Player" && hit.distance <= maxDis)
            {
                turretHead.LookAt(player.position);
                beam.startColor = alphaRed;
                beam.endColor = alphaRed;

                if(shotProjectile == false)
                {
                    shotProjectile = true;
                    StartCoroutine(Shoot());
                }
            }
        }
    }
}
