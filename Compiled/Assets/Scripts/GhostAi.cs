using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostAi : MonoBehaviour {
    public Transform[] spawnPoints;
    SphereCollider col;
    private Animation animations;

    private int flag;
    int current = 0;
    bool searching = true;

    public Transform target;
    public float speed;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start(){
        Debug.Log("Starting Game");
        flag = 0;
        col = GetComponent<SphereCollider>();
        animations = GetComponent<Animation>();
        animations.Play("ghost idle");
    }
    
    // Search Mode
    void Update(){
        // Update is called once per frame, once it reaches condition, the ghost moves
        // This simulates a wait function
        
        flag += 1;

        if (flag == 100 & searching == true){
            // Get Position of Next Random Spawn Point
            current = Random.Range(0, spawnPoints.Length);

            // Move To that Position
            transform.position = Vector3.MoveTowards(transform.position, spawnPoints[current].transform.position, 1000);
            flag = 0; 
        }


        // Set active chase Mode, deactivate ghost AI script
        else if (searching == false){
            // CHASE ANIMATION
            float dist = Vector3.Distance(target.position, transform.position);
            transform.LookAt(target, Vector3.up);
            transform.position += transform.forward * speed * Time.deltaTime;

            // DEATH CUTSCENE
            if (dist < 2.0){
                Debug.Log("I caught you");
                animations.Play("ghost attack");
                if (!animations.IsPlaying("ghost attack"))
                {
                    SceneManager.LoadScene(2);
                }

            }
        }
    }

    // Chase Mode
    void OnTriggerEnter(Collider other)
    {
        //This increases the Collider radius when the GameObject collides with a trigger Collider
        if (other.tag == "Player"){
            searching = false;
            Debug.Log("I found you");
            animations.Play("chasing");
        }
    }

    // Back to Search Mode
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player"){
            searching = true;
            flag = 0;
            Debug.Log("I lost you");
            animations.Play("ghost idle");
        }
    }

    public void pagePicked()
    {
        speed += 0.75f;
        col.radius += 3;
    }
}
