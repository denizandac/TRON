using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteractable : MonoBehaviour
{
    public float interactDistance = 3f;
    private GameObject currentInteractable = null;
    public GameObject wallTrailPrefab;
    public bool isRiding = false;
    public bool isCreatingWall = false;
    private Vector3 wallPosition;
    // Update is called once per frame
    void Update()
    {
        wallPosition = transform.position - transform.forward * 1.5f + transform.up * 0.6f;
        if(Input.GetKeyDown(KeyCode.E)){
            if(isRiding){
                isRiding = false;
                currentInteractable.transform.parent = null;
                currentInteractable = null;
            }
            else{
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.forward, out hit, interactDistance)){
                    isRiding = true;
                    currentInteractable = hit.collider.gameObject;
                    currentInteractable.transform.position = new Vector3(transform.position.x, currentInteractable.transform.position.y, transform.position.z);
                    transform.rotation = currentInteractable.transform.rotation;
                    currentInteractable.transform.parent = transform;
                }
            }
        }
        if(isRiding && Input.GetKeyDown(KeyCode.LeftShift)){
            isCreatingWall = true;
        }
        if(isCreatingWall){
            GameObject wall = Instantiate(wallTrailPrefab, wallPosition, transform.rotation);
            // her framede duvar üretmesine engel ol
            Destroy(wall, 2.0f);
        }
    }
}