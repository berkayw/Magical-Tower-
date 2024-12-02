using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject fireballPrefab;
    public GameObject barrageProjectilePrefab;
    
    [Header("ShootPoint")]
    public Transform shootPoint;
    
    [Header("Cooldowns")]
    public float fireballCooldown = 1f;  
    public float barrageCooldown = 2f;  
    
    public float maxFireballCooldown = 1f; 
    public float maxBarrageCooldown = 2f;  

    [Header("Icons")]
    public Image fireballIcon;
    public Image barrageIcon;

    
    void Update()
    {
        fireballIcon.fillAmount =  1 - fireballCooldown / maxFireballCooldown;
        barrageIcon.fillAmount = 1 - barrageCooldown / maxBarrageCooldown;

        
        if (fireballCooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                Debug.Log("fireball");
                CastFireball();
                fireballCooldown = maxFireballCooldown;
            }
        }
        else
        {
            fireballCooldown -= Time.deltaTime; 
        }
        
        if (barrageCooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log("barrage");
                CastBarrage();
                barrageCooldown = maxBarrageCooldown;
            }
        }
        else
        {
            barrageCooldown -= Time.deltaTime; 
        }
        
    }
    
    public void CastFireball()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        
        if (enemies.Length > 0)
        {
            Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];

            GameObject fireballSpell = Instantiate(fireballPrefab, shootPoint.position, Quaternion.identity);
            fireballSpell.GetComponent<FireballSpell>().LaunchTowards(randomEnemy.transform.position);
        }
        
    }
    
    public void CastBarrage()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        
        if (enemies.Length > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                GameObject barrageSpell = Instantiate(barrageProjectilePrefab, shootPoint.position, Quaternion.identity);
                barrageSpell.GetComponent<BarrageSpell>().LaunchTowards(enemy.transform.position);
            }
        }    
    }
}