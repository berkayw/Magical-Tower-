using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 5f;
    public float damage = 40f;
    public float explosionRadius = 3f;  

    private Vector3 targetPosition;

    public GameObject fireballExplodeVFX;

    public void LaunchTowards(Vector3 target)
    {
        targetPosition = target;
    }
    
    void Update()
    {
        transform.Translate(targetPosition.normalized * speed * Time.deltaTime, Space.World);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    

    void OnCollisionEnter(Collision collision)
    {
        GameObject newFireballExplodeVFX = Instantiate(fireballExplodeVFX, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z) , Quaternion.identity);
        Destroy(newFireballExplodeVFX, 1f);
        
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
                
            }
        }

        Destroy(gameObject);  
    }
}