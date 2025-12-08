using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float health;
    private float lerpSpeed = 0.05f;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10);
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }
    }

    void takeDamage(float damage)
    {
        health -= damage;
    }
}
