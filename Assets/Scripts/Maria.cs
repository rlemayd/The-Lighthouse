using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maria : MonoBehaviour
{
    public float speed = 3f;
    AudioSource audio;
    int sustos=3;
    bool OnOffSustos = false;
    public float timer = 10;
    float pointToStartX;
    float pointToStartY;
    ScareBoard board;
    Vector3 positionMaria;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        pointToStartX = this.transform.position.x;
        pointToStartY = this.transform.position.y;
        positionMaria = this.transform.position;
        Debug.Log(pointToStartX);
        board = FindObjectOfType<ScareBoard>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (sustos >0){
            if (OnOffSustos == true)
            {
                if (timer<0)
                {
                    audio.Stop();
                    OnOffSustos = false;
                }
                else{
                    timer -= Time.deltaTime;
                }
 
            }
            else{
                gameObject.transform.Translate(new Vector3(1*speed * Time.deltaTime,0,0));
            }
                 
        }
        else{
            audio.Stop();
            Time.timeScale = 0;
           
        }
        
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Enemy"))
        {
            sustos-=1;
            timer = 10;
            audio.Play();
            board.UpdateBoard(sustos);
            gameObject.transform.position = positionMaria;
            OnOffSustos = true;   
        }
    }
}
