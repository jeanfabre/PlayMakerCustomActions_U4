using UnityEngine;
using System.Collections;

namespace Com.HutongGames.Ecosystem
{
	[System.Serializable]
	public class Done_Boundary 
	{
		public float xMin, xMax, zMin, zMax;
	}

	public class MovementConstraint : MonoBehaviour
	{
		public float speed;
		public float tilt;
		public Done_Boundary boundary;

		void FixedUpdate ()
		{
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			GetComponent<Rigidbody>().velocity = movement * speed;
			
			GetComponent<Rigidbody>().position = new Vector3
				(
					Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
					0.0f, 
					Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
					);
			
			GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
		}
	}
}