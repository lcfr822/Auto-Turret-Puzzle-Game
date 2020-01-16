using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    // Targeting Variables
    public Transform turretPivot, turretMuzzlePoint;
    public List<GameObject> trackedTargets = new List<GameObject>();
    //public List<Target> trackedTargets = new List<Target>();
    public Camera turretTrackingCamera;
    public float translationSpeed = 1.0f;

    protected bool isPrioritizing = false;
    protected Coroutine targetRoutine;

    // Shooting Variables
    [Range(0.1f, 2.5f)]
    public float fireDelay = 0.25f;
    public float maximumRange = 100.0f;
    public bool canFire = true;

    public void ReportTarget(GameObject newTarget)
    {
        if (trackedTargets.Contains(newTarget)) { return; }
        //trackedTargets.Add(newTarget);
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
        Vector3 updatedDirection = Vector3.RotateTowards(turretPivot.forward, trackedTargets[0].transform.position - turretPivot.position, translationSpeed * Time.deltaTime, 0.0f);
        turretPivot.rotation = Quaternion.LookRotation(updatedDirection);
    }

    public IEnumerator FireTurret()
    {
        canFire = false;
        turretMuzzlePoint.GetComponent<ParticleSystem>().Emit(20);
        yield return new WaitForSeconds(fireDelay);
        canFire = true;
    }

    public void PrioritizeTargets()
    {
        if (trackedTargets.Count >= 2)
        {
            trackedTargets = trackedTargets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).ToList();
        }
    }

    protected virtual void PassiveAction() { }
}
