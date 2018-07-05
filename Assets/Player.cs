using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;

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

        GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime);
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