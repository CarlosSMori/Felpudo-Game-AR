using UnityEngine;
using System.Collections;

public class ApagaObjeto : MonoBehaviour {

	public float tempo;

	void Start () {
		Invoke("Apaga", tempo);
	} 

	void Apaga(){ 
		Destroy(this.gameObject); 
	}
}
