using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memento : MonoBehaviour
{
    private Vector3[] state = new Vector3[1];
    private Transform trsfPACMAN;

    // Start is called before the first frame update
    void Awake()
    {
        trsfPACMAN = FindObjectOfType<DeplacementPACMAN>().gameObject.GetComponent<Transform>();
        var posPACMAN = new Vector3(trsfPACMAN.position.x, trsfPACMAN.position.y, 0f);
        state[0] = posPACMAN;
    }

    // Update is called once per frame
    public Vector3[] GetState()
    {
        return state;
    }
}
