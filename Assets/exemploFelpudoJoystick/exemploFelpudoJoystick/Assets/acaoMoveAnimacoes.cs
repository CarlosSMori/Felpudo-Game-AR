using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;		
public class acaoMoveAnimacoes : MonoBehaviour {

	private float velocidade = 3.5f;
	private float giro = 180.0f;
	private float gravidade = 3.5f;
	private float pulo = 5.0f;

	private CharacterController objetoCharControler;
	

	private Vector3 vetorDirecao = new Vector3(0,0,0); 
	
	public GameObject jogador;
	public Animation animacao;
	
	void Start () {
		objetoCharControler = GetComponent<CharacterController>();
		 
		animacao = jogador.GetComponent<Animation>();
	}
	
	void Update () 
	{ 
		//if (Input.GetKey(KeyCode.LeftShift)) { velocidade = 2.5f; } else{velocidade = 5;}
		
		Vector3 forward = CrossPlatformInputManager.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * velocidade;
		transform.Rotate(new Vector3(0,CrossPlatformInputManager.GetAxis("Horizontal") * giro *Time.deltaTime,0));
		
		objetoCharControler.Move(forward * Time.deltaTime);
		objetoCharControler.SimpleMove(Physics.gravity);
		
		if(CrossPlatformInputManager.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) {
				vetorDirecao.y = pulo;
				animacao.CrossFade("JUMP", 0.15f);
				//jogador.GetComponent<Animation>().Play("JUMP");
			}
		}else
		{
			if((CrossPlatformInputManager.GetAxis("Horizontal") != 0.0f) || (CrossPlatformInputManager.GetAxis("Vertical") != 0.0f) )
			{
				if (!animacao.IsPlaying("JUMP"))
				{	 
					animacao.CrossFade("WALK", 0.5f);
					//jogador.GetComponent<Animation>().Play("WALK");
				}
				
				
				
			}else
			{
				if (objetoCharControler.isGrounded == true) 
				{	
					animacao.CrossFade("IDLE", 0.5f);
					//jogador.GetComponent<Animation>().Play("IDLE");
				}
			}
		}
		 
		vetorDirecao.y -= gravidade * Time.deltaTime;	
	    objetoCharControler.Move(vetorDirecao * Time.deltaTime);
	}
}
