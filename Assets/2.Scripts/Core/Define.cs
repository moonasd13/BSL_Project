using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{

    [System.Serializable]
    public class BGMClipData
    {
        public BGMType type;
        public AudioClip clip;
    }

    [System.Serializable]
    public class SFXClipData
    {
        public SFXType type;
        public AudioClip clip;
    }
    public enum SFXType
    {
        PlayerHit,
        PlayerAttack,
        MonsterHit,
        MonsterDie,
        ButtonClick,
        LevelUp
    }

    public enum BGMType
    {
        MainMenu,
        Game,
        Boss
    }
    public enum ItemType
    {
        Weapon,
        Stats,
        //AdditionalEffects
    }
    public enum MonsterType
    {
        Normal,
        Boss
    }
    public enum ItemName
    {
        Axe,
        HockeyStick,
        SniperRifle
    }
    public enum ItemState
    {
        Store,
        Inven
    }
    public enum eInitItems
    {
        Axe,
        HockeyStick,
        SniperRifle,
        
        Hpcharm,
        Speedcharm,

        
    }
    public enum GameState
    {
        combat,

    }
}
