using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    /*[SerializeField] Rigidbody2D rigid2d;
    [SerializeField] float walkForce = 30.0f;
    [SerializeField] float maxWalkSpeed = 2.0f;
    //[SerializeField] int key;

    [SerializeField] float speed;
    */

    void Start()
    {
        //this.rigid2d = GetComponent<Rigidbody2D>();
        Debug.Log("nyaa");
    }

    // Update is called once per frame
    void Update()
    {
        /*key = 0;

        if(Input.GetKey(KeyCode.RightArrow)){
            key = 1;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            key = -1;
        }
        */

        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(1,0,0);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(-1,0,0);
        }

        /*speed = Mathf.Abs(this.rigid2d.velocity.x);
        if(speed < this.maxWalkSpeed){
            this.rigid2d.AddForce(transform.right*key*this.walkForce);
        }*/
    }
}
