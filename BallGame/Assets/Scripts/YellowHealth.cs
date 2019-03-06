using UnityEngine;
using UnityEngine.UI;

public class YellowHealth : MonoBehaviour 
{

    public GameObject healthController;

    public Slider healthBar;
    public float health = 100;

    private float currentHealth;


    // Use this for initialization
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            healthController.GetComponent<HealthController>().yellowDead = true;
        }

        healthBar.value = currentHealth;
    }

    public void GetHealth(float health)
    {
        currentHealth += health;

        if (currentHealth > 100)
        {
            currentHealth = 100;
        }

        healthBar.value = currentHealth;
    }

}
