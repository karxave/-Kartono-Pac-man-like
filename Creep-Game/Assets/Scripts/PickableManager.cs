using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class PickableManager : MonoBehaviour
{
    // field berikut akan dipakai utk mengakses PickPowerUp di script Player
    // dipanggil dengan menggunakan method OnPickablePicked yg ada di script ini
    [SerializeField] private Player _player;

    // field berikut dipakai utk mengakses SetMaxScore() di script ScoreManager
    [SerializeField] private ScoreManager _scoreManager;

    private List<Pickable> _pickableList = new List<Pickable>();

    [SerializeField] private AudioSource _audioSourceCoin;
    [SerializeField] private AudioSource _audioSourcePowerUp;

    [Header("Timing")]
    [SerializeField] private float _pauseDuration = 3f; // how long to pause before loading

    [Header("UI")]
    [SerializeField] private GameObject _pauseScreen; // assign your pause panel in inspector

    // Start is called before the first frame update
    void Start()
    {
        InitPickableList();
    }

   
    private void InitPickableList()
    {   // tampung semua gameobject yang punya script Pickable
        // jadi gunakan array : Pickable[]
        // simpan array tsb dengan nama pickableObjects
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);

            //hubungkan OnPickablePicked dengan OnPickedAction yg ada di script Pickable
            pickableObjects[i].OnPickedAction += OnPickablePicked;

        }

        Debug.Log("Pickable List : " + _pickableList.Count);

        _scoreManager.SetMaxScore(_pickableList.Count);
    }

    
    private void OnPickablePicked(Pickable pickable)
    {
             Debug.Log("OnPickablePicked: " + pickable );

        if (pickable.Pickabletype == PickableType.Coin)
        {
            _audioSourceCoin.enabled = true;
            _audioSourceCoin.Play();
        }
        

        _pickableList.Remove(pickable);
        Debug.Log("will be destroy : " + pickable);
        Debug.Log("catatan  : " + gameObject);

            Destroy(pickable.gameObject);
            

        Debug.Log("after  destroy -- > : " + pickable);

        Debug.Log(" OnPickablePicked - Number of Pickeable  : " + _pickableList.Count);

       

        // cek dulu jenis pickablenya apakah power up 
        if (pickable.Pickabletype == PickableType.PowerUp)
        {

            _audioSourcePowerUp.enabled = true;
            _audioSourcePowerUp.Play();

            _player?.PickPowerUp();
            Debug.Log("Number of Pickeable CHECK powerup  : " + _pickableList.Count);
        }

        if (_scoreManager != null)
        {
            _scoreManager.AddScore(1);
        }


        if (_pickableList.Count <= 0)
        {
            Debug.Log("Pickable List : " + _pickableList.Count);
            Debug.Log("Win");

            StartCoroutine(PauseThenLoadNext());            
        }
    }

    private IEnumerator PauseThenLoadNext()
    {
        // Show pause screen
        if (_pauseScreen != null)
            _pauseScreen.SetActive(true);

        // Pause gameplay but let audio continue
        Time.timeScale = 0f;
        AudioListener.pause = false; // ensure audio is not paused globally

        // Wait in *real time* (ignores timescale)
        yield return new WaitForSecondsRealtime(_pauseDuration);

        // Restore time scale before loading next scene
        Time.timeScale = 1f;

        // Hide pause screen
        if (_pauseScreen != null)
            _pauseScreen.SetActive(false);

        // Load next scene
        SceneManager.LoadScene("WinScene");
    }

}
