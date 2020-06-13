using UnityEngine;
using System.Collections;

public class acaoMoveJogador : MonoBehaviour {

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
				jogador.GetComponent<Animation>().Play("JUMP");
			}
		}else
		{
			if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")  )
			{
								if (!animacao.IsPlaying("JUMP"))
								{	 
									jogador.GetComponent<Animation>().Play("WALK");
								}
				
				
				
			}else
			{
				if (objetoCharControler.isGrounded == true) 
				{	
					jogador.GetComponent<Animation>().Play("IDLE");
				}
			}
		}




		vetorDirecao.y -= gravidade * Time.deltaTime;	
	    objetoCharControler.Move(vetorDirecao * Time.deltaTime);
	}
}
