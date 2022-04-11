using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPACMAN : MonoBehaviour
{
    [SerializeField]
    private GameObject pacman;
    [SerializeField]
    private Rigidbody2D rgbdPacman;
    private Vector2 playerInput;
    [SerializeField, Range(0.1f, 10f)]
    private float speed;

    void Update()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        Movement(playerInput);
    }

    private void Movement(Vector2 direction)
    {
        //pacman.transform.position += new Vector3 (direction.x, direction.y, 0f) * speed * Time.deltaTime;
        rgbdPacman.velocity = new Vector3(direction.x, direction.y, 0f) * speed;
    }
}
