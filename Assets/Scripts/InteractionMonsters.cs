using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMonsters : MonoBehaviour
{
    public float power = 6f;

    public void iteractionWithMonsters(bool isInvincible, GameObject character, GameObject monster, Rigidbody2D rb){
        if (!isInvincible) {
            Vector3 knockbackDirection = (character.transform.position - monster.transform.position).normalized;
            rb.AddForce(knockbackDirection * power, ForceMode2D.Impulse);
        }
    }
}
