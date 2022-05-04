using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private AudioClip losingSound;
    [SerializeField] private AudioClip winningSound;

    [SerializeField] private bool isMenu = false;
    [SerializeField] private bool isLevel = false;
    [SerializeField] private bool isLosing = false;
    [SerializeField] private bool isWinning = false;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        Menu();
    }

    private void Update() {
        if (MenuUIHandler.Instance.isLevel && !isLevel) {
            Level();
            isLevel = true;

        } else if (MenuUIHandler.Instance.isMenu && !isMenu) {
            Menu();
            isMenu = true;

        } else if (MenuUIHandler.Instance.isWin && !isWinning) {
            Winner();
            isWinning = true;

        } else if (MenuUIHandler.Instance.isLose && !isLosing) {
            Loser();
            isLosing = true;
        }
    }

    private void Menu() {        
        audio.Stop();
        audio.loop = true;
        audio.clip = menuMusic;
        audio.Play();
        ResetBool();
    }
    private void Level() {        
        audio.Stop();
        audio.loop = true;
        audio.clip = levelMusic;
        audio.Play();
        ResetBool();
    }
    private void Winner() {        
        audio.Stop();
        audio.loop = false;
        audio.clip = winningSound;
        audio.Play();
        ResetBool();
    }
    private void Loser() {        
        audio.Stop();
        audio.loop = false;
        audio.clip = losingSound;
        audio.Play();
        ResetBool();
    }

    private void ResetBool() {
        isMenu = false;
        isLevel = false;
        isLosing = false;
        isWinning = false;
    }
}
