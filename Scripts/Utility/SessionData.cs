using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    public List<TurretSessionData> turretData = new List<TurretSessionData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterTurret(GameObject unregisteredTurret)
    {
        TurretSessionData registeredTurret = new TurretSessionData(unregisteredTurret.GetComponentInChildren<Camera>(), unregisteredTurret.GetComponent<BaseTurret>());
        turretData.Add(registeredTurret);
        Debug.Log("Registered new turret: " + unregisteredTurret.name);
    }
}

public struct TurretSessionData
{
    public Camera trackingCamera;
    public BaseTurret baseTurret;

    public TurretSessionData(Camera newTrackingCamera, BaseTurret newBaseTurret)
    {
        trackingCamera = newTrackingCamera;
        baseTurret = newBaseTurret;
    }
}
