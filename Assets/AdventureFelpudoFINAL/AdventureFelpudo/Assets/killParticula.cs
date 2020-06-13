using UnityEngine;
using System.Collections;

public class killParticula : MonoBehaviour {

	// Use this for initialization
	void Start () {


		InvokeRepeating("mataParticle", 1, 2.0f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void mataParticle()
	{ Destroy(this.gameObject);
	}

}
