using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Text healthText;
    private int health = 100;

 
    public int Health { get => health;
    set 
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
        }

        health = value;
        healthText.text = health.ToString();
    }}

    void Start()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        Health = 100;
    }

// When enemy receivis damage lerp the color to red
    public void DamageEffect()
    {
         spriteRenderer.color = Color.Lerp(Color.white, Color.red, 2f);

    }

//When enemy stops receiving damage lerp the color back to white
    public void ReverseDamageEffect()
    {
         spriteRenderer.color = Color.Lerp(Color.red, Color.white, 2f);
    }

    
    
    
  
}
