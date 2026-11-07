using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.AdditionalEffect;

public partial class StatusProperty {
    [XmlAttribute] public int statChangeBase;
    [XmlAttribute] public int statChangeResult;
    [XmlAttribute] public float statChangeRate;
    [XmlAttribute] public long finalCapV;
    [XmlAttribute] public float finalCapR;
    [XmlAttribute] public long finalAbpV;
    [XmlAttribute] public float finalAbpR;
    [XmlAttribute] public long finalAtpV;
    [XmlAttribute] public float finalAtpR;
    [XmlAttribute] public long finalEvpV;
    [XmlAttribute] public float finalEvpR;
    [XmlAttribute] public float itemCriticalRecoveryRate;
    [XmlAttribute] public float itemRecoveryHPRate;
    [XmlAttribute] public float healRecoveryHPRate;
    [XmlAttribute] public float resWapR;
    [XmlAttribute] public float resBapR;
    [XmlAttribute] public float resCadR;
    [XmlAttribute] public float resAtpR;
    [XmlAttribute] public float resEvpR;
    [XmlAttribute] public float resPenR;
    [XmlAttribute] public float resAspR;
    [XmlAttribute] public bool deathResistanceHP;

    [M2dArray] public int[] compulsionEventTypes = Array.Empty<int>();
    [M2dArray] public float[] compulsionEventRate = Array.Empty<float>();
    [M2dArray] public int[] compulsionEventSkillCodes = Array.Empty<int>();

    [XmlElement] public Stat Stat; // long AND float
    [XmlElement] public SpecialAbility SpecialAbility;

    // Ignored by client.
    [XmlAttribute] public string saValue = string.Empty;
    [XmlAttribute] public string saRate = string.Empty;
    [XmlAttribute] public string value = string.Empty;
    [XmlAttribute] public string rate = string.Empty;
}

public partial class StatusPropertyNew {
    [XmlAttribute] public int statChangeBase;
    [XmlAttribute] public int statChangeResult;
    [XmlAttribute] public float statChangeRate;
    [XmlAttribute] public long finalCapV;
    [XmlAttribute] public float finalCapR;
    [XmlAttribute] public long finalAbpV;
    [XmlAttribute] public float finalAbpR;
    [XmlAttribute] public long finalAtpV;
    [XmlAttribute] public float finalAtpR;
    [XmlAttribute] public long finalEvpV;
    [XmlAttribute] public float finalEvpR;
    [XmlAttribute] public float itemCriticalRecoveryRate;
    [XmlAttribute] public float itemRecoveryHPRate;
    [XmlAttribute] public float healRecoveryHPRate;
    [XmlAttribute] public float resWapR;
    [XmlAttribute] public float resBapR;
    [XmlAttribute] public float resCadR;
    [XmlAttribute] public float resAtpR;
    [XmlAttribute] public float resEvpR;
    [XmlAttribute] public float resPenR;
    [XmlAttribute] public float resAspR;
    [XmlAttribute] public bool deathResistanceHP;

    [M2dArray] public int[] compulsionEventTypes = Array.Empty<int>();
    [M2dArray] public float[] compulsionEventRate = Array.Empty<float>();
    [M2dArray] public int[] compulsionEventSkillCodes = Array.Empty<int>();

    // Ignored by client.
    [XmlAttribute] public string saValue = string.Empty;
    [XmlAttribute] public string saRate = string.Empty;
    [XmlAttribute] public string value = string.Empty;
    [XmlAttribute] public string rate = string.Empty;
}

public class SpecialAbilityNew {
    [XmlElement] public List<SpecialAbilityEntry> v;
}

public class SpecialAbilityEntry {
    [XmlAttribute] public string type = string.Empty;
    [XmlAttribute] public float value;
}

public class Stat {
    [XmlAttribute] public long strvalue;
    [XmlAttribute] public long dexvalue;
    [XmlAttribute] public long intvalue;
    [XmlAttribute] public long lukvalue;
    [XmlAttribute] public long hpvalue;
    [XmlAttribute] public long hp_rgpvalue;
    [XmlAttribute] public long hp_invvalue;
    [XmlAttribute] public long spvalue;
    [XmlAttribute] public long sp_rgpvalue;
    [XmlAttribute] public long sp_invvalue;
    [XmlAttribute] public long epvalue;
    [XmlAttribute] public long ep_rgpvalue;
    [XmlAttribute] public long ep_invvalue;
    [XmlAttribute] public long aspvalue;
    [XmlAttribute] public long mspvalue;
    [XmlAttribute] public long atpvalue;
    [XmlAttribute] public long evpvalue;
    [XmlAttribute] public long capvalue;
    [XmlAttribute] public long cadvalue;
    [XmlAttribute] public long carvalue;
    [XmlAttribute] public long nddvalue;
    [XmlAttribute] public long abpvalue;
    [XmlAttribute] public long jmpvalue;
    [XmlAttribute] public long papvalue;
    [XmlAttribute] public long mapvalue;
    [XmlAttribute] public long parvalue;
    [XmlAttribute] public long marvalue;
    [XmlAttribute] public long wapminvalue;
    [XmlAttribute] public long wapmaxvalue;

    [XmlAttribute] public long dmgvalue;

    //[XmlAttribute] public long dmgvalue;
    [XmlAttribute] public long penvalue;
    [XmlAttribute] public long rmspvalue;
    [XmlAttribute] public long bapvalue;
    [XmlAttribute] public long bap_petvalue;

    [XmlAttribute] public float strrate;
    [XmlAttribute] public float dexrate;
    [XmlAttribute] public float intrate;
    [XmlAttribute] public float lukrate;
    [XmlAttribute] public float hprate;
    [XmlAttribute] public float hp_rgprate;
    [XmlAttribute] public float hp_invrate;
    [XmlAttribute] public float sprate;
    [XmlAttribute] public float sp_rgprate;
    [XmlAttribute] public float sp_invrate;
    [XmlAttribute] public float eprate;
    [XmlAttribute] public float ep_rgprate;
    [XmlAttribute] public float ep_invrate;
    [XmlAttribute] public float asprate;
    [XmlAttribute] public float msprate;
    [XmlAttribute] public float atprate;
    [XmlAttribute] public float evprate;
    [XmlAttribute] public float caprate;
    [XmlAttribute] public float cadrate;
    [XmlAttribute] public float carrate;
    [XmlAttribute] public float nddrate;
    [XmlAttribute] public float abprate;
    [XmlAttribute] public float jmprate;
    [XmlAttribute] public float paprate;
    [XmlAttribute] public float maprate;
    [XmlAttribute] public float parrate;
    [XmlAttribute] public float marrate;
    [XmlAttribute] public float wapminrate;
    [XmlAttribute] public float wapmaxrate;

    [XmlAttribute] public float dmgrate;

    //[XmlAttribute] public float dmgrate;
    [XmlAttribute] public float penrate;
    [XmlAttribute] public float rmsprate;
    [XmlAttribute] public float baprate;
    [XmlAttribute] public float bap_petrate;

    public long Value(byte i) {
        return i switch {
            0 => strvalue,
            1 => dexvalue,
            2 => intvalue,
            3 => lukvalue,
            4 => hpvalue,
            5 => hp_rgpvalue,
            6 => hp_invvalue,
            7 => spvalue,
            8 => sp_rgpvalue,
            9 => sp_invvalue,
            10 => epvalue,
            11 => ep_rgpvalue,
            12 => ep_invvalue,
            13 => aspvalue,
            14 => mspvalue,
            15 => atpvalue,
            16 => evpvalue,
            17 => capvalue,
            18 => cadvalue,
            19 => carvalue,
            20 => nddvalue,
            21 => abpvalue,
            22 => jmpvalue,
            23 => papvalue,
            24 => mapvalue,
            25 => parvalue,
            26 => marvalue,
            27 => wapminvalue,
            28 => wapmaxvalue,
            29 => dmgvalue,
            30 => dmgvalue,
            31 => penvalue,
            32 => rmspvalue,
            33 => bapvalue,
            34 => bap_petvalue,
            _ => throw new ArgumentOutOfRangeException(nameof(i), i, "Invalid Stat value."),
        };
    }

    public float Rate(byte i) {
        return i switch {
            0 => strrate,
            1 => dexrate,
            2 => intrate,
            3 => lukrate,
            4 => hprate,
            5 => hp_rgprate,
            6 => hp_invrate,
            7 => sprate,
            8 => sp_rgprate,
            9 => sp_invrate,
            10 => eprate,
            11 => ep_rgprate,
            12 => ep_invrate,
            13 => asprate,
            14 => msprate,
            15 => atprate,
            16 => evprate,
            17 => caprate,
            18 => cadrate,
            19 => carrate,
            20 => nddrate,
            21 => abprate,
            22 => jmprate,
            23 => paprate,
            24 => maprate,
            25 => parrate,
            26 => marrate,
            27 => wapminrate,
            28 => wapmaxrate,
            29 => dmgrate,
            30 => dmgrate,
            31 => penrate,
            32 => rmsprate,
            33 => baprate,
            34 => bap_petrate,
            _ => throw new ArgumentOutOfRangeException(nameof(i), i, "Invalid Stat rate."),
        };
    }
}

