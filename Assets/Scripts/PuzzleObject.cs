using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEditor.VersionControl;
public enum PuzzleType
{
    Strawberry,
    Apple,
    Banana,
    End
}
public class PuzzleObject : MonoBehaviour
{
    [AutoProperty]
    [SerializeField]
    private SpriteRenderer sr;
    private BoxCollider2D box2d;
    public PuzzleType type;
    public GameObject InstPuzzleObject;
    public PuzzleObject overlapPuzzle;
    [SerializeField]
    private bool isSelected;

    private Vector2 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        sr =GetComponent<SpriteRenderer>();  
        box2d=GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.phase == TouchPhase.Began)
                    {
                        Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                       
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                        
                    }
                }
        
        
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
                if (hitInformation.transform.gameObject == gameObject && !isSelected)
                {
                    isSelected = true;
                    offset.x = transform.position.x - touchPos.x;
                    offset.y = transform.position.y - touchPos.y;
                }

            }
            if (isSelected && Input.GetMouseButton(0))
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = touchPos + offset;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isSelected)
                {
                    isSelected = false;
                    CheckMerge();
                }
            }
        }

       
    }
    void CheckMerge()
    {
        if(overlapPuzzle)
        {
            if(type==overlapPuzzle.type)
            {
                Instantiate(InstPuzzleObject, transform.position, Quaternion.identity);
                Destroy(overlapPuzzle.gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("PuzzleObject"))
        {
            PuzzleObject colpuzzle=collision.gameObject.GetComponent<PuzzleObject>();
            overlapPuzzle = colpuzzle;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("PuzzleObject"))
        {
            PuzzleObject colpuzzle = collision.gameObject.GetComponent<PuzzleObject>();
            if (overlapPuzzle == colpuzzle)
                overlapPuzzle = null;
        }
            
    }
}
