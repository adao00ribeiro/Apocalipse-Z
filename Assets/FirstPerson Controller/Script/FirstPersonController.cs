using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class FirstPersonController : MonoBehaviour {
	[SerializeField]
	private float walkSpeed=3f;
	[SerializeField]
	private float runSpeed=6f;
	[SerializeField]
	private float crouchSpeed=1f;
	[SerializeField]
	private float proneSpeed=0.25f;
	
	[SerializeField]
	private float speed;
	
	[SerializeField]
	private bool inAirControl = true;

	[SerializeField]
	private float maxJumpPower=6;
	private float jumpPower;

	private float maxGravity=12;
	private float gravity;
	private float gravityDelta=0.25f;

	private float moveX;
	private float moveY;

	private Vector3 direction=Vector3.zero;
	private Vector3 xDir=Vector3.zero;
	private Vector3 yDir=Vector3.zero;

	private float mouseX;
	private float mouseY;
	
	public float mouseSens=3;
	
	[SerializeField]
	private Vector2 mouseLimmit = new Vector2 (80, 280);

	private Vector3 chRot=Vector3.zero;
	private Vector3 camRot=Vector3.zero;

	private bool standing;
	private bool running;
	private bool crouching;
	private bool proning;
	private bool jumping;
	private bool falling;
	private bool isGrounded = false;
	
	[SerializeField]
	private float DeAcceleration=0.05f;
	
	[SerializeField]
	private float stateChangeSpeed=0.03f;

	private bool changingState=false;

	[SerializeField]
	private CharacterState normal = new CharacterState(0.35f,1.8f);
	[SerializeField]
	private CharacterState crouch = new CharacterState(0.35f,1.2f);
	[SerializeField]
	private CharacterState prone = new CharacterState(0.2f,0.4f);
	
	public KeyCode runKey = KeyCode.LeftShift;
	public KeyCode crouchKey = KeyCode.C;
	public KeyCode proneKey = KeyCode.LeftControl;
	public KeyCode jumpKey = KeyCode.Space;
	
	private CharacterState old_state;
	
	private float rayCheckHeight = 0.15f;

	private CharacterController ch;
	private Transform cam;
	// Use this for initialization
	void Start () {
		old_state =  normal;
		ch = GetComponent<CharacterController>();
		cam = transform.FindChild("Camera");
	}

	// Update is called once per frame
	void Update () {
		Look ();
		Move ();
	}

	IEnumerator Fall()
	{
		falling = true;
		gravity = 0;
		while (!isGrounded)
		{
			gravity = Mathf.MoveTowards (gravity, maxGravity, gravityDelta);
			yield return new WaitForEndOfFrame ();
		}
		falling = false;
		gravity = 0;
	}

	IEnumerator Jump()
	{
		jumping = true;
		jumpPower = maxJumpPower;
		while (jumpPower!=0)
		{
			if (ch.collisionFlags == CollisionFlags.CollidedAbove)
				break;
			jumpPower = Mathf.MoveTowards (jumpPower, 0, gravityDelta);
			yield return new WaitForEndOfFrame ();
		}
		jumping = false;
	}

	IEnumerator GoToState(CharacterState st)
	{
		CharacterState newState = st;
		
		if (changingState)
			yield break;
		changingState = true;
		Vector3 newCamPos=Vector3.zero;
		while (ch.height!=newState.height || ch.radius != newState.radius)
		{
			if (old_state.height < newState.height && HitHead())
			{
				newState = old_state;
				if(newState == crouch)
					crouching=true;
				else if(newState == prone)
					proning=true;
			}
			ch.height = Mathf.MoveTowards (ch.height, newState.height, stateChangeSpeed);
			ch.radius = Mathf.MoveTowards (ch.radius, newState.radius, stateChangeSpeed/5);
			ch.center = new Vector3 (0, ch.height/2);
			newCamPos = cam.localPosition;
			newCamPos.y = ch.height - 0.1f;
			cam.localPosition = newCamPos;
			yield return new WaitForEndOfFrame ();
		}
			changingState = false;
			old_state = newState;
	}
	
	bool HitHead()
	{
		Vector3 dwn = transform.TransformDirection(Vector3.up);
		Vector3 startPos = transform.position;
		startPos.y += ch.height + rayCheckHeight;
		if (Physics.Raycast(startPos, dwn, rayCheckHeight)) {
			return true;
		}

		return false;
	}

	bool CheckOnGround()
	{
		Vector3 dwn = transform.TransformDirection(Vector3.down);
		Vector3 startPos = transform.position;
		if (Physics.Raycast(startPos, dwn, rayCheckHeight)) {
			return true;
		}

		return false;
	}

	void Look()
	{
		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");

		chRot = transform.eulerAngles;
		//camRot = cam.localEulerAngles;

		chRot.y += mouseX * mouseSens;
		camRot.x += -mouseY * mouseSens;

		if (camRot.x > mouseLimmit.x && camRot.x < 180)
			camRot.x = mouseLimmit.x;
		if(camRot.x < mouseLimmit.y && camRot.x > 180)
			camRot.x = mouseLimmit.y;
		
		transform.eulerAngles = chRot;
		//cam.localEulerAngles = camRot;
	}

	void Move()
	{
		if(isGrounded)
		{
		moveX = Input.GetAxis ("Horizontal");
		moveY = Input.GetAxis ("Vertical");
		}
		
		if (!isGrounded && inAirControl) {
			moveX = Input.GetAxis ("Horizontal");
			moveY = Input.GetAxis ("Vertical");
		}
		
		isGrounded = CheckOnGround ();

		if (ch.isGrounded)
			isGrounded = true;

		xDir = moveX*transform.right;
		yDir = moveY*transform.forward;

		if (Input.GetKey (runKey) && !crouching && ! proning && !changingState && isGrounded) {
			if (moveY > 0) {
				if(isGrounded)
					speed = runSpeed;
				running = true;
				standing = false;
			}
			else {
				if(isGrounded)
					speed = walkSpeed;
				running = false;
			}
		}

		else if (Input.GetKeyUp (runKey)) {
			running = false;
		}

		else if (Input.GetKeyDown (crouchKey) && !changingState) {
			crouching = !crouching;
			proning = false;
			standing = false;
			if (crouching)
				StartCoroutine(GoToState(crouch));
			else
				StartCoroutine(GoToState(normal));
		}

		else if (Input.GetKeyDown (proneKey) && !changingState) {
			proning = !proning;
			crouching = false;
			standing = false;
			if (proning)
				StartCoroutine(GoToState(prone));
			else
				StartCoroutine(GoToState(normal));
		}

		else if (crouching) {
			if(isGrounded)
				speed = crouchSpeed;
		}

		else if (proning) {
			if(isGrounded)
				speed = proneSpeed;
		}

		else {
			if(isGrounded)
				speed = walkSpeed;
			standing = true;
		}
		
		if(!isGrounded && speed > 0)
		{
			speed = Mathf.MoveTowards(speed,0,DeAcceleration);
			if(speed < 0)
				speed = 0;
		}
		
		xDir *= speed;
		yDir *= speed;
		
		direction = xDir + yDir;

		if (Input.GetKeyDown (jumpKey)) {
			if (crouching || proning && !changingState) {
				crouching = false;
				proning = false;
				StartCoroutine (GoToState (normal));
			} else if (isGrounded && !changingState)
				StartCoroutine (Jump());
		}

		if (!isGrounded && !jumping) {
			if (!falling) {
				StartCoroutine (Fall());
			}
			direction.y -= gravity;
		}

		if (!jumping && !falling)
			direction.y -= maxGravity;
		if (jumping)
			direction.y += jumpPower;
		if (!isGrounded) {
			running = false;
		}
		
		ch.Move (direction *Time.deltaTime);
	}
}

[System.Serializable]
public class CharacterState
{
	public float radius;
	public float height;

	public CharacterState(float rad,float hei)
	{
		radius = rad;
		height = hei;
	}
}