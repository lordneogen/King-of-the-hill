using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IDragHandler,IBeginDragHandler,IEndDragHandler,IDropHandler
{
    public GameObject TextObj;
    public ItemCount Item;
    private Image ImageCeil;
    [SerializeField] private Sprite EmptySlot;
    private TextMeshProUGUI Text;
    //
    private CanvasGroup canvasGroup;
    private RectTransform _rectTransform;
    private bool isDragging = false;
    private Vector2 initialPosition;
    [SerializeField] private float Yscroll=1f;
    [SerializeField] private float Xscroll=1f;
    //
    private Player Player;

    void Start()
    {
        ImageCeil = GetComponent<Image>();
        Text = TextObj.GetComponent<TextMeshProUGUI>();
        Text.SetText("");
        //
        canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
        //
        Player = GameObject.FindObjectOfType<Player>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(initialPosition);
        }
        
        if (!Item.Empty&&Item.Count>0)
        {
            ImageCeil.sprite = Item.Item.Icon;
            if (Item.Count > 1)
            {
                Text.SetText(Item.Count.ToString());
            }
            else
            {
                Text.SetText("");
            }
        }
        else
        {
            Item.Count = 0;
            Item.Item = null;
            Item.Empty = true;
            //enable
            ImageCeil.sprite = EmptySlot;
            Text.SetText("");
        }
        
        if (Player.SelectCeil == Item)
        {
            ImageCeil.color=Color.green;
        }
        else
        {
            ImageCeil.color=Color.gray;
        }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = _rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
        if (!(Item.Count <= 0 || Item.Empty || Item.Craft))
        {
            canvasGroup.alpha = .6f;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!(Item.Count <= 0 || Item.Empty || Item.Craft))
        {
            float deltaX = eventData.delta.x * Xscroll;
            float deltaY = eventData.delta.y * Yscroll;
            _rectTransform.anchoredPosition += new Vector2(deltaX, deltaY);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!(Item.Count <= 0 || Item.Empty || Item.Craft))
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            Debug.Log(eventData.pointerDrag.GetComponent<Slot>().Item.Count);
            _rectTransform.anchoredPosition = initialPosition;
        }
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        _rectTransform.anchoredPosition = initialPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Slot upperSlot = eventData.pointerDrag.GetComponent<Slot>();
            Slot downSlot = gameObject.GetComponent<Slot>();
            // downSlot.Item.Empty = false;
            // downSlot.Item.Item = upperSlot.Item.Item;
            // downSlot.Item.Count = upperSlot.Item.Count;
            if (!upperSlot.Item.Craft && !downSlot.Item.Craft)
            {
                SwapSlot(ref upperSlot.Item, ref downSlot.Item);
            }
        }
    }
    
    private void SwapSlot(ref ItemCount upperItem, ref ItemCount downItem)
    {
        if (downItem.Empty)
        {
            downItem.Item = upperItem.Item;
            downItem.Count = upperItem.Count;
            downItem.Empty = false;
            upperItem.Item = null;
            upperItem.Count = 0;
            upperItem.Empty = true;
        }
        else if(downItem.Item.Name==upperItem.Item.Name)
        {
            upperItem = downItem.AddItem(upperItem);
            if (upperItem.Count==0)
            {
                upperItem.Empty = true;
                upperItem.Item = null;
            }
        }
    }

}
