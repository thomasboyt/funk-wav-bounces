using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public GameObject player;
    private Transform originalParent;
    private Rigidbody rb;
    //private bool caught = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        originalParent = transform.parent;
        //GetComponent<Rigidbody>().velocity = 10 * transform.localScale.x * transform.forward;
        CaughtByPlayer();
	}

    public void CaughtByPlayer() {
        // player takes ownership of ball position
        rb.isKinematic = true;
        //caught = true;
        transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0, 1f, 0);
        transform.rotation = player.transform.rotation;
    }

    public void Throw() {
        // move ball in front of player so player + ball dont collide
        transform.localPosition = transform.forward * 2f;
        rb.isKinematic = false;
        rb.velocity = transform.forward * 30f;
        transform.SetParent(originalParent);
    }

    //void OnCollisionEnter(Collision otherObj) {
    //    if (otherObj.gameObject.name == "Player") {
    //        CaughtByPlayer();
    //    }
    //}
	
	// Update is called once per frame
	void Update () {
	}
}
