using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private BoxCollider boxCollider;
    [SerializeField]
    private TMP_Text speedText;

    private int JumpBool = Animator.StringToHash("isJump");
    private int RunBool = Animator.StringToHash("isRun");
    private int WalkBool = Animator.StringToHash("isWalking");
    
    private bool isOnGround = true;
    private Vector3 centerBoxColider;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private float startSpeed;


    private int timeForNewBehaviour = 3;
    private Coroutine coroutine;

    public void ResetPosition()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        speed = startSpeed;
        speedText.text = "";

        animator.SetBool(RunBool, false);
    }

    public void UpdateBahaviour(string label)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        switch (label)
        {
            case "Fast":
                speed += 2;
                break;
            case "Low":
                if (speed > 6) speed -= 2;
                break;
            default:
                break;
        }

        UpdateSpeedText();
        
        coroutine = StartCoroutine(SafeSpeed());
    }

    public void UpdateSpeedText()
    {
        speedText.text = $"Speed: {Mathf.RoundToInt(speed)}";
    }

    public void ResetSpeedText()
    {
        speedText.text = "";
    }

    private void Awake()
    {
        centerBoxColider = boxCollider.center;
        startPosition = transform.position;
        startRotation = transform.rotation;
        startSpeed = speed;
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");


        var newPosition = transform.position + transform.forward * speed * vertical * Time.deltaTime;
        transform.position = newPosition;
        transform.Rotate(Vector3.up, 90 * horizontal * Time.deltaTime);

        animator.SetBool(RunBool, vertical != 0);
        animator.SetBool(WalkBool, horizontal != 0 && vertical.Equals(0));


        //if (Input.GetKey(KeyCode.Space) && isOnGround)
        //{
        //    animator.SetBool(JumpBool, true);
        //    boxCollider.center = new Vector3(boxCollider.center.x, boxCollider.center.y, boxCollider.center.z);
        //    isOnGround = false;
        //}

        if (!isOnGround && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            animator.SetBool(JumpBool, false);
            boxCollider.center = centerBoxColider;
            isOnGround = true;
        }

        //personCamera.transform.position = Vector2.Lerp( personCamera.transform.position + cameraOffset, transform.position, Time.deltaTime * 1f);
    }

    private IEnumerator SafeSpeed()
    {
        yield return new WaitForSeconds(timeForNewBehaviour);
        speed = startSpeed;
        UpdateSpeedText();
    }
}
