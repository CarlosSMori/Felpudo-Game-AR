using UnityEngine;
using System.Collections;

public class ControleNormalizado : MonoBehaviour {

	public GameObject peninhas;

	CharacterController objetoCharControler;
	Transform transformCamera;

	Vector3 moveCameraFrente;
	Vector3 moveMove;
	Vector3 normalZeroPiso = new Vector3(0,0,0); 
	Vector3 vetorDirecao = new Vector3(0,0,0);

	public GameObject jogador;
	public Animation animacao;   

	float giro = 3.0f;
	float frente = 3.0f;
	float velocidade = 5.0f;
	float pulo = 5.0f;

	int numeroObjetos; 



	void Start () { 
		objetoCharControler = GetComponent<CharacterController>(); 
		animacao = jogador.GetComponent<Animation>(); 
		transformCamera = Camera.main.transform;
		objetoCharControler.material.bounciness = 0.0f;
	}

	void Update (){ 

		moveCameraFrente = Vector3.Scale(transformCamera.forward, new Vector3(1, 0, 1)).normalized;
		moveMove = Input.GetAxis("Vertical")*moveCameraFrente + Input.GetAxis("Horizontal")*transformCamera.right;

		vetorDirecao.y -= 5.0f * Time.deltaTime;	
		objetoCharControler.Move(vetorDirecao * Time.deltaTime);
		objetoCharControler.Move(moveMove * velocidade * Time.deltaTime);

		if (moveMove.magnitude > 1f) moveMove.Normalize();
		moveMove = transform.InverseTransformDirection(moveMove);

		moveMove = Vector3.ProjectOnPlane(moveMove, normalZeroPiso);
		giro = Mathf.Atan2(moveMove.x, moveMove.z);
		frente = moveMove.z;

		objetoCharControler.SimpleMove(Physics.gravity);
		aplicaRotacao();

		if(Input.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) {
				vetorDirecao.y = pulo;
				jogador.GetComponent<Animation>().Play("jump"); 
//				Instantiate(peninhas, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y+1, this.gameObject.transform.position.z), Quaternion.identity);
			
				GameObject particula = Instantiate(peninhas);
				particula.transform.position = this.transform.position; 
			
			}
		}else
		{
			if((Input.GetAxis("Horizontal") != 0.0f) || (Input.GetAxis("Vertical") != 0.0f) )
			{
				if (!animacao.IsPlaying("jump"))
				{	 
					jogador.GetComponent<Animation>().Play("walk");
				}
			}else
			{
				if (objetoCharControler.isGrounded == true) 
				{	
					jogador.GetComponent<Animation>().Play("idle");
				}
			}
		}
	}


	void aplicaRotacao()
	{
		float turnSpeed = Mathf.Lerp(180, 360, frente);
		transform.Rotate(0, giro * turnSpeed * Time.deltaTime, 0);
	}
}
