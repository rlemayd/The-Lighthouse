using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maria : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float minSpeed = 0.1f;
    [SerializeField] private float maxSpeed = 0.8f;
    [SerializeField] private float minDistance = 0f;
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private float distanceOffset = 2f;
    public float currentSpeed;
    public Vector3 positionMaria;
    public Mr_Bright mrBright;

    [Header("Scares")]
    int sustos = 3;
    bool OnOffSustos = false;
    public float timer = 5;
    AudioSource mariaAudio;
    ScareBoard board;

    void Start()
    {
        mariaAudio = GetComponent<AudioSource>();
        positionMaria = this.transform.position;
        mrBright = FindObjectOfType<Mr_Bright>();
        board = FindObjectOfType<ScareBoard>();
        currentSpeed = minSpeed;
    }

    void Update()
    {
        CalculateSpeed();

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
                transform.Translate(new Vector3(currentSpeed * Time.deltaTime,0,0));
            }
        }
        else{
            mariaAudio.Stop();
        }
    }

    public void restartSustos()
    {
        sustos = 3;
    }

    private void CalculateSpeed()
    {
        float distance = (transform.position.x - mrBright.transform.position.x) + distanceOffset;
        if(distance > maxDistance)
        {
            currentSpeed = minSpeed;
        }
        else if (distance < minDistance)
        {
            currentSpeed = maxSpeed;
        }
        else
        {
            var distRatio = (maxDistance-distance) / (maxDistance);
            var diffSpeed = maxSpeed - minSpeed;
            currentSpeed = (distRatio * diffSpeed) + minSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Enemy"))
        {
            sustos-=1;
            timer = 5;
            mariaAudio.Play();
            board.UpdateBoard(sustos, this);
            gameObject.transform.position = positionMaria;
            OnOffSustos = true;   
        }
    }
}
