using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public GameObject target;

    private float target_poseY;
    private float target_poseX;

    private float posY;
    private float posX;

    public float derechaMax;
    public float izquierdaMax;

    public float alturaMax;
    public float alturaMin;

    public float speed;
    public bool encendida = true;
    // Start is called before the first frame update
    void Awake()
    {
        posX = target_poseX + derechaMax;
        posY = target_poseY + alturaMax;
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), 1);
    }
    public void ResetStart()
    {
        target_poseY= 0;
        target_poseX = 0;
        posX = target_poseX + derechaMax;
        posY = target_poseY + alturaMax;
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), 1);

    }
    void Move_Cam()
    {
        if (encendida)
        {
            if (target)
            {
                target_poseX = target.transform.position.x;
                target_poseY = target.transform.position.y;

                if (target_poseX > derechaMax && target_poseX < izquierdaMax)
                {
                    posX = target_poseX;
                }
                if (target_poseY > alturaMax && target_poseY < alturaMin)
                {
                    posY = target_poseY;
                }
            }
            transform.position =  Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), speed*Time.deltaTime);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Move_Cam();
    }
}
