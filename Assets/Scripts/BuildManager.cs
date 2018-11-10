using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    
    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that turret.");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
        Debug.Log("Turret built. Money left = " + PlayerStats.Money);

        GameObject fx = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(fx, 5f);
    }
}
