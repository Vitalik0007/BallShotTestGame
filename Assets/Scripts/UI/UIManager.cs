using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup endPanel;
    [SerializeField] private Text messageText;

    [SerializeField] private Button restartButton;

    private void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerLose += ShowLoseUI;
        GameEvents.OnPlayerWin += ShowWinUI;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerLose -= ShowLoseUI;
        GameEvents.OnPlayerWin -= ShowWinUI;
    }

    private void ShowWinUI()
    {
        ShowMessage("YOU WIN!", Color.blue);
    }

    private void ShowLoseUI()
    {
        ShowMessage("GAME OVER!", Color.red);
    }

    private void ShowMessage(string message, Color color)
    {
        messageText.text = message;
        messageText.color = color;

        endPanel.gameObject.SetActive(true);
        endPanel.alpha = 0;
        endPanel.transform.localScale = Vector3.one * 0.8f;

        Sequence seq = DOTween.Sequence();
        seq.Append(endPanel.DOFade(1f, 0.5f).SetEase(Ease.OutQuad));
        seq.Join(endPanel.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack));
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
