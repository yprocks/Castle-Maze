using UnityEngine;
using System.Collections;

public class FinishScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("FPSController").SendMessage("finish");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
