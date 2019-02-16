using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldUpdate : ScriptableObject
{

    public abstract void StartWorld(World world);
    public abstract void UpdateWorld(World world);
    public abstract void EndWorld(World world);

}
