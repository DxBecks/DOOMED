using UnityEngine;
using System.Collections;

public class KiwiMotor : MonoBehaviour {
    /*DATA MEMBERS*/

    //Instantiate private class level objects.
    private KiwiState state;
    private new Rigidbody2D rigidbody;  //A reference to the RigidBody2D. 

    [SerializeField][Range(1.0f, 20.0f)] private Vector2 velocityLimit = new Vector2();     //The maximum velocity of the Kiwi.


    [SerializeField][Range(0.1f, 10.0f)] private float walkSpeed;
    [SerializeField][Range(0.1f, 30.0f)] private float sprintSpeed;
    [SerializeField][Range(0.1f, 40.0f)] private float panicSpeed;

    [SerializeField][Range(1.0f, 15.0f)] private float walkVelocity;
    [SerializeField][Range(1.0f, 15.0f)] private float sprintVelocity;
    [SerializeField][Range(1.0f, 15.0f)] private float panicVelocity;

    [SerializeField][Range(0.0f, 5.0f)] private float airDrag = 0.3f;       //The amount of drag to apply to the character when airbourne.
    [SerializeField][Range(0.0f, 5.0f)] private float movingDrag = 1.5f;        //The amount of drag to apply to the Kiwi when moving.
    [SerializeField][Range(0.0f, 5.0f)] private float idleDrag = 2.5f;      //The amount of drag to apply to the Kiwi when stationary.

    [SerializeField]                   private Transform ground;                //The position of the ground.
    [SerializeField]                   private LayerMask whatIsGround;          //The layers that are considered a ground layer.
    [SerializeField][Range(0.0f, 1.0f)]private float groundRadius;			    //The radius of the circle used to check if the Kiwi is on the ground.

    [SerializeField]
    private AnimationCurve slopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));

    /*UNITY METHODS*/

    /**
	 * Method Name: Awake
	 * Description: Method is called upon script activation.
	 */
    void Awake() {
        state = GetComponent<KiwiState>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        //Update the drag.
        UpdateDrag();
        //Update Speed
        UpdateSpeed();
    }

    /**
	 * Method Name: FixedUpdate
	 * Description: Method updates once every physics step.
	 */
    void FixedUpdate() {
        GroundCheck();
        Move();
    }
    /*PRIVATE MUTATOR METHODS*/
    private void Move() {
        //Declare local variables.
        float xForce = HorizontalMovement();
        float yForce = VerticalMovement();
        //Apply the movement force to the Kiwi
        rigidbody.AddForce(new Vector2(xForce, yForce), ForceMode2D.Force);
    }

    private void GroundCheck() {
        state.WasGrounded = state.Grounded;
        state.Grounded = Physics2D.OverlapCircle(ground.position, groundRadius, whatIsGround);	//Check if the Kiwi is on the ground.
        if(state.Grounded) {
            if(state.Jump) {
                state.Jumping = true;
            }
            else {
                state.Jumping = false;
                state.Falling = false;
            }
            
        }
        else if (!state.Jumping && !state.Falling) {
            float distance = 0.1f;
            RaycastHit2D hit = Physics2D.Raycast(ground.position, Vector2.down, distance, whatIsGround);
            if (hit.collider != null) {
                StickToGround();
            }
            else {
                state.Falling = true;
            }   
        }
    }
	/**
	 * Method Name: HorizontalMovement
	 * Description: Method handles the Kiwi movement for the horizontal axis.
	 */
	private float HorizontalMovement() {
		//Declare local variables.
		float force    = 0;									//The movement force applied to the Kiwi. Default value of zero every method execution.
		float velocity = Mathf.Abs(rigidbody.velocity.x);	//The horizontal velocity of the Kiwi.

		//If there is movement along the horizontal axis...
		if(state.Moving && !state.AirCollision) {
			//...If the current velocity is less than the maximum velocity.
			if(velocity < velocityLimit.x) {
				//...Update the movement force.
				force = controller.movement.x * movementSpeed.x;
				//...Update the direction of the Kiwi.
				this.transform.localScale = new Vector3(controller.direction > 0 ? 0.35f : -0.35f, 0.35f, 0.35f);
			}
		}
		//Return the horizontal force.
		return force;
	}
	/**
	 * Method Name: VerticalMovement
	 * Description: Method handles the Kiwi movement for the vertical axis.
	 */
	private float VerticalMovement() {
		//Declare local variables.
		float force    = 0;									//The movement force applied to the Kiwi. Default value of zero every method execution.
		float velocity = Mathf.Abs(rigidbody.velocity.y);	//The horizontal velocity of the Kiwi.

		//If there is movement along the vertical axis and the Kiwi is on the ground...
		if(state.Jumping && state.Grounded) {
			//If the current velocity is less than the maximum velocity.
			if(velocity < velocityLimit.y) {
                //...Update the force
                force = state.SpeedY;
			}
		}
		//Return the vertical force.
		return force;
	}
	/**
	 * Method Name: UpdateSpeed
	 * Description: Method updates the speed and velocity limit of the Kiwi.
	 */ 
	private void UpdateSpeed() {
		//If the Kiwi is panicing...
		if(state.Panicing) {
            //...Update the speed to the panic speed.
            state.SpeedX = state.MovementX * panicSpeed;
            //...Update the velocity limit to the panic velocity.
            velocityLimit.x = panicVelocity; 
		}
		//...Else if Kiwi is sprinting...
		else if(state.Sprinting) {
            //...Update the speed to the sprint speed.
            state.SpeedX = state.MovementX * sprintSpeed;
            //...Update the velocity limit to the sprint velocity.
            velocityLimit.x = sprintVelocity;
		}
		//...Else the Kiwi is walking
		else {
			//...Update the speed to the walk speed.
			state.SpeedX = state.MovementX * walkSpeed;
            //...Update the velocity limit to the walk velocity.
            velocityLimit.y = walkVelocity;
		}
	}
	/**
	 * Method Name: UpdateDrag
	 * Description: Method updates the drag of the Kiwi.
	 */
	private void UpdateDrag() {
        if (!state.Grounded){
            rigidbody.drag = airDrag;
        }
        else if (state.MovementX != 0) {
            rigidbody.drag = movingDrag;
            
        }
        else {
            rigidbody.drag = idleDrag;
        }
	}

    private void StickToGround() {
        if(!state.Grounded && state.WasGrounded && !state.Jumping && !state.Falling) {
            rigidbody.AddForce(-transform.up, ForceMode2D.Force);
        }
    }
}
/*End of Script*/