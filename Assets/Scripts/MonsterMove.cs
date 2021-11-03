using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed=0.5f;
    public float changeDirectionDistance = 0.1f;
    private Vector3 directionVector = Vector3.zero;
    [SerializeField]private GameObject initialPosition;
    [SerializeField]private GameObject finalPosition;

    void Start()
    {
        transform.position = initialPosition.transform.position;
        directionVector = (finalPosition.transform.position - transform.position).normalized;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, finalPosition.transform.position) < changeDirectionDistance)
        {
            directionVector = (initialPosition.transform.position - transform.position).normalized;
        }
        else if (Vector3.Distance(transform.position, initialPosition.transform.position) < changeDirectionDistance)
        {
            directionVector = (finalPosition.transform.position - transform.position).normalized;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(directionVector * speed * Time.deltaTime);
    }
}
