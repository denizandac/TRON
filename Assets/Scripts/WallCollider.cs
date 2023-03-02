using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public bool GameOver = false;
    void onCollisionEnter(Collision other){
        if(other.gameObject.tag == "Finish"){
            Debug.Log("Wall");
            GameOver = true;
        }
    }
}
