using UnityEngine;
using System.Collections;

public class KiwiController : MonoBehaviour {

	/*DATA MEMBERS*/

	//Instantiate public class level objects.
	public Vector2 movement = new Vector2();	//Movment vector stores the x and y movement values of the Kiwi.

	public Transform ground;				//The position of the ground.
	public LayerMask whatIsGround;			//The layers that are considered a ground layer.
	[SerializeField][Range(0.0f, 1.0f)] 
	private float    groundRadius;			//The radius of the circle used to check if the Kiwi is on the ground.
	public  bool     onGround;				//If the Kiwi is currently on the ground.

	//Instantiate object references for rigidbody.
	private Rigidbody2D rigidBody;				//A reference to the Kiwi's rigidbody.
	public  Rigidbody2D dung;

	//Instantiate object references for animation.
	private Animator  	    animator;    		//A reference to the Animator.
	public  AnimationClip   deathAnimation;		//The death animation clip.
	public  AnimationClip   scaredAnimation;	//The scared animation clip.

	//Instantiate object references for audio.
	private AudioSource[] sources;				//An array of all the AudioSources attached to Kiwi's child GameObject's.
	private AudioSource	  walkSound;			//The Kiwi's walk sound AudioSource.
	private AudioSource	  jumpSound;			//The Kiwi's jump sound AudioSource.
	private AudioSource	  heartSound;			//The Kiwi's heart sound AudioSource.
	private AudioSource	  deathSound;			//The Kiwi's death sound AudioSource.
	private AudioSource	  panicSound;			//The Kiwi's panic sound AudioSource.

	//Instantiate object references for scripts.
	private KiwiHealth health;					//A reference to the Kiwi Health script.

	//Declare public class level variables.
	private bool peckButton;	
	private bool digButton;
	private bool sprintButton;
	
	//Declare public script level variables.
	public bool  airCollision;			//Boolean value is true the Kiwi collides with an object mid-air.
	public bool  xAxisMovement;			//If the Kiwi is moving along the x-axis.
	public bool  jump;					//If the Kiwi is moving along the y-axis.
	public bool  idle;					//If the Kiwi is doing nothing.
	public bool  walk;					//If the Kiwi is walking.
	public bool  sprint;				//If the Kiwi is sprinting.
	public float direction;				//The direction the Kiwi is facing
	public bool  scared;				//If the Kiwi is scared.
	public bool  panic;					//If the Kiwi is in panic mode.
	public bool  dig;					//If the Kiwi is digging.
	public bool  peck;					//If the Kiwi is pecking

