using UnityEngine;

public abstract class SavedObject : MonoBehaviour
{
    public abstract object GetOtherData();
    public abstract void SetOtherData(object Data);

    public virtual void DoAfterLoad()
    { }
}
