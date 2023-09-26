using System;
using System.Collections;
using UnityEngine;


public static class Timer
{

    public static IEnumerator Countdown(Action alarm, int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            --counter;
        }
        alarm();
    }

    public static IEnumerator Countdown(Action alarm, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        alarm();
    }
}