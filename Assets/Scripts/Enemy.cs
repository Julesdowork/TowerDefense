﻿using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [HideInInspector] public float speed;
    public float startSpeed = 10f;
    public float health;
    public int value = 50;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private float startHealth = 100f;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        PlayerStats.Money += value;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
