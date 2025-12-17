using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 버튼에서 호출할 함수
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // (선택) 빌드 인덱스로 이동
    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    // (선택) 게임 종료
    public void QuitGame()
    {
        Application.Quit();
    }
}
