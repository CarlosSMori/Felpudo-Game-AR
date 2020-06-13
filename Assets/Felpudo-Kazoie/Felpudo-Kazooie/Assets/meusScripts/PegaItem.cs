using UnityEngine;
using System.Collections;

public class PegaItem : MonoBehaviour {
	GameObject objetoPrincipal;
 

	public Color corParticulas;
	public GameObject particula;

	void Start () {
		objetoPrincipal = GameObject.Find("GameEngine");  
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "Player")
		{   
			

			switch (gameObject.tag){
			case "Ovo": objetoPrincipal.SendMessage("PegaOvo"); break;
			case "Pena": objetoPrincipal.SendMessage("PegaPena"); break;
			case "Estrela": objetoPrincipal.SendMessage("PegaEstrela"); break;
			case "Fogo": objetoPrincipal.SendMessage("EfeitoDePancada"); break;
			case "Finish": objetoPrincipal.SendMessage("CaiuNoBuraco"); break;
				default: break;
			}

			if(particula != null){
				GameObject minhaParticula = Instantiate(particula);
				minhaParticula.transform.position = this.transform.position; 

				minhaParticula.GetComponent<ParticleSystem>().startColor = corParticulas;
				Destroy(this.gameObject); 
			}
			  
		} 
	}


}
