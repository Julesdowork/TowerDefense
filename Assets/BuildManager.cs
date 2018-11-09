﻿using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;

    private GameObject turretToBuild;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
}
