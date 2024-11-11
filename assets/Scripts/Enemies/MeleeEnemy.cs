using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField]private float attackCooldown;
    [SerializeField]private float range;
    [SerializeField]private int damage;
    [SerializeField]private float colliderDistance;
    
    [Header ("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField]private BoxCollider2D boxCollider;
    [SerializeField]private LayerMask playerLayer;

    [Header("Enemyfireball Sound")]
    [SerializeField]private AudioClip fireballSound;
    private float cooldownTimer= Mathf.Infinity;

    private Health playerHealth;

    private Animator anim;



    private void Awake()
    {
        anim= GetComponent<Animator>();
    }


    private void Update()
    {

        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            
        if(cooldownTimer >= attackCooldown)
        {
            cooldownTimer= 0;
            anim.SetTrigger("rangedAttack");
        }

        }

    }


    private bool PlayerInSight()
    {
        RaycastHit2D hit= Physics2D.BoxCast(boxCollider.bounds.center+transform.right * range * transform.localScale.x * colliderDistance ,new Vector3(boxCollider.bounds.size.x *range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),0,Vector2.left,0,playerLayer);
        if(hit.collider!=null)
        {
            playerHealth= hit.transform.GetComponent<Health>();
        }
        return hit.collider!=null;       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center+ transform.right * range * transform.localScale.x * colliderDistance,new Vector3(boxCollider.bounds.size.x *range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // private void DamagePlayer()
    // {
    //     if(PlayerInSight())
    //     {
    //         playerHealth.TakeDamage(damage); 
    //     }
    // }

    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        cooldownTimer= 0;
        fireballs[FindFireball()].transform.position= firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireball()
    {
        for( int i=0; i< fireballs.Length; i++)
        {
            if(fireballs[i].activeInHierarchy)
                 return  i;
        }
        return 0;
    }
   
}
