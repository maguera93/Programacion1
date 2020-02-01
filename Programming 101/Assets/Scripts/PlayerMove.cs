using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    private Transform myTransform;
    private Vector2 axisMove;
    private Vector2 axis;
    [SerializeField] private LayerMask studentLayer;
    private bool studentTouched;
    private bool helping;
    private StudentBehaviour student;
    private Collider2D[] hits = new Collider2D[10];

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!helping)
        {
            Move();

            if (studentTouched && Input.GetButtonDown("Jump"))
            {
                helping = true;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                student.GetHelp(1);
            }
        }

        animator.SetBool("repairing", helping);
    }

    private void Move()
    {
        axisMove.x = Input.GetAxis("Horizontal");

        axisMove.y = Input.GetAxis("Vertical");

        MoveHorizontal();
        MoveVertical();

        animator.SetFloat("X", axisMove.x);
        animator.SetFloat("Y", axisMove.y);

        myTransform.Translate(axis * Time.deltaTime);
    }

    void MoveHorizontal()
    {
        if (myTransform.position.x >= 8f && axisMove.x > 0 || myTransform.position.x < -8f && axisMove.x < 0)
        {
            axis.x = 0;
            return;
        }

        axis.x = axisMove.x * speed;
    }
    void MoveVertical()
    {
        if (myTransform.position.y >= 4.5f && axisMove.y > 0 || myTransform.position.y < -4.5f && axisMove.y < 0)
        {
            axis.y = 0;
            return;
        }

        axis.y = axisMove.y * speed;
    }

    private void FixedUpdate()
    {
        studentTouched = false;

        hits = new Collider2D[10];
        Physics2D.OverlapBoxNonAlloc(myTransform.position, new Vector2(1, 1), 0, hits, studentLayer);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] != null)
            {
                studentTouched = true;
                student = hits[i].transform.parent.GetComponent<StudentBehaviour>();
            }
        }
    }

    public void StudentHelped()
    {
        helping = false;
    }
}
