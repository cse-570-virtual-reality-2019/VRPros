using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputLaserPointerVR : MonoBehaviour
{

    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean interactUI;
    public float fireRate = 0.5f;

    public GameObject laserPrefab; // 1
    private GameObject laser; // 2
    private Transform laserTransform; // 3
    private Vector3 hitPoint; // 4

    private float timeDifference;
    // 1
    public Transform cameraRigTransform;
    // 2
    public GameObject teleportReticlePrefab;
    // 3
    private GameObject reticle;
    // 4
    private Transform teleportReticleTransform;
    // 5
    public Transform headTransform;
    // 6
    public Vector3 teleportReticleOffset;
    // 7
    public LayerMask teleportMask;
    // 8
    private bool shouldTeleport;


    // Start is called before the first frame update
    void Start()
    {
        timeDifference = Time.time;
        // 1
        laser = Instantiate(laserPrefab);
        // 2
        laserTransform = laser.transform;

        // 1
        reticle = Instantiate(teleportReticlePrefab);
        // 2
        teleportReticleTransform = reticle.transform;

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        // 1
        if (interactUI.GetState(handType))
        {
           

            // 2
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, teleportMask)
)
            {
                hitPoint = hit.point;
                if (Mathf.Abs(timeDifference - Time.time) >= fireRate)
                {
                    ShowLaser(hit);
                    timeDifference = Time.time;

               
                }
                // 1
                reticle.SetActive(true);
                // 2
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                // 3
                shouldTeleport = true;

            }
        }
        else // 3
        {
            laser.SetActive(false);
            reticle.SetActive(false);

        }
    



    }
    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
        // 3
        laserTransform.LookAt(hitPoint);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                hit.distance);
     
            Teleport(hit.transform.gameObject);
      
    }

    private void Teleport(GameObject obj)
    {
        // 1
        shouldTeleport = false;
        // 2
        reticle.SetActive(false);
        // 3
        //Vector3 difference = cameraRigTransform.position - headTransform.position;
        // 4
        //difference.y = 0;
        // 5
        //cameraRigTransform.position = hitPoint + difference;
        Debug.Log("Button pressed");
        if (obj.tag == "toggle")
        {
            Toggle toggle = obj.GetComponent<Toggle>();
            toggle.isOn = !toggle.isOn;
        }
        else
        {
            Button clickHandler = obj.GetComponent<Button>();
            clickHandler.onClick.Invoke();
        }
            //clickHandler.OnPointerClick(pointerEventData);

    }
}
