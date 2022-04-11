using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memento : MonoBehaviour
{
    private Vector3[] state = new Vector3[6];
    private Transform trsfPACMAN;
    private Transform trsfFANTOM1;
    private Transform trsfFANTOM2;
    private Transform trsfFANTOM3;
    private Transform trsfFANTOM4;
    private Transform trsfFANTOM5;

    // Start is called before the first frame update
    void Awake()
    {
        trsfPACMAN = FindObjectOfType<DeplacementPACMAN>().gameObject.GetComponent<Transform>();
        var posPACMAN = new Vector3(trsfPACMAN.position.x, trsfPACMAN.position.y, 0f);
        state[0] = posPACMAN;
        trsfFANTOM1 = GameObject.FindGameObjectsWithTag("FANTOM")[0].gameObject.GetComponent<Transform>();
        var posFANTOM1 = trsfFANTOM1.position;
        state[1] = posFANTOM1;
        trsfFANTOM2 = GameObject.FindGameObjectsWithTag("FANTOM")[1].gameObject.GetComponent<Transform>();
        var posFANTOM2 = trsfFANTOM2.position;
        state[2] = posFANTOM2;
        trsfFANTOM3 = GameObject.FindGameObjectsWithTag("FANTOM")[2].gameObject.GetComponent<Transform>();
        var posFANTOM3 = trsfFANTOM3.position;
        state[3] = posFANTOM3;
        trsfFANTOM4 = GameObject.FindGameObjectsWithTag("FANTOM")[3].gameObject.GetComponent<Transform>();
        var posFANTOM4 = trsfFANTOM4.position;
        state[4] = posFANTOM4;
        trsfFANTOM5 = GameObject.FindGameObjectsWithTag("FANTOM")[4].gameObject.GetComponent<Transform>();
        var posFANTOM5 = trsfFANTOM5.position;
        state[5] = posFANTOM5;
    }

    // Update is called once per frame
    public Vector3[] GetState()
    {
        return state;
    }
}
