using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health = 100;
    
    public void Hit() {

        health -= 50;
        if (health <= 0) {
            Die();
        }
    }

    public void Headshot() {
        Die();
    }

    private void Die() {
        Destroy(gameObject);
    }
}
