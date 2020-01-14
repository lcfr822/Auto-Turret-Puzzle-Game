using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public Camera testCamera;
    private BaseTurret alertedTurret;
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
        if (GetComponent<Renderer>().IsVisibleFrom(testCamera) && !FindObjectOfType<BaseTurret>().trackedTargets.Contains(gameObject))
        {
            FindObjectOfType<BaseTurret>().ReportTarget(gameObject);
            alertedTurret = FindObjectOfType<BaseTurret>();
        }
        else
        {
            alertedTurret.ReleaseTarget(gameObject);
            alertedTurret = null;
        }
    }
}
