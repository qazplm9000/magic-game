using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Timer
{
    private float previousFrame = 0;
    private float currentFrame = 0;

    public void Tick()
    {
        previousFrame = currentFrame;
        currentFrame += Time.deltaTime;
    }

    /// <summary>
    /// Returns true if time is between previous and current frame
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool AtTime(float time)
    {
        return previousFrame <= time && currentFrame > time;
    }

    /// <summary>
    /// Returns true if the current frame is greater than time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool PastTime(float time)
    {
        return currentFrame > time;
    }
}
