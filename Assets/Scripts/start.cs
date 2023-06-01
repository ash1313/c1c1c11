using UnityEngine;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    public Button canvasButton;

    private void Update()
    {
        // A 버튼을 누를 때
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            // Canvas의 버튼을 클릭한 것처럼 호출
            canvasButton.onClick.Invoke();
        }
    }
}