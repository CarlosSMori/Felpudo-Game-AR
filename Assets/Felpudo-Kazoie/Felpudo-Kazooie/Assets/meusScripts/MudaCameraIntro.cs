using UnityEngine;
using System.Collections;

public class MudaCameraIntro : MonoBehaviour {

	public GameObject cameraPersonagem;
 
	public void MudaCamera(){

		this.gameObject.SetActive(false);
		cameraPersonagem.SetActive(true);

	}
}
