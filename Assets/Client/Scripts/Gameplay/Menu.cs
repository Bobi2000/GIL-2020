using UnityEngine;

public class Menu : MonoBehaviour
{
//    public List<MenuButton> buttons = new List<MenuButton>();
//    private Vector2 mousePosition;
//    private Vector2 fromVector2M = new Vector2(0.5f,1f);
//    private Vector2 centerCircle = new Vector2(0.5f,1f);
//    private Vector2 toVector2M;

//    public int MenuItems;
//    public int CurMenuItem;
//    private int oldMenuItems;
//    void Start()
//    {
//        MenuItems = buttons.Count;
//        foreach (var button in buttons)
//        {
//            button.screenImage.color = button.normalColor;
//        }
//        CurMenuItem = 0;
//        oldMenuItems = 0;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        GetCurrentMenuItem();
//        if (Input.GetKeyDown(KeyCode.G))
//        {
//            ButtonAction();
//        }
//    }
//    public void GetCurrentMenuItem()
//    {
//        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

//        toVector2M = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);
//        float angle = (Mathf.Atan2(fromVector2M.y - centerCircle.y, fromVector2M.x - centerCircle.x) -
//            Mathf.Atan2(toVector2M.y - centerCircle.y, fromVector2M.x - centerCircle.x))*Mathf.Rad2Deg;
//        if (angle<0)
//        {
//            angle += 360;
//        }
//        CurMenuItem = (int)(angle / 360 / MenuItems);
//        if (CurMenuItem!=oldMenuItems)
//        {
//            buttons[oldMenuItems].screenImage.color = buttons[oldMenuItems].normalColor;
//            oldMenuItems = CurMenuItem;
//            buttons[CurMenuItem].screenImage.color = buttons[CurMenuItem].hoverColor;
//        }
//    }
//    public void ButtonAction()
//    {
//        buttons[CurMenuItem].screenImage.color = buttons[CurMenuItem].pressedColor;
//        if (CurMenuItem==0)
//        {
//            Debug.Log("0");
//        }
//        else if (CurMenuItem == 1)
//        {
//            Debug.Log("1");
//        }

//    }
//    [System.Serializable]
//    public class MenuButton
//    {
//        public string name;
//        public Image screenImage;
//        public Color normalColor = Color.white;
//        public Color hoverColor = Color.gray;
//        public Color pressedColor = Color.gray;
//    }
}
