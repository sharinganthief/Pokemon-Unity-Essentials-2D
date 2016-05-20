using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	Vector3 pos;                                // For movement
	float speed = 1.0f;                         // Speed of movement
	float distance = 0.32f;
	private Rigidbody2D rbody;
	private Animator anim;
	private SpriteRenderer sprite;
	private float leftTime = 0.0f;
	private float rightTime = 0.0f;
	private float upTime = 0.0f;
	private float downTime = 0.0f;
	public int facing = 0;
	bool transferred = false;


	// Use this for initialization
	void Start () {
		pos = transform.position;          // Take the initial position
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();
		updateFacing();
	}


	// Update is called once per frame
	void Update () {
		if (transferred) {
			transferred = false;
			pos = transform.position;
			leftTime = 0.0f;
			rightTime = 0.0f;
			upTime = 0.0f;
			downTime = 0.0f;
			anim.SetBool ("is_walking", false);
		}
		else {
			if(Input.GetButton("Left") && transform.position == pos) {        // Left
					leftTime += Time.deltaTime;
					rightTime = 0.0f;
					upTime = 0.0f;
					downTime = 0.0f;
					if (leftTime > 0.1f ) {
						if (PassabilityCheck.canPass(rbody, new Vector2(distance*-1, 0.0f))){
							pos += new Vector3(distance*-1, 0.0f, 0.0f);
							anim.SetBool ("is_walking", true);
						}
					}
					anim.SetFloat("input_x", -1);
					anim.SetFloat("input_y", 0);
			 }
			 if(Input.GetButton("Right") && transform.position == pos) {        // Right
					leftTime = 0.0f;
					rightTime += Time.deltaTime;
					upTime = 0.0f;
					downTime = 0.0f;
					if ( rightTime > 0.1f ) {
						if (PassabilityCheck.canPass(rbody, new Vector2(distance, 0.0f))){
							pos += new Vector3(distance, 0.0f, 0.0f);
							anim.SetBool ("is_walking", true);
					  }
					}
					anim.SetFloat("input_x", 1);
					anim.SetFloat("input_y", 0);
			 }
			 if(Input.GetButton("Up") && transform.position == pos) {        // Up
					leftTime = 0.0f;
					rightTime = 0.0f;
					upTime += Time.deltaTime;
					downTime = 0.0f;
					if ( upTime > 0.1f ) {
						if (PassabilityCheck.canPass(rbody, new Vector2(0.0f, distance))){
							pos += new Vector3(0.0f, distance, 0.0f);
							anim.SetBool ("is_walking", true);
						}
					}
					anim.SetFloat("input_x", 0);
					anim.SetFloat("input_y", 1);
			 }
			 if(Input.GetButton("Down") && transform.position == pos) {        // Down
					leftTime = 0.0f;
					rightTime = 0.0f;
					upTime = 0.0f;
					downTime += Time.deltaTime;
					if ( downTime > 0.1f ) {
						if (PassabilityCheck.canPass(rbody, new Vector2(0.0f, distance*-1))){
							pos += new Vector3(0.0f, distance*-1, 0.0f);
							anim.SetBool ("is_walking", true);
						}
					}
					anim.SetFloat("input_x", 0);
					anim.SetFloat("input_y", -1);
			 }
		 }

		 transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there

		 if(Vector3.Distance(transform.position, pos) < 0.05f){
			 	//if the player isn't hold down any buttons
				if (!Input.GetButton("Down") && !Input.GetButton("Up") && !Input.GetButton("Right") && !Input.GetButton("Left")){
					anim.SetBool ("is_walking", false);
				}
		 }
	}

	public void setTransferred(bool hasTransferred, int setDirection){
		transferred = hasTransferred;
		if (setDirection>=-2 && setDirection<=2 && setDirection!=0){
			Debug.Log(setDirection);
			facing = setDirection;
			updateFacing();
		}
	}

	void updateFacing(){
		anim.SetFloat("input_x", 0);
		anim.SetFloat("input_y", 0);
		//face character based on facing var
		if (facing == 1 || facing == -1){
			anim.SetFloat("input_x", facing);
		}
		if (facing == 2 || facing == -2) {
			anim.SetFloat("input_y", facing);
		}
	}




}
