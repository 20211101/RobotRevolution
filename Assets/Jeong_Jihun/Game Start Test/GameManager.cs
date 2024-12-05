using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Button startButton; // 버튼 연결
    public Image startButtonImage;  // 버튼 속 이미지
    public GameObject explosionEffect; // 폭발 이펙트
    public Transform cameraTarget; // 카메라 이동 목표 지점
    public float holdTime = 2f; // 누르는 시간 기준
    private float holdTimer = 0f; // 누른 시간 체크
    private bool isHolding = false; // 버튼 누름 상태
    private bool gameStarted = false; // 게임 시작 여부
    public Slider buttonSlider; // 버튼 슬라이더
    public GameObject containerObject; // 컨테이너 오브젝트

    private Image buttonImage; // 버튼의 이미지

    void Start()
    {
        // 버튼의 Image 컴포넌트를 가져옴
        buttonImage = startButton.GetComponent<Image>();

        // 폭발 이펙트 비활성화 (시작 전에 보이지 않게)
        explosionEffect.SetActive(false);
    }

    void Update()
    {
        if (isHolding && !gameStarted)          // 게임이 아직 시작되지 않았을 때
        {
            holdTimer += Time.deltaTime;

            // 버튼 색상 변화: 흰색 → 빨간색
            float t = holdTimer / holdTime;
            startButtonImage.color = Color.Lerp(Color.white, Color.red, t);
            buttonSlider.value = Mathf.Lerp(0.1f, 1.0f, t);

            if (holdTimer >= holdTime)
            {
                StartGame();                    // 버튼 누른 시간이 충족되면 게임 시작
            }
        }
    }

    public void OnPointerDown()                 // 버튼 누르기 시작
    {
        if (!gameStarted)                       // 이미 시작된 상태가 아니라면
        {
            isHolding = true;
            holdTimer = 0f;

            // 버튼 색 초기화
            buttonImage.color = Color.white;
        }
    }

    public void OnPointerUp() // 버튼에서 손 떼기
    {
        isHolding = false;

        // 버튼 색 초기화
        buttonImage.color = Color.white;
        startButtonImage.color = Color.white;
        buttonSlider.value = 0.1f;
    }

    void StartGame()
    {
        gameStarted = true; // 게임 시작 상태로 변경
        isHolding = false; // 버튼 눌림 상태 해제

        // 버튼 비활성화 (사라지게 처리)
        startButton.gameObject.SetActive(false);
        startButtonImage.gameObject.SetActive(false);

        // 폭발 이펙트 활성화 (게임 시작 시 폭발 효과 발생)
        explosionEffect.SetActive(true);
        // 컨테이너 오브젝트 삭제
        containerObject.SetActive(false);

        // 카메라 이동 후 씬 전환
        StartCoroutine(MoveCameraAndLoadScene());
    }

    IEnumerator MoveCameraAndLoadScene()
    {
        // 카메라 이동 시작
        float duration = 2f; // 카메라 이동 시간
        Vector3 startPosition = Camera.main.transform.position;
        Quaternion startRotation = Camera.main.transform.rotation;
        Vector3 endPosition = cameraTarget.position;
        Quaternion endRotation = cameraTarget.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // 카메라의 위치와 회전을 부드럽게 보간
            Camera.main.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            Camera.main.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            yield return null;
        }

        // 씬 전환
        SceneManager.LoadScene("CollapsScene_GamePlay");
    }
}
