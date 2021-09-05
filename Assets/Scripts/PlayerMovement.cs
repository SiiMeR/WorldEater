using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public EaterController eater;
    public List<GameObject> characters;
    // Start is called before the first frame update
    void Awake()
    {
        SetColorOfAllSprites();

        eater = FindObjectOfType<EaterController>();
    }

    [SerializeField] private float _playerSpeed = 1f;

    public List<GameObject> bubbles;
    public TextMeshProUGUI text;
        
    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public GameObject GetCurrentActive()
    {
        if (characters.Count == 0)
        {
            return null;
        }
        return characters?.FirstOrDefault(character => character.activeInHierarchy);
    }
    private void SetColorOfAllSprites()
    {
        // Camera.main.backgroundColor = Colors.Background;
        var randomColor = Colors.GetRandomColor();
        foreach (var spriteRenderer in FindObjectsOfType<SpriteRenderer>())
        {
            // spriteRenderer.color = randomColor;
        }
    }

    public void GiveControl(GameObject other)
    {
        StartCoroutine(Bubbles(other.name));
        transform.localPosition = other.transform.localPosition;
        
        foreach (var character in characters)
        {
            if (character.name == other.name)
            {
                character.SetActive(true);
                try
                {
                    Destroy(GetComponent<CircleCollider2D>());
                    Destroy(GetComponent<BoxCollider2D>());
                }
                catch (Exception)
                {
            
                }
                if (character.name == "Porg")
                {
                    var collider = gameObject.AddComponent<BoxCollider2D>();
                    var spr = character.GetComponentInChildren<SpriteRenderer>().sprite;
                    collider.offset = new Vector2(0, 0);
                    collider.size = new Vector3((spr.bounds.size.x / transform.lossyScale.x) -1.6f,
                        (spr.bounds.size.y / transform.lossyScale.y) - 1.2f,
                        (spr.bounds.size.z / transform.lossyScale.z) - 1);
                }
                else
                {
                    var collider = gameObject.AddComponent<CircleCollider2D>();
                    var spr = character.GetComponentInChildren<SpriteRenderer>().sprite;
                    collider.offset = new Vector2(0, 0);
                    collider.radius = (spr.bounds.size.x / 2) - 1;
                }
            }
            else
            {
                character.SetActive(false);
            }
        }

        
        eater.gameObject.SetActive(false);
        FindObjectOfType<CinemachineVirtualCamera>().Follow = gameObject.transform;
        Destroy(other);

    }

    private IEnumerator Bubbles(string targ)
    {
        bubbles[0].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        bubbles[1].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        bubbles[2].SetActive(true);

        if (targ.Contains("Porg"))
            text.text = "I turned myself into a carrot";
        else if (targ.Contains("Sidrun"))
            text.text = "I turned myself into a lemon";
        else if (targ.Contains("Vorst")) text.text = "I turned myself into a sausage";

        text.gameObject.SetActive(true);

        yield return new WaitForSeconds(4f);
        
        foreach (var bubble in bubbles)
        {
            bubble.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var bubble in bubbles)
        {
            bubble.transform.localRotation = Quaternion.Euler(0,0,0);
        }
        
        text.transform.localRotation= Quaternion.Euler(0,0,0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // SelectNextCharacter();
        }
        
        MovePlayer();
    }

    private void SelectNextCharacter()
    {
        int indexOfCurrent = (transform.Cast<Transform>()
            .Where(child => child.gameObject.activeSelf)
            .Select(child => characters.IndexOf(child.gameObject))).FirstOrDefault();

        var next = (int) (indexOfCurrent + 1) % characters.Count;
        
        characters[indexOfCurrent].SetActive(false);
        characters[next].SetActive(true);

        try
        {
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<BoxCollider2D>());
        }
        catch (Exception)
        {
            
        }
        if (characters[next].name == "Porg")
        {
            var collider = gameObject.AddComponent<BoxCollider2D>();
            var spr = characters[next].GetComponentInChildren<SpriteRenderer>().sprite;
            collider.offset = new Vector2(0, 0);
            collider.size = new Vector3((spr.bounds.size.x / transform.lossyScale.x) -1.6f,
                (spr.bounds.size.y / transform.lossyScale.y) - 1.2f,
                (spr.bounds.size.z / transform.lossyScale.z) - 1);
        }
        else
        {
            var collider = gameObject.AddComponent<CircleCollider2D>();
            var spr = characters[next].GetComponentInChildren<SpriteRenderer>().sprite;
            collider.offset = new Vector2(0, 0);
            collider.radius = (spr.bounds.size.x / 2) - 1;
        }
    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontalInput * _playerSpeed, _playerRigidbody.velocity.y);
    }
}
