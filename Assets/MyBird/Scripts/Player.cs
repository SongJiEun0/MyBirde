using UnityEngine;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        //����
        private bool keyJump = false;  //���� Ű ��ǲ üũ
        [SerializeField]
        private float jumpForce = 5f;  //���������� �ִ� ��

        //ȸ��
        private Vector3 birdRotaton;
        //���� �ö󰥶� ȸ�� �ӵ�
        [SerializeField]private float upRotate = 2.5f;
        //�������� �ö󰥶� ȸ�� �ӵ�
        [SerializeField]private float downRotate = -5f;

        //�̵�
        //�̵��ӵ� - Translate �����ϸ� �ڵ� ���������� �̵�
        [SerializeField] private float moveSpeed = 5f;
        #endregion

        #region Unity Event Method

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //����
            rb2D = this.GetComponent<Rigidbody2D>();

            //�ʱ�ȭ
        }

        // Update is called once per frame
        void Update()
        {
            //��ǲ ó��
            InputBird();

            //���� ȸ��
            RotateBird();

            //���� �̵�
            MoveBird();
        }

        private void FixedUpdate()
        {
            //�����ϱ�
            if (keyJump)
            {
                JumpBird();
                keyJump = false;
            }
        }
        #endregion

        #region Custom Method
        //��ǲó��
        void InputBird()
        {
            //�����̽�Ű �Ǵ� ���콺 ��Ŭ�� �Է� �ޱ�
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);
        }

        //���� �����ϱ�
        void JumpBird()
        {
            //�Ʒ��ʿ��� �������� ���� �ش�
            //rb2D.AddForce(Vector2.up * ��);
            rb2D.linearVelocity = Vector2.up * jumpForce;
        }

        //���� ȸ���ϱ�
        void RotateBird()
        {
            //�ö󰥶� �ִ� +30�� ���� ȸ�� : rotateSpeed = 2.5;(upRotate)
            //�������� �ּ� -90�� ���� ȸ�� : rotateSpeed = -5f;(downRotate)
            float rotateSpeed = 0f;
            if(rb2D.linearVelocity.y > 0f)  //�ö󰥶�
            {
                rotateSpeed = upRotate;
            }
            else if (rb2D.linearVelocity.y < 0f)  //��������
            {
                rotateSpeed = downRotate;
            }

            birdRotaton = new Vector3(0f, 0f, Mathf.Clamp(birdRotaton.z + rotateSpeed, -90f, 30f));
            this.transform.eulerAngles = birdRotaton;
        }

        //���� �̵�
        void MoveBird()
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }
        #endregion
    }
}