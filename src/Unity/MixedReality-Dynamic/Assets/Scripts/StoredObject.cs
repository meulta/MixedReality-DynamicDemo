using HoloToolkit.Unity.InputModule;
using MixedRealityData.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredObject : MonoBehaviour
{
    public UnityPrimitive storedObject;

    // Use this for initialization
    void Start()
    {
        var draggable = GetComponent<HandDraggable>();
        draggable.StoppedDragging += () =>
        {
            if (storedObject != null)
            {
                storedObject.Vector.X = transform.position.x;
                storedObject.Vector.Y = transform.position.y;
                storedObject.Vector.Z = transform.position.z;
                ObjectsAPI.UpdateObject(storedObject);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
