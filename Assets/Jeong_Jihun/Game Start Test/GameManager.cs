using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Button startButton; // ��ư ����
    public Image startButtonImage;  // ��ư �� �̹���
    public GameObject explosionEffect; // ���� ����Ʈ
    public Transform cameraTarget; // ī�޶� �̵� ��ǥ ����
    public float holdTime = 2f; // ������ �ð� ����
    private float holdTimer = 0f; // ���� �ð� üũ
    private bool isHolding = false; // ��ư ���� ����
    private bool gameStarted = false; // ���� ���� ����
    public Slider buttonSlider; // ��ư �����̴�
    public GameObject containerObject; // �����̳� ������Ʈ

    private Image buttonImage; // ��ư�� �̹���

    void Start()
    {
        // ��ư�� Image ������Ʈ�� ������
        buttonImage = startButton.GetComponent<Image>();

        // ���� ����Ʈ ��Ȱ��ȭ (���� ���� ������ �ʰ�)
        explosionEffect.SetActive(false);
    }

    void Update()
    {
        if (isHolding && !gameStarted)          // ������ ���� ���۵��� �ʾ��� ��
        {
            holdTimer += Time.deltaTime;

            // ��ư ���� ��ȭ: ��� �� ������
            float t = holdTimer / holdTime;
            startButtonImage.color = Color.Lerp(Color.white, Color.red, t);
            buttonSlider.value = Mathf.Lerp(0.1f, 1.0f, t);

            if (holdTimer >= holdTime)
            {
                StartGame();                    // ��ư ���� �ð��� �����Ǹ� ���� ����
            }
        }
    }

    public void OnPointerDown()                 // ��ư ������ ����
    {
        if (!gameStarted)                       // �̹� ���۵� ���°� �ƴ϶��
        {
            isHolding = true;
            holdTimer = 0f;

            // ��ư �� �ʱ�ȭ
            buttonImage.color = Color.white;
        }
    }

    public void OnPointerUp() // ��ư���� �� ����
    {
        isHolding = false;

        // ��ư �� �ʱ�ȭ
        buttonImage.color = Color.white;
        startButtonImage.color = Color.white;
        buttonSlider.value = 0.1f;
    }

    void StartGame()
    {
        gameStarted = true; // ���� ���� ���·� ����
        isHolding = false; // ��ư ���� ���� ����

        // ��ư ��Ȱ��ȭ (������� ó��)
        startButton.gameObject.SetActive(false);
        startButtonImage.gameObject.SetActive(false);

        // ���� ����Ʈ Ȱ��ȭ (���� ���� �� ���� ȿ�� �߻�)
        explosionEffect.SetActive(true);
        // �����̳� ������Ʈ ����
        containerObject.SetActive(false);

        // ī�޶� �̵� �� �� ��ȯ
        StartCoroutine(MoveCameraAndLoadScene());
    }

    IEnumerator MoveCameraAndLoadScene()
    {
        // ī�޶� �̵� ����
        float duration = 2f; // ī�޶� �̵� �ð�
        Vector3 startPosition = Camera.main.transform.position;
        Quaternion startRotation = Camera.main.transform.rotation;
        Vector3 endPosition = cameraTarget.position;
        Quaternion endRotation = cameraTarget.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // ī�޶��� ��ġ�� ȸ���� �ε巴�� ����
            Camera.main.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            Camera.main.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            yield return null;
        }

        // �� ��ȯ
        SceneManager.LoadScene("CollapsScene_GamePlay");
    }
}
