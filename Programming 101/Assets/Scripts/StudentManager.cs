using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentManager : MonoBehaviour
{
    [SerializeField] private GameObject studentPrefab;
    [SerializeField] private int xSize, ySize;
    private StudentBehaviour[] students;
    private Vector3[] positions;
    [SerializeField] private string[] messages;
    private float activateStudentTime;

    // Start is called before the first frame update
    void Start()
    {
        students = new StudentBehaviour[xSize * ySize];
        positions = new Vector3[xSize * ySize];
        GenerateStudents();
        StartCoroutine(ActivateStudent());
    }

    private void GenerateStudents()
    {
        positions[0]= transform.position;
        for (int i = 0, y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++, i++)
            {
                positions[i] = transform.position + new Vector3(3f * x, -3f * y, 0);
                GameObject s = Instantiate(studentPrefab, positions[i], Quaternion.identity);
                students[i] = s.GetComponent<StudentBehaviour>();
            }
        }
    }

    private IEnumerator ActivateStudent()
    {
        while(true)
        {
            activateStudentTime = Random.Range(3f, 5f);
            int s = Random.Range(0, students.Length);

            while(students[s].NeedHelp)
            {
                s = Random.Range(0, students.Length);
            }

            students[s].SendHelp(messages[Random.Range(0, messages.Length)], Random.Range(5, 20));

            yield return new WaitForSeconds(activateStudentTime);
        }
    }

}
