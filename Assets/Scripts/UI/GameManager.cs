using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
        // 소화기 분사 Particle
    //[SerializeField] private ParticleSystem fireExParticle;
    //[SerializeField] private ParticleSystem fireParticle;
    //[SerializeField] private Transform fireExTransform; // 소화기 위치
    //
    public GameManager Manager;
    public PlayerExControll player; // 소화기 조작
    public Playerinteraction interection; // 상호작용
    public PlayerPickUpDrop PickUpDrop; // 들었다놓기
    public AidPersonFollowTarget AidPerson; // 응급환자
    public int stage; // 시나리오
    public float playTime; // 게임 이용 시간

    public GameObject menuCam; // 메인화면 카메라
    public GameObject gameCam; // 게임화면 카메라
    public GameObject menuPanel; // 메뉴 UI
    public GameObject gamePanel; // 게임 UI
    public Text maxScoreText; // 최대점수 Text

    // 게임 UI
    public Text scoreText; // 점수 Text
    public Text stageText; // 스테이지(시나리오) Text
    public Text playTimeText; // 플레이타임 Text
    public Text questText1; // 퀘스트1 Text
    public Text questText2; // 퀘스트2 Text
    public Text questText3; // 퀘스트3 Text
    public Text questText4; // 퀘스트4 Text
    public Text questText5; // 퀘스트5 Text

    public GameObject TTS1;
    public GameObject TTS2;
    public GameObject TTS3;
    public GameObject TTS4;
    public GameObject TTS5;
    public GameObject TTS6;
    public GameObject TTS7;

    public Text playerHealthText; // 플레이어 체력
    public Text interectText; // 상호작용 Text

    // 오디오
    public AudioSource audioData; // 오디오 Component
    public AudioClip bgm0; // 소화 경보를 울리라는 오디오
    public AudioClip bgm1; // 소화기 잡으라는 오디오
    public AudioClip bgm2; // 불 끄라는 오디오
    public AudioClip bgm3; // 다친 사람을 구하라는 오디오
    public AudioClip bgm4; // 건물을 탈출하라는 오디오
    public AudioClip bgm5; // 엘리베이터를 피하라는 오디오

    // 게임 Object
    public GameObject user; // 유저
    public GameObject fire; // 불
    public GameObject fireEx; // 소화기
    public GameObject firedrill; // 소화벨
    public GameObject elevator; // 엘리베이터
    public GameObject firewall; // 방화벽
    public GameObject aidperson; // 응급처치가 필요한 사람

    // 확인 여부
    public static ObjectGrabbable objectGrabbable; // 소화기 잡기 여부
    public float limitTime = 100;
    public bool didAid;
    public bool ttsTime = false;
    public bool ttscheck = false;

    void Awake()
    {
        //StopParticle();
        // maxScoreText.text = string.Format("{0:n0}", PlayerPrefs.GetInt("maxScore"));
    }
    public void GameStart()
    {
        menuCam.SetActive(false);
        gameCam.SetActive(true);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    void Start()
    {
        //audioData = GetComponent<AudioSource>();
    }

    void Update()
    
    {
        playTime += Time.deltaTime; // 플레이 시간 계산
        if (limitTime > 0)
        {
            limitTime -= Time.deltaTime;
            if (limitTime <= 97)
            {
                ttsTime = true;
            }
        }
    }
}
        // if (playTime >= 0 && player.firelive == true)
        // {
        //     player.health = Mathf.Round(limitTime);
        // }
         // A 버튼을 누를 때
        // if (OVRInput.GetDown(OVRInput.Button.Two))
        // {
        //     if (nozzleRotationCoroutine != null)
        //         StopCoroutine(nozzleRotationCoroutine);

        //     rotateNozzle = true;
        //     nozzleRotationCoroutine = StartCoroutine(RotateNozzleCoroutine1(buttonRotationSpeed));
        // }
        // else if (OVRInput.GetUp(OVRInput.Button.Two))
        // {
        //     rotateNozzle = false;
        // }
        //         // B 버튼을 누를 때
        // if (OVRInput.GetDown(OVRInput.Button.One))
        // {
        //     if (nozzleRotationCoroutine != null)
        //         StopCoroutine(nozzleRotationCoroutine);

        //     rotateNozzle = true;
        //     nozzleRotationCoroutine = StartCoroutine(RotateNozzleCoroutine2(buttonRotationSpeed));
        // }
        // else if (OVRInput.GetUp(OVRInput.Button.One))
        // {
        //     rotateNozzle = false;
        // }
    //                 // X 버튼을 누를 때
    //    if (OVRInput.GetDown(OVRInput.Button.Three))
    //     {
    //         if (LeverRotationCoroutine != null)
    //             StopCoroutine(LeverRotationCoroutine);

    //         rotateLever = true;
    //         LeverRotationCoroutine = StartCoroutine(RotateLeverCoroutine1(buttonRotationSpeed,15f));
    //         if(seallock == false)
    //         {
    //             PlayParticle();
    //             OVRInput.SetControllerVibration(1.0f, 1.0f, OVRInput.Controller.RTouch); // 오른쪽 컨트롤러 진동 트리거
    //             OVRInput.SetControllerVibration(1.0f, 1.0f, OVRInput.Controller.LTouch); //왼쪽 컨트롤러 진동 트리거
    //             RaycastHit hit;
    //             Ray ray = new Ray(transform.position, transform.forward);

    //             if (Physics.Raycast(ray, out hit))
    //             {
    //                 if (hit.collider.gameObject == fireExParticle.gameObject)
    //                 {
    //                     // fireExParticle이 충돌한 경우
    //                     if (fireParticle != null)
    //                     {
    //                         fireParticle.GetComponent<ParticleSystem>().Stop(); // fireParticle 비활성화
    //                     }
    //                 }
    //             }
    //         }
    //         }
    //    else if (OVRInput.GetUp(OVRInput.Button.Three))
    //    {
    //        rotateLever = false;
    //        lever.localRotation = Quaternion.identity;
    //        StopParticle();
        

                // Y 버튼을 누르거나 뗄 때
    //    if (OVRInput.GetDown(OVRInput.Button.Four))
    //    {
    //        seallock = false;
    //        seal.transform.position = new Vector3(1000,0,0);
    //    }
    //}

    // void LateUpdate()
    // {
    //     // scoreText.text = string.Format("{0:n0}", player.score);
    //     stageText.text = "STAGE" + stage;

    //     int hour = (int)(playTime / 3600);
    //     int min = (int)((playTime - hour * 3600) / 60);
    //     int second = (int)(playTime % 60);
    //     playTimeText.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);
    //     // playerHealthText.text = player.health + " / " + player.maxHealth; // 플레이어 UI

    //     /* --------bgm0-------- */
    //     if (ttsTime == true && ttscheck == false)
    //     {
    //         audioData.PlayOneShot(bgm0);
    //         ttscheck = true;
    //     }

    //     // Quest 달성 조건
    //     if (interection.interectFireDrill == true) // "Ring FireDrill" 퀘스트 빨간 텍스트 반영
    //     {
    //         questText1.text = "<color=#ff0000>" + "+ Ring FireDrill" + "</color>";
    //         TTS2.SetActive(true);
    //     }

    //     if (PickUpDrop.pickup == true) // "Hold FireEx" 퀘스트 빨간 텍스트 반영
    //     {
    //         questText2.text = "<color=#ff0000>" + "+ Hold FireEx" + "</color>";
    //         TTS2.SetActive(false);
    //         TTS3.SetActive(true);
    //     }

    //     if (player.firelive == false) // "Turn Off Fire" 퀘스트 빨간 텍스트 반영
    //     {
    //         questText3.text = "<color=#ff0000>" + "+ Turn Off Fire" + "</color>";
    //         TTS3.SetActive(false);
    //         TTS4.SetActive(true);
    //     }

    //     if (AidPerson.liveperson == true) // "Aid The Injured" 퀘스트 빨간 텍스트 반영
    //     {
    //         questText4.text = "<color=#ff0000>" + "+ Aid The Injured" + "</color>";
    //         TTS4.SetActive(false);
    //         TTS5.SetActive(true);
    //     }

    //     if (interection.interectElevator == true) // 엘리베이터 사용 경고 TTS 작동
    //     {
    //         TTS5.SetActive(false);
    //         TTS6.SetActive(true);
    //         TTS7.SetActive(true);
    //     }

    //     if (PickUpDrop.goal == true) // "Escape Office" 퀘스트 빨간 텍스트 반영
    //     {
    //         questText5.text = "<color=#ff0000>" + "+ Escape Office" + "</color>";
    //         TTS6.SetActive(false);
    //         TTS7.SetActive(false);
    //     }
    // }
    // }
    //     private void PlayParticle()
    // {
    //     if (!fireExParticle.GetComponent<ParticleSystem>().isPlaying)
    //     {
    //         fireExParticle.GetComponent<ParticleSystem>().Play();
    //     }
    // }

    // // 소화기 분사 종료
    // private void StopParticle()
    // {
    //     if (fireExParticle.GetComponent<ParticleSystem>().isPlaying)
    //     {
    //         fireExParticle.GetComponent<ParticleSystem>().Stop();
    //     }
    // }
