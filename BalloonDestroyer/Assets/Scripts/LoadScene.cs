using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void LoadLevelSelectScene()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }
    public void LoadWinScene()
    {
        SceneManager.LoadScene("LevelWinScene");
    }
    public void LoadLoseScene()
    {
        SceneManager.LoadScene("LevelLoseScene");
    }
    public void Quit()
    {
        // 在編譯成遊戲時會退出
        Application.Quit();

        // 在編輯器裡會停止播放模式
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
