using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ActivableBehaviour : MonoBehaviour
{
    [SerializeField]private List<ActivableObject> activableObjects;
    [SerializeField]private List<ActivableObject> deactibavleObjects;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem particles;
    private BoxCollider2D fuseCollider;
    private Light2D fireLight;

    private void Start()
    {
        fuseCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        particles = GetComponent<ParticleSystem>();
        fireLight = GetComponent<Light2D>();
        fireLight.enabled = false;
        particles.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mr Bright"))
        {
            foreach(ActivableObject activableObject in activableObjects)
            {
                activableObject.Activate();
            }
            
            foreach(ActivableObject activableObject in deactibavleObjects)
            {
                activableObject.Deactivate();
            }
            spriteRenderer.color = new Color(0.3f, 0.3f, 0.3f);
            particles.Play();
            fuseCollider.enabled = false;
            fireLight.enabled = true;
        }
    }
}
