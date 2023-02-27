using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Walls : MonoBehaviour
{
    RaycastHit hit;
    Vector3 targetPosition;
    GameObject player;
    Transform playerTransform;
    Vector3 desiredPosition;
    LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = gameObject.transform;
        desiredPosition = transform.localPosition;
        layer = LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        playerTransform = gameObject.transform;

        if (Physics.Raycast(desiredPosition, fwd, out hit, desiredPosition.z, layer))
        {
            Debug.Log("Hitting");
            targetPosition = (hit.point - playerTransform.localPosition) * 0.8f + playerTransform.localPosition;
    /*
       Note that I move the camera to 80 % of the distance
               to the point where an obstruction has been found
       to help keep the sides of the frustrum from still clipping through the wall
    */
        }
        else
        {
            targetPosition = desiredPosition;
        }
        transform.localPosition = targetPosition;
    }

    private void OnTriggerEnter(Collider collision)
    {
        this.transform.localPosition = new Vector3(0.522f, 1.526f, 0.5f);
    }
}
