using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject prefab;

    [Header("AR Plane")]
    [SerializeField] private GameObject arPlane;

    private Pose placementPose;
    private bool placementPoseValid;

    private GameObject spawnObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private static bool isPrefabPlaced;

    private static bool isARPlaneManagerActive;


    public ARTapToPlaceObject()
    {
    }

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();

        placementPoseValid = false;

    }


    bool TryGetTouchPosition(out Vector2 touchPosition)
    {

        bool touchCount = Input.touchCount > 0;

        if (touchCount)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isPrefabPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        PrefabPlacing();

        if (getARPlaneManagerActive())
        {
            arPlane.SetActive(false);
            setARPlaneManagerActive(false);
        }

    }

    public void setIsPrefabPlaced(bool value)
    {
        isPrefabPlaced = value;
    }

    public bool getIsPrefabPlaced()
    {
        return isPrefabPlaced;
    }

    public void setARPlaneManagerActive(bool value)
    {
        isARPlaneManagerActive = value;
    }

    public bool getARPlaneManagerActive()
    {
        return isARPlaneManagerActive;
    }

    void PrefabPlacing()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;

            if (spawnObject == null)
            {
                Vector3 hitPoseVec = new Vector3(hitPose.position.x, hitPose.position.y, hitPose.position.z);
                spawnObject = Instantiate(prefab, hitPoseVec, hitPose.rotation);
                
                setIsPrefabPlaced(true);

                setARPlaneManagerActive(true);
            }
            else
            {
                PrefabSpawn(hitPose);
            }
        }
    }

    /// <summary>
    /// Spawning of the Prefab..
    /// </summary>
    /// <param name="hitPose"></param>
    void PrefabSpawn(Pose hitPose)
    {

        Vector3 hitPoseVec = new Vector3(hitPose.position.x, hitPose.position.y, hitPose.position.z);
        spawnObject.transform.position = hitPoseVec;

    }


    public void ResetPlane()
    {
        Debug.Log("Reset Of Place is working.");
        arPlane.SetActive(true);
    }




    

}
