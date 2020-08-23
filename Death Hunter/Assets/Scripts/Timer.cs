using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
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

    public bool AtInterval(float interval)
    {
        int prevDiv = (int)(previousFrame / interval);
        int currDiv = (int)(currentFrame / interval);
        return prevDiv != currDiv;
    }

    public int TotalIntervals(float interval)
    {
        int prevDiv = (int)(previousFrame / interval);
        int currDiv = (int)(currentFrame / interval);
        return currDiv - prevDiv;
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

    public float GetCurrentTime()
    {
        return currentFrame;
    }
}
