using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;

public class TouchInteraction : MonoBehaviour
{

    [SerializeField] ARRaycastManager m_RayCastManager;

    [SerializeField] private bool isDragging;

    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();


    // Start is called before the first frame update
    void Start()
    {
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        Touch();
    }

    void Touch()
    {

        float dist;
        Vector3 offset;
        Transform toDrag;
        Vector3 v3;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 pos = touch.position;


            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(pos);

                if(Physics.Raycast(ray, out hit) && (hit.collider.tag.Equals("prefab")))
                {
                    Debug.Log(hit.collider.gameObject.name+ " is being hit..");
                    Debug.Log(pos+ " Position.....");

                    toDrag = hit.transform;
                    dist = hit.transform.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v3;
                    Debug.Log("Offset - "+offset);
                    isDragging = true;
                }

                
                Debug.Log("Began.........");
            }

            if(touch.phase == TouchPhase.Moved && isDragging)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(pos);
                if (Physics.Raycast(ray, out hit))
                {
                    toDrag = hit.transform;
                    dist = hit.transform.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v3;
                    toDrag.position = v3 + offset;
                    Debug.Log("Moving Offset - " + toDrag.position);
                }
                Debug.Log("Moving........");
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("Stationary........");
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Ended.............");
            }
        }



    }
}
