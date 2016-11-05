using UnityEngine;
using System.Collections;

public class Bike : MonoBehaviour {

    public WheelJoint2D rearWheel;

	void Start () {

    }

	void Update () {
        rearWheel.useMotor = Input.GetKey(KeyCode.X);
        Debug.Log("key: " + Input.GetKey(KeyCode.X));
	}

    void FixedUpdate() {

    }
}
