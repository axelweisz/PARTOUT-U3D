using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

//on met automatiquement un ARRaycastManager 
[RequireComponent(typeof(ARRaycastManager))]

public class PlacementController : MonoBehaviour
{
    //L'objet à poser
    [SerializeField]
    private GameObject prefab2place;
    public GameObject Prefab2place
    {
        get
        {
            return prefab2place;
        }

        set
        {
            prefab2place = value;
        }
    }
    //delcarationdu ARRaycastManager
    private ARRaycastManager aRaycastManager;
    //Liste pour sauvegardé les points de Hits venant du RayCast
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private void Awake()
    {
        aRaycastManager = GetComponent<ARRaycastManager>();
    }


    void Start()
    {
        
    }


    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if(aRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            Instantiate(prefab2place, hitPose.position, hitPose.rotation);
        }
        
    }


    //la méthode pour trouver le point touché ou écran retourn un bool ET la position du point sur l'écran au même temps
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;


    }

}
