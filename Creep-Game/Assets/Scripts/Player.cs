using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Player: MonoBehaviour
{   // field berikut akan dipakai di script Enemy : StartRetreating dan StopRetreating
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;
    

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _acceleration = 10f;   // Higher = faster acceleration
    [SerializeField] private float _deceleration = 10f;   // Higher = faster stop
    [SerializeField] private Camera _camera;
    [SerializeField] private float _powerUpDuration;
    [SerializeField] private int _healthIntValue;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private AudioSource _audioSourceEnemy;
    [SerializeField] private AudioClip _audioSourceEnemyClip;
    [SerializeField] private AudioSource _audioSourcePlayer;

    private bool _isPowerUpActive = false;

    private Coroutine _powerUpCoroutine;

    private Rigidbody _rigidBody;
    private Vector3 _currentVelocity;
    
    private bool _isHittedByEnemy = false;

    [Header("Timing")]
    [SerializeField] private float _pauseDuration = 3f; // how long to pause before loading

    [Header("UI")]
    [SerializeField] private GameObject _pauseScreen; // assign your pause panel in inspector

    private void Awake()
    {
        

        UpdateUI_HealthText();

        _rigidBody = GetComponent<Rigidbody>();
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        HideAndLockCursor();
    }

    public void PickPowerUp()
    {   // ini nanti dipake oleh PickableManager
        Debug.Log("Pick Power Up");

        // cek dulu sudah ada Coroutine yg running  atau belum
        // jika ternyata sudah ada, stop dulu Coroutine yg ada
        // supaya tidak dobel Coroutine
        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
        }

        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private void FixedUpdate()
    {
        // --- INPUT ---
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // --- CAMERA-RELATIVE DIRECTIONS ---
        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // --- TARGET MOVEMENT DIRECTION ---
        Vector3 targetDirection = (horizontal * right + vertical * forward).normalized;
        Vector3 targetVelocity = targetDirection * _speed;

        // --- PRESERVE Y VELOCITY ---
        targetVelocity.y = _rigidBody.velocity.y;

        // --- SMOOTH ACCEL/DECEL ---
        float lerpSpeed = (targetDirection.magnitude > 0.1f) ? _acceleration : _deceleration;
        _currentVelocity = Vector3.Lerp(_rigidBody.velocity, targetVelocity, lerpSpeed * Time.fixedDeltaTime);

        // --- APPLY VELOCITY ---
        _rigidBody.velocity = _currentVelocity;
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private IEnumerator StartPowerUp()
    {
        //set TRUE dulu bool _isPowerUpActive
        _isPowerUpActive = true;

        //Debug.Log("Start PowerUp");
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        
        // tunggu selama : _powerUpDuration detik
        yield return new WaitForSeconds(_powerUpDuration);
        // set FAlSE _isPowerUpActive
        _isPowerUpActive = false;

        //Debug.Log("End of PowerUp");
        if (OnPowerUpStop!= null)
        {
            OnPowerUpStop();
        }
               
    }

    

    private void OnCollisionEnter(Collision collisionObject)
    {
        // if (_isPowerUpActive)
        // {   
        // cek Tag collision apakah tag Enemy
        //     if (collisionObject.gameObject.CompareTag("Enemy"))
        //     {
        // panggil method EnemyIsDead yg ada di script Enemy
        //         collisionObject.gameObject.GetComponent<Enemy>().EnemyIsDead();
        //     }
        // }

        if (_isPowerUpActive && collisionObject.gameObject.CompareTag("Enemy"))
        {
           
            if (_audioSourceEnemyClip != null)
            {
                Debug.Log(_audioSourceEnemyClip);
                AudioSource.PlayClipAtPoint(_audioSourceEnemyClip, transform.position);
            }

            collisionObject.gameObject.GetComponent<Enemy>().EnemyIsDead();

            Debug.Log("Enemy dies");
        }
    }

    private void UpdateUI_HealthText()
    {
        Debug.Log("Nilai Health pas mo diupdate :" + _healthIntValue);
        _healthText.text = "Health : " + _healthIntValue;

        if (_healthIntValue <= 0) { Debug.Log("Game Over -> You lose"); }

    }


    public void PlayerIsDead()
    {

        if (_isHittedByEnemy) return;
    
        _isHittedByEnemy = true;
        StartCoroutine(ResetDamageFlag());

        Debug.Log("Nilai Health before dikurangi 1 :" + _healthIntValue);
        _healthIntValue -= 1;
        Debug.Log("Nilai Health after dikurangi 1 :" + _healthIntValue);

        if (_healthIntValue <= 0) { Debug.Log("Game Over -> You lose"); }

        _audioSourcePlayer.Play();

        if (_healthIntValue > 0)
        {
           transform.position = _respawnPoint.position;
        }
        else
        {         

            _healthIntValue = 0;
          
            
            StartCoroutine(PauseThenLoadNext());
        }

        UpdateUI_HealthText();
    }
       

    private IEnumerator ResetDamageFlag()
    {
        yield return null; // wait one frame
        _isHittedByEnemy = false;
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
        SceneManager.LoadScene("LoseScene");
    }




}
