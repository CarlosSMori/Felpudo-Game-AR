using UnityEngine;
using System.Collections;

public class actionPeninha : MonoBehaviour {

	private bool diminui;

	// Use this for initialization
	void Start () {
		Invoke("DiminuiPeninha", 1); 
	}
	
	// Update is called once per frame
	void Update () {
	 if(diminui)
		 {
			 if(this.transform.localScale.x>0.0f)
			 {
			 	transform.localScale -= new Vector3(0.01F,0.01F,0.01F);
				
			}else{
			 	Destroy(this.gameObject);
			 }
		 }
 }

	void DiminuiPeninha()
		
	{ 
		diminui = true;
		
	}

}
