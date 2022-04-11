using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_FANTOM1 : MonoBehaviour
{
    private State currentState;
    public LayerMask layerMask;

    public Transform trsfPACMAN;
    public Caretaker caretaker;

    [SerializeField]
    private SpriteRenderer eyeL, eyeR;

    private void Start()
    {
        SetState(new MoveRandomly(this, eyeL, eyeR, trsfPACMAN, caretaker));
        trsfPACMAN = FindObjectOfType<DeplacementPACMAN>().gameObject.transform;
    }

    private void Update()
    {
        currentState.Tick();
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        gameObject.name = "FANTOM1 - " + state.GetType().Name;

        if (currentState != null)
            currentState.OnStateEnter();
    }
}

public abstract class State
{
    protected Behaviour_FANTOM1 fantom1;
    protected SpriteRenderer eyeL, eyeR;
    protected Transform trsfPACMAN;
    protected Caretaker caretaker;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(Behaviour_FANTOM1 fantom1, SpriteRenderer eyeL, SpriteRenderer eyeR, Transform trsfPACMAN, Caretaker caretaker)
    {
        this.fantom1 = fantom1;
        this.eyeL = eyeL;
        this.eyeR = eyeR;
        this.trsfPACMAN = trsfPACMAN;
        this.caretaker = caretaker;
    }
}

public class MoveRandomly : State
{
    private float detectionRadius = 4f;
    private float rotatingAngle;
    private Vector3 currentDirection;
    private Vector3[] directions = new Vector3[4] { Vector3.right, Vector3.up, Vector3.down, Vector3.left
};

    public MoveRandomly(Behaviour_FANTOM1 fantom1, SpriteRenderer eyeL, SpriteRenderer eyeR, Transform trsfPACMAN, Caretaker caretaker) : base(fantom1, eyeL, eyeR, trsfPACMAN, caretaker)
    {
    }

    public override void Tick()
    {
        rotatingAngle += 1.5f;

        if (playerInSight())
        {
            fantom1.SetState(new ChasePlayer(fantom1, eyeL, eyeR, trsfPACMAN, caretaker));
        }
        else
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(fantom1.transform.position, currentDirection, 0.5f, fantom1.layerMask);
            var rgbdTouche = raycastHit2D.rigidbody;
            if (rgbdTouche != null)
            {
                var randomInt = UnityEngine.Random.Range(0, 4);
                currentDirection = directions[randomInt];
            }
            Movement();
            MoveEyes();
        }
    }

    public override void OnStateEnter()
    {
        eyeL.color = Color.black;
        eyeR.color = Color.black;
        rotatingAngle = UnityEngine.Random.Range(0f, 2 * Mathf.PI);
        currentDirection = Vector3.right;
    }

    private bool playerInSight()
    {
        return Vector2.Distance(trsfPACMAN.position, fantom1.transform.position) < detectionRadius;
    }

    private void Movement()
    {
        if (caretaker.GetEnregistre())
        {
            fantom1.GetComponent<Rigidbody2D>().velocity = currentDirection * 4f;
        }
        else
        {
            fantom1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    private void MoveEyes()
    {
        var angle = rotatingAngle;
        eyeL.transform.localPosition = new Vector3(Mathf.Cos(2f * Mathf.PI * angle / 360f), Mathf.Sin(2f * Mathf.PI * angle / 360f), 0f) * 0.27f;
        angle = -rotatingAngle;
        eyeR.transform.localPosition = new Vector3(Mathf.Cos(2f * Mathf.PI * angle / 360f), Mathf.Sin(2f * Mathf.PI * angle / 360f), 0f) * 0.27f;
    }
}

public class ChasePlayer : State
{
    private float detectionRadius = 4f;
    private Vector3 currentDirection;
    private int directionTried;
    private Vector3[] directionsToTry;

    public ChasePlayer(Behaviour_FANTOM1 fantom1, SpriteRenderer eyeL, SpriteRenderer eyeR, Transform trsfPACMAN, Caretaker caretaker) : base(fantom1, eyeL, eyeR, trsfPACMAN, caretaker)
    {
    }

    public override void Tick()
    {
        if (playerInSight())
        {
            if (directionTried == 0) directionsToTry = FastestDirection();
            RaycastHit2D raycastHit2D = Physics2D.Raycast(fantom1.transform.position, directionsToTry[directionTried], 0.5f, fantom1.layerMask);
            var rgbdTouche = raycastHit2D.rigidbody;
            if (rgbdTouche != null)
            {
                directionTried ++;
                if (directionTried == 4)
                {
                    directionsToTry = FastestDirection();
                    directionTried = 0;
                }
            }
            Movement();
            MoveEyes();
        }
        else
        {
            fantom1.SetState(new MoveRandomly(fantom1, eyeL, eyeR, trsfPACMAN, caretaker));
        }
    }

    public override void OnStateEnter()
    {
        eyeL.color = Color.red;
        eyeR.color = Color.red;
        directionTried = 0;
        directionsToTry = FastestDirection();
    }

    private bool playerInSight()
    {
        return Vector2.Distance(trsfPACMAN.position, fantom1.transform.position) < detectionRadius;
    }

    private void MoveEyes()
    {
        var angle = Vector2.SignedAngle(Vector3.right, trsfPACMAN.position - eyeL.transform.position);
        eyeL.transform.localPosition = new Vector3(Mathf.Cos(2f * Mathf.PI * angle / 360f), Mathf.Sin(2f * Mathf.PI * angle / 360f), 0f) * 0.27f;
        angle = Vector2.SignedAngle(Vector3.right, trsfPACMAN.position - eyeR.transform.position);
        eyeR.transform.localPosition = new Vector3(Mathf.Cos(2f * Mathf.PI * angle / 360f), Mathf.Sin(2f * Mathf.PI * angle / 360f), 0f) * 0.27f;
    }

    private Vector3[] FastestDirection()
    {
        var angle = Vector2.SignedAngle(Vector3.right, trsfPACMAN.position - eyeL.transform.position);
        if (-45f <= angle && angle <= 45f)
        {
            return new Vector3[4] { Vector3.right, Vector3.up, Vector3.down, Vector3.left};
        }
        else if (45f <= angle && angle <= 135f)
        {
            return new Vector3[4] { Vector3.up, Vector3.left, Vector3.right, Vector3.down};
        }
        else if (-135f <= angle && angle <= -45f)
        {
            return new Vector3[4] { Vector3.down, Vector3.right, Vector3.left, Vector3.up };
        }
        else
        {
            return new Vector3[4] { Vector3.left, Vector3.down, Vector3.up, Vector3.right};
        }
    }

    private void Movement()
    {
        if (caretaker.GetEnregistre())
        {
            fantom1.GetComponent<Rigidbody2D>().velocity = directionsToTry[directionTried] * 4f;
        }
        else
        {
            fantom1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}
