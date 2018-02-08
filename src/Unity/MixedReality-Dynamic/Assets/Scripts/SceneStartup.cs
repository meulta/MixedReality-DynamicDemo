using HoloToolkit.Unity.InputModule;
using MixedRealityData.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SceneStartup : MonoBehaviour
{
 

    // Use this for initialization
    void Start()
    {
        Load3DObjects();

        
    }

    async void Load3DObjects()
    {
        IEnumerable<UnityPrimitive> objects = await ObjectsAPI.FetchSceneData();
        foreach (var obj in objects)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new UnityEngine.Vector3(obj.Vector.X, obj.Vector.Y, obj.Vector.Z);
            var draggableHandComponent = go.AddComponent<HandDraggable>();
            draggableHandComponent.RotationMode = HandDraggable.RotationModeEnum.LockObjectRotation;

            var storedObjectComponent = go.AddComponent<StoredObject>();
            storedObjectComponent.storedObject = obj;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
