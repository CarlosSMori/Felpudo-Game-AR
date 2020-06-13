using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ControleJoystick : MonoBehaviour {

	CharacterController objetoCharControler;
	Transform transformCamera;
	public GameObject peninhas;
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
	}

	void Update (){ 
		 
		moveCameraFrente = Vector3.Scale(transformCamera.forward, new Vector3(1, 0, 1)).normalized;
		moveMove = CrossPlatformInputManager.GetAxis("Vertical")*moveCameraFrente + CrossPlatformInputManager.GetAxis("Horizontal")*transformCamera.right;

		vetorDirecao.y -= 3.0f * Time.deltaTime;	
		objetoCharControler.Move(vetorDirecao * Time.deltaTime);
		objetoCharControler.Move(moveMove * velocidade * Time.deltaTime);

		if (moveMove.magnitude > 1f) moveMove.Normalize();
		moveMove = transform.InverseTransformDirection(moveMove);

		moveMove = Vector3.ProjectOnPlane(moveMove, normalZeroPiso);
		giro = Mathf.Atan2(moveMove.x, moveMove.z);
		frente = moveMove.z;

		objetoCharControler.SimpleMove(Physics.gravity);
		aplicaRotacao();

		if(CrossPlatformInputManager.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) {
				vetorDirecao.y = pulo;
				jogador.GetComponent<Animation>().Play("jump");
				GameObject particula = Instantiate(peninhas);
				particula.transform.position = this.transform.position;
			}
		}else
		{
			if((CrossPlatformInputManager.GetAxis("Horizontal") != 0.0f) || (CrossPlatformInputManager.GetAxis("Vertical") != 0.0f) )
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
