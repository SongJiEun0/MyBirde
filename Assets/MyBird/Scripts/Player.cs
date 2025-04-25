using UnityEngine;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        //점프
        private bool keyJump = false;  //점프 키 인풋 체크
        [SerializeField]
        private float jumpForce = 5f;  //위방향으로 주는 힘

        //회전
        private Vector3 birdRotaton;
        //위로 올라갈때 회전 속도
        [SerializeField]private float upRotate = 2.5f;
        //내려갈때 올라갈때 회전 속도
        [SerializeField]private float downRotate = -5f;

        //이동
        //이동속도 - Translate 시작하면 자동 오른쪽으로 이동
        [SerializeField] private float moveSpeed = 5f;
        #endregion

        #region Unity Event Method

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //참조
            rb2D = this.GetComponent<Rigidbody2D>();

            //초기화
        }

        // Update is called once per frame
        void Update()
        {
            //인풋 처리
            InputBird();

            //버드 회전
            RotateBird();

            //버드 이동
            MoveBird();
        }

        private void FixedUpdate()
        {
            //점프하기
            if (keyJump)
            {
                JumpBird();
                keyJump = false;
            }
        }
        #endregion

        #region Custom Method
        //인풋처리
        void InputBird()
        {
            //스페이스키 또는 마우스 왼클릭 입력 받기
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);
        }

        //버드 점프하기
        void JumpBird()
        {
            //아래쪽에서 위쪽으로 힘을 준다
            //rb2D.AddForce(Vector2.up * 힘);
            rb2D.linearVelocity = Vector2.up * jumpForce;
        }

        //버드 회전하기
        void RotateBird()
        {
            //올라갈때 최대 +30도 까지 회전 : rotateSpeed = 2.5;(upRotate)
            //내려갈때 최소 -90도 까지 회전 : rotateSpeed = -5f;(downRotate)
            float rotateSpeed = 0f;
            if(rb2D.linearVelocity.y > 0f)  //올라갈때
            {
                rotateSpeed = upRotate;
            }
            else if (rb2D.linearVelocity.y < 0f)  //내려갈때
            {
                rotateSpeed = downRotate;
            }

            birdRotaton = new Vector3(0f, 0f, Mathf.Clamp(birdRotaton.z + rotateSpeed, -90f, 30f));
            this.transform.eulerAngles = birdRotaton;
        }

        //버드 이동
        void MoveBird()
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }
        #endregion
    }
}