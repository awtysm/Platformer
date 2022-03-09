using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//originates from popitochka player model
public class CameraController2D : MonoBehaviour
{
    float interpVelocity;
    public GameObject target;
    [HideInInspector]public Vector3 offset;
    Vector3 targetPosition;
    float CameraLerpTime = 0.25F;
    public float CameraSpeed = 40F;

    float CamX, CamY;
    
    void Start ()
        {
            targetPosition = transform.position;
            offset.x = 0; //can be manipulated to move camera around the target 
            offset.y = 0; //can be manipulated to move camera around the target
            offset.z = 0; //do not touch it
        }

    void FixedUpdate () 
        {
            CamX = Mathf.Clamp(Input.GetAxisRaw("Mouse X"), -1f, 1f);
            CamY = Mathf.Clamp(Input.GetAxisRaw("Mouse Y"), -1f, 1f);


            offset.x = CamX;
            offset.y = CamY;
            Vector3 StaticOffset = transform.position;
            StaticOffset.z = target.transform.position.z;
            Vector3 targetDirection = (target.transform.position - StaticOffset);

            interpVelocity = targetDirection.magnitude * CameraSpeed;
            targetPosition = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 
            transform.position = Vector3.Lerp( transform.position, targetPosition + offset, CameraLerpTime);
        }
}
