using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct ObjectInfo
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;
    public object otherData;
}


public class StateSaver : MonoBehaviour
{
    [SerializeField]
    // which objects to save at the awake
    private List<GameObject> savedObjects;
    private List<ObjectInfo> objectInfos = new List<ObjectInfo>();

    private Dictionary<GameObject, ObjectInfo> objectToInfo = new Dictionary<GameObject, ObjectInfo>();

    private void Start()
    {
        foreach (var obj in savedObjects)
        {
            SaveGO(obj);
        }
    }

    public void SaveGO(GameObject saved)
    {
        SavedObject so = saved.GetComponent<SavedObject>();
        object otherData = null;
        if (so != null)
            otherData = so.GetOtherData();

        objectToInfo[saved] = new ObjectInfo()
        {
            Position = saved.transform.localPosition,
            Rotation = saved.transform.localRotation,
            Scale = saved.transform.localScale,
            otherData = otherData
        };
    }

    public void LoadGO(GameObject saved)
    {
        SavedObject so = saved.GetComponent<SavedObject>();
        if (so != null)
            so.SetOtherData(objectToInfo[saved].otherData);

        saved.transform.localPosition = objectToInfo[saved].Position;
        saved.transform.localRotation = objectToInfo[saved].Rotation;
        saved.transform.localScale = objectToInfo[saved].Scale;

        so?.DoAfterLoad();
    }

    public void SaveState()
    {
        foreach (GameObject go in savedObjects)
        {
            SaveGO(go);
        }
    }

    public void LoadState()
    {
        foreach (GameObject go in savedObjects)
        {
            LoadGO(go);
        }
    }


}