	void Awake() {
		//Initialize component references.
		animator  = GetComponent<Animator>();				//Initialize reference to the animator component.
		sources	  = GetComponentsInChildren<AudioSource>();	//Initialize reference to the AudioSource components.
		rigidBody = GetComponent<Rigidbody2D>();			//Initialize reference to the rigidbody component.
		//Initialize script references.
		health    = GetComponent<KiwiHealth>();				//Initialize the reference to  the Kiwi Health script.
		//Initialize variables.
		walkSound  = sources[0];							//Initialize the walk sound AudioSource to the first cell in the sources array.
		jumpSound  = sources[1];							//Initialize the jump sound AudioSource to the second cell in the sources array.
		heartSound = sources[2];							//Initialize the heart sound AudioSource to the third cell in the sources array.
		deathSound = sources[3];							//Initialize the death sound AudioSource to the fourth cell in the sources array.
		panicSound = sources[4];							//Initialize the panic sound AudioSource to the fifth cell in the sources array.
		scared 	   = false;									//Initialize the scared variable to false, the Kiwi does not start out scared.
	}
	/**
	 * Method Name: Update
	 * Description: Method excutes once per frame.
	 */ 
	void Update () {
		//Get the input for movements.
		Movement();
		//Get the input for actions.
		Action();
		//Get the state of the Kiwi.
		State();
	}
	void FixedUpdate() {
		onGround = Physics2D.OverlapCircle(ground.position, groundRadius, whatIsGround);	//Check if the Kiwi is on the ground.
	}
	/**
	 * Method Name: LateUpdate
	 * Description: Method updates once per frame after Update method completes.
	 */
	void LateUpdate() {
		//Update the sound.
		Sound();
		//Update the animation.
		Animation();
	}
	/**
	 * Method Name: actions
	 * Description: Method captures the input for Kiwi actions.
	 */ 
	private void Action() {
		digButton    = false; //Input.GetButton("Dig");
		peckButton   = false; //Input.GetButtonDown("Peck");
		sprintButton = Input.GetButton("Sprint");
	}
	private void Animation() {
		animator.SetBool("OnGround", onGround);
		animator.SetBool("Dig",  dig);
		animator.SetBool("Peck", peck);
		animator.SetFloat("xVelocity", Mathf.Abs(rigidBody.velocity.x));
		animator.SetFloat("yVelocity", rigidBody.velocity.y);
	}
	/**
	 * Method Name: AirCollision
	 * Description: Method sets if an air collision took place.
	 */
	private void AirCollision() {
		//Set air collision to true if the Kiwi is not on the ground and the Kiwi is moving on the x-axis.
		airCollision = xAxisMovement && !onGround;
	}
	/**
	 * Method Name: movements
	 * Description: Method captures horizontal and vertical movement of the Kiwi.
	 */ 
	private void Movement() {
		movement.x = Input.GetAxis("Horizontal");
		movement.y = Input.GetButton("Jump") ? 1 : 0;
	}
	/**
	 * Method Name: State
	 * Description: Method updates the current state of the Kiwi.
	 */ 
	private void State() {
		xAxisMovement = movement.x != 0 && !(scared || health.dying);							//Check if the Kiwi is moving along the x-axis.
		walk   		  = xAxisMovement && !sprintButton && !panic;								//Check if the Kiwi is walking.
		sprint 		  = xAxisMovement && sprintButton && !panic;								//Check if the Kiwi is sprinting.
		jump 		  = movement.y != 0 && !(scared || health.dying);							//Check if the Kiwi is moving along the y-axis.
		peck   		  = peckButton && !(scared || health.dying); 								//Check if the Kiwi is pecking.
		dig    		  = digButton && onGround && !(scared || health.dying); 					//Check if the Kiwi is digging.
		idle   		  = onGround && !(xAxisMovement || peck || dig || health.dying);			//Check if the Kiwi is idle.
		direction 	  = movement.x >= 0 ? 1 : -1;												//Check the direction of the Kiwi.
	}
	/**
	 * Method Name: Sound
	 * Description: Method handles the execution and modification sound effects for the Kiwi.
	 */ 
	private void Sound() {
		//If the Kiwi is moving along the x-axis, is not jumping, and the walk sound is not playing.
		if (xAxisMovement && onGround && !walkSound.isPlaying && !jump) {
			//...If the Kiwi is walking...
			if (walk) {
				//...Modify the walk pitch to a low pitch.
				walkSound.pitch = 1.0f;
			}
			//...If the Kiwi is sprinting....
			else if (sprint) {
				//...Modify the walk pitch to a medium pitch.
				walkSound.pitch = 2.0f;
			}
			//...If the Kiwi is in panic mode...
			else if (panic) {
				//...Modify the walk pitch to a high pitch.
				walkSound.pitch = 3.0f;
			}
			//...Play the walk sound.
			walkSound.Play();
		}
		//...Else if the walk sound is playing and the kiwi is not moving along the x-axis...
		else if(walkSound.isPlaying && (!xAxisMovement || !onGround || jump)) {
			//...Stop playing the walk sound.
			walkSound.Stop();

		}
		//If the Kiwi bird is jumping and the jump sound is not playing...
		if(jump && onGround && !jumpSound.isPlaying) {
			//...Play the jump sound.
			jumpSound.Play();
		}
		//If the Kiwi bird is dying or dead...
		if (health.dying || health.dead) {
			//...Stop the heart beat.
			heartSound.Stop();

		} 
		//...Else if the Kiwi is scared or in panic mode...
		else if (scared || panic) {
			//...Increase the volume of the heart beat.
			heartSound.volume = 1.0f;
			//...Increase the pitch of the heart beat (heart is beating faster).
			heartSound.pitch = 2.0f;
		}
		//...Else (Kiwi is just walking or at rest...
		else {
			//...Set heart beat volume to default level.
			heartSound.volume = 0.25f;
			//...Set heart beat pitch to default level.
			heartSound.pitch  = 0.5f;
		}
	}
	/**
	 * Method Name: Dying
	 * Description: Method initiates the DyingCoroutine.
	 */ 
	private void Dying() {
		//Stop all coroutines before proceeding.
		StopAllCoroutines();
		//Start the DyingCoroutine.
		StartCoroutine(DyingCoroutine());
	}
	/**
	 * Method Name: Scared
	 * Description: Method initiates the ScaredCoroutine.
	 */ 
	private void Scared() {
		//Start the ScaredCoroutine.
		StartCoroutine(ScaredCoroutine());
	}
	/**
	 * Method Name: Unscared
	 * Description: Method enables the Kiwi bird to return from panic mode.
	 */ 
	private void Unscared() {
		panic = false;
	}
	/**
	 * Coroutine Name: DyingCoroutine
	 * Description   : Coroutine handles Kiwi behavior while the Kiwi is dying.
	 */ 
	private IEnumerator DyingCoroutine() {
		//Create a reference to the lantern GameObject.
		GameObject lantern = GameObject.FindGameObjectWithTag("Lantern");
		//Disable physics on the Kiwi.
		rigidBody.isKinematic = true;
		//Send a message to the lantern to disable it.
		lantern.gameObject.SendMessage("DisableLantern");
		//Update the location of the Kiwi.
		this.transform.position = new Vector3(0, 0, 100);
		//Set the Dying trigger to play the death animation in the animator.
		animator.SetTrigger("Dying");
		//Set the Dead bool in the animator.
		animator.SetBool("Dead", true);
		//Play the death sound effect.
		deathSound.PlayDelayed(1.0f);
		//Wait until the animation finishes playing before continuing.
		yield return new WaitForSeconds(deathAnimation.length);
		//The Kiwi is now dead.
		health.dead = true;
	}
	/**
	 * Coroutine Name: ScaredCoroutine
	 * Description   : Coroutine handles Kiwi behavior while the Kiwi is scared/entering panic mode.
	 */ 
	private IEnumerator ScaredCoroutine() {
		//The Kiwi is scared.
		scared = true;
		//Disable physics on the Kiwi.
		rigidBody.isKinematic = true;
		//Set the scared trigger to play the scared animation in the animator.
		animator.SetTrigger("Scared");
		//Play the scared sound effect.
		panicSound.Play();
		//Wait for the length of the animation.
		yield return new WaitForSeconds(scaredAnimation.length);
		//Kiwi is no longer in the scared state.
		scared = false;
		//Kiwi is now in the panic state.
		panic = true;
		//Renable physics on the Kiwi.
		rigidBody.isKinematic = false;
	}

	/*COLLISION METHODS*/
	/**
	 * Method Name: OnCollisionEnter2D
	 * Description: Method sets the behaviour when a collider enters another collider.
	 */
	private void OnCollisionEnter2D(Collision2D actor) {
		//Check if for air collision.
		AirCollision();
	}
	/**
	 * Method Name: OnCollisionStay2D
	 * Description: Method sets the behaviour when a collider enters another collider.
	 */
	private void OnCollisionStay2D(Collision2D actor) {
		//Check if for air collision.
		AirCollision();
	}
	/**
	 * Method Name: OnCollisionExit2D
	 * Description: Method sets behaviour when a collider exits another collider.
	 */
	private void OnCollisionExit2D() {
		//Set air collision to false.
		airCollision = false;
	}
	/**
	 * Method Name: Shit
	 * Description: Method instantiates a new dung prefab.
	 */ 
	public void Shit() {
		//Instantiate a new prefab.
		Instantiate(dung, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.2f), transform.rotation);
	}
}