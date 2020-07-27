using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class StateManager : MonoBehaviour
{

    public bool isCasting = false;
    public bool canMove = true;
    public bool isGrounded = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCasting)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }
}
