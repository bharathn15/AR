using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interaction {
    public class PrefabInteraction : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject prefab;


        [Header("AR Tap To Place")]
        [SerializeField] private ARTapToPlaceObject aRTapToPlaceObject;

        public bool isTappedOnce;

        private void Awake()
        {
            isTappedOnce = true;
        }

        void Start()
        {

        }


        void Update()
        {
            if (aRTapToPlaceObject.getIsPrefabPlaced())
            {
                // Scale(prefab);
            }
            Scale(prefab);
        }


        public void Scale(GameObject obj)
        {
            int TouchOnScreen = 0;
            float distBetweenFingers;

            foreach (Touch touch in Input.touches)
            {
                TouchOnScreen += 1;

                if (TouchOnScreen.Equals(2) && touch.phase.Equals(TouchPhase.Moved))
                {
                    distBetweenFingers = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                    Debug.Log("Finger Distance - " + distBetweenFingers * Time.deltaTime);



                    float scaleValue = distBetweenFingers * Time.deltaTime * Time.deltaTime * Time.deltaTime;

                    prefab.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                }
            }
        }


    }

}



