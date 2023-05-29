using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private GameObject player; // 플레이어 GameObject
    [SerializeField] private Transform playerCameraTransform; // 플레이어 카메라 위치
    [SerializeField] private Transform playerunderTransform; // 플레이어 아래 방향 위치
    [SerializeField] private Transform objectGrabPointTransform; // 소화기를 잡을 위치
    /* 잡을 수 있는 LayerMask 지정 (해당 프로젝트의 경우 소화기에 Object Grabbable 스크립트를 추가하여 제한함) */
    [SerializeField] private LayerMask pickUpLayerMask; // 소화기 LayerMask
    /* 소화기를 잡은 상태를 확인 (다른 스크립트에서 확인하도록 public static으로 설정) */
    [SerializeField] private GameObject FireEXText; // 소화기 잡을 때 나오는 Text
    [SerializeField] private LayerMask GroundLayerMask; // 도착 지점 LayerMask
    public AidPersonFollowTarget AidPerson; // 응급환자
    public static ObjectGrabbable objectGrabbable;
    public float pickUpDistance = 2f; // 소화기를 들기 위해 필요한 거리
    public bool pickup = false; // 소화기 잡기 여부(일회용 확인)
    public bool goal = false; // 도착지점 확인 여부(일회용 확인)
    public bool ttscheck = false;

    public AudioSource audioData; // 오디오 Component
    public AudioClip bgm2; // 불 끄라는 오디오
    public AudioClip bgm4; // 건물을 탈출하라는 오디오

    private void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward, Color.red); // 플레이어 Raycast 궤도(빨강)
        if (Input.GetKeyDown(KeyCode.E)) /* 키보드 'E'키 입력시 */
        {
            if (objectGrabbable == null) /* 소화기를 잡고있지 않은 상태라면 잡는다. */
            {
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        Debug.Log("Grab " + objectGrabbable);
                        pickup = true;
                        /* --------bgm2-------- */
                        audioData.PlayOneShot(bgm2);
                    }
                }
            }
            else /* 현재 소화기를 잡고 있다면 놓는다. */
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
                Debug.Log("Put a fireEx");
            }
        }

        RaycastHit hitinfo;
        Debug.DrawRay(playerunderTransform.position, playerCameraTransform.forward, Color.blue); // 플레이어 바닥 Raycast 궤도(파랑)
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hitinfo, 3, GroundLayerMask))
        {
            goal = true;
        }
        /* --------bgm4-------- */
        if (AidPerson.liveperson == true && ttscheck == false)
        {
            audioData.PlayOneShot(bgm4);
            ttscheck = true;
        }
    }
}