using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;//速度
    public Vector3 MoveVelocity;//進むべき方向
    void Start()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
        MoveVelocity = gameObject.transform.rotation * new Vector3(0.0f,speed,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //transform.Translate(0,0.05f,0);
    }
    void Move(){
        gameObject.transform.position += MoveVelocity*Time.deltaTime;
    }
    void OnBecameInvisible(){//画面外に出たら消える
        Destroy(this.gameObject);
    }
}
