using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GameStartUI : UI_Scene,IListener
{
    private bool isCharacterSelected;
    private int characterCode;
 
    #region UI오브젝트 관리
    enum EGameObjects
    {
        DifficultyEasySelectEffect,
        DifficultyNormalSelectEffect,
        DifficultyHardSelectEffect,

        ShowScriptButtonIsClickEffect,
        SkillScriptIsClickEffect,
        LoadIsClickEffect,

        AboutScript,
        AboutSkill,
        AboutLoad,

        PassiveReactPannel,
        M1SkillReactPannel,
        M2SkillReactPannel,
        ShiftSkillReactPannel,
        RSkillReactPannel,

    }
    enum ETexts
    {
        DifficultyText,
        ExtendText,
        RelicsText,
        RelicsUnLockText,
        RightPannelBackButtonText,

        ShowScriptButtonText,
        SkillScriptText,
        LoadText,

        CharacterNameText,
        MoreDetailText1,
        MoreDetailText2,
        MoreDetailText3,
        MoreDetailText4,
       

        PassiveSkillTitle,
        PassiveSkillText,
        M1SkillTitle,
        M1SkillText,
        M2SkillTitle,
        M2SkillText,
        RSkillTitle,
        RSkillText,
        ShiftSkillTitle,
        ShiftSkillText,




        GameReadyButtonText,
    }
    enum EButtons
    {
        RightPannelBackButton,

        ShowScriptButton,
        SkillScriptButton,
        LoadButton,


        DifficultyEasy,
        DifficultyNormal,
        DifficultyHard,


        GameReadyButton,


        LoadShiftSkillButton_1,
        LoadShiftSkillButton_2,
        LoadM2SkillButton_1,
        LoadM2SkillButton_2,
        LoadRSkillButton_1,
        LoadRSkillButton_2,


        ExtendSubButton,
        RelicsSubButton,


    }
    enum EImages
    {
        BackGround,

        DiffidcultyEasyBackGround,
        DiffidcultyNormalBackGround,
        DiffidcultyHardBackGround,


        PassiveSkillImage,
        M1SkilIImage,
        M2SkilIImage,
        RSkilIImage,
        ShiftSkillImage,

        LoadPassiveSkillImage,
        LoadShiftSkillImage_1,
        LoadShiftSkillImage_2,


        LoadM2SkillImage_1,
        LoadM2SkillImage_2,
        LoadRSkillImage_1,
        LoadRSkillImage_2,




    }
    #endregion
    #region 난이도,캐릭터 설명 변경을 위한 Enum값
    enum EDifficulty
    {
        Easy,
        Normal,
        Hard,
    }
    enum ECharacterDetail
    {
        AboutScript,
        AboutSkill,
        AboutLoad,

    }
    #endregion
    void Start()
    {
        Init();
      
    }
    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = 5;
        Bind<GameObject>(typeof(EGameObjects));
        Bind<Button>(typeof(EButtons));
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Image>(typeof(EImages));
        Managers.Event.AddListener(Define.EVENT_TYPE.SelectCharacter, this);
        //글자 관련 초기화
        InitText();
        //게임 오브젝트 관련 초기화, 효과 off
        InitGameObect();
        //버튼 오브젝트 관련 초기화
        InitButton();









        //디폴트 이미지 효과 Easy
        DifficultyEffectChange(EDifficulty.Easy);

    }

    private void InitText()
    {
        GetText((int)ETexts.DifficultyText).text = $"난이도";
        GetText((int)ETexts.ExtendText).text = $"확장팩";
        GetText((int)ETexts.RelicsText).text = $"유물";

        GetText((int)ETexts.ShowScriptButtonText).text = $"개요";
        GetText((int)ETexts.SkillScriptText).text = "스킬";
        GetText((int)ETexts.LoadText).text = "장전";
                     
        GetText((int)ETexts.CharacterNameText).text = "";
        GetText((int)ETexts.MoreDetailText1).text = "";
        GetText((int)ETexts.MoreDetailText2).text = "";
        GetText((int)ETexts.MoreDetailText3).text = "";
        GetText((int)ETexts.MoreDetailText4).text = "";
        GetText((int)ETexts.PassiveSkillTitle).text="";
        GetText((int)ETexts.PassiveSkillText).text="";
        GetText((int)ETexts.M1SkillTitle).text="";
        GetText((int)ETexts.M1SkillText).text="";
        GetText((int)ETexts.M2SkillTitle).text="";
        GetText((int)ETexts.M2SkillText).text="";
        GetText((int)ETexts.RSkillTitle).text="";
        GetText((int)ETexts.RSkillText).text = "";
        GetText((int)ETexts.ShiftSkillText).text = "";
        GetText((int)ETexts.ShiftSkillTitle).text = "";
        GetText((int)ETexts.RelicsUnLockText).text = "이용가능한 유물이 없습니다.";




        GetText((int)ETexts.GameReadyButtonText).text = $"게임시작";
    }

    private void InitGameObect()
    {
        Get<GameObject>((int)EGameObjects. DifficultyEasySelectEffect).SetActive(false);
        Get<GameObject>((int)EGameObjects. DifficultyNormalSelectEffect).SetActive(false);
        Get<GameObject>((int)EGameObjects. DifficultyHardSelectEffect).SetActive(false);
        Get<GameObject>((int)EGameObjects. ShowScriptButtonIsClickEffect).SetActive(false);
        Get<GameObject>((int)EGameObjects. SkillScriptIsClickEffect).SetActive(false);
        Get<GameObject>((int)EGameObjects. LoadIsClickEffect).SetActive(false);
        Get<GameObject>((int)EGameObjects. AboutScript).SetActive(false);
        Get<GameObject>((int)EGameObjects. AboutSkill).SetActive(false);
        Get<GameObject>((int)EGameObjects.AboutLoad).SetActive(false);
        #region 스킬창 포인터 엔터 아웃시 나타나는 색상 이벤트
        Get<GameObject>((int)EGameObjects.PassiveReactPannel)
            .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.PassiveReactPannel), Color.white),Define.UIEvent.PointerEnter);
        Get<GameObject>((int)EGameObjects.M1SkillReactPannel)
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.M1SkillReactPannel), Color.white), Define.UIEvent.PointerEnter);
        Get<GameObject>((int)EGameObjects.M2SkillReactPannel)
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.M2SkillReactPannel), Color.white), Define.UIEvent.PointerEnter);
        Get<GameObject>((int)EGameObjects.ShiftSkillReactPannel)
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.ShiftSkillReactPannel), Color.white), Define.UIEvent.PointerEnter);
        Get<GameObject>((int)EGameObjects.RSkillReactPannel)
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.RSkillReactPannel), Color.white), Define.UIEvent.PointerEnter);
      

        Get<GameObject>((int)EGameObjects.PassiveReactPannel)
           .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.PassiveReactPannel), Color.white), Define.UIEvent.PointerExit);
        Get<GameObject>((int)EGameObjects.M1SkillReactPannel)
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.M1SkillReactPannel), Color.white), Define.UIEvent.PointerExit);
        Get<GameObject>((int)EGameObjects.M2SkillReactPannel)                     
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.M2SkillReactPannel), Color.white), Define.UIEvent.PointerExit);
        Get<GameObject>((int)EGameObjects.ShiftSkillReactPannel)                  
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.ShiftSkillReactPannel), Color.white), Define.UIEvent.PointerExit);
        Get<GameObject>((int)EGameObjects.RSkillReactPannel)                      
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.RSkillReactPannel), Color.white), Define.UIEvent.PointerExit);
       
        #endregion
    }

    private void SetColor(GameObject go,Color color)
    {
        if (go.GetComponent<Image>().color.a.Equals(1))
        {
            color.a = 0;
            go.GetComponent<Image>().color = color;
        }
        else
        {
            color.a = 1;
            go.GetComponent<Image>().color = color;
        }
    }

    private void InitButton()
    {
        GetButton((int)EButtons.RightPannelBackButton).gameObject
            .BindEvent((PointerEventData data) => ReturnToMain());
        GetButton((int)EButtons.GameReadyButton).gameObject
            .BindEvent((PointerEventData data) => GameStart());
        GetButton((int)EButtons.DifficultyEasy).gameObject
           .BindEvent((PointerEventData data) => DifficultyEffectChange(EDifficulty.Easy));
        GetButton((int)EButtons.DifficultyNormal).gameObject
           .BindEvent((PointerEventData data) => DifficultyEffectChange(EDifficulty.Normal));
        GetButton((int)EButtons.DifficultyHard).gameObject
           .BindEvent((PointerEventData data) => DifficultyEffectChange(EDifficulty.Hard));
        GetButton((int)EButtons.ShowScriptButton).gameObject
            .BindEvent((PointerEventData data) => DetaillCharacterScriptChange(ECharacterDetail.AboutScript));
        GetButton((int)EButtons.SkillScriptButton).gameObject
            .BindEvent((PointerEventData data) => DetaillCharacterScriptChange(ECharacterDetail.AboutSkill));
        GetButton((int)EButtons.LoadButton).gameObject
            .BindEvent((PointerEventData data) => DetaillCharacterScriptChange(ECharacterDetail.AboutLoad));

        GetButton((int)EButtons.ExtendSubButton).gameObject
            .BindEvent((PointerEventData data) => Managers.UI.ShowPopupUI<RelicExtendPopupUI>().MyType=RelicExtendPopupUI.EClickType.Extend);
        GetButton((int)EButtons.RelicsSubButton).gameObject
          .BindEvent((PointerEventData data) => Managers.UI.ShowPopupUI<RelicExtendPopupUI>().MyType = RelicExtendPopupUI.EClickType.Relic);
        

        //   GetButton((int)Buttons.ShowScriptButton).gameObject
        //       .BindEvent((PointerEventData data) => )
    }


    private void ReturnToMain()
    {
        Debug.Log("다시 게임 시작 누를 수 있는 메인 화면으로 돌아감 BGM 변경 시 여기!");
        Managers.Resource.Destroy(gameObject);

    }
    private void GameStart()
    {
        Debug.Log("게임화면 넘어가는 코드!");
    }
    private void DetaillCharacterScriptChange(ECharacterDetail ShowMenu)
    {
        if (!isCharacterSelected)
        {
            Debug.Log("캐릭터를 설정후 클릭해야 가능합니다.");
            return;
        }
        Get<GameObject>((int)EGameObjects.AboutLoad).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.AboutScript).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.AboutSkill).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.ShowScriptButtonIsClickEffect).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.SkillScriptIsClickEffect).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.LoadIsClickEffect).gameObject.SetActive(false);

        switch (ShowMenu)
        {
            case ECharacterDetail.AboutScript:
                Get<GameObject>((int)EGameObjects.AboutScript).gameObject.SetActive(true);
                Get<GameObject>((int)EGameObjects.ShowScriptButtonIsClickEffect).gameObject.SetActive(true);
                break;
            case ECharacterDetail.AboutSkill:
                Get<GameObject>((int)EGameObjects.AboutSkill).gameObject.SetActive(true);
                Get<GameObject>((int)EGameObjects.SkillScriptIsClickEffect).gameObject.SetActive(true);
                break;
            case ECharacterDetail.AboutLoad:
                Get<GameObject>((int)EGameObjects.AboutLoad).gameObject.SetActive(true);
                Get<GameObject>((int)EGameObjects.LoadIsClickEffect).gameObject.SetActive(true);
                break;
        }
    }
    private void DifficultyEffectChange(EDifficulty difficulty)
    {
        Get<GameObject>((int)EGameObjects.DifficultyEasySelectEffect).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.DifficultyNormalSelectEffect).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.DifficultyHardSelectEffect).gameObject.SetActive(false);

        switch (difficulty)
        {
            case EDifficulty.Easy:
                Get<GameObject>((int)EGameObjects.DifficultyEasySelectEffect).gameObject.SetActive(true);
                break;
            case EDifficulty.Normal:
                Get<GameObject>((int)EGameObjects.DifficultyNormalSelectEffect).gameObject.SetActive(true);
                break;
            case EDifficulty.Hard:
                Get<GameObject>((int)EGameObjects.DifficultyHardSelectEffect).gameObject.SetActive(true);
                break;
        }
        Debug.Log("게임 난이도 소리 설정 시 여기다!");
        Debug.Log("게임 시작 난이도 관련 설정은 여기서! (ex.난이도 클릭 시 소리 변경하고 싶으면 여기!)");


    }
    private void ChangeImage(int charcode)
    {
        GetImage((int)EImages.PassiveSkillImage).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].passiveskilliconpath);
       GetImage((int)EImages. M1SkilIImage          ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m1skilliconpath);
       GetImage((int)EImages. M2SkilIImage          ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m2skill_1iconpath);
       GetImage((int)EImages. RSkilIImage           ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].rskill_1iconpath);
       GetImage((int)EImages.ShiftSkillImage).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].shiftskill_1iconpath);
       GetImage((int)EImages. LoadPassiveSkillImage ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].passiveskilliconpath);
       GetImage((int)EImages. LoadShiftSkillImage_1 ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].shiftskill_1iconpath);
       GetImage((int)EImages. LoadShiftSkillImage_2 ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].shiftskill_2iconpath);
       GetImage((int)EImages. LoadM2SkillImage_1    ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m2skill_1iconpath);
       GetImage((int)EImages. LoadM2SkillImage_2    ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m2skill_2iconpath);
       GetImage((int)EImages. LoadRSkillImage_1     ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].rskill_1iconpath);
       GetImage((int)EImages. LoadRSkillImage_2     ).sprite= Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].r_skill2iconpath);
    }

    private void SkillScriptButtonEvent()
    {

    }
    private void LoadButtonEvent()
    {
     
    }

    private void DesCribeChange(int charcode)
    {
        Debug.Log("나중에 마우스 캔버스 한테도 이벤트 발송해 줘야합니다. => 미니 UI 생성 ");
        GetText((int)ETexts.CharacterNameText).text = $"{Managers.Data.CharacterDataDict[charcode].Name}";
        GetText((int)ETexts.MoreDetailText1).text = $"{Managers.Data.CharacterDataDict[charcode].script1}";
        GetText((int)ETexts.MoreDetailText2).text = $"{Managers.Data.CharacterDataDict[charcode].script2}";
        GetText((int)ETexts.MoreDetailText3).text = $"{Managers.Data.CharacterDataDict[charcode].script3}";
        GetText((int)ETexts.MoreDetailText4).text = $"{Managers.Data.CharacterDataDict[charcode].script4}";
        GetText((int)ETexts.PassiveSkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].passiveskill}";
        GetText((int)ETexts.PassiveSkillText).text = $"{Managers.Data.CharacterDataDict[charcode].passiveskillscript}";
        GetText((int)ETexts.M1SkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].m1skill}";
        GetText((int)ETexts.M1SkillText).text = $"{Managers.Data.CharacterDataDict[charcode].m1skillscript}";
        GetText((int)ETexts.M2SkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].m2skill_1}";
        GetText((int)ETexts.M2SkillText).text = $"{Managers.Data.CharacterDataDict[charcode].m2skill_1script}";
        GetText((int)ETexts.RSkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].rskill_1}";
        GetText((int)ETexts.RSkillText).text = $"{Managers.Data.CharacterDataDict[charcode].rskill_1script}";
        GetText((int)ETexts.ShiftSkillTitle).text= $"{Managers.Data.CharacterDataDict[charcode].shiftskill_1}";
        GetText((int)ETexts.ShiftSkillText).text= $"{Managers.Data.CharacterDataDict[charcode].shiftskill_1script}";


    }
    
    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {   
        isCharacterSelected=true;
        DetaillCharacterScriptChange(ECharacterDetail.AboutScript);
        characterCode =Sender.GetComponent<CharacterSelectButton>().Charactercode;
        DesCribeChange(characterCode);
        ChangeImage(characterCode);

     
    }
}
