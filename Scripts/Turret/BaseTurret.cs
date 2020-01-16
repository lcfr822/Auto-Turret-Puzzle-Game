using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public Transform turretPivot, turretMuzzlePoint;
    public List<GameObject> trackedTargets = new List<GameObject>();
    public Camera turretTrackingCamera;
    public float maxTranslationSpeed = 5.0f;

    protected bool isPrioritizing = false;
    protected Coroutine targetRoutine;

    public void ReportTarget(GameObject newTarget)
    {
        if (trackedTargets.Contains(newTarget)) { return; }
        trackedTargets.Add(newTarget);
        PrioritizeTargets();

        if (trackedTargets.Count >= 1) { TranslateToTarget(); }
    }

    public void ReleaseTarget(GameObject lostTarget)
    {
        trackedTargets.Remove(lostTarget);
        PrioritizeTargets();

        if (trackedTargets.Count >= 1) { TranslateToTarget(); }
    }

    public void TranslateToTarget()
    {
        Vector3 targetLock = trackedTargets[0].transform.position;
        Vector3 updatedDirection = Vector3.RotateTowards(turretPivot.forward, targetLock - turretPivot.position, Time.deltaTime, 0.0f);
        turretPivot.rotation = Quaternion.LookRotation(updatedDirection);
    }

    private void PrioritizeTargets()
    {
        if (trackedTargets.Count >= 2)
        {
            trackedTargets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position));
            if(Vector3.Distance(trackedTargets[0].transform.position, turretPivot.position) > Vector3.Distance(trackedTargets[1].transform.position, turretPivot.position))
            {
                trackedTargets.Reverse();
            }
        }
    }

    protected IEnumerator TargetPriorityUpdate()
    {
        if (trackedTargets.Count >= 2)
        {
            isPrioritizing = true;
            trackedTargets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position));
            if (Vector3.Distance(trackedTargets[0].transform.position, turretPivot.position) > Vector3.Distance(trackedTargets[1].transform.position, turretPivot.position))
            {
                trackedTargets.Reverse();
            }
        }

        yield return new WaitForSeconds(1.0f);
        isPrioritizing = false;
        targetRoutine = StartCoroutine(TargetPriorityUpdate());
    }
}
