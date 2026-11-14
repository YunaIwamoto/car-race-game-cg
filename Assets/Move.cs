using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 startPos;
    private float dir = 1.0f;     //z軸方向＋
    private float x = 0.05f;      //y軸方向に0.05f    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }  
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time);
        if (this.gameObject.name == "Move Cube")
        {
             transform.position = startPos + new Vector3(x, 0, 0);

             if (x > 15)
            {
                dir = -1.0f;
            }
            if(x < -15)
            {
                dir = 1.0f;
            }
            x += 0.05f * dir;

        }
    }
}