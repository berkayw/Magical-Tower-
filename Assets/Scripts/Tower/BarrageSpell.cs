using System.Collections;
using UnityEngine;

public class BarrageSpell : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 10f;  
    public float damage = 40f;  
    private Vector3 targetPosition;
    
    public void LaunchTowards(Vector3 target)
    {
        targetPosition = target;
        StartCoroutine(MoveTowardsTarget());
    }

    private IEnumerator MoveTowardsTarget() //dont move instantly
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        DealDamage();
    }

    private void DealDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f);  

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        
        Destroy(gameObject);
    }
}