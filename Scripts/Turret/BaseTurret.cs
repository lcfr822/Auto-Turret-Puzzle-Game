using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    [SerializeField]
    private Transform turretPivot, turretBarrel, turretReceiver;
    public List<GameObject> trackedTargets = new List<GameObject>();
    public Camera turretTrackingCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReportTarget(GameObject newTarget)
    {
        Debug.Log("Repeat? " + trackedTargets.Contains(newTarget));
        if (trackedTargets.Contains(newTarget)) { return; }
        Debug.LogWarning("Logging new target: " + newTarget.name);
        trackedTargets.Add(newTarget);
        trackedTargets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position));

        int targetIndex = 0;
        foreach(GameObject target in trackedTargets)
        {
            Debug.Log(targetIndex + ". " + Vector3.Distance(transform.position, target.transform.position));
            targetIndex++;
        }
    }

    public void ReleaseTarget(GameObject lostTarget)
    {
        trackedTargets.Remove(lostTarget);
        trackedTargets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position));

        int targetIndex = 0;
        foreach (GameObject target in trackedTargets)
        {
            Debug.Log(targetIndex + ". " + Vector3.Distance(transform.position, target.transform.position));
            targetIndex++;
        }
    }

    public void TranslateToTarget()
    {

    }
}
