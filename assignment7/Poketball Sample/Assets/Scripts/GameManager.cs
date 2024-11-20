using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UIManager MyUIManager;

    public GameObject BallPrefab;   // prefab of Ball

    // Constants for SetupBalls
    public static Vector3 StartPosition = new Vector3(0, 0, -6.35f);
    public static Quaternion StartRotation = Quaternion.Euler(0, 90, 90);
    const float BallRadius = 0.286f;
    const float RowSpacing = 0.02f;

    GameObject PlayerBall;
    GameObject CamObj;

    const float CamSpeed = 3f;

    const float MinPower = 15f;
    const float PowerCoef = 1f;

    void Awake()
    {
        // PlayerBall, CamObj, MyUIManager를 얻어온다.
        // ---------- TODO ---------- 
        MyUIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        PlayerBall = GameObject.Find("PlayerBall");
        CamObj = GameObject.Find("Main Camera");
        // -------------------- 
    }

    void Start()
    {
        SetupBalls();
    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭시 raycast하여 클릭 위치로 ShootBallTo 한다.
        // ---------- TODO ---------- 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 ClickPosition = hit.point;
                ClickPosition.y = PlayerBall.transform.position.y;

                ShootBallTo(ClickPosition);
            }
        }
        // -------------------- 
    }

    void LateUpdate()
    {
        CamMove();
    }

    void SetupBalls()
    {
        // 15개의 공을 삼각형 형태로 배치한다.
        // 가장 앞쪽 공의 위치는 StartPosition이며, 공의 Rotation은 StartRotation이다.
        // 각 공은 RowSpacing만큼의 간격을 가진다.
        // 각 공의 이름은 {index}이며, 아래 함수로 index에 맞는 Material을 적용시킨다.
        // Obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ball_1");
        // ---------- TODO ---------- 
        Vector3 LeftPosition; //줄에서 제일 왼쪽에 있는 공의 위치

        float BallSpace = 2 * BallRadius + RowSpacing; //공 사이 간격
    
        float X = BallRadius + RowSpacing/2;
        float Z = Mathf.Sqrt(BallSpace * BallSpace - X * X); // 제일 왼쪽에 있는 공 간의 X, Z 좌표 간격
        
        int j = 0;
        int index = 1;

        for (int i = 0; i < 5; i++)
        {
            Vector3 XZIntervalPosition = new Vector3(X, 0, Z) * j;
            LeftPosition = StartPosition - XZIntervalPosition;
            for (int k = 0; k <= j; k++)
            {
                Vector3 XIntervalPosition = new Vector3(BallSpace * k, 0, 0);
                GameObject NewBall = Instantiate(BallPrefab, LeftPosition + XIntervalPosition, StartRotation);
                NewBall.GetComponent<MeshRenderer>().material = Resources.Load<Material>($"Materials/ball_{index}");
                NewBall.name = index.ToString();
                index ++;
            }
            j ++;
        }
        // -------------------- 
    }

    void CamMove()
    {
        // CamObj는 PlayerBall을 CamSpeed의 속도로 따라간다. Unity Lerp
        // ---------- TODO ---------- 
        Vector3 BallCamPosition = new Vector3(PlayerBall.transform.position.x, CamObj.transform.position.y, PlayerBall.transform.position.z);

        Vector3 MiddlePosition = Vector3.Lerp(CamObj.transform.position, BallCamPosition, CamSpeed * Time.deltaTime);
        CamObj.transform.position = MiddlePosition;        
        // -------------------- 
    }

    float CalcPower(Vector3 displacement)
    {
        return MinPower + displacement.magnitude * PowerCoef;
    }

    void ShootBallTo(Vector3 targetPos)
    {
        // targetPos의 위치로 공을 발사한다.
        // 힘은 CalcPower 함수로 계산하고, y축 방향 힘은 0으로 한다.
        // ForceMode.Impulse를 사용한다.
        // ---------- TODO ---------- 
        Rigidbody PlayerBallRigidbody = PlayerBall.GetComponent<Rigidbody>();
        Vector3 direction = targetPos - PlayerBall.transform.position;
        
        Vector3 UnitDirectionVector = direction.normalized;
        float VectorMagnitude = CalcPower(direction);
        Vector3 force = UnitDirectionVector * VectorMagnitude;

        PlayerBallRigidbody.AddForce(force, ForceMode.Impulse);
        // -------------------- 
    }
    
    // When ball falls
    public void Fall(string ballName)
    {
        // "{ballName} falls"을 1초간 띄운다.
        // ---------- TODO ---------- 
        MyUIManager.DisplayText($"{ballName} falls", 1f);
        // -------------------- 
    }
}
