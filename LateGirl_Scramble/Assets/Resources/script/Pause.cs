using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    // ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;
    // 選択する矢印
    [SerializeField]
    private GameObject arrow;
    // リトライのボタン
    [SerializeField]
    private GameObject retryButton;
    // タイトルボタン
    [SerializeField]
    private GameObject titleButton;
    // 矢印の位置…1上2真ん中3下
    private int arrowPoint;

    private void Start()
    {
        arrowPoint = 1;
        pauseUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("right ctrl") || Input.GetKeyDown("left ctrl"))
        {
            PauseActive();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && pauseUI.activeSelf)
        {
            ArrowDownMove();
        }else if (Input.GetKeyDown(KeyCode.UpArrow) && pauseUI.activeSelf)
        {
            ArrowUpMove();
        }
        if (Input.GetKeyDown(KeyCode.Return) && pauseUI.activeSelf)
        {
            switch (arrowPoint)
            {
                case 1: ReStartButton(); break;
                case 2: HomeButton(); break;
            }
        }
    }
    public void PauseActive()
    {

        arrowPoint = 1;
        //　ポーズUIのアクティブ、非アクティブを切り替え
        pauseUI.SetActive(!pauseUI.activeSelf);

        //　ポーズUIが表示されてる時は停止
        if (pauseUI.activeSelf)
        {
            Time.timeScale = 0f;
            //　ポーズUIが表示されてなければ通常通り進行
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void HomeButton()
    {
        SceneLoadManager.LoadScene("StartScene");
        return;
    }
    public void NextButton()
    {
        SceneLoadManager.LoadScene("StartScene");
        return;
    }
    public void ReStartButton()
    {
        SceneLoadManager.LoadScene("mainScene");
        return;
    }
    /// <summary>
    /// 下ボタンを押されたら選択矢印を上にあげるよ
    /// </summary>
    private void ArrowDownMove()
    {
        switch (arrowPoint)
        {
            case 1:
                arrow.transform.position = new Vector3(arrow.transform.position.x, titleButton.transform.position.y, arrow.transform.position.z);
                arrowPoint++;
                break;
            case 2: 
                arrow.transform.position = new Vector3(arrow.transform.position.x, titleButton.transform.position.y, arrow.transform.position.z);
                
                break;
        }
    }
    /// <summary>
    /// 上ボタンを押されたら選択矢印を上にあげるよ
    /// </summary>
    private void ArrowUpMove()
    {
        switch (arrowPoint)
        {
            case 1:
                arrow.transform.position = new Vector3(arrow.transform.position.x, retryButton.transform.position.y, arrow.transform.position.z);
                
                break;
            case 2:
                arrow.transform.position = new Vector3(arrow.transform.position.x, retryButton.transform.position.y, arrow.transform.position.z);
                arrowPoint--;
                break;
        }
    }
}