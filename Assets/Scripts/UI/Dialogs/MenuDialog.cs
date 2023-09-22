using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuDialog : Dialog
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingsButton;

    protected void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        _shopButton.onClick.AddListener(OnShopButtonClick);
    }

    private void OnPlayButtonClick()
    {
        SceneManager.LoadScene(StringConstants.GAME_SCENE_NAME);
    }

    private void OnShopButtonClick()
    {
        DialogManager.ShowDialog<ShopDialog>();
    }

    private void OnSettingsButtonClick()
    {
        DialogManager.ShowDialog<SettingsDialog>();
    }
}
