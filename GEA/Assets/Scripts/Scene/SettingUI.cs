using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public GameObject settingsPanel;

    bool isOpen = false;

    void Start()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        isOpen = false;
    }

    void Update()
    {
        //ESC 키 처리
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
                CloseSettings();
            else
                OpenSettings();
        }
    }

    // 설정 열기
    public void OpenSettings()
    {
        if (settingsPanel == null) return;

        settingsPanel.SetActive(true);
        Time.timeScale = 0f;   // 게임 일시정지
        isOpen = true;

        // 마우스 커서 활성화 (FPS용)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // 설정 닫기
    public void CloseSettings()
    {
        if (settingsPanel == null) return;

        settingsPanel.SetActive(false);
        Time.timeScale = 1f;   // 게임 재개
        isOpen = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
