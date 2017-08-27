using UnityEngine;

public class VRFirstPersonCameraController : MonoBehaviour
{
    Quaternion gyro;
	Transform m_transform;

	#if UNITY_EDITOR
    float longitude = 0.0f;
    float latitude = 0.0f;
	[SerializeField]
    float speed = 20.0f;
    bool canMouseControl = false;
	//キャッシュ
    Vector3 zeroVec = Vector3.zero;
	#endif
    void Start()
    {
        Input.gyro.enabled = true;
		m_transform = transform;
    }

    void Update()
    {
		
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Escape)) ChangeMouseControl();
        EditorCameraController();
        return;
#else
        if (!Input.gyro.enabled) return;
        gyro = Input.gyro.attitude;
        //ジャイロはデフォルトで下を向いているので90度修正。X軸もY軸も逆のベクトルに変換
        gyro = Quaternion.Euler(90.0f, 0.0f, 0.0f) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
		m_transform.localRotation = gyro;
#endif
    }

	#if UNITY_EDITOR
    void EditorCameraController()
    {
        Vector2 input = GetKeyInputVector();
        input *= Time.deltaTime * speed;
        longitude += input.x;
        latitude += input.y;

        longitude = longitude % 360.0f;
        latitude = Mathf.Clamp(latitude, -60.0f, 80.0f);

        Vector3 targetPosition = transform.position + SphereCoordinate(longitude, latitude, 10.0f);
        transform.LookAt(targetPosition);
    }

    Vector2 GetKeyInputVector()
    {
        Vector2 input;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (canMouseControl)
        {
            input.x = Input.GetAxis("Mouse X");
            input.y = Input.GetAxis("Mouse Y");
        }

        return input;
    }

    void ChangeMouseControl()
    {
        canMouseControl = !canMouseControl;

        if (canMouseControl)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    /// <summary>
    /// 指定した角度の球体座標を返す
    /// </summary>
    /// <param name="longitude">経度</param>
    /// <param name="latitude">緯度</param>
    /// <returns></returns>
    public Vector3 SphereCoordinate(float longitude, float latitude, float distance)
    {
        Vector3 position = zeroVec;

        //重複した計算
        longitude *= Mathf.Deg2Rad;
        latitude *= Mathf.Deg2Rad;
        float temp = distance * Mathf.Cos(latitude);

        position.x = temp * Mathf.Sin(longitude);
        position.y = distance * Mathf.Sin(latitude);
        position.z = temp * Mathf.Cos(longitude);

        return position;
    }

	#endif
}
