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

        // İŞØ áæ ÇááÇÚÈ íÊÍÑß
        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            // äÍÕá Úáì ÇáÓÑÚÉ ãä ÇááÇÚÈ
            float speed = playerController.GetSpeed();

            // äÊÍÑß ÈÚßÓ ÇáÇÊÌÇå
            transform.Translate(Vector3.left * horizontalInput * speed * Time.deltaTime);
        }
    }

   
}
