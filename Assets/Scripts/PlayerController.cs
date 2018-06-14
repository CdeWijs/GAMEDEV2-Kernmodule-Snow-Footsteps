using UnityEngine;

public class PlayerController : MonoBehaviour {

    enum State {
        IDLE,
        WALK,
        RUN
    }

    private float walkSpeed = 0.4f;
    private Animator animator;
    private bool isWalking;
    private bool isIdle;
    private State currentState;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * walkSpeed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed;

        //transform.Rotate(0, x, 0);
        transform.Translate(x, 0, z);

        if (Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1) {
            currentState = State.WALK;
        } else if (Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal") == -1) {
            currentState = State.WALK;
        } else {
            currentState = State.IDLE;
        }

        switch (currentState) {
            case State.IDLE:
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", true);
                break;
            case State.WALK:
                animator.SetBool("Walk", true);
                animator.SetBool("Idle", false);
                break;
            case State.RUN:
                
                break;
            default:
                break;
        }
    }
}