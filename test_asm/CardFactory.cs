using LOR_DiceSystem;
using System.Collections.Generic;

public class CardFactory
{
    public void SetName(string name)
    {
        this._name = name;
    }

    public void SetTextId(int textId)
    {
        this._textId = textId;
    }

    public void SetArtwork(string artwork)
    {
        this._artwork = artwork;
    }

    public void SetRarity(Rarity rarity)
    {
        this._rarity = rarity;
    }

    public void SetSpec(DiceCardSpec spec)
    {
        this._spec = spec;
    }

    public void SetCardCost(int cost)
    {
        this._spec.Cost = cost;
    }

    public void SetCardRange(CardRange range)
    {
        this._spec.Ranged = range;
    }

    public void SetCardAffection(CardAffection affection)
    {
        this._spec.affection = affection;
    }

    public void SetCardEmotionLimit(int limit)
    {
        this._spec.emotionLimit = limit;
    }

    public void SetScript(string script)
    {
        this._script = script;
    }

    public void SetScriptDesc(string desc)
    {
        this._scriptDesc = desc;
    }

    public void AddDice(DiceBehaviour dice)
    {
        this._dices.Add(dice);
    }

    public void SetDices(List<DiceBehaviour> dice)
    {
        this._dices = dice != null ? dice : new List<DiceBehaviour>();
    }

    public void SetChapter(int chapter)
    {
        this._chapter = chapter;
    }

    public void SetSpecialEffect(string effect)
    {
        this._specialEffect = effect;
    }

    public void SetSkinChange(string skin)
    {
        this._skinChange = skin;
    }

    public void SetSkinType(CardSkinType ty)
    {
        this._skinChangeType = ty;
    }

    public void SetSkinHeight(int height)
    {
        this._skinHeight = height;
    }

    public void SetMapChange(string map)
    {
        this._mapChange = map;
    }

    public void SetPriority(int priority)
    {
        this._priority = priority;
    }

    public void SetPriorityScript(string script)
    {
        this._priorityScript = script;
    }

    public void SetCategory(BookCategory category)
    {
        this._category = category;
    }

    public void SetEgoCool(int time)
    {
        this._egoMaxCooltimeValue = time;
    }

    public void SetMaxNum(int maxNum)
    {
        this._maxNum = maxNum;
    }

    public DiceCardXmlInfo Generate(LorId lorId)
    {
        DiceCardXmlInfo info = new DiceCardXmlInfo(lorId);

        if (this._name != null)
        {
            info.workshopName = this._name;
        }

        info.workshopName = this._name;

        if (this._artwork != null)
        {
            info.Artwork = this._artwork;
        }

        info.Rarity = this._rarity;

        info.optionList = this.options != null ? this.options : new List<CardOption>();

        info.Spec = this._spec;

        info.Keywords = this.keywords != null ? this.keywords : new List<string>();

        if (this._script != null)
        {
            info.Script = this._script;
        }

        if (this._scriptDesc != null)
        {
            info.ScriptDesc = this._scriptDesc;
        }

        info.DiceBehaviourList = this._dices;

        info.Chapter = this._chapter;

        if (this._specialEffect != null)
        {
            info.SpecialEffect = this._specialEffect;
        }

        if (this._skinChange != null)
        {
            info.SkinChange = this._skinChange;
        }

        info.SkinChangeType = this._skinChangeType;

        info.SkinHeight = this._skinHeight;

        if (this._mapChange != null)
        {
            info.MapChange = this._mapChange;
        }

        info.Priority = this._priority;

        if (this._priorityScript != null)
        {
            info.PriorityScript = this._priorityScript;
        }

        info.category = this._category;

        info.EgoMaxCooltimeValue = this._egoMaxCooltimeValue;

        info.MaxNum = this._maxNum;

        return info;
    }

    private string _name;

    private int _textId = -1;

    private string _artwork;

    private Rarity _rarity = Rarity.Common;

    public List<CardOption> options = new List<CardOption>();

    private DiceCardSpec _spec = new DiceCardSpec()
    {
        Ranged = CardRange.Near,
        affection = CardAffection.One,
        Cost = 0,
        emotionLimit = 0,
    };

    public List<string> keywords = new List<string>();

    private string _script;

    private string _scriptDesc;

    private List<DiceBehaviour> _dices = new List<DiceBehaviour>();

    private int _chapter = 0;

    private string _specialEffect;

    private string _skinChange;

    private CardSkinType _skinChangeType;

    private int _skinHeight;

    private string _mapChange;

    private int _priority;

    private string _priorityScript;

    private BookCategory _category;

    private int _egoMaxCooltimeValue = 9;

    private int _maxNum = 150;
}
