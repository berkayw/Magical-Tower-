using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Default, Fast, BigAndSlow, Custom }
    
    [Header("Enemy Type")]
    public EnemyType enemyType;  

    [Header("Custom Settings")]
    [Header("Speed Settings")]
    [SerializeField] private float defaultSpeed = 1.5f;     
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float speed;     

    [Header("Health Settings")]
    [SerializeField] private float defaultHealth = 20f;   
    [SerializeField] private float healthMultiplier = 2f;
    [SerializeField] private float health;     
    
    [Header("Damage Settings")]
    [SerializeField] private float damage = 10f;   
    [SerializeField] private float damageCooldown;

    
    [Header("Material Settings")]
    private Renderer enemyRenderer; 
    [SerializeField] private Material defaultEnemyMaterial;
    [SerializeField] private Material fastEnemyMaterial;
    [SerializeField] private Material bigAndSlowEnemyMaterial;
    [SerializeField] private Material customEnemyMaterial;

    [Header("Scale Settings")]
    [SerializeField] private float scaleX;   
    [SerializeField] private float scaleY;   
    [SerializeField] private float scaleZ;   


    [Header("Target Settings")]
    [SerializeField]private Transform tower;     
    [SerializeField] private bool isCollidingWithTower; 

    [Header("Particles")]
    public GameObject damageParticles;

    
    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        
        AdjustEnemyStats();
        tower = GameObject.FindWithTag("Tower").transform;
        
        if (tower != null)
        {
            Vector3 direction = (tower.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void Update()
    {
        if (!isCollidingWithTower)
        {
            MoveTowardsTower();
        }
        else
        {
            ApplyDamageToTower();
        }
    }

    void MoveTowardsTower()
    {
        Vector3 targetPosition = new Vector3(tower.position.x, transform.position.y, tower.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void AdjustEnemyStats()
    {
        switch (enemyType)
        {
            case EnemyType.Custom:
                if (scaleX != 0 || scaleY != 0 || scaleZ != 0)
                {
                    transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                }
                if (customEnemyMaterial != null)
                {
                    enemyRenderer.material = customEnemyMaterial;
                }
                break;
                
            case EnemyType.Fast:
                speedMultiplier = 2f;
                healthMultiplier = 1f;
                transform.localScale = new Vector3(0.4f, 0.3f, 1f);
                enemyRenderer.material = fastEnemyMaterial;
                break;
            
            case EnemyType.BigAndSlow:
                speedMultiplier = 0.5f;
                healthMultiplier = 2f;
                transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);  
                enemyRenderer.material = bigAndSlowEnemyMaterial;
                break;
            
            default:
                speedMultiplier = 1f;
                healthMultiplier = 1f;
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                enemyRenderer.material = defaultEnemyMaterial;
                break;
        }

        speed = defaultSpeed * speedMultiplier;
        health = defaultHealth * healthMultiplier;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            isCollidingWithTower = true;
        }
    }
    
    private void ApplyDamageToTower()
    {
        if (tower != null)
        {
            damageCooldown += Time.deltaTime;
            if (damageCooldown >= 1f)
            {
                tower.GetComponent<TowerHealth>().TakeDamage(damage);
                damageCooldown = 0f;
            }
        }
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage; 
        if (health <= 0)
        {
            GameObject newDamageParticles = Instantiate(damageParticles, transform.position, Quaternion.identity);
            Destroy(newDamageParticles, 1f);

            Destroy(gameObject);
        }
    }
    
}