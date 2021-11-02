using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMonsters : MonoBehaviour
{
    public float power = 6f;
    

    public void iteractionWithMonsters(bool status, GameObject character, GameObject monster, Rigidbody2D rb){
        if (status == false) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
                if (monster.transform.position.x < character.transform.position.x)
                {
                    rb.AddForce(Vector2.up * power, ForceMode2D.Impulse);
                }
                else{
                    rb.AddForce(Vector2.up * power, ForceMode2D.Impulse);
                }
        }
    }
}
