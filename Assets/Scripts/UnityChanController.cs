using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    private Rigidbody rigid;

    private float forwardForce = 800.0f;
    private float turnForce = 500.0f;
    private float upForce = 500.0f;

    private float movableRange = 3.4f;
    //動きを減速させる計数
    private float coefficient = 0.95f;

    private int score;

    //ゲーム終了の判定
    private bool isEnd = false;

    private GameObject stateText;
    private GameObject scoreText;

    private bool isLButtonDown = false;
    private bool isRButtonDown = false;


    // Use this for initialization
    void Start() {

        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1f);

        this.rigid = GetComponent<Rigidbody>();

        stateText = GameObject.Find("GameResultText");
        scoreText = GameObject.Find("ScoreText");

        score = 0;
    }
    void Update() {

        if (isEnd) {
            forwardForce *= coefficient;
            turnForce *= coefficient;
            upForce *= coefficient;
            myAnimator.speed *= coefficient;
        }

        rigid.AddForce(transform.forward * forwardForce);

        if((Input.GetKey(KeyCode.LeftArrow) || isLButtonDown) && -this.movableRange < this.transform.position.x) {
            this.rigid.AddForce(-this.turnForce, 0, 0);
        } else if ((Input.GetKey(KeyCode.RightArrow) || isRButtonDown) && this.movableRange > this.transform.position.x) {
            this.rigid.AddForce(this.turnForce, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.Space)&& this.transform.position.y < 0.5f) {
            this.myAnimator.SetBool("Jump", true);
            this.rigid.AddForce(this.transform.up * this.upForce);
        }

        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
            this.myAnimator.SetBool("Jump", false);
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
            isEnd = true;
            stateText.GetComponent<Text>().text = "GAME OVER";
        }

        if(other.gameObject.tag == "GoalTag") {
            isEnd = true;
            stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        if(other.gameObject.tag == "CoinTag") {
            Destroy(other.gameObject);
            GetComponent<ParticleSystem>().Play();
            score += 10;
            scoreText.GetComponent<Text>().text = "Score " + score + "pt";
        }
    }

    public void GetMyLeftButtonDown() {
        isLButtonDown = true;
    }

    public void GetMyLeftButtonUp() {
        isLButtonDown = false;
    }

    public void GetMyRightButtonDown() {
        isRButtonDown = true;
    }

    public void GetMyRightButtonUp() {
        isRButtonDown = false;
    }

    public void GetMyJumpButtonDown() {
        if (this.transform.position.y < 0.5f) {
            this.myAnimator.SetBool("Jump", true);
            this.rigid.AddForce(this.transform.up * this.upForce);
        }
    }
}
