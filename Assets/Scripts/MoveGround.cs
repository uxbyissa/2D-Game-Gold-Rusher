using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // ��� �� ������ �����
        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            // ���� ��� ������ �� ������
            float speed = playerController.GetSpeed();

            // ����� ���� �������
            transform.Translate(Vector3.left * horizontalInput * speed * Time.deltaTime);
        }
    }

   
}
