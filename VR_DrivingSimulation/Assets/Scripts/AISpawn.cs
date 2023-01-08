using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawn : MonoBehaviour
{
    public Rigidbody car;   //player vehicle
    //public GameObject AI_obj;
    public GameObject[] AI_obj;
    private int random_active = 0;  //random active number
    
    // Start is called before the first frame update
    void Start()
    {
        //플레이어 차량 오브젝트
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        AI_obj = new GameObject[transform.childCount];
        //AI 오브젝트
        //AI_obj = transform.GetChild(0).gameObject;//FindGameObjectWithTag("AI");
        for(int i = 0; i < transform.childCount; i++){
            if(transform.GetChild(i).gameObject.tag == "AI"){
                AI_obj[i] = transform.GetChild(i).gameObject;
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            random_active = 0;//Random.Range(0, 3);

            Debug.Log(random_active);
            if(random_active == 0){
                //AI_obj.SetActive(true);
                for(int i = 0; i < transform.childCount; i++){
                    AI_obj[i].SetActive(true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
