using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Combatant target;
    public GameObject focusPoint;
    
    public float offsetHeight = 2;
    public float minDistance = 2;
    public float maxDistance = 5;
    public float speed = 5;
    [Range(0,1)]
    public float lerpFactor = 0.3f;

    public float maxHeight = 3;
    public float minHeight = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowTargetHorizontally();
        MoveAwayFromTarget();
        SetCameraHeight();
        LookAtTarget();
        MoveVertically();
    }
    


    private void FollowTargetHorizontally()
    {
        Vector3 distanceVec = focusPoint.transform.position - transform.position;
        float distance = distanceVec.magnitude;
        if(distance > maxDistance)
        {
            distanceVec.y = 0;
            distanceVec.Normalize();
            float minDistance = distance - maxDistance;
            Vector3 movementVec = distanceVec * speed * Time.deltaTime;
            float movementDist = movementVec.magnitude;
            if(minDistance < movementDist)
            {
                movementVec = distanceVec * minDistance;
            }
            transform.position = Vector3.Lerp(transform.position, 
                                        transform.position + movementVec,
                                        lerpFactor);
        }
    }

    private void MoveAwayFromTarget()
    {
        Vector3 distanceVec = focusPoint.transform.position - transform.position;
        float distance = distanceVec.magnitude;
        if (distance < minDistance)
        {
            Vector3 movementVec = -transform.forward;
            movementVec.y = 0;
            movementVec.Normalize();
            movementVec *= Time.deltaTime * speed;
            transform.position = Vector3.Lerp(transform.position,
                                        transform.position + movementVec,
                                        lerpFactor);
        }
    }

    private void MoveVertically()
    {
        if (Input.GetKey(KeyCode.Keypad8))
        {
            offsetHeight += Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            offsetHeight -= Time.deltaTime * speed;
        }

        offsetHeight = Mathf.Clamp(offsetHeight, minHeight, maxHeight);
    }

    private void MoveHorizontally()
    {
        if (Input.GetKey(KeyCode.Keypad4))
        {
            
        }
    }

    private void SetCameraHeight()
    {
        transform.position = new Vector3(transform.position.x, focusPoint.transform.position.y + offsetHeight, transform.position.z);
    }

    private void LookAtTarget()
    {
        Combatant targetsTarget = target.GetCurrentTarget();
        if (targetsTarget != null)
        {
            transform.LookAt(targetsTarget.transform);
        }
        else
        {
            transform.LookAt(focusPoint.transform);
        }
    }
}
