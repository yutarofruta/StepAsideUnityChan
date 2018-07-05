using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerController : MonoBehaviour {

    public GameObject unitychan;

    private void Start() {
    }

    private void Update() {
        transform.position = new Vector3(0, 1, unitychan.transform.position.z - 2);
        //Debug.Log(unitychan.transform.position);
    }

    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag != "RoadTag" && other.gameObject.tag != "FieldTag") {
            Destroy(other.gameObject);
        }
    }
}
