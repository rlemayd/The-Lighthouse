using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float range=6f;
    public float speed=0.5f;
    float positionInicial;
    int change=1;
    // Start is called before the first frame update
    void Start()
    {
        positionInicial = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= positionInicial+range)
        {
            change=1;
        }
        else if (transform.position.x <= positionInicial-range ){
            change =0;
        }

        if (change == 1){
            transform.Translate(new Vector3(speed*-1 * Time.deltaTime,0,0));
        }
        else if (change == 0){
            transform.Translate(new Vector3(speed*1 * Time.deltaTime,0,0));
        }
        
    }
}
