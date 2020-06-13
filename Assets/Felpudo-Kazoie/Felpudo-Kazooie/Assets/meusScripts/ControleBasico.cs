using UnityEngine;
using System.Collections;

public class ControleBasico : MonoBehaviour {

	private float velocidade = 1.0f;
	private float giro = 180.0f;
	private float gravidade = 3.5f;
	private float pulo = 5.0f;
	private CharacterController objetoCharControler;
	private Vector3 vetorDirecao = new Vector3(0,0,0); 

	void Start () { 
		objetoCharControler = GetComponent<CharacterController>(); 
	}

	void Update (){ 
		if (Input.GetKey(KeyCode.LeftShift)) { velocidade = 2.5f; } else{velocidade = 5;}

		Vector3 forward = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * velocidade;
		transform.Rotate(new Vector3(0,Input.GetAxis("Horizontal") * giro *Time.deltaTime,0));

		objetoCharControler.Move(forward * Time.deltaTime);
		objetoCharControler.SimpleMove(Physics.gravity);

		if(Input.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) { vetorDirecao.y = pulo; }
		} 
		vetorDirecao.y -= gravidade * Time.deltaTime;    
		objetoCharControler.Move(vetorDirecao * Time.deltaTime);
	}
}