public class SpecialAbility {
    [XmlAttribute] public float segvalue;
    [XmlAttribute] public float smdvalue;
    [XmlAttribute] public float sssvalue;
    [XmlAttribute] public float dashdistancevalue;
    [XmlAttribute] public float spdvalue;
    [XmlAttribute] public float sidvalue;
    [XmlAttribute] public float finaladditionaldamagevalue;
    [XmlAttribute] public float crivalue;
    [XmlAttribute] public float sgivalue;
    [XmlAttribute] public float sgi_leadervalue;
    [XmlAttribute] public float sgi_elitevalue;
    [XmlAttribute] public float sgi_bossvalue;
    [XmlAttribute] public float killhprestorevalue;
    [XmlAttribute] public float killsprestorevalue;
    [XmlAttribute] public float killeprestorevalue;
    [XmlAttribute] public float healvalue;
    [XmlAttribute] public float receivedhealincreasevalue;
    [XmlAttribute] public float icedamagevalue;
    [XmlAttribute] public float firedamagevalue;
    [XmlAttribute] public float darkdamagevalue;
    [XmlAttribute] public float lightdamagevalue;
    [XmlAttribute] public float poisondamagevalue;
    [XmlAttribute] public float thunderdamagevalue;
    [XmlAttribute] public float nddincreasevalue;
    [XmlAttribute] public float lddincreasevalue;
    [XmlAttribute] public float parpenvalue;
    [XmlAttribute] public float marpenvalue;
    [XmlAttribute] public float icedamagereducevalue;
    [XmlAttribute] public float firedamagereducevalue;
    [XmlAttribute] public float darkdamagereducevalue;
    [XmlAttribute] public float lightdamagereducevalue;
    [XmlAttribute] public float poisondamagereducevalue;
    [XmlAttribute] public float thunderdamagereducevalue;
    [XmlAttribute] public float stunreducevalue;
    [XmlAttribute] public float conditionreducevalue;
    [XmlAttribute] public float skillcooldownvalue;
    [XmlAttribute] public float neardistancedamagereducevalue;
    [XmlAttribute] public float longdistancedamagereducevalue;
    [XmlAttribute] public float knockbackreducevalue;
    [XmlAttribute] public float stunprocnddvalue;
    [XmlAttribute] public float stunproclddvalue;
    [XmlAttribute] public float knockbackprocnddvalue;
    [XmlAttribute] public float knockbackproclddvalue;
    [XmlAttribute] public float snareprocnddvalue;
    [XmlAttribute] public float snareproclddvalue;
    [XmlAttribute] public float aoeprocnddvalue;
    [XmlAttribute] public float aoeproclddvalue;
    [XmlAttribute] public float npckilldropitemincratevalue;
    [XmlAttribute] public float seg_questrewardvalue;
    [XmlAttribute] public float smd_questrewardvalue;
    [XmlAttribute] public float seg_fishingrewardvalue;
    [XmlAttribute] public float seg_arcaderewardvalue;
    [XmlAttribute] public float seg_playinstrumentrewardvalue;
    [XmlAttribute] public float invoke_effect1value;
    [XmlAttribute] public float invoke_effect2value;
    [XmlAttribute] public float invoke_effect3value;
    [XmlAttribute] public float pvpdamageincreasevalue;
    [XmlAttribute] public float pvpdamagereducevalue;
    [XmlAttribute] public float improveguildexpvalue;
    [XmlAttribute] public float improveguildcoinvalue;
    [XmlAttribute] public float improvemassiveeventbexpballvalue;
    [XmlAttribute] public float reduce_meso_trade_feevalue;
    [XmlAttribute] public float reduce_enchant_matrial_feevalue;
    [XmlAttribute] public float reduce_merat_revival_feevalue;
    [XmlAttribute] public float improve_mining_reward_itemvalue;
    [XmlAttribute] public float improve_breeding_reward_itemvalue;
    [XmlAttribute] public float improve_blacksmithing_reward_masteryvalue;
    [XmlAttribute] public float improve_engraving_reward_masteryvalue;
    [XmlAttribute] public float improve_gathering_reward_itemvalue;
    [XmlAttribute] public float improve_farming_reward_itemvalue;
    [XmlAttribute] public float improve_alchemist_reward_masteryvalue;
    [XmlAttribute] public float improve_cooking_reward_masteryvalue;
    [XmlAttribute] public float improve_acquire_gathering_expvalue;
    [XmlAttribute] public float skill_levelup_tier_1value;
    [XmlAttribute] public float skill_levelup_tier_2value;
    [XmlAttribute] public float skill_levelup_tier_3value;
    [XmlAttribute] public float skill_levelup_tier_4value;
    [XmlAttribute] public float skill_levelup_tier_5value;
    [XmlAttribute] public float skill_levelup_tier_6value;
    [XmlAttribute] public float skill_levelup_tier_7value;
    [XmlAttribute] public float skill_levelup_tier_8value;
    [XmlAttribute] public float skill_levelup_tier_9value;
    [XmlAttribute] public float skill_levelup_tier_10value;
    [XmlAttribute] public float skill_levelup_tier_11value;
    [XmlAttribute] public float skill_levelup_tier_12value;
    [XmlAttribute] public float skill_levelup_tier_13value;
    [XmlAttribute] public float skill_levelup_tier_14value;
    [XmlAttribute] public float improve_massive_ox_expvalue;
    [XmlAttribute] public float improve_massive_trapmaster_expvalue;
    [XmlAttribute] public float improve_massive_finalsurvival_expvalue;
    [XmlAttribute] public float improve_massive_crazyrunner_expvalue;
    [XmlAttribute] public float improve_massive_sh_crazyrunner_expvalue;
    [XmlAttribute] public float improve_massive_escape_expvalue;
    [XmlAttribute] public float improve_massive_springbeach_expvalue;
    [XmlAttribute] public float improve_massive_dancedance_expvalue;
    [XmlAttribute] public float improve_massive_ox_mspvalue;
    [XmlAttribute] public float improve_massive_trapmaster_mspvalue;
    [XmlAttribute] public float improve_massive_finalsurvival_mspvalue;
    [XmlAttribute] public float improve_massive_crazyrunner_mspvalue;
    [XmlAttribute] public float improve_massive_sh_crazyrunner_mspvalue;
    [XmlAttribute] public float improve_massive_escape_mspvalue;
    [XmlAttribute] public float improve_massive_springbeach_mspvalue;
    [XmlAttribute] public float improve_massive_dancedance_mspvalue;
    [XmlAttribute] public float npc_hit_reward_sp_ballvalue;
    [XmlAttribute] public float npc_hit_reward_ep_ballvalue;
    [XmlAttribute] public float improve_honor_tokenvalue;
    [XmlAttribute] public float improve_pvp_expvalue;
    [XmlAttribute] public float improve_darkstream_damagevalue;
    [XmlAttribute] public float reduce_darkstream_recive_damagevalue;
    [XmlAttribute] public float improve_darkstream_evpvalue;
    [XmlAttribute] public float fishing_double_masteryvalue;
    [XmlAttribute] public float playinstrument_double_masteryvalue;
    [XmlAttribute] public float complete_fieldmission_mspvalue;
    [XmlAttribute] public float improve_glide_vertical_velocityvalue;
    [XmlAttribute] public float additionaleffect_95000018value;
    [XmlAttribute] public float additionaleffect_95000012value;
    [XmlAttribute] public float additionaleffect_95000014value;
    [XmlAttribute] public float additionaleffect_95000020value;
    [XmlAttribute] public float additionaleffect_95000021value;
    [XmlAttribute] public float additionaleffect_95000022value;
    [XmlAttribute] public float additionaleffect_95000023value;
    [XmlAttribute] public float additionaleffect_95000024value;
    [XmlAttribute] public float additionaleffect_95000025value;
    [XmlAttribute] public float additionaleffect_95000026value;
    [XmlAttribute] public float additionaleffect_95000027value;
    [XmlAttribute] public float additionaleffect_95000028value;
    [XmlAttribute] public float additionaleffect_95000029value;
    [XmlAttribute] public float reduce_recovery_ep_invvalue;
    [XmlAttribute] public float improve_stat_wap_uvalue;
    [XmlAttribute] public float mining_double_rewardvalue;
    [XmlAttribute] public float breeding_double_rewardvalue;
    [XmlAttribute] public float gathering_double_rewardvalue;
    [XmlAttribute] public float farming_double_rewardvalue;
    [XmlAttribute] public float blacksmithing_double_rewardvalue;
    [XmlAttribute] public float engraving_double_rewardvalue;
    [XmlAttribute] public float alchemist_double_rewardvalue;
    [XmlAttribute] public float cooking_double_rewardvalue;
    [XmlAttribute] public float mining_double_masteryvalue;
    [XmlAttribute] public float breeding_double_masteryvalue;
    [XmlAttribute] public float gathering_double_masteryvalue;
    [XmlAttribute] public float farming_double_masteryvalue;
    [XmlAttribute] public float blacksmithing_double_masteryvalue;
    [XmlAttribute] public float engraving_double_masteryvalue;
    [XmlAttribute] public float alchemist_double_masteryvalue;
    [XmlAttribute] public float cooking_double_masteryvalue;
    [XmlAttribute] public float improve_chaosraid_wapvalue;
    [XmlAttribute] public float improve_chaosraid_aspvalue;
    [XmlAttribute] public float improve_chaosraid_atpvalue;
    [XmlAttribute] public float improve_chaosraid_hpvalue;
    [XmlAttribute] public float improve_recovery_ballvalue;
    [XmlAttribute] public float improve_fieldboss_kill_expvalue;
    [XmlAttribute] public float improve_fieldboss_kill_dropvalue;
    [XmlAttribute] public float reduce_fieldboss_recive_damagevalue;
    [XmlAttribute] public float additionaleffect_95000016value;
    [XmlAttribute] public float improve_pettrap_rewardvalue;
    [XmlAttribute] public float mining_multiactionvalue;
    [XmlAttribute] public float breeding_multiactionvalue;
    [XmlAttribute] public float gathering_multiactionvalue;
    [XmlAttribute] public float farming_multiactionvalue;
    [XmlAttribute] public float reduce_damage_by_targetmaxhpvalue;
    [XmlAttribute] public float reduce_meso_revival_feevalue;
    [XmlAttribute] public float improve_riding_run_speedvalue;
    [XmlAttribute] public float improve_dungeon_reward_mesovalue;
    [XmlAttribute] public float improve_shop_buying_mesovalue;
    [XmlAttribute] public float improve_itembox_reward_mesovalue;
    [XmlAttribute] public float reduce_remakeoption_feevalue;
    [XmlAttribute] public float reduce_airtaxi_feevalue;
    [XmlAttribute] public float improve_socket_unlock_probabilityvalue;
    [XmlAttribute] public float reduce_gemstone_upgrade_feevalue;
    [XmlAttribute] public float reduce_pet_remakeoption_feevalue;
    [XmlAttribute] public float improve_riding_speedvalue;
    [XmlAttribute] public float improve_survival_kill_expvalue;
    [XmlAttribute] public float improve_survival_time_expvalue;
    [XmlAttribute] public float offensive_physicaldamagevalue;
    [XmlAttribute] public float offensive_magicaldamagevalue;
    [XmlAttribute] public float reduce_gameitem_socket_unlock_feevalue;

