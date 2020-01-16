using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Target
{
    public GameObject targetObject, turretObject;
    private float targetDistance;
    public float TargetDistance { get { return Vector3.Distance(targetObject.transform.position, turretObject.transform.position); } private set { targetDistance = value; } }

    public Target(GameObject newTargetObject, GameObject newTurretObject)
    {
        targetObject = newTargetObject;
        turretObject = newTurretObject;
        targetDistance = 0;
    }
}
