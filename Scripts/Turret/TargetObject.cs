using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private List<BaseTurret> alertedTurrets = new List<BaseTurret>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        foreach (TurretSessionData sessionData in FindObjectOfType<SessionData>().turretData)
        {
            if (GetComponent<Renderer>().IsVisibleFrom(sessionData.trackingCamera) && !sessionData.baseTurret.trackedTargets.Contains(gameObject))
            {
                if(alertedTurrets == null) { alertedTurrets = new List<BaseTurret>(); }
                sessionData.baseTurret.ReportTarget(gameObject);
                alertedTurrets.Add(sessionData.baseTurret);
            }
            else if (!GetComponent<Renderer>().IsVisibleFrom(sessionData.trackingCamera) && sessionData.baseTurret.trackedTargets.Contains(gameObject))
            {
                sessionData.baseTurret.ReleaseTarget(gameObject);
                alertedTurrets.Remove(sessionData.baseTurret);
                alertedTurrets = null;
            }
        }
    }
}
