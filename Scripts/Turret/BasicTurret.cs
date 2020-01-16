using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : BaseTurret
{
    public Transform targetObject;
    public MeshFilter[] meshFilters = new MeshFilter[1];
    public Vector3 passiveLookBounds;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SessionData>().RegisterTurret(gameObject);
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
        else
        {
            PassiveAction();
        }
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(new Ray(turretPivot.position, turretPivot.forward), out RaycastHit hitInfo, maximumRange) && trackedTargets.Count > 0)
        {
            if ((hitInfo.transform.gameObject == trackedTargets[0] && canFire) || (hitInfo.transform.gameObject.GetComponent<TargetObject>() != null && canFire))
            {
                StartCoroutine(FireTurret());
            }
        }
    }

    protected override void PassiveAction()
    {
        transform.Rotate(new Vector3(0, translationSpeed * Time.deltaTime * 50));
    }
}
