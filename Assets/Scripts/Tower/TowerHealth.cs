using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    public Image healthBarFill; 
    
    [Header("Health Stats")]
    public float maxHealth = 100f;  
    private float currentHealth; 

    [Header("Particles")]
    public GameObject damageParticles;
    
    void Start()
    {
        currentHealth = maxHealth; 
        UpdateHealthBar();  
    }

    public void TakeDamage(float damage)
    {
        GameObject newDamageParticles = Instantiate(damageParticles, transform.position, Quaternion.identity);
        Destroy(newDamageParticles, 1f);
        currentHealth -= damage;  
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  
        UpdateHealthBar(); 
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void UpdateHealthBar()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;  
    }
}