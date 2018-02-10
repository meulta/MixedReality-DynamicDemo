using HoloToolkit.Unity.InputModule;
using MixedRealityData.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandlers : MonoBehaviour
{

    private UnityEngine.Vector3 defaultPosition = new UnityEngine.Vector3(1, 1, 1);

    public void CreateCube()
    {
        CreatePrimitive(PrimitiveType.Cube);
    }

    public void CreateSphere()
    {
        CreatePrimitive(PrimitiveType.Sphere);
    }

#if UNITY_EDITOR
    public void CreatePrimitive(PrimitiveType type)
#else
    public async void CreatePrimitive(PrimitiveType type)
#endif
    {
        var go = GameObject.CreatePrimitive(type);
        go.transform.position = new UnityEngine.Vector3(defaultPosition.x, defaultPosition.y, defaultPosition.z);
        var draggableHandComponent = go.AddComponent<HandDraggable>();
        draggableHandComponent.RotationMode = HandDraggable.RotationModeEnum.LockObjectRotation;

        var primitive = new UnityPrimitive();
        primitive.Type = (int)type;
        primitive.Vector = new MixedRealityData.Models.Vector3() { X = defaultPosition.x, Y = defaultPosition.y, Z = defaultPosition.z };

#if UNITY_EDITOR
        UnityPrimitive prim = ObjectsAPI.CreateObject(primitive);
#else
        UnityPrimitive prim = await ObjectsAPI.CreateObject(primitive);
#endif

        var storedObjectComponent = go.AddComponent<StoredObject>();
        storedObjectComponent.storedObject = prim;
    }
}
