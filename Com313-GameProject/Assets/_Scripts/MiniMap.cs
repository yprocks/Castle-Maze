﻿using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

    public Transform Target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z);

	
	}
}
