using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    private float ySpeed;
    private CharacterController con;
    public bool isGrounded;

    public int startingLives = 3; // Initial number of lives
    private int currentLives; // Current number of lives
    public LifeDisplay lifeDisplay; // Reference to the LifeDisplay script

    public Camera mainCamera; // Reference to the main camera
    public Transform firstPersonCameraTransform; // Transform for the first-person camera position

    public GameObject PlayerBullet;
    private bool canShoot = false;

    public int CurrentLives // Public property to access currentLives
    {
        get { return currentLives; }
    }

    private void Start()
    {
        con = GetComponent<CharacterController>();
        currentLives = startingLives;
        UpdateLifeDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletCube"))
        {
            // Enable bullet prefab inside player
            PlayerBullet.SetActive(true);
            canShoot = true;
            Destroy(other.gameObject); // Destroy the bullet cube after collecting it
        }
    }

    void Update()
    {
        // Define axis of movement 
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove);
        moveDirection = mainCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        moveDirection.Normalize();
        float magnitude = moveDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);

        con.SimpleMove(moveDirection * magnitude * speed);

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            ySpeed = -0.5f;
            isGrounded = false;
        }

        Vector3 vel = moveDirection * magnitude;
        vel.y = ySpeed;

        con.Move(vel * Time.deltaTime);

        if (con.isGrounded)
        {
            ySpeed = -0.5f;
            isGrounded = true;
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
                isGrounded = false;
            }
        }

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }

        if (canShoot && Input.GetKeyDown(KeyCode.Q))
        {
            // Shoot bullet prefab
            GameObject bullet = Instantiate(PlayerBullet, transform.position, transform.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = transform.forward * 10f; 
            }
            canShoot = false;
        }
    }

    public void GainLife()
    {
        currentLives++;
        UpdateLifeDisplay(); // Update the life display if applicable
        Debug.Log("Life gained! Current lives: " + currentLives);
    }

    public void LoseLife()
    {
        currentLives--;
        if (currentLives <= 0)
        {
            int currentScore = CoinCounter.instance.GetCurrentScore();
            HighscoreManager.instance.AddNewScore(currentScore); // Add current score to high scores
            Debug.Log("Game Over");
            // Load end scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
        }
        UpdateLifeDisplay();
    }

    public void HandleDamage()
    {
        LoseLife();
    }

    private void UpdateLifeDisplay()
    {
        if (lifeDisplay != null)
        {
            lifeDisplay.OnLifeCountChanged();
        }
    }
}
