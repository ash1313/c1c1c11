using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinteraction : MonoBehaviour
{   public OVRPlayerController ovrplayer;
    public GameManager manager; // 게임 매니저
    public Playerinteraction player; // 소화기 조작
    public Playerinteraction interection; // 상호작용
    public Playerinteraction PickUpDrop; // 들었다놓기
    public AidPersonFollowTarget AidPerson; // 응급환자
    public DoorAnimatorParameter FireDoor; // 문 상호작용
    public Wangang wangang; // 완강기
    public Cylinder cylinder; // 실린더
    [SerializeField] private Transform playerCameraTransform; // 플레이어 카메라 위치
    [SerializeField] private Component fireDrill; // 소화벨 Component
    [SerializeField] private Component elevator; // 엘리베이터 Component
    [SerializeField] private GameObject wallAid; // 벽걸이응급처치도구 GameObject
    [SerializeField] private GameObject aid; // 응급처치도구 GameObject
    [SerializeField] private Component aidperson; // 응급환자 Component
    [SerializeField] private GameObject fireWall; // 방화벽 GameObject
    [SerializeField] private GameObject doru1; // 탁자 위의 도르래
    [SerializeField] private GameObject doru2; // 캐릭터 위의 도르래
    [SerializeField] private GameObject doru3; // 실린더에 걸린 도르래
    [SerializeField] private GameObject handle1; // 탁자 위의 벨트
    [SerializeField] private GameObject handle2; // 플레이어가 장착한 벨트
    [SerializeField] private GameObject heel; // 휠
    [SerializeField] LayerMask firedrillLayer; // 소화벨 LayerMask
    [SerializeField] LayerMask elevatorLayer; // 엘리베이터 LayerMask
    [SerializeField] LayerMask wallAidLayer; // 벽걸이응급처치도구
    [SerializeField] LayerMask aidpersonLayer; // 응급환자 LayerMask
    [SerializeField] LayerMask fireWallLayer; // 방화벽 LayerMask
    [SerializeField] LayerMask wangangLayer; // 완강기 LayerMask
    [SerializeField] LayerMask cylinderLayer; // 실린더 LayerMask
    [SerializeField] LayerMask handleLayer; // 벨트 LayerMask
    [SerializeField] LayerMask doru1Layer; // 테이블 도르래 LayerMask
    [SerializeField] LayerMask doru2Layer; // 실린더 도르래 LayerMask
    [SerializeField] LayerMask heelLayer; // 휠 LayerMask
    [SerializeField] LayerMask bungeeLayer; // 점프대 LayerMask
    [SerializeField] LayerMask fireex; // 점프대 LayerMask
    [SerializeField] LayerMask GoalPointLayer; // 점프대 LayerMask
    public AidPersonFollowTarget doAidPersonMove; // 응급환자이동
    public float interactionDistance = 4f; // 상호작용을 위해 필요한 거리
    private OVRInput.Controller controller = OVRInput.Controller.LTouch; // 왼손 컨트롤러를 사용하는 경우
    // 상호작용 여부
    public bool interectFireDrill = false;
    public bool interectElevator = false;
    public bool interectAid = false;
    public bool interectDoor = false;
    public bool doorOpen = false;
    public bool interectFireWall = false;
    public bool interecthandle = false;
    public bool hangdoru = false; // 실린더에 도르래를 거는 여부
    public bool interectfireex = false;
    public bool goal = false;
    public bool gravitylow = false;

    Animator animator;
    Animator anim;
    LineRenderer lr;

    public AudioSource audioData; // 오디오 Component
    public AudioClip bgm1; // 소화기 잡으라는 오디오
    public AudioClip bgm3; // 다친 사람을 구하라는 오디오
    public AudioClip bgm4; // 건물을 탈출하라는 오디오
    public AudioClip bgm5; // 엘리베이터를 타지 말라는 오디오

    void Start()
    {
        // audioData = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        anim = GetComponentInChildren<Animator>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (interecthandle == true && OVRInput.GetDown(OVRInput.Button.One))
        {
            animator.SetBool("isDown", true);
        }

        lr.positionCount = 3;   
        if (hangdoru == true)
        {
            if (interecthandle == false) // 테이블 벨트에 실 연결
            {
                lr.SetPosition(0, handle1.transform.position);
                lr.SetPosition(1, doru3.transform.position);
                lr.SetPosition(2, heel.transform.position);
            }
            else // 플레이어 벨트에 실 연결
            {
                lr.SetPosition(0, handle2.transform.position);
                lr.SetPosition(1, doru3.transform.position);
                lr.SetPosition(2, heel.transform.position);
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Ray ray = new Ray(playerCameraTransform.position, playerCameraTransform.forward);
            RaycastHit hitinfo1;
            RaycastHit hitinfo2;
            RaycastHit hitinfo3;
            RaycastHit hitinfo4;
            RaycastHit hitinfo5;
            RaycastHit hitinfo6;
            RaycastHit hitinfo7;
            RaycastHit hitinfo8;
            RaycastHit hitinfo9;
            RaycastHit hitinfo10;
            RaycastHit hitinfo11;
            RaycastHit hitinfo12;
            RaycastHit hitinfo13;
            RaycastHit hitinfo14;
            if (Physics.Raycast(ray, out hitinfo1, interactionDistance, firedrillLayer)) // 소화벨 상호작용
            {
                PlayFireDrill();
            }
            if (Physics.Raycast(ray, out hitinfo2, interactionDistance, elevatorLayer)) // 엘리베이터 상호작용
            {
                PlayElevator();
                // 점수 깎는 로직 작성 예정
            }
            if (Physics.Raycast(ray, out hitinfo3, interactionDistance, wallAidLayer)) // 벽걸이응급처치도구 상호작용
            {
                wallAid.SetActive(false);
                aid.SetActive(true);
            }
            if (Physics.Raycast(ray, out hitinfo4, interactionDistance, aidpersonLayer) && aid.activeSelf == true) // 응급환자 상호작용
            {
                DoAidPerson(); // 환자 일어남
            }
            if (Physics.Raycast(ray, out hitinfo5, interactionDistance, fireWallLayer)) // 방화벽 상호작용
            {
                interectDoor = true;
                OpenFireWall();
            }
            if (Physics.Raycast(ray, out hitinfo6, interactionDistance, wangangLayer)) // 완강기 상호작용
            {
                wangang.interectwangang = true;
            }
            if (Physics.Raycast(ray, out hitinfo7, interactionDistance, cylinderLayer)) // 완강기의 실린더 상호작용
            {
                cylinder.interectcylinder = true;
            }
            if (Physics.Raycast(ray, out hitinfo8, interactionDistance, handleLayer)) // 벨트 상호작용
            {
                handle1.SetActive(false);
                handle2.SetActive(true);
                interecthandle = true;
            }
            if (Physics.Raycast(ray, out hitinfo9, interactionDistance, doru1Layer)) // 도르래 상호작용
            {
                if (doru1.activeSelf == true)
                {
                    doru1.SetActive(false);
                    doru2.SetActive(true);
                }
            }
            if (Physics.Raycast(ray, out hitinfo10, interactionDistance, doru2Layer)) // 도르래 상호작용
            {
                if(doru2.activeSelf == true)
                {
                    doru2.SetActive(false);
                    doru3.SetActive(true);
                    hangdoru = true;
                }
            }
            // if (Physics.Raycast(ray, out hitinfo11, interactionDistance, heelLayer)) // 휠 상호작용
            // {
                
            // }
            if (Physics.Raycast(ray, out hitinfo12, interactionDistance, bungeeLayer))
            {
                // this.animator.enabled = true;
                ovrplayer.GravityModifier = 0.001f;
                gravitylow = true;
            }
            if (Physics.Raycast(ray, out hitinfo13, interactionDistance, fireex))
            {
                interectfireex = true;
            }
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hitinfo14, 1.7f, GoalPointLayer))
        {
            goal = true;
        }
        }
    }

    private void PlayFireDrill() // FireDrill 상호작용
    {
        if (!fireDrill.GetComponent<AudioSource>().isPlaying)
        {
            fireDrill.GetComponent<AudioSource>().Play();
            Debug.Log("fireDrill Sound");
            interectFireDrill = true;
            audioData.PlayOneShot(bgm1);
        }
    }

    private void PlayElevator() // Elevator 상호작용
    {
        if (!elevator.GetComponent<AudioSource>().isPlaying)
        {
            elevator.GetComponent<AudioSource>().Play();
            Debug.Log("elevator Sound");
            interectElevator = true;
            /* --------tts5-------- */
            audioData.PlayOneShot(bgm5); // 엘리베이터를 피하라는 오디오
        }
    }

    private void DoAidPerson()
    {
        //aidperson.transform.localRotation = Quaternion.Euler(0, 0, 0);
        aid.SetActive(false); // 응급처치도구 비활성화
        doAidPersonMove.liveperson = true; // 응급환자 일어남
        // 치료 되었다는 tts 작성 예정
    }

    void OnTriggerEnter(Collider other)
    { // 불에 닿을 시 체력 급격하게 감소
        if (other.tag == "Fire" && manager.limitTime > 30)
        {
            manager.limitTime -= 30;
        }
    }
    private void OpenFireWall()
    {
        if (doorOpen == false)
        {
            doorOpen = true;

        }
        else if (doorOpen == true)
        {
            doorOpen = false;
        }
    }
    



}
