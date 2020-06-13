using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Principal : MonoBehaviour {

	int _ovos;
	int _penas=4;
	int _vidas;

	int contaPisca;

	GameObject jogador;

	public Image imagemVidas;
	public Image iconeFelpudo;
	public Text textoVidas;
	public Text textoOvos;

	public Sprite[] iconesHudVida;
    
	public GameObject objetoVidro;

	public GameObject cameraIntro;
	public GameObject cameraJogador;
	public GameObject cameraEstrela;


	void Start(){

		jogador = GameObject.Find("JogadorFelpudo");

		iconeFelpudo.transform.position = new Vector2(iconeFelpudo.GetComponent<RectTransform>().sizeDelta.x/2 + 10 , Screen.height - iconeFelpudo.GetComponent<RectTransform>().sizeDelta.y/2 - 10);
		imagemVidas.transform.position = new Vector2(Screen.width/2 ,Screen.height - imagemVidas.GetComponent<RectTransform>().sizeDelta.y/2 - 10); 
		textoOvos.transform.position = new Vector2(Screen.width - textoOvos.GetComponent<RectTransform>().sizeDelta.x/2 -10 , Screen.height - textoOvos.GetComponent<RectTransform>().sizeDelta.y/2 - 10);

		var objects = GameObject.FindGameObjectsWithTag("Ovo");
		_ovos = objects.Length; 
		textoOvos.text = _ovos.ToString();

		imagemVidas.GetComponent<Image>().sprite = iconesHudVida[_penas];

		cameraEstrela.SetActive(false);
		cameraIntro.SetActive(true);
		cameraJogador.SetActive(true);
	}
	public void PegaOvo(){
		_ovos--;
		if(_ovos<=0)
		{
			_ovos=0;
			PegouTodosOvos();
		}
		textoOvos.text = _ovos.ToString();
	}
	public void PegaPena(){
		_penas++;
		if (_penas>8){_penas=8;}
		imagemVidas.GetComponent<Image>().sprite = iconesHudVida[_penas];
	}

	public void PerdePena(){
		_penas--;
		if (_penas<0){_penas=0; PerdeJogo();}
		imagemVidas.GetComponent<Image>().sprite = iconesHudVida[_penas];
	}

	public void PegaEstrela(){Invoke("RecarregaCena", 2f);}
	void PegouTodosOvos(){
		cameraEstrela.SetActive(true);
		cameraIntro.SetActive(false);
		cameraJogador.SetActive(false);
		Invoke("SomeVidro", 1.5f);
	}
	void GanhaJogo(){}
	void PerdeJogo(){}

	void SomeVidro(){
		objetoVidro.SetActive(false);
		Invoke("VoltaCamera", 1.5f);

	}

	void VoltaCamera(){
		cameraEstrela.SetActive(false);
		cameraIntro.SetActive(false);
		cameraJogador.SetActive(true);
	}

	public void CaiuNoBuraco(){
		Invoke("RecarregaCena", 2f);

	}
	public void RecarregaCena(){
		Application.LoadLevel("MinhaCena");

	}

	void EfeitoDePancada(){ 
		PerdePena();
		InvokeRepeating("PiscaFelpudo",0,0.15f);
		jogador.GetComponent<CharacterController>().Move(jogador.transform.TransformDirection(Vector3.back));

	}

	void PiscaFelpudo()
	{
		contaPisca++;
		jogador.SetActive(!jogador.activeInHierarchy);
		 
		if (contaPisca>7) {contaPisca=0;jogador.SetActive(true); CancelInvoke();}

	}
}
