using UnityEngine;
using System.Collections;

public class GiraObjeto : MonoBehaviour {

	void Update () {
		transform.Rotate(new Vector3(0, 55, 0) * Time.deltaTime);
	}

}
