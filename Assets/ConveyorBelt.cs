using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {
	public float speed;

	void FixedUpdate()
    {
    	Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
		Vector2 moveChange = transform.right * speed * Time.deltaTime;
        rigidbody.position -= moveChange;
        rigidbody.MovePosition (rigidbody.position + moveChange);

    }


}
