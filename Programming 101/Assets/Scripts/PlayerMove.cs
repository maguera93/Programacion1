using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    private Transform myTransform;
    private Vector2 axisMove;
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

        animator.SetFloat("X", axisMove.x);
        animator.SetFloat("Y", axisMove.y);

        myTransform.Translate(axisMove * speed * Time.deltaTime);
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
