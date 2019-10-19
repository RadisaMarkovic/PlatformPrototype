using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// type of damage which player will deal to enemy it can be basic attack or strong attack
  public enum DamageType 
   {
       BASIC_ATTACK = 1,
       SPECIAL_ATTACK
   }
public class WeaponAxe : MonoBehaviour
{
    
    
    private Animator anim;
    private CircleCollider2D axeColider;
    [SerializeField]
    private int basicAttackDamage = 10;
    [SerializeField]
    private int specialAttackDamage = 20;
    private float basicAttackDuration;
    private float specialAttackDuration;
    private bool canDoBasicAttack;
    private bool canDoSpecialAttack;


    private DamageType typeOfDamage;
    public DamageType TypeOfDamage
    {
        get => typeOfDamage;
    }
    
    private Enemy attackedEnemy;
  
    void Start()
    {
        axeColider = GetComponent<CircleCollider2D>();
        axeColider.enabled = false;
        anim  = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        canDoBasicAttack = true;
        canDoSpecialAttack = true;
        
        basicAttackDuration = anim.runtimeAnimatorController.animationClips
                              .First(clip => clip.name == "attack").length;

        specialAttackDuration = anim.runtimeAnimatorController.animationClips
                              .First(clip => clip.name == "special attack").length;
    }

    void Update()
    {
         if(Input.GetMouseButtonDown(0) && canDoBasicAttack)
        {
            StartCoroutine(BasicAttack());
        }

        if(Input.GetMouseButtonDown(1) && canDoSpecialAttack)
        {
            StartCoroutine(SpecialAttack());
        }
    }
//Corutine for basic attack it will be executed when player press left click
      private IEnumerator BasicAttack() 
    {
        canDoBasicAttack = false;
        anim.SetBool("attack", true);
        typeOfDamage = DamageType.BASIC_ATTACK;
        axeColider.enabled = true;
        yield return new WaitForSeconds(basicAttackDuration);
        canDoBasicAttack = true;
        axeColider.enabled = false;
        
    }

// Corutine for special attack it will be executed when player press right click
    private IEnumerator SpecialAttack()
    {
        canDoSpecialAttack = false;
        anim.SetBool("special",true);
        typeOfDamage = DamageType.SPECIAL_ATTACK;
        
        axeColider.enabled = true;
        yield return new WaitForSeconds(specialAttackDuration);
        canDoSpecialAttack = true;
        axeColider.enabled = false;
        
    }

//Deal damage on enemy based on attack type 
    private void DealDamageToEnemy(Enemy enemy)
    {
        switch(typeOfDamage)
        {
            case DamageType.BASIC_ATTACK:
                enemy.Health -= basicAttackDamage;
                break;
            case DamageType.SPECIAL_ATTACK:
                enemy.Health -= specialAttackDamage;
                break;
        }
    }

//When axe enter the enemy colider
     void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("Enemy"))
        {  
            attackedEnemy = other.collider.GetComponent<Enemy>();

            attackedEnemy.DamageEffect();
            DealDamageToEnemy(attackedEnemy);
           
            
        }
    }

//When axe leave the enemy colider
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.collider.CompareTag("Enemy") && attackedEnemy != null)
        {
            attackedEnemy.ReverseDamageEffect();
            DealDamageToEnemy(attackedEnemy);
            
        }
    }
}
