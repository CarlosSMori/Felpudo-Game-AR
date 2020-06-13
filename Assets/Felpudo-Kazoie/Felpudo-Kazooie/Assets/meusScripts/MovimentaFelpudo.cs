using UnityEngine;
using System.Collections;
//using UnityStandardAssets.CrossPlatformInput;
//if(CrossPlatformInputManager.GetButton("Jump"))
//moveMove = CrossPlatformInputManager.GetAxis("Vertical")*moveCameraFrente + CrossPlatformInputManager.GetAxis("Horizontal")*transformCamera.right;

public class MovimentaFelpudo : MonoBehaviour {


	/*
 
 CHARACTER CONTROLLER IMPLEMENTACAO BASICA
 
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



 




	private float velocidade = 1.0f;
	private float giro = 180.0f;
	private float gravidade = 3.5f;
	private float pulo = 6.0f;
	private CharacterController objetoCharControler;
	private Vector3 vetorDirecao = new Vector3(0,0,0); 

	public GameObject jogador;
	public Animation animacao;

	void Start () { 
		objetoCharControler = GetComponent<CharacterController>(); 
		animacao = jogador.GetComponent<Animation>();
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

		if(Input.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) {
				vetorDirecao.y = pulo;
				jogador.GetComponent<Animation>().Play("jump");
			}
		}else
		{
			if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")  )
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

		vetorDirecao.y -= gravidade * Time.deltaTime;    
		objetoCharControler.Move(vetorDirecao * Time.deltaTime);
	}
*/


	CharacterController objetoCharControler;
	Transform transformCamera;

	Vector3 moveCameraFrente;
	Vector3 moveMove;
	Vector3 normalZeroPiso = new Vector3(0,0,0); 
	Vector3 vetorDirecao = new Vector3(0,0,0);

	public GameObject jogador;
	public Animation animacao; 

	float giro = 180.0f;
	float frente = 3.0f;
	float velocidade = 2.5f;
	float pulo = 5.0f;

	int numeroObjetos; 

	void Start () { 
		objetoCharControler = GetComponent<CharacterController>(); 
		animacao = jogador.GetComponent<Animation>(); 
		transformCamera = Camera.main.transform;
	}

	void Update (){ 

		moveCameraFrente = Vector3.Scale(transformCamera.forward, new Vector3(1, 0, 1)).normalized;
		moveMove = Input.GetAxis("Vertical")*moveCameraFrente + Input.GetAxis("Horizontal")*transformCamera.right;

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

		if(Input.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) {
				vetorDirecao.y = pulo;
				jogador.GetComponent<Animation>().Play("jump");
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