    [XmlAttribute] public float segrate;
    [XmlAttribute] public float smdrate;
    [XmlAttribute] public float sssrate;
    [XmlAttribute] public float dashdistancerate;
    [XmlAttribute] public float spdrate;
    [XmlAttribute] public float sidrate;
    [XmlAttribute] public float finaladditionaldamagerate;
    [XmlAttribute] public float crirate;
    [XmlAttribute] public float sgirate;
    [XmlAttribute] public float sgi_leaderrate;
    [XmlAttribute] public float sgi_eliterate;
    [XmlAttribute] public float sgi_bossrate;
    [XmlAttribute] public float killhprestorerate;
    [XmlAttribute] public float killsprestorerate;
    [XmlAttribute] public float killeprestorerate;
    [XmlAttribute] public float healrate;
    [XmlAttribute] public float receivedhealincreaserate;
    [XmlAttribute] public float icedamagerate;
    [XmlAttribute] public float firedamagerate;
    [XmlAttribute] public float darkdamagerate;
    [XmlAttribute] public float lightdamagerate;
    [XmlAttribute] public float poisondamagerate;
    [XmlAttribute] public float thunderdamagerate;
    [XmlAttribute] public float nddincreaserate;
    [XmlAttribute] public float lddincreaserate;
    [XmlAttribute] public float parpenrate;
    [XmlAttribute] public float marpenrate;
    [XmlAttribute] public float icedamagereducerate;
    [XmlAttribute] public float firedamagereducerate;
    [XmlAttribute] public float darkdamagereducerate;
    [XmlAttribute] public float lightdamagereducerate;
    [XmlAttribute] public float poisondamagereducerate;
    [XmlAttribute] public float thunderdamagereducerate;
    [XmlAttribute] public float stunreducerate;
    [XmlAttribute] public float conditionreducerate;
    [XmlAttribute] public float skillcooldownrate;
    [XmlAttribute] public float neardistancedamagereducerate;
    [XmlAttribute] public float longdistancedamagereducerate;
    [XmlAttribute] public float knockbackreducerate;
    [XmlAttribute] public float stunprocnddrate;
    [XmlAttribute] public float stunproclddrate;
    [XmlAttribute] public float knockbackprocnddrate;
    [XmlAttribute] public float knockbackproclddrate;
    [XmlAttribute] public float snareprocnddrate;
    [XmlAttribute] public float snareproclddrate;
    [XmlAttribute] public float aoeprocnddrate;
    [XmlAttribute] public float aoeproclddrate;
    [XmlAttribute] public float npckilldropitemincraterate;
    [XmlAttribute] public float seg_questrewardrate;
    [XmlAttribute] public float smd_questrewardrate;
    [XmlAttribute] public float seg_fishingrewardrate;
    [XmlAttribute] public float seg_arcaderewardrate;
    [XmlAttribute] public float seg_playinstrumentrewardrate;
    [XmlAttribute] public float invoke_effect1rate;
    [XmlAttribute] public float invoke_effect2rate;
    [XmlAttribute] public float invoke_effect3rate;
    [XmlAttribute] public float pvpdamageincreaserate;
    [XmlAttribute] public float pvpdamagereducerate;
    [XmlAttribute] public float improveguildexprate;
    [XmlAttribute] public float improveguildcoinrate;
    [XmlAttribute] public float improvemassiveeventbexpballrate;
    [XmlAttribute] public float reduce_meso_trade_feerate;
    [XmlAttribute] public float reduce_enchant_matrial_feerate;
    [XmlAttribute] public float reduce_merat_revival_feerate;
    [XmlAttribute] public float improve_mining_reward_itemrate;
    [XmlAttribute] public float improve_breeding_reward_itemrate;
    [XmlAttribute] public float improve_blacksmithing_reward_masteryrate;
    [XmlAttribute] public float improve_engraving_reward_masteryrate;
    [XmlAttribute] public float improve_gathering_reward_itemrate;
    [XmlAttribute] public float improve_farming_reward_itemrate;
    [XmlAttribute] public float improve_alchemist_reward_masteryrate;
    [XmlAttribute] public float improve_cooking_reward_masteryrate;
    [XmlAttribute] public float improve_acquire_gathering_exprate;
    [XmlAttribute] public float skill_levelup_tier_1rate;
    [XmlAttribute] public float skill_levelup_tier_2rate;
    [XmlAttribute] public float skill_levelup_tier_3rate;
    [XmlAttribute] public float skill_levelup_tier_4rate;
    [XmlAttribute] public float skill_levelup_tier_5rate;
    [XmlAttribute] public float skill_levelup_tier_6rate;
    [XmlAttribute] public float skill_levelup_tier_7rate;
    [XmlAttribute] public float skill_levelup_tier_8rate;
    [XmlAttribute] public float skill_levelup_tier_9rate;
    [XmlAttribute] public float skill_levelup_tier_10rate;
    [XmlAttribute] public float skill_levelup_tier_11rate;
    [XmlAttribute] public float skill_levelup_tier_12rate;
    [XmlAttribute] public float skill_levelup_tier_13rate;
    [XmlAttribute] public float skill_levelup_tier_14rate;
    [XmlAttribute] public float improve_massive_ox_exprate;
    [XmlAttribute] public float improve_massive_trapmaster_exprate;
    [XmlAttribute] public float improve_massive_finalsurvival_exprate;
    [XmlAttribute] public float improve_massive_crazyrunner_exprate;
    [XmlAttribute] public float improve_massive_sh_crazyrunner_exprate;
    [XmlAttribute] public float improve_massive_escape_exprate;
    [XmlAttribute] public float improve_massive_springbeach_exprate;
    [XmlAttribute] public float improve_massive_dancedance_exprate;
    [XmlAttribute] public float improve_massive_ox_msprate;
    [XmlAttribute] public float improve_massive_trapmaster_msprate;
    [XmlAttribute] public float improve_massive_finalsurvival_msprate;
    [XmlAttribute] public float improve_massive_crazyrunner_msprate;
    [XmlAttribute] public float improve_massive_sh_crazyrunner_msprate;
    [XmlAttribute] public float improve_massive_escape_msprate;
    [XmlAttribute] public float improve_massive_springbeach_msprate;
    [XmlAttribute] public float improve_massive_dancedance_msprate;
    [XmlAttribute] public float npc_hit_reward_sp_ballrate;
    [XmlAttribute] public float npc_hit_reward_ep_ballrate;
    [XmlAttribute] public float improve_honor_tokenrate;
    [XmlAttribute] public float improve_pvp_exprate;
    [XmlAttribute] public float improve_darkstream_damagerate;
    [XmlAttribute] public float reduce_darkstream_recive_damagerate;
    [XmlAttribute] public float improve_darkstream_evprate;
    [XmlAttribute] public float fishing_double_masteryrate;
    [XmlAttribute] public float playinstrument_double_masteryrate;
    [XmlAttribute] public float complete_fieldmission_msprate;
    [XmlAttribute] public float improve_glide_vertical_velocityrate;
    [XmlAttribute] public float additionaleffect_95000018rate;
    [XmlAttribute] public float additionaleffect_95000012rate;
    [XmlAttribute] public float additionaleffect_95000014rate;
    [XmlAttribute] public float additionaleffect_95000020rate;
    [XmlAttribute] public float additionaleffect_95000021rate;
    [XmlAttribute] public float additionaleffect_95000022rate;
    [XmlAttribute] public float additionaleffect_95000023rate;
    [XmlAttribute] public float additionaleffect_95000024rate;
    [XmlAttribute] public float additionaleffect_95000025rate;
    [XmlAttribute] public float additionaleffect_95000026rate;
    [XmlAttribute] public float additionaleffect_95000027rate;
    [XmlAttribute] public float additionaleffect_95000028rate;
    [XmlAttribute] public float additionaleffect_95000029rate;
    [XmlAttribute] public float reduce_recovery_ep_invrate;
    [XmlAttribute] public float improve_stat_wap_urate;
    [XmlAttribute] public float mining_double_rewardrate;
    [XmlAttribute] public float breeding_double_rewardrate;
    [XmlAttribute] public float gathering_double_rewardrate;
    [XmlAttribute] public float farming_double_rewardrate;
    [XmlAttribute] public float blacksmithing_double_rewardrate;
    [XmlAttribute] public float engraving_double_rewardrate;
    [XmlAttribute] public float alchemist_double_rewardrate;
    [XmlAttribute] public float cooking_double_rewardrate;
    [XmlAttribute] public float mining_double_masteryrate;
    [XmlAttribute] public float breeding_double_masteryrate;
    [XmlAttribute] public float gathering_double_masteryrate;
    [XmlAttribute] public float farming_double_masteryrate;
    [XmlAttribute] public float blacksmithing_double_masteryrate;
    [XmlAttribute] public float engraving_double_masteryrate;
    [XmlAttribute] public float alchemist_double_masteryrate;
    [XmlAttribute] public float cooking_double_masteryrate;
    [XmlAttribute] public float improve_chaosraid_waprate;
    [XmlAttribute] public float improve_chaosraid_asprate;
    [XmlAttribute] public float improve_chaosraid_atprate;
    [XmlAttribute] public float improve_chaosraid_hprate;
    [XmlAttribute] public float improve_recovery_ballrate;
    [XmlAttribute] public float improve_fieldboss_kill_exprate;
    [XmlAttribute] public float improve_fieldboss_kill_droprate;
    [XmlAttribute] public float reduce_fieldboss_recive_damagerate;
    [XmlAttribute] public float additionaleffect_95000016rate;
    [XmlAttribute] public float improve_pettrap_rewardrate;
    [XmlAttribute] public float mining_multiactionrate;
    [XmlAttribute] public float breeding_multiactionrate;
    [XmlAttribute] public float gathering_multiactionrate;
    [XmlAttribute] public float farming_multiactionrate;
    [XmlAttribute] public float reduce_damage_by_targetmaxhprate;
    [XmlAttribute] public float reduce_meso_revival_feerate;
    [XmlAttribute] public float improve_riding_run_speedrate;
    [XmlAttribute] public float improve_dungeon_reward_mesorate;
    [XmlAttribute] public float improve_shop_buying_mesorate;
    [XmlAttribute] public float improve_itembox_reward_mesorate;
    [XmlAttribute] public float reduce_remakeoption_feerate;
    [XmlAttribute] public float reduce_airtaxi_feerate;
    [XmlAttribute] public float improve_socket_unlock_probabilityrate;
    [XmlAttribute] public float reduce_gemstone_upgrade_feerate;
    [XmlAttribute] public float reduce_pet_remakeoption_feerate;
    [XmlAttribute] public float improve_riding_speedrate;
    [XmlAttribute] public float improve_survival_kill_exprate;
    [XmlAttribute] public float improve_survival_time_exprate;
    [XmlAttribute] public float offensive_physicaldamagerate;
    [XmlAttribute] public float offensive_magicaldamagerate;
    [XmlAttribute] public float reduce_gameitem_socket_unlock_feerate;

    // KMS2/TMS2 class-balance stats. No GMS2 SA slot, so Value/Rate(i) do not index them.
    [XmlAttribute] public float aevprate;
    [XmlAttribute] public float cap_ignore_limit_extrarate;
    [XmlAttribute] public float reduce_do_heal_hprate;
    // Crit rate and crit damage points added past the client clamp, in capvalue/cadvalue units.
    [XmlAttribute] public long cap_ignore_limitvalue;
    [XmlAttribute] public long cad_ignore_limitvalue;

    public float Value(byte i) {
        return i switch {
            0 => segvalue,
            1 => smdvalue,
            2 => sssvalue,
            3 => dashdistancevalue,
            4 => spdvalue,
            5 => sidvalue,
            6 => finaladditionaldamagevalue,
            7 => crivalue,
            8 => sgivalue,
            9 => sgi_leadervalue,
            10 => sgi_elitevalue,
            11 => sgi_bossvalue,
            12 => killhprestorevalue,
            13 => killsprestorevalue,
            14 => killeprestorevalue,
            15 => healvalue,
            16 => receivedhealincreasevalue,
            17 => icedamagevalue,
            18 => firedamagevalue,
            19 => darkdamagevalue,
            20 => lightdamagevalue,
            21 => poisondamagevalue,
            22 => thunderdamagevalue,
            23 => nddincreasevalue,
            24 => lddincreasevalue,
            25 => parpenvalue,
            26 => marpenvalue,
            27 => icedamagereducevalue,
            28 => firedamagereducevalue,
            29 => darkdamagereducevalue,
            30 => lightdamagereducevalue,
            31 => poisondamagereducevalue,
            32 => thunderdamagereducevalue,
            33 => stunreducevalue,
            34 => conditionreducevalue,
            35 => skillcooldownvalue,
            36 => neardistancedamagereducevalue,
            37 => longdistancedamagereducevalue,
            38 => knockbackreducevalue,
            39 => stunprocnddvalue,
            40 => stunproclddvalue,
            41 => knockbackprocnddvalue,
            42 => knockbackproclddvalue,
            43 => snareprocnddvalue,
            44 => snareproclddvalue,
            45 => aoeprocnddvalue,
            46 => aoeproclddvalue,
            47 => npckilldropitemincratevalue,
            48 => seg_questrewardvalue,
            49 => smd_questrewardvalue,
            50 => seg_fishingrewardvalue,
            51 => seg_arcaderewardvalue,
            52 => seg_playinstrumentrewardvalue,
            53 => invoke_effect1value,
            54 => invoke_effect2value,
            55 => invoke_effect3value,
            56 => pvpdamageincreasevalue,
            57 => pvpdamagereducevalue,
            58 => improveguildexpvalue,
            59 => improveguildcoinvalue,
            60 => improvemassiveeventbexpballvalue,
            61 => reduce_meso_trade_feevalue,
            62 => reduce_enchant_matrial_feevalue,
            63 => reduce_merat_revival_feevalue,
            64 => improve_mining_reward_itemvalue,
            65 => improve_breeding_reward_itemvalue,
            66 => improve_blacksmithing_reward_masteryvalue,
            67 => improve_engraving_reward_masteryvalue,
            68 => improve_gathering_reward_itemvalue,
            69 => improve_farming_reward_itemvalue,
            70 => improve_alchemist_reward_masteryvalue,
            71 => improve_cooking_reward_masteryvalue,
            72 => improve_acquire_gathering_expvalue,
            73 => skill_levelup_tier_1value,
            74 => skill_levelup_tier_2value,
            75 => skill_levelup_tier_3value,
            76 => skill_levelup_tier_4value,
            77 => skill_levelup_tier_5value,
            78 => skill_levelup_tier_6value,
            79 => skill_levelup_tier_7value,
            80 => skill_levelup_tier_8value,
            81 => skill_levelup_tier_9value,
            82 => skill_levelup_tier_10value,
            83 => skill_levelup_tier_11value,
            84 => skill_levelup_tier_12value,
            85 => skill_levelup_tier_13value,
            86 => skill_levelup_tier_14value,
            87 => improve_massive_ox_expvalue,
            88 => improve_massive_trapmaster_expvalue,
            89 => improve_massive_finalsurvival_expvalue,
            90 => improve_massive_crazyrunner_expvalue,
            91 => improve_massive_sh_crazyrunner_expvalue,
            92 => improve_massive_escape_expvalue,
            93 => improve_massive_springbeach_expvalue,
            94 => improve_massive_dancedance_expvalue,
            95 => improve_massive_ox_mspvalue,
            96 => improve_massive_trapmaster_mspvalue,
            97 => improve_massive_finalsurvival_mspvalue,
            98 => improve_massive_crazyrunner_mspvalue,
            99 => improve_massive_sh_crazyrunner_mspvalue,
            100 => improve_massive_escape_mspvalue,
            101 => improve_massive_springbeach_mspvalue,
            102 => improve_massive_dancedance_mspvalue,
            103 => npc_hit_reward_sp_ballvalue,
            104 => npc_hit_reward_ep_ballvalue,
            105 => improve_honor_tokenvalue,
            106 => improve_pvp_expvalue,
            107 => improve_darkstream_damagevalue,
            108 => reduce_darkstream_recive_damagevalue,
            109 => improve_darkstream_evpvalue,
            110 => fishing_double_masteryvalue,
            111 => playinstrument_double_masteryvalue,
            112 => complete_fieldmission_mspvalue,
            113 => improve_glide_vertical_velocityvalue,
            114 => additionaleffect_95000018value,
            115 => additionaleffect_95000012value,
            116 => additionaleffect_95000014value,
            117 => additionaleffect_95000020value,
            118 => additionaleffect_95000021value,
            119 => additionaleffect_95000022value,
            120 => additionaleffect_95000023value,
            121 => additionaleffect_95000024value,
            122 => additionaleffect_95000025value,
            123 => additionaleffect_95000026value,
            124 => additionaleffect_95000027value,
            125 => additionaleffect_95000028value,
            126 => additionaleffect_95000029value,
            127 => reduce_recovery_ep_invvalue,
            128 => improve_stat_wap_uvalue,
            129 => mining_double_rewardvalue,
            130 => breeding_double_rewardvalue,
            131 => gathering_double_rewardvalue,
            132 => farming_double_rewardvalue,
            133 => blacksmithing_double_rewardvalue,
            134 => engraving_double_rewardvalue,
            135 => alchemist_double_rewardvalue,
            136 => cooking_double_rewardvalue,
            137 => mining_double_masteryvalue,
            138 => breeding_double_masteryvalue,
            139 => gathering_double_masteryvalue,
            140 => farming_double_masteryvalue,
            141 => blacksmithing_double_masteryvalue,
            142 => engraving_double_masteryvalue,
            143 => alchemist_double_masteryvalue,
            144 => cooking_double_masteryvalue,
            145 => improve_chaosraid_wapvalue,
            146 => improve_chaosraid_aspvalue,
            147 => improve_chaosraid_atpvalue,
            148 => improve_chaosraid_hpvalue,
            149 => improve_recovery_ballvalue,
            150 => improve_fieldboss_kill_expvalue,
            151 => improve_fieldboss_kill_dropvalue,
            152 => reduce_fieldboss_recive_damagevalue,
            153 => additionaleffect_95000016value,
            154 => improve_pettrap_rewardvalue,
            155 => mining_multiactionvalue,
            156 => breeding_multiactionvalue,
            157 => gathering_multiactionvalue,
            158 => farming_multiactionvalue,
            159 => reduce_damage_by_targetmaxhpvalue,
            160 => reduce_meso_revival_feevalue,
            161 => improve_riding_run_speedvalue,
            162 => improve_dungeon_reward_mesovalue,
            163 => improve_shop_buying_mesovalue,
            164 => improve_itembox_reward_mesovalue,
            165 => reduce_remakeoption_feevalue,
            166 => reduce_airtaxi_feevalue,
            167 => improve_socket_unlock_probabilityvalue,
            168 => reduce_gemstone_upgrade_feevalue,
            169 => reduce_pet_remakeoption_feevalue,
            170 => improve_riding_speedvalue,
            171 => improve_survival_kill_expvalue,
            172 => improve_survival_time_expvalue,
            173 => offensive_physicaldamagevalue,
            174 => offensive_magicaldamagevalue,
            175 => reduce_gameitem_socket_unlock_feevalue,
            _ => throw new ArgumentOutOfRangeException(nameof(i), i, "Invalid StatusProperty value."),
        };
    }

    public float Rate(byte i) {
        return i switch {
            0 => segrate,
            1 => smdrate,
            2 => sssrate,
            3 => dashdistancerate,
            4 => spdrate,
            5 => sidrate,
            6 => finaladditionaldamagerate,
            7 => crirate,
            8 => sgirate,
            9 => sgi_leaderrate,
            10 => sgi_eliterate,
            11 => sgi_bossrate,
            12 => killhprestorerate,
            13 => killsprestorerate,
            14 => killeprestorerate,
            15 => healrate,
            16 => receivedhealincreaserate,
            17 => icedamagerate,
            18 => firedamagerate,
            19 => darkdamagerate,
            20 => lightdamagerate,
            21 => poisondamagerate,
            22 => thunderdamagerate,
            23 => nddincreaserate,
            24 => lddincreaserate,
            25 => parpenrate,
            26 => marpenrate,
            27 => icedamagereducerate,
            28 => firedamagereducerate,
            29 => darkdamagereducerate,
            30 => lightdamagereducerate,
            31 => poisondamagereducerate,
            32 => thunderdamagereducerate,
            33 => stunreducerate,
            34 => conditionreducerate,
            35 => skillcooldownrate,
            36 => neardistancedamagereducerate,
            37 => longdistancedamagereducerate,
            38 => knockbackreducerate,
            39 => stunprocnddrate,
            40 => stunproclddrate,
            41 => knockbackprocnddrate,
            42 => knockbackproclddrate,
            43 => snareprocnddrate,
            44 => snareproclddrate,
            45 => aoeprocnddrate,
            46 => aoeproclddrate,
            47 => npckilldropitemincraterate,
            48 => seg_questrewardrate,
            49 => smd_questrewardrate,
            50 => seg_fishingrewardrate,
            51 => seg_arcaderewardrate,
            52 => seg_playinstrumentrewardrate,
            53 => invoke_effect1rate,
            54 => invoke_effect2rate,
            55 => invoke_effect3rate,
            56 => pvpdamageincreaserate,
            57 => pvpdamagereducerate,
            58 => improveguildexprate,
            59 => improveguildcoinrate,
            60 => improvemassiveeventbexpballrate,
            61 => reduce_meso_trade_feerate,
            62 => reduce_enchant_matrial_feerate,
            63 => reduce_merat_revival_feerate,
            64 => improve_mining_reward_itemrate,
            65 => improve_breeding_reward_itemrate,
            66 => improve_blacksmithing_reward_masteryrate,
            67 => improve_engraving_reward_masteryrate,
            68 => improve_gathering_reward_itemrate,
            69 => improve_farming_reward_itemrate,
            70 => improve_alchemist_reward_masteryrate,
            71 => improve_cooking_reward_masteryrate,
            72 => improve_acquire_gathering_exprate,
            73 => skill_levelup_tier_1rate,
            74 => skill_levelup_tier_2rate,
            75 => skill_levelup_tier_3rate,
            76 => skill_levelup_tier_4rate,
            77 => skill_levelup_tier_5rate,
            78 => skill_levelup_tier_6rate,
            79 => skill_levelup_tier_7rate,
            80 => skill_levelup_tier_8rate,
            81 => skill_levelup_tier_9rate,
            82 => skill_levelup_tier_10rate,
            83 => skill_levelup_tier_11rate,
            84 => skill_levelup_tier_12rate,
            85 => skill_levelup_tier_13rate,
            86 => skill_levelup_tier_14rate,
            87 => improve_massive_ox_exprate,
            88 => improve_massive_trapmaster_exprate,
            89 => improve_massive_finalsurvival_exprate,
            90 => improve_massive_crazyrunner_exprate,
            91 => improve_massive_sh_crazyrunner_exprate,
            92 => improve_massive_escape_exprate,
            93 => improve_massive_springbeach_exprate,
            94 => improve_massive_dancedance_exprate,
            95 => improve_massive_ox_msprate,
            96 => improve_massive_trapmaster_msprate,
            97 => improve_massive_finalsurvival_msprate,
            98 => improve_massive_crazyrunner_msprate,
            99 => improve_massive_sh_crazyrunner_msprate,
            100 => improve_massive_escape_msprate,
            101 => improve_massive_springbeach_msprate,
            102 => improve_massive_dancedance_msprate,
            103 => npc_hit_reward_sp_ballrate,
            104 => npc_hit_reward_ep_ballrate,
            105 => improve_honor_tokenrate,
            106 => improve_pvp_exprate,
            107 => improve_darkstream_damagerate,
            108 => reduce_darkstream_recive_damagerate,
            109 => improve_darkstream_evprate,
            110 => fishing_double_masteryrate,
            111 => playinstrument_double_masteryrate,
            112 => complete_fieldmission_msprate,
            113 => improve_glide_vertical_velocityrate,
            114 => additionaleffect_95000018rate,
            115 => additionaleffect_95000012rate,
            116 => additionaleffect_95000014rate,
            117 => additionaleffect_95000020rate,
            118 => additionaleffect_95000021rate,
            119 => additionaleffect_95000022rate,
            120 => additionaleffect_95000023rate,
            121 => additionaleffect_95000024rate,
            122 => additionaleffect_95000025rate,
            123 => additionaleffect_95000026rate,
            124 => additionaleffect_95000027rate,
            125 => additionaleffect_95000028rate,
            126 => additionaleffect_95000029rate,
            127 => reduce_recovery_ep_invrate,
            128 => improve_stat_wap_urate,
            129 => mining_double_rewardrate,
            130 => breeding_double_rewardrate,
            131 => gathering_double_rewardrate,
            132 => farming_double_rewardrate,
            133 => blacksmithing_double_rewardrate,
            134 => engraving_double_rewardrate,
            135 => alchemist_double_rewardrate,
            136 => cooking_double_rewardrate,
            137 => mining_double_masteryrate,
            138 => breeding_double_masteryrate,
            139 => gathering_double_masteryrate,
            140 => farming_double_masteryrate,
            141 => blacksmithing_double_masteryrate,
            142 => engraving_double_masteryrate,
            143 => alchemist_double_masteryrate,
            144 => cooking_double_masteryrate,
            145 => improve_chaosraid_waprate,
            146 => improve_chaosraid_asprate,
            147 => improve_chaosraid_atprate,
            148 => improve_chaosraid_hprate,
            149 => improve_recovery_ballrate,
            150 => improve_fieldboss_kill_exprate,
            151 => improve_fieldboss_kill_droprate,
            152 => reduce_fieldboss_recive_damagerate,
            153 => additionaleffect_95000016rate,
            154 => improve_pettrap_rewardrate,
            155 => mining_multiactionrate,
            156 => breeding_multiactionrate,
            157 => gathering_multiactionrate,
            158 => farming_multiactionrate,
            159 => reduce_damage_by_targetmaxhprate,
            160 => reduce_meso_revival_feerate,
            161 => improve_riding_run_speedrate,
            162 => improve_dungeon_reward_mesorate,
            163 => improve_shop_buying_mesorate,
            164 => improve_itembox_reward_mesorate,
            165 => reduce_remakeoption_feerate,
            166 => reduce_airtaxi_feerate,
            167 => improve_socket_unlock_probabilityrate,
            168 => reduce_gemstone_upgrade_feerate,
            169 => reduce_pet_remakeoption_feerate,
            170 => improve_riding_speedrate,
            171 => improve_survival_kill_exprate,
            172 => improve_survival_time_exprate,
            173 => offensive_physicaldamagerate,
            174 => offensive_magicaldamagerate,
            175 => reduce_gameitem_socket_unlock_feerate,
            _ => throw new ArgumentOutOfRangeException(nameof(i), i, "Invalid StatusProperty rate."),
        };
    }
}
