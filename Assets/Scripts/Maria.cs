using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maria : MonoBehaviour
{
    public float speed;
    AudioSource mariaAudio;
    int sustos=3;
    bool OnOffSustos = false;
    public float timer = 10;
    ScareBoard board;
    Vector3 positionMaria;
    // Start is called before the first frame update
    void Start()
    {
        mariaAudio = GetComponent<AudioSource>();
        positionMaria = this.transform.position;
        board = FindObjectOfType<ScareBoard>();
        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (sustos >0){
            if (OnOffSustos == true)
            {
                if (timer<0)
                {
                    mariaAudio.Stop();
                    OnOffSustos = false;
                }
                else{
                    timer -= Time.deltaTime;
                }
 
            }
            else{
                transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
            }
                 
        }
        else{
            mariaAudio.Stop();
            //Time.timeScale = 0;
           
        }
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Enemy"))
        {
            sustos-=1;
            timer = 10;
            mariaAudio.Play();
            board.UpdateBoard(sustos);
            gameObject.transform.position = positionMaria;
            OnOffSustos = true;   
        }
    }
}
