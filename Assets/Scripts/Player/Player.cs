using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
// using Zenject;
public class Player : MonoBehaviour
{
    

    public ItemCount CraftTest;
    private Vector2 MovementInput;
    [FormerlySerializedAs("speed")]
    [Header("Параметры объекта")]
    [Space]
    [Header("Параметры персоны")]
    [Range(0f, 100f)]
    public float Speed ;
    private float MaxSpeed;
    private float MinSpeed;
    [Space]
    [Header("Параметры инвентаря")]
    [FormerlySerializedAs("inventory")] public ItemContainerSlot Inventory;
    [Header("Подбираемость")] public bool Magnit = true;
    // [HideInInspector]
    public ItemCount SelectCeil;
    public int MaxHorizontalHud;
    public int HorizontalHud;
    [Space]
    [FormerlySerializedAs("offset")] [Range(0f, 0.1f)]
    public float Offset = 0.05f;
    [FormerlySerializedAs("max_health")] [Range(0, 12)]
    public int MaxHealth;
    [FormerlySerializedAs("Health")] [FormerlySerializedAs("health")] [HideInInspector]
    public int HealthPoint;
    [FormerlySerializedAs("max_hunger")] [Range(0f, 10000f)]
    public int MaxHunger;
    [FormerlySerializedAs("HungerScore")] [FormerlySerializedAs("hunger")] [HideInInspector]
    public int HungerPoint;
    [FormerlySerializedAs("hunger_speed")] [Range(0.1f, 2f)]
    public float HungerSpeed;
    private float HungerMaxSpeed;
    [FormerlySerializedAs("takedamge")] [Space]
    public bool TakeDamge = true;
    [FormerlySerializedAs("movementFilter")]
    [Space]
    [Header("Параметры дополнительные")]
    [Space]
    // public float knockbackForce = 1f;
    public ContactFilter2D MovementFilter;
    private Animator Animator;
    [FormerlySerializedAs("invtime")] public int InvinsibleTime=5;
    [FormerlySerializedAs("fullheart")] public Sprite FullHeart;
    [FormerlySerializedAs("emptyheart")] public Sprite EmptyHeart;
    [FormerlySerializedAs("heards")] public Image[] HeardsImage;
    private Vector3 PostionRespawn;
    private Rigidbody2D PlayerRigidbody2D;
    public Image HungerScale;
    private SpriteRenderer SpriteRender;
    private List<RaycastHit2D> CastColl = new List<RaycastHit2D>();

    public LayerMask objects;
    public LayerMask interactive;
    
    
    private IEnumerator Invinsible()
    {
        TakeDamge = false;
        for(int i=0;i<InvinsibleTime*4;i++)
        {
            yield return new WaitForSeconds(0.125f);
            SpriteRender.enabled = false;
            // Debug.Log("Start_anim");
            yield return new WaitForSeconds(0.125f);
            SpriteRender.enabled=true;
        }
        TakeDamge = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        HealthPoint = MaxHealth;
        HungerPoint = MaxHunger;
        MaxSpeed = Speed*2;
        MinSpeed = Speed;
        HungerMaxSpeed = HungerSpeed * 2;
        Inventory.ItemContainerStart();
        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRender = GetComponent<SpriteRenderer>();
        PostionRespawn = transform.position;
        StartCoroutine(Hunger());
    }
    private bool TryMove(Vector2 dir)
    {
        
        int count = PlayerRigidbody2D.Cast(
            MovementInput,
            MovementFilter,
            CastColl,
            Speed * Time.fixedDeltaTime + Offset
        );
        if (count == 0)
        {
            PlayerRigidbody2D.MovePosition(PlayerRigidbody2D.position + MovementInput * Speed * Time.deltaTime);
            return true;
        }
        else
        {
            return false;
        }

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Magnit = !Magnit;
        }
        HungerScale.transform.localScale = new Vector3( ((float)HungerPoint) / ((float)MaxHunger),HungerScale.transform.localScale.y,HungerScale.transform.localScale.z);
        if (Input.GetKey(KeyCode.Space))
        {
            Speed = MaxSpeed;
            HungerSpeed = HungerMaxSpeed;
        }
        else
        {
            HungerSpeed = HungerMaxSpeed / 2;
            Speed = MinSpeed;
        }

        if (HungerPoint <= 0)
        {
            HungerPoint = MaxHunger;
            HealthPoint -= 1;
            StartCoroutine(Invinsible());
        }
        
        for (int i = 0; i < MaxHealth; i++)
        {
            if (i < HealthPoint)
            {
                HeardsImage[i].enabled = true;
            }
            else
            {
                HeardsImage[i].enabled = false;
            }
        }
        
        if (HealthPoint <= 0)
        {

            transform.position = PostionRespawn;
            HealthPoint = MaxHealth;
            StartCoroutine(Invinsible());
            // Time.timeScale = 0;
        }
        if (MovementInput != Vector2.zero)
        {
            Animator.enabled = true;
            bool success=TryMove(MovementInput);
            if (!success)
            {
                success = TryMove(new Vector2(MovementInput.x, 0));
                if (!success)
                {
                    success = TryMove(new Vector2(0, MovementInput.y));
                
                }
            }
            Animator.SetBool("IsMoving",success);
        }
        else
        {
            Animator.enabled = false;
        }
        if (MovementInput.y > 0)
        {
            Animator.SetBool("IsUp",true);
            Animator.SetBool("IsDown",false);
        }
        else if (MovementInput.y < 0)
        {
            Animator.SetBool("IsDown",true);
            Animator.SetBool("IsUp",false);
        }
        else
        {
            Animator.SetBool("IsDown",false);
            Animator.SetBool("IsUp",false);
        }
        
        if (MovementInput.x < 0)
        {
            SpriteRender.flipX = false;
        }
        else if (MovementInput.x > 0)
        {
            SpriteRender.flipX = true;
        }
        float scrollWheelVertical = Input.mouseScrollDelta.y;

        if (scrollWheelVertical > 0)
        {
            HorizontalHud += 1;
            HorizontalHud = Math.Min(MaxHorizontalHud, HorizontalHud);
            // Debug.Log("Scroll Up");
        }
        else if (scrollWheelVertical < 0)
        {
            HorizontalHud -= 1;
            HorizontalHud = Math.Max(0, HorizontalHud);
            // Scroll Down
            // Debug.Log("Scroll Down");
        }
        SelectCeil = Inventory.Items[HorizontalHud];
    }

    private IEnumerator Hunger()
    {
        while (true)
        {
            if (HungerPoint > 0)
            {
                HungerPoint -= 1;
                yield return new WaitForSeconds(HungerSpeed);
            }
        }
    }

    public void GetHit()
    {
        if (TakeDamge)
        {
            HealthPoint -= 1;
            StartCoroutine(Invinsible());
        }
    }
    
    void OnMove(InputValue momentValue)
    {
        MovementInput = momentValue.Get<Vector2>();
    }
}
