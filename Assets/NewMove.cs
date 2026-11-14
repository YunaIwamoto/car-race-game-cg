using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NewMove : MonoBehaviour {
    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque = 300f;   // 最大
    public float maxSteeringAngle = 30f;  // 最大ハンドルの角度
    public float brakeTorque = 500f; // ブレーキの強さ

    public float acceleration = 5f;       // 加速度
    public float maxSpeed = 20f;          // 最大速度
    public float reverseMaxSpeed = 10f;   //バックの最大速度
    private float currentSpeed = 0f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpeedPanel" )
        {
            if(other.gameObject.name == "Upspeed")
            {
                currentSpeed *= 2;
            }
            else
            {
                currentSpeed /= 2;
            }
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
            GetComponent<ParticleSystem>().Play();
        }
        else
        {
            // キーを離したら自然に減速
            currentSpeed = 0;
        }
        float motor = currentSpeed * maxMotorTorque;
        float steering = 0f;
        bool isBraking = Input.GetKey(KeyCode.Space);

        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            steering = -maxSteeringAngle;
        }
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            steering = maxSteeringAngle;
        }

        // 各車軸に反映
        foreach (AxleInfo axle in axleInfos)
        {
            if (axle.isSteering)

            {
                axle.isLeftWheel.steerAngle = steering;
                axle.isRightWheel.steerAngle = steering;
            }

            if (isBraking)
            {
                axle.isLeftWheel.brakeTorque = brakeTorque;
                axle.isRightWheel.brakeTorque = brakeTorque;

            }
            else
            {
                axle.isLeftWheel.brakeTorque = 0f;
                axle.isRightWheel.brakeTorque = 0f;

            }
            if (axle.isMotor)
            {
                axle.isLeftWheel.motorTorque = motor;
                axle.isRightWheel.motorTorque = motor;
                //Debug.Log("motor: " + motor);
            }

            ApplyWheelVisuals(axle.isLeftWheel);
            ApplyWheelVisuals(axle.isRightWheel);
        }
    }

    // WheelCollider の見た目を回転させる
    void ApplyWheelVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) return;

        Transform wheel = collider.transform.GetChild(0);
        collider.GetWorldPose(out Vector3 pos, out Quaternion rot);

        wheel.position = pos;
        wheel.rotation = rot;
    }
}
[System.Serializable]
public class AxleInfo {
    public WheelCollider isLeftWheel;
    public WheelCollider isRightWheel;
    public bool isMotor; //駆動輪か?
    public bool isSteering; //ハンドル操作をしたときに角度が変わるか？
}