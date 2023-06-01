using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerExControll : MonoBehaviour
{
        // 소화기
    public Transform lever; // 레버
    public Transform nozzle; // 노즐
    public Transform seal; // 노즐
    public float buttonRotationSpeed = 10f; // 버튼 회전 속도
    public GameObject canvas;

    private bool rotateNozzle = false; // 노즐 회전 여부
    private bool rotateLever = false; // 레버 회전 여부
    private bool seallock = true; // 안전핀 해제 여부
    private Coroutine nozzleRotationCoroutine;
    private Coroutine LeverRotationCoroutine;
    [SerializeField] private Transform fireExTransform; // 소화기 위치
    [SerializeField] private ParticleSystem fireExParticle; // 소화기 분사 Particle
    [SerializeField] private ParticleSystem fireParticle; // 화재 Particle
    [SerializeField] private LayerMask fire; // 불 LayerMask
    [SerializeField] private Transform playerCameraTransform; // 플레이어 카메라 위치

    public float fireExDistance = 3f; // 소화기를 들고 fireParticle과 상호작용하기 위해 필요한 거리
    public float fireDecreseRate = 0.01f; // 소화기 분사당 불 끄는 비율(fireParticle Start Size 감소 매개변수)
    private int layerMask = 1 << 6; // Raycast에서 fireParticle을 특정하기 위해 지정된 fireParticle LayerMask

    public AudioSource audioData; // 오디오 Component
    public AudioClip bgm3; // 다친 사람을 구하라는 오디오

    /* 플레이어 능력치 */
    public int score = 0;
    public float health = 100;
    public int maxHealth = 100;
    public string success_text = "Clear";
    public bool firelive = true;
    public bool ttscheck = false;

    private void Awake()
    {
        StopParticle();
    }

    private void Update()
    {
        Debug.DrawRay(fireExTransform.position, fireExTransform.forward, Color.green); // 소화기 Raycast 궤도(초록)
        // B 버튼을 누를 때
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (nozzleRotationCoroutine != null)
                StopCoroutine(nozzleRotationCoroutine);

            rotateNozzle = true;
            nozzleRotationCoroutine = StartCoroutine(RotateNozzleCoroutine1(buttonRotationSpeed));
        }
        else if (OVRInput.GetUp(OVRInput.Button.Two))
        {
            rotateNozzle = false;
        }
                // A 버튼을 누를 때
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (nozzleRotationCoroutine != null)
                StopCoroutine(nozzleRotationCoroutine);

            rotateNozzle = true;
            nozzleRotationCoroutine = StartCoroutine(RotateNozzleCoroutine2(buttonRotationSpeed));
        }
        else if (OVRInput.GetUp(OVRInput.Button.One))
        {
            rotateNozzle = false;
        }
        //x버튼
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            RaycastHit hit;
            Ray ray = new Ray(fireExTransform.position, fireExTransform.forward);
            if (LeverRotationCoroutine != null)
                StopCoroutine(LeverRotationCoroutine);
            rotateLever = true;
            LeverRotationCoroutine = StartCoroutine(RotateLeverCoroutine1(buttonRotationSpeed,15f));
            if(seallock == false)
            {
                PlayParticle();
                OVRInput.SetControllerVibration(1.0f, 1.0f, OVRInput.Controller.RTouch); // 오른쪽 컨트롤러 진동 트리거
                OVRInput.SetControllerVibration(1.0f, 1.0f, OVRInput.Controller.LTouch); //왼쪽 컨트롤러 진동 트리거
                
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    /* fireDecreaseRate만큼 fireParticle의 startSize을 Update 갱신시마다 줄인다. */
                    fireParticle.GetComponent<ParticleSystem>().startSize -= 0.01f;
                    Debug.Log("fire particle reducing. current size: " + fireParticle.GetComponent<ParticleSystem>().startSize);
                }
            }
        }
        else if(OVRInput.GetUp(OVRInput.Button.Three))
            {
                rotateLever = false;
                lever.localRotation = Quaternion.identity;
                StopParticle();
            }
        

                        // Y 버튼을 누르거나 뗄 때
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            seallock = false;
            seal.transform.position = new Vector3(1000,0,0);
            canvas.SetActive(!canvas.activeSelf);

        }

        if (fireParticle.startSize <= 0.2) /* 불의 크기가 0.2 이하로 줄어들면 꺼진다. */
        {
            fireParticle.GetComponent<ParticleSystem>().Stop();
            firelive = false;
        }
        /* --------bgm3-------- */
        if (firelive == false && ttscheck == false)
        {
            audioData.PlayOneShot(bgm3);
            ttscheck = true;
        }
    }
        IEnumerator RotateNozzleCoroutine1(float rotationSpeed)
    {
        while (rotateNozzle)
        {
            Quaternion targetRotation = nozzle.rotation * Quaternion.Euler(rotationSpeed * Time.deltaTime, 0f, 0f);
            nozzle.rotation = targetRotation;

            yield return null;
        }
    }
        IEnumerator RotateNozzleCoroutine2(float rotationSpeed)
    {
        while (rotateNozzle)
        {
            Quaternion targetRotation = nozzle.rotation * Quaternion.Euler(-rotationSpeed * Time.deltaTime, 0f, 0f);
            nozzle.rotation = targetRotation;

            yield return null;
        }
    }
    IEnumerator RotateLeverCoroutine1(float rotationSpeed, float maxRotation)
    {
        Quaternion startRotation = lever.rotation;
        Quaternion targetRotation = lever.rotation * Quaternion.Euler(maxRotation, 0f, 0f);

        while (rotateLever && lever.rotation != targetRotation)
        {
            lever.rotation = Quaternion.RotateTowards(lever.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        if (!rotateLever)
            lever.rotation = startRotation;
    }
    private void PlayParticle() // 소화기 분사
    {
        if (!fireExParticle.GetComponent<ParticleSystem>().isPlaying)
            fireExParticle.GetComponent<ParticleSystem>().Play();
    }

    private void StopParticle() // 소화기 분사 종료
    {
        if (fireExParticle.GetComponent<ParticleSystem>().isPlaying)
            fireExParticle.GetComponent<ParticleSystem>().Stop();
    }
}
