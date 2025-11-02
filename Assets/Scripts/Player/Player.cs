using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public static float StartTurnSpeed = 400;
    public static float TurnSpeed = 400;
    public static float scaleSpeed = 0.2f;
    [SerializeField] private Rigidbody2D rigidbody2;
    [SerializeField] private Transform characterContainer;
    public List<PlayerChar> chars;
    [SerializeField] private GameObject conector;
    [SerializeField] private PlayerChar selectedChar;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    [SerializeField] private float blindZone;
    [SerializeField] private List<float> scales;

    public static Action OnMovementStart;
    private int scaleIndex =1 ;
    private bool movementStarted;
    public static bool continueMoveing = true;

    public static bool ScreenPressed { get; private set; }
    public static bool MousePressed { get; private set; }
    public static float RelativeScale { get; private set; }
    public static Vector2 TouchStartPosition { get; private set; }
    public static Vector2 TouchPosition { get; private set; }
    public static Vector2 InputVector { get; private set; }

    private List<GameObject> collidingObjects = new List<GameObject>();
    public static Action <float> OnScale;
    public static Action<Vector3> OnCollision;
    private GameObject collidingObj;
    [SerializeField] GameObject obj;

    private void Awake()
    {
        TurnSpeed = StartTurnSpeed;
        Instance = this;
        GameController.OnGameStart?.Invoke();
    }

    public void ChangeScale(bool grow)
    {
        if (grow)
        {
            if (scaleIndex < scales.Count - 1)
            {
                scaleIndex++;
            }
        }
        else
        {
            if (scaleIndex > 0)
            {
                scaleIndex--;
            }
        }
        StartCoroutine(ScaleAjustStep(grow));
    }

    private IEnumerator ScaleAjustStep( bool grow)
    {
        yield return null;
        float scale = grow ? scaleSpeed : -scaleSpeed;
        if (AjustScale(scale))
        {
            StartCoroutine(ScaleAjustStep(grow));
        }
    }

    private bool AjustScale(float scaleValue)
    {
        if ((selectedChar.canGrow && scaleValue > 0) || (selectedChar.canShrink && scaleValue < 0))
        {
            List<PlayerChar> unselectedChars = new List<PlayerChar>();
            unselectedChars.AddRange(chars);
            unselectedChars.Remove(selectedChar);
            Vector3 MoveVector = (unselectedChars[0].transform.position - selectedChar.transform.position).normalized;
            if (((selectedChar.transform.position - unselectedChars[0].transform.position).magnitude < scales[scaleIndex] && scaleValue > 0)
                || ((selectedChar.transform.position - unselectedChars[0].transform.position).magnitude > scales[scaleIndex] && scaleValue < 0))
            {
                unselectedChars[0].transform.position += MoveVector * scaleValue;
                OnScale?.Invoke(scaleValue);
                return true;
            }
        }
        return false;
    }

    private bool Rescale()
    {
        float scaleValue = Mathf.Clamp(((InputVector.y) - blindZone) * -scaleSpeed * Time.deltaTime, -scaleSpeed, scaleSpeed);
        if ((selectedChar.canGrow && scaleValue > 0) || (selectedChar.canShrink && scaleValue < 0))
        {
            List<PlayerChar> unselectedChars = new List<PlayerChar>();
            unselectedChars.AddRange(chars);
            unselectedChars.Remove(selectedChar);
            foreach (var item in unselectedChars)
            {
                Vector3 MoveVector = (item.transform.position - selectedChar.transform.position).normalized;
                if ((item.transform.position - selectedChar.transform.position + MoveVector * scaleValue).magnitude < maxScale
                    && (item.transform.position - selectedChar.transform.position + MoveVector * scaleValue).magnitude > minScale)
                {
                    item.transform.position += MoveVector * scaleValue;
                    OnScale?.Invoke(scaleValue);
                    return true;
                }
            }
        }
        return false;
    }

    private void Start()
    {
        selectedChar = chars[0];
    }

    void Update()
    {
        RelativeScale = ((Vector3.Distance(chars[0].transform.position, chars[1].transform.position) - minScale) / (maxScale - minScale));
        
        if (ScreenPressed)
        {
            if (Mathf.Abs(InputVector.x) > blindZone)
            {
                CheckForTurn();
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

    private void CheckForTurn()
    {
        if (!GameController.CanPlay)
        {
            return;
        }

        float rotationValue =  Mathf.Clamp(((InputVector.x) - blindZone) *(TurnSpeed * Time.deltaTime * Time.timeScale), TurnSpeed * Time.deltaTime * -1, TurnSpeed * Time.deltaTime);

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
        if (collision.gameObject.layer == 11)
        {
            LifeManager.TakeLife();
        }
        if (collision.gameObject.TryGetComponent(out CollectibleObject collectible))
        {
            CollectItem(collectible);
        }
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
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            collidingObjects.Remove(collision.gameObject);
            Debug.Log(collidingObjects.Count);
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
}
