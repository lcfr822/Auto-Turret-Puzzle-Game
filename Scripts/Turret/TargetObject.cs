using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private List<BaseTurret> alertedTurrets = new List<BaseTurret>();
    private Vector3 prevPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        prevPosition = transform.position;
    }

    private void FixedUpdate()
    {
        foreach (TurretSessionData sessionData in FindObjectOfType<SessionData>().turretData)
        {
            if (GetComponent<Renderer>().IsVisibleFrom(sessionData.trackingCamera) && !sessionData.baseTurret.trackedTargets.Contains(gameObject))
            {
                sessionData.baseTurret.ReportTarget(gameObject);
                alertedTurrets.Add(sessionData.baseTurret);
            }
            else if (!GetComponent<Renderer>().IsVisibleFrom(sessionData.trackingCamera) && sessionData.baseTurret.trackedTargets.Contains(gameObject))
            {
                sessionData.baseTurret.ReleaseTarget(gameObject);
                alertedTurrets.Remove(sessionData.baseTurret);
            }
        }

        if (Vector3.Distance(transform.position, prevPosition) > 0.1f)
        {
            foreach (BaseTurret turret in alertedTurrets)
            {
                turret.PrioritizeTargets();
            }
            prevPosition = transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, "Distance: " + Vector3.Distance(transform.position, FindObjectOfType<BaseTurret>().transform.position));
    }
}
