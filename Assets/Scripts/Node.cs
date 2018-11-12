using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretBlueprint turretBlueprint;
    [HideInInspector] public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;
    private BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to build that turret.");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        // Get rid of old turret
        Destroy(turret);

        // Build a new one
        GameObject t = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = t;
        isUpgraded = true;

        GameObject fx = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(fx, 5f);

        Debug.Log("Turret upgraded.");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        // Spawn a cool effect
        GameObject fx = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(fx, 5f);

        Destroy(turret);
        turretBlueprint = null;
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that turret.");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        GameObject t = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = t;
        turretBlueprint = blueprint;

        GameObject fx = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(fx, 5f);

        Debug.Log("Turret built.");
    }
}
