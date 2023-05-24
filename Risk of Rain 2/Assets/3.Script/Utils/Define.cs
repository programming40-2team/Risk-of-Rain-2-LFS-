﻿public class Define
{

    public static readonly int MaxCharacterCount = 15;
    public enum Scene
    {
        None,
        MainMenu,
       

    }
    public enum SortingOrder
    {
        CharacterSelectButton=15,
        GameStartUI=5,
        LogBookUI = 6,
        DetailInLogBook=20,
        MouseInteraction=100,
    }
    public enum UIEvent
    {
        Click,
        OnDrag,
        PointerEnter,
        PointerExit,
        OnBeginDrag,
        OnEndDrag,
        OnDrop

    }
    public enum ItemType
    {
       Passive,
       Active,

    }
    public enum EVENT_TYPE
    {
        SelectCharacter,
        LogBookItem,
        ClickLogBookDetail,

        MousePointerEnter,
        MousePointerExit,
        DifficultyChange,
        AddItem,

        PlayerUseSkill,
        PlayerHpChange,
        EnemyHpChange,
        BossHpChange,
        PlayerExpChange,

        StartBattle,
        EndBattle,

        
    }
    public enum WhenItemActivates
    {
        Always,
        AfterBattle,
        InBattle,
        NotBattle,

    }
    public enum ECurrentClickType
    {
        ItemAndEquip,
        Monster,
        Character,
        Enviroment,
        None,

    }
    public enum EDifficulty
    {
       
        Easy,
        Normal,
        Hard,
        VeryHard,
        VeryHard2,
        VeryHard3,
        VeryHard4,
        VeryHard5,
 

    }
}
