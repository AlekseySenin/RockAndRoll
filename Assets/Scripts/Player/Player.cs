using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private Rigidbody2D rigidbody2;
    [SerializeField] private Transform characterContainer;
    public List<PlayerChar> chars;
    [SerializeField] private GameObject conector;
    [SerializeField] private PlayerChar selectedChar;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    [SerializeField] private float blindZone;
    [SerializeField] private List<float> scales;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _jumpPower = 10;
    [SerializeField] private float _pushPower = 5;

    public float TurnSpeed { get { return _turnSpeed; } }
    public float ScaleSpeed { get { return _scaleSpeed; } }


    private int scaleIndex =1 ;
    private bool movementStarted;

    public static bool continueMoveing = true;
    public static Action OnMovementStart;
    public static bool ScreenPressed { get; private set; }
    public static float RelativeScale { get; private set; }
    public static Vector2 TouchStartPosition { get; private set; }
    public static Vector2 InputVector { get; private set; }

    private List<GameObject> collidingObjects = new List<GameObject>();
    public static Action <float> OnScale;
    public static Action<Vector3> OnCollision;
    private GameObject collidingObj;
    [SerializeField] GameObject obj;


    private int _collisionCount;

    private void Awake()
    {
        Instance = this;
        GameController.OnGameStart?.Invoke();
    }


    private void Start()
    {
        selectedChar = chars[0];
    }

    void FixedUpdate()
    {
        RelativeScale = ((Vector3.Distance(chars[0].transform.position, chars[1].transform.position) - minScale) / (maxScale - minScale));

        if (ScreenPressed)
        {
            if (Mathf.Abs(InputVector.x) > blindZone)
            {
                CheckForTurn();
            }
            if (Mathf.Abs(InputVector.y) > blindZone)
            {
                AjustScale(InputVector.y * _scaleSpeed);
            }

            
        }
    }

    private void AjustScale(float scaleValue)
    {
        if ((selectedChar.canGrow && scaleValue > 0) || (selectedChar.canShrink && scaleValue < 0))
        {
            List<PlayerChar> unselectedChars = new List<PlayerChar>();
            unselectedChars.AddRange(chars);
            unselectedChars.Remove(selectedChar);
            Vector3 MoveVector = (unselectedChars[0].transform.position - selectedChar.transform.position).normalized;
            var selectedCharPosition = selectedChar.transform.position;
            var unselectedCharPosition = unselectedChars[0].transform.position;
            var charDistance = (selectedCharPosition - unselectedCharPosition).magnitude;
            if ((charDistance < maxScale && scaleValue > 0) ||
                (charDistance > minScale && scaleValue < 0))
            {
                Debug.Log(MoveVector * scaleValue);
                unselectedChars[0].transform.position += MoveVector * scaleValue;
                OnScale?.Invoke(scaleValue);
            }
        }
    }




    void MoveFocuseToCharacter(Vector3 newPosition)
    {
        transform.position += newPosition;
        characterContainer.position -= newPosition;
    }

    public void InpupReceived(Vector2 vector, bool isPressed)
    {
        if (!movementStarted)
        {
            movementStarted = true;
            OnMovementStart.Invoke();
        }
        ScreenPressed = isPressed;
        rigidbody2.freezeRotation = isPressed;
        InputVector = vector;
    }

    public void JumpReceived(bool isPressed) 
    {
        if(_collisionCount>0 && isPressed)
            rigidbody2.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }


    private void CheckForTurn()
    {
        if (!GameController.CanPlay)
        {
            return;
        }

        float rotationValue =  (InputVector.x *_turnSpeed);

        if(rotationValue > 0)
        {
            if(selectedChar == MostSideChar(true)|| MostSideChar(true) == null)
            {
                rigidbody2.rotation += rotationValue;
            }
            else
            {
                    SelectCharacter(MostSideChar(true));
            }
        }
        else
        {
            if (selectedChar == MostSideChar(false)|| MostSideChar(false) == null)
            {
                rigidbody2.rotation += rotationValue;
            }
            else
            {
                SelectCharacter(MostSideChar(false));
            }
        }
        
    }

    private void CheckForScaleChange()
    {
        if (true)
        {
            AjustScale(0.02f);
        }
    }

    public PlayerChar MostSideChar(bool left)
    {
        List<PlayerChar> playerChars = chars.FindAll(x => x.IsStanding());
        if (playerChars.Count > 0)
        {
            PlayerChar playerChar = playerChars[0];
            foreach (var item in playerChars)
            {
                if (left)
                {
                    if(item.transform.position.x< playerChar.transform.position.x)
                    {
                        playerChar = item;
                    }
                }
                else
                {
                    if(item.transform.position.x> playerChar.transform.position.x)
                    {
                        playerChar = item;
                    }
                }
            }
            return playerChar;
        }

        return null;
    }

    void MoveWithPlatform(Vector3 moveVector)
    {
        transform.position += moveVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CollectibleObject collectible))
        {
            if (collision.gameObject != collidingObj)
            {
                collidingObj = collision.gameObject;
                collectible.Collect();
                LevelController.CollectItem();
                Destroy(collectible.gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2d))
        {
            rb2d.AddForce(InputVector * -_pushPower, ForceMode2D.Impulse);
        }
        _collisionCount ++;
        if (collision.gameObject.layer == 14)
        {
            GameController.OnGameWin?.Invoke();
        }

        else
        {
            OnCollision?.Invoke(new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y,transform.position.z));
        }

        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (!collidingObjects.Contains(collision.gameObject))
            {
                platform.OnMove += MoveWithPlatform;
            }
            collidingObjects.Add (collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _collisionCount--;
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            collidingObjects.Remove(collision.gameObject);
            if (!collidingObjects.Contains(collision.gameObject))
            {
                platform.OnMove -= MoveWithPlatform;
            }
        }
    }

    void CollectItem(CollectibleObject collectedItem)
    {
        collectedItem.Collect();
        LevelController.CollectItem();
        Destroy(collectedItem.gameObject);
    }

    public void SelectCharacter(PlayerChar selected)
    {
        selectedChar = selected;
        MoveFocuseToCharacter(selected.RelativePosition(transform.position));
    }

    public void SetTurnSpeed(float val)
    {
        _turnSpeed = val;   
    }

    public void SetScaleSpeed(float val)
    {
       _scaleSpeed = val;
    }
}
