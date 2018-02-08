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
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new UnityEngine.Vector3(defaultPosition.x, defaultPosition.y, defaultPosition.z);
        var draggableHandComponent = go.AddComponent<HandDraggable>();
        draggableHandComponent.RotationMode = HandDraggable.RotationModeEnum.LockObjectRotation;

        var primitive = new UnityPrimitive();
        primitive.Type = "Cube";
        primitive.Vector = new MixedRealityData.Models.Vector3 () { X = defaultPosition.x, Y = defaultPosition.y, Z = defaultPosition.z };

        UnityPrimitive prim = ObjectsAPI.CreateObject(primitive);

        var storedObjectComponent = go.AddComponent<StoredObject>();
        storedObjectComponent.storedObject = prim;
    }
}
