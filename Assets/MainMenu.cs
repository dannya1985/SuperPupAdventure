using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameObject menuBG;
    private GameObject menuTitleImage;
    private GameObject menuVolumeSlider;
    
    [SerializeField] public float bgRotation;
    [SerializeField] public Sprite[] spriteArray;
    [SerializeField] public float titleAnimationDelay;
    private float deltaTimeCounter;
    private int spriteIndex = 0;

    private VolumeHandler androidVolumeHandler = null;

    void Start()
    {
        menuBG = GameObject.Find("Menu BG");
        bgRotation = 2.4f;
        menuTitleImage = GameObject.Find("Super Pup Title Image");
        titleAnimationDelay = 0.15f;

        Resolution[] resolutions = Screen.resolutions;

        // Print the resolutions
        foreach (var res in resolutions)
        {
            Debug.Log(res.width + "x" + res.height + " : " + res.refreshRate);
            if (res.width > 1920 && res.height > 1080)
            {
                Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen, 60);
            }
        }

        if(Application.platform == RuntimePlatform.WindowsEditor)
        {

        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            androidVolumeHandler = new VolumeHandler();
            float derp = androidVolumeHandler.GetSystemVolume();
            Debug.Log("androidVolumeHandler returned a volume of: " + derp);
        }

        if(PlayerPrefs.GetFloat("MusicVolume") < -1)
        {
            menuVolumeSlider = GameObject.Find("Volume Slider");
            menuVolumeSlider.GetComponent<Slider>().value = 0.15f;
            PlayerPrefs.SetFloat("MusicVolume", 0.15f);
            AudioListener.volume = 0.15f;
        }
    }

    void Update()
    {
        menuBG.transform.Rotate(0f, 0f, bgRotation * Time.deltaTime, Space.Self);
        deltaTimeCounter += Time.deltaTime;

        //change the sprite if the time accumulated is greater than the animationdelay specified
        if(deltaTimeCounter >= titleAnimationDelay)
        {
            deltaTimeCounter = 0;
            if (spriteIndex == 0)
            {
                menuTitleImage.GetComponent<Image>().sprite = spriteArray[1];
                spriteIndex = 1;
            }
            else
            {
                spriteIndex = 0;
                menuTitleImage.GetComponent<Image>().sprite = spriteArray[0];
            }
        }
    }

    public void PlayGame(string difficulty = "easy")
    {
        PlayerPrefs.SetString("difficulty", difficulty);
        Debug.Log("SceneManager.LoadScene('FlappyDogGameScene') called.");
        SceneManager.LoadScene("FlappyDogGameScene");
    }

    public void QuitGame()
    {
        Debug.Log("MainMenu.QuitGame() called.");
        Application.Quit();
    }

    public void AdjustVolume(System.Single vol)
    {
        Debug.Log("vol: " + vol);
        AudioListener.volume = vol;
        PlayerPrefs.SetFloat("MusicVolume", vol);
    }
}
