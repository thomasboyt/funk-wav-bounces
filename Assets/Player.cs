using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject boundary;
    private CharacterController characterController;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update() {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        if (Input.GetKey(KeyCode.Space)) {
            // throw ball
            var ball = transform.Find("Ball");
            if (ball) {
                ball.GetComponent<Ball>().Throw();
            }
        }
        if (Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        characterController.Move(moveDirection * Time.deltaTime);

        var boundaryCollider = boundary.GetComponent<MeshCollider>();
        var extents = boundaryCollider.bounds.extents;

        if (transform.position.z > boundary.transform.position.z + extents.z) {
            var diff = boundary.transform.position.z + extents.z - transform.position.z;
            characterController.Move(transform.forward * diff);
        } else if (transform.position.z < boundary.transform.position.z - extents.z) {
            var diff = transform.position.z - (boundary.transform.position.z - extents.z);
            characterController.Move(-transform.forward * diff);
        }
    }


    // called when player is moving
    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.name == "Ball") {
            hit.gameObject.GetComponent<Ball>().CaughtByPlayer();
        }
    }

    // called when player isn't moving
    void OnCollisionEnter(Collision hit) {
        if (hit.gameObject.name == "Ball") {
            hit.gameObject.GetComponent<Ball>().CaughtByPlayer();
        }
    }
}