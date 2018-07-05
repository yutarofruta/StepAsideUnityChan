using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour {

    private GameObject unitychan;
    private float offsetZ = 10f;

    // Use this for initialization
    void Start () {
        unitychan = GameObject.Find("unitychan");
	}
	
	// Update is called once per frame
	void Update () {

        if(this.transform.position.z + offsetZ < unitychan.transform.position.z) {
            Destroy(gameObject);
        }

    }
}
