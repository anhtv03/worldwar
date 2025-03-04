using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UltimateAttackScript : MonoBehaviour {

    [SerializeField]
    GameObject powerIncrease;

    [SerializeField]
    GameObject powerEnd;

    private float speed = 8;
    private float speedSize = 8;
    public float Speeds {
        get { return speed; }
        set { speed = value; }
    }
    private bool isUltimate = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private GameObject _character;

    private void Start() {
        rb = powerEnd.gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = powerIncrease.GetComponent<SpriteRenderer>();

        powerIncrease.SetActive(false);
        powerEnd.SetActive(false);

    }

    // Update is called once per frame
    void Update() {
        if (isUltimate) {
            Vector2 size = spriteRenderer.size;
            float currentSize = size.x;
            size.x = currentSize + speedSize * Time.deltaTime;
            spriteRenderer.size = size;

            rb.velocity = new Vector2(speed, rb.velocity.y);

            if (Mathf.Abs(size.x) > 10) {
                Destroy(gameObject);
                setMovementOn(_character);
            }
        }
    }

    //====================logic monster=================================
    public void UltimateAttack() {
        powerIncrease.SetActive(true);
        powerEnd.SetActive(true);
        isUltimate = true;
    }

    private void setMovementOn(GameObject character) {
        GamePadController gamePadController = character.GetComponent<GamePadController>();
        gamePadController.enabled = true;
    }

    public void setCharacter(GameObject character) {
        _character = character;
    }

}
