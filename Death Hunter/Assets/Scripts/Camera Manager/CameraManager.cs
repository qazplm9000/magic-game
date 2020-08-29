using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Combatant target;
    //public GameObject focusPoint;
    public Vector3 focusOffset;
    
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
        if (target.GetCurrentTarget() == null)
        {
            FollowTargetHorizontally();
            MoveAwayFromTarget();
            SetCameraHeight();
            MoveVertically();
        }
        else
        {
            StayBehindTarget();
        }
        LookAtTarget();
    }
    

    public void ChangeTarget(Combatant newTarget)
    {
        target = newTarget;
    }





    private void FollowTargetHorizontally()
    {
        Vector3 distanceVec = (target.transform.position + focusOffset) - transform.position;
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
        Vector3 distanceVec = (target.transform.position + focusOffset) - transform.position;
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

    private void StayBehindTarget()
    {
        transform.position = target.transform.position - target.transform.forward * maxDistance + target.transform.up * maxHeight;
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
        transform.position = new Vector3(transform.position.x, (target.transform.position + focusOffset).y + offsetHeight, transform.position.z);
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
            transform.LookAt((target.transform.position + focusOffset));
        }
    }
}
