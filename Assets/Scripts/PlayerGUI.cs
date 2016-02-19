using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerGUI : MonoBehaviour {

    private Vector2 size = new Vector2(240,40);

    // Health variables
    private Vector2 healthPos = new Vector2(20,20);
    private float healthDisplay = 1;
    public Texture2D healthBarEmpty;
    public Texture2D healthBarFull;
    public bool isHit = false;

    // Stamina variables
    private Vector2 staminaPos = new Vector2(20, 60);
    private float staminaDisplay = 1;
    public Texture2D staminaBarEmpty;
    public Texture2D staminaBarFull;

    //Fall rate
    private int healthFallRate = 150;
    private int staminaFallRate = 150;

    private ThirdPersonCharacter controller;
    private CharacterInfo chInfo;

    private bool canJump;
    private float jumpTimer = 0.7f;
    // Use this for initialization
    void Start () {
        controller = GetComponent<ThirdPersonCharacter>();
	}
	
	void OnGUI () {

        // Health GUI
        GUI.BeginGroup(new Rect(healthPos.x, healthPos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), healthBarEmpty);

        GUI.BeginGroup(new Rect(0, 0, size.x * healthDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), healthBarFull);

        GUI.EndGroup();
        GUI.EndGroup();

        // Health GUI
        GUI.BeginGroup(new Rect(staminaPos.x, staminaPos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), staminaBarEmpty);

        GUI.BeginGroup(new Rect(0, 0, size.x * staminaDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), staminaBarFull);

        GUI.EndGroup();
        GUI.EndGroup();
    }

    // Update is called once per frame
    void Update() {

        // Health control section
        if (isHit)
        {

        }else {

        }

        if (healthDisplay <= 0)
        {
            CharacterDeath();
        }

        // Stamina control section
        if (Input.GetKey(KeyCode.W))
        {
            staminaDisplay -= Time.deltaTime / staminaFallRate;
        }else {
            staminaDisplay += Time.deltaTime / staminaFallRate;
        }

        // Jumping section
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            staminaDisplay -= 0.2f;
            Wait();
        }

        if (canJump == false)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (jumpTimer <= 0)
        {
            canJump = true;
        }

        if (staminaDisplay <= 0.2f)
        {
            canJump = false;
        }

        if (staminaDisplay >= 1)
        {
            staminaDisplay = 1;
        }

        if (staminaDisplay <= 0)
        {
            staminaDisplay = 0;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        canJump = false;
    }

    private void CharacterDeath()
    {
        Application.LoadLevel("Death");
    }
}
