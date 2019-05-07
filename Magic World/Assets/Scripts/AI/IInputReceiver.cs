using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputReceiver
{

    void ReceiveInput(Vector2 direction, CharacterAction action);

    bool InputIsAllowed(CharacterAction action);
}
