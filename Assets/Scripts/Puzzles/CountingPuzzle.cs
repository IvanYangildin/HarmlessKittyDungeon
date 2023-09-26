using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountingPuzzle : MonoBehaviour
{
    [SerializeField]
    int currValue, finalValue;
    [SerializeField]
    // if true, then LostValue invokes only if current value becomes less
    bool notLess = false;

    int CurrValue => currValue;
    int FinalValue => finalValue;

    public UnityEvent ReachedFinal = new UnityEvent();
    public UnityEvent LostFinal = new UnityEvent();

    public void AddValue()
    {
        bool wasFinal = (currValue == finalValue);
        currValue++;
        if (currValue == finalValue) ReachedFinal.Invoke();
        else if (wasFinal && !notLess) LostFinal.Invoke();
    }
    public void SubValue()
    {
        bool wasFinal = (currValue == finalValue);
        currValue--;
        if ((currValue == finalValue) && !notLess) ReachedFinal.Invoke();
        else if (wasFinal) LostFinal.Invoke();
    }
}
