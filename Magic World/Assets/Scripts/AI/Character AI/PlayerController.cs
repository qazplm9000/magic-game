using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterAI
{

    public override ActionList GetActionList(CharacterManager character, CharacterController controller)
    {
        return new ActionList();
    }
    
}
