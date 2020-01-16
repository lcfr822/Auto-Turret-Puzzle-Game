using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : BaseTurret
{
    public Transform targetObject;
    public MeshFilter[] meshFilters = new MeshFilter[1];

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SessionData>().RegisterTurret(gameObject);
        targetRoutine = StartCoroutine(TargetPriorityUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        if(trackedTargets.Count >= 1) 
        { 
            TranslateToTarget();
            Debug.DrawRay(turretPivot.position, trackedTargets[0].transform.position - turretPivot.position, Color.red);
            Debug.DrawRay(turretPivot.position, turretPivot.forward * 5.0f, Color.blue);
        }        
    }
}
