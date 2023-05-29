using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinteraction : MonoBehaviour
{
    public GameManager manager; // 게임 매니저
    public Playerinteraction player; // 소화기 조작
    public Playerinteraction interection; // 상호작용
    public Playerinteraction PickUpDrop; // 들었다놓기
    public AidPersonFollowTarget AidPerson; // 응급환자
    [SerializeField] private Transform playerCameraTransform; // 플레이어 카메라 위치
    [SerializeField] private Component fireDrill; // 소화벨 Component
    [SerializeField] private Component elevator; // 엘리베이터 Component
    [SerializeField] private GameObject wallAid; // 벽걸이응급처치도구 GameObject
    [SerializeField] private GameObject aid; // 응급처치도구 GameObject
    [SerializeField] private Component aidperson; // 응급환자 Component
    [SerializeField] LayerMask firedrillLayer; // 소화벨 LayerMask
    [SerializeField] LayerMask elevatorLayer; // 엘리베이터 LayerMask
    [SerializeField] LayerMask wallAidLayer; // 벽걸이응급처치도구
    [SerializeField] LayerMask aidpersonLayer; // 응급환자 LayerMask
    public AidPersonFollowTarget doAidPersonMove; // 응급환자이동
    public float interactionDistance = 4f; // 상호작용을 위해 필요한 거리
    private OVRInput.Controller controller = OVRInput.Controller.LTouch; // 왼손 컨트롤러를 사용하는 경우
    // 상호작용 여부
    public bool interectFireDrill = false;
    public bool interectElevator = false;
    public bool interectAid = false;

    public AudioSource audioData; // 오디오 Component
    public AudioClip bgm1; // 소화기 잡으라는 오디오
    public AudioClip bgm3; // 다친 사람을 구하라는 오디오
    public AudioClip bgm4; // 건물을 탈출하라는 오디오
    public AudioClip bgm5; // 엘리베이터를 타지 말라는 오디오

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Ray ray = new Ray(playerCameraTransform.position, playerCameraTransform.forward);
            RaycastHit hitinfo1;
            RaycastHit hitinfo2;
            RaycastHit hitinfo3;
            RaycastHit hitinfo4;

            if (Physics.Raycast(ray, out hitinfo1, Mathf.Infinity, firedrillLayer)) // 소화벨 상호작용
            {
                PlayFireDrill();
            }
            if (Physics.Raycast(ray, out hitinfo2, Mathf.Infinity, elevatorLayer)) // 엘리베이터 상호작용
            {
                PlayElevator();
                // 점수 깎는 로직 작성 예정
            }
            if (Physics.Raycast(ray, out hitinfo3, Mathf.Infinity, wallAidLayer)) // 벽걸이응급처치도구 상호작용
            {
                wallAid.SetActive(false);
                aid.SetActive(true);
            }
            if (Physics.Raycast(ray, out hitinfo4, Mathf.Infinity, aidpersonLayer) && aid.activeSelf == true) // 응급환자 상호작용
            {
                DoAidPerson(); // 
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
        aidperson.transform.localRotation = Quaternion.Euler(0, 0, 0);
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


}
