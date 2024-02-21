using UnityEngine.SceneManagement;

public class RestartButton : GameButton
{
    protected override void TaskOnClick()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.Restart();
    }
}
