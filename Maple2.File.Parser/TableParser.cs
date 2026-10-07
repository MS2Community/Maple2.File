using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using M2dXmlGenerator;
using Maple2.File.IO;
using Maple2.File.Parser.Enum;
using Maple2.File.Parser.Tools;
using Maple2.File.Parser.Xml.String;
using Maple2.File.Parser.Xml.Table;

namespace Maple2.File.Parser;

public class TableParser {
    private readonly M2dReader xmlReader;
    private readonly XmlSerializer featureSettingSerializer;
    private readonly XmlSerializer featureSerializer;
    private readonly XmlSerializer nameSerializer;
    private readonly XmlSerializer bankTypeSerializer;
    private readonly XmlSerializer chatStickerSerializer;
    private readonly XmlSerializer defaultItemsSerializer;
    private readonly XmlSerializer dungeonRoomSerializer;
    private readonly XmlSerializer dungeonMissionSerializer;
    private readonly XmlSerializer dungeonRoundDataSerializer;
    private readonly XmlSerializer dungeonRankRewardSerializer;
    private readonly XmlSerializer dungeonConfigSerializer;
    private readonly XmlSerializer dungeonRewardCouponSerializer;
    private readonly XmlSerializer enchantScrollSerializer;
    private readonly XmlSerializer fishSerializer;
    private readonly XmlSerializer fishHabitatSerializer;
    private readonly XmlSerializer fishingRodSerializer;
    private readonly XmlSerializer fishingSpotSerializer;
    private readonly XmlSerializer guildBuffSerializer;
    private readonly XmlSerializer guildContributionSerializer;
    private readonly XmlSerializer guildEventSerializer;
    private readonly XmlSerializer guildExpSerializer;
    private readonly XmlSerializer guildHouseSerializer;
    private readonly XmlSerializer guildNpcSerializer;
    private readonly XmlSerializer guildPropertySerializer;
    private readonly XmlSerializer guildQuestRewardFactorSerializer;
    private readonly XmlSerializer instrumentCategoryInfoSerializer;
    private readonly XmlSerializer instrumentInfoSerializer;
    private readonly XmlSerializer interactObjectSerializer;
    private readonly XmlSerializer itemBreakIngredientSerializer;
    private readonly XmlSerializer itemExchangeScrollSerializer;
    private readonly XmlSerializer itemExtractionSerializer;
    private readonly XmlSerializer itemGemstoneUpgradeSerializer;
    private readonly XmlSerializer itemLapenshardUpgradeSerializer;
    private readonly XmlSerializer itemRemakeScrollSerializer;
    private readonly XmlSerializer itemRepackingScrollSerializer;
    private readonly XmlSerializer itemSocketSerializer;
    private readonly XmlSerializer itemSocketScrollSerializer;
    private readonly XmlSerializer jobSerializer;
    private readonly XmlSerializer jobNewSerializer;
    private readonly XmlSerializer magicPathSerializer;
    private readonly XmlSerializer mapSpawnTagSerializer;
    private readonly XmlSerializer masteryRecipeSerializer;
    private readonly XmlSerializer masteryRewardSerializer;
    private readonly XmlSerializer paletteSerializer;
    private readonly XmlSerializer petExpSerializer;
    private readonly XmlSerializer petEvolutionSerializer;
    private readonly XmlSerializer petPropertySerializer;
    private readonly XmlSerializer petSpawnInfoSerializer;
    private readonly XmlSerializer premiumClubEffectSerializer;
    private readonly XmlSerializer premiumClubItemSerializer;
    private readonly XmlSerializer premiumClubPackageSerializer;
    private readonly XmlSerializer setItemInfoSerializer;
    private readonly XmlSerializer setItemOptionSerializer;
    private readonly XmlSerializer setItemOptionNewSerializer;
    private readonly XmlSerializer titleTagSerializer;
    private readonly XmlSerializer individualItemDropSerializer;
    private readonly XmlSerializer individualItemDropNewSerializer;
    private readonly XmlSerializer gachaInfoSerializer;
    private readonly XmlSerializer shopBeautyCouponSerializer;
    private readonly XmlSerializer shopFurnishingSerializer;
    private readonly XmlSerializer meretmarketCategorySerializer;
    private readonly XmlSerializer expBaseSerializer;
    private readonly XmlSerializer nextExpSerializer;
    private readonly XmlSerializer adventureLevelAbilitySerializer;
    private readonly XmlSerializer adventureLevelMissionSerializer;
    private readonly XmlSerializer adventureLevelRewardSerializer;
    private readonly XmlSerializer ugcDesignSerializer;
    private readonly XmlSerializer masteryUgcHousingSerializer;
    private readonly XmlSerializer ugcHousingPointRewardSerializer;
    private readonly XmlSerializer bannerSerializer;
    private readonly XmlSerializer nameTagSymbolSerializer;
    private readonly XmlSerializer commonExpSerializer;
    private readonly XmlSerializer chapterBookSerializer;
    private readonly XmlSerializer learningQuestSerializer;
    private readonly XmlSerializer masteryDifferentialFactorSerializer;
    private readonly XmlSerializer rewardContentSerializer;
    private readonly XmlSerializer rewardContentItemSerializer;
    private readonly XmlSerializer rewardContentExpStaticSerializer;
    private readonly XmlSerializer rewardContentMesoSerializer;
    private readonly XmlSerializer rewardContentMesoStaticSerializer;
    private readonly XmlSerializer survivalLevelSerializer;
    private readonly XmlSerializer survivalLevelRewardSerializer;
    private readonly XmlSerializer blackMarketSerializer;
    private readonly XmlSerializer changeJobSerializer;
    private readonly XmlSerializer fieldMissionSerializer;
    private readonly XmlSerializer fieldMetaDataSerializer;
    private readonly XmlSerializer worldMapSerializer;
    private readonly XmlSerializer mapleSurvivalSkinInfoSerializer;
    private readonly XmlSerializer weddingExpSerializer;
    private readonly XmlSerializer weddingPackageSerializer;
    private readonly XmlSerializer weddingRewardSerializer;
    private readonly XmlSerializer weddingSkillSerializer;
    private readonly XmlSerializer smartPushSerializer;
    private readonly XmlSerializer seasonDataSerializer;
    private readonly XmlSerializer statStringSerializer;
    private readonly XmlSerializer autoActionPricePackageSerializer;
    private readonly XmlSerializer pvpRankingDuelModeSerializer;
    private readonly XmlSerializer questGroupSerializer;
    private readonly XmlSerializer darkStreamSerializer;
    private readonly XmlSerializer clubBuffSerializer;
    private readonly XmlSerializer characterCreateSelectSerializer;
    private readonly XmlSerializer fameSerializer;
    private readonly XmlSerializer fameDefSerializer;
    private readonly XmlSerializer fameLimitSerializer;
    private readonly XmlSerializer fameLogSerializer;
    private readonly XmlSerializer famePickMethodSerializer;
    private readonly XmlSerializer characterAbilitySerializer;
    private readonly XmlSerializer maidExpSerializer;
    private readonly XmlSerializer maidPropertySerializer;
    private readonly XmlSerializer maidRecipeSerializer;
    private readonly XmlSerializer maidRecipeGroupSerializer;
    private readonly XmlSerializer maidSalarySerializer;
    private readonly XmlSerializer reactorSerializer;
    private readonly XmlSerializer reactorStateSerializer;

    private readonly string locale;
    private readonly string language;

    public TableParser(M2dReader xmlReader, string language) {
        this.xmlReader = xmlReader;
        featureSettingSerializer = new XmlSerializer(typeof(FeatureSetting));
        featureSerializer = new XmlSerializer(typeof(FeatureRoot));
        nameSerializer = new XmlSerializer(typeof(StringMapping));
        bankTypeSerializer = new XmlSerializer(typeof(BankTypeRoot));
        chatStickerSerializer = new XmlSerializer(typeof(ChatStickerRoot));
        defaultItemsSerializer = new XmlSerializer(typeof(DefaultItems));
        dungeonRoomSerializer = new XmlSerializer(typeof(DungeonRoomRoot));
        dungeonMissionSerializer = new XmlSerializer(typeof(DungeonMissionRoot));
        dungeonRoundDataSerializer = new XmlSerializer(typeof(DungeonRoundDataRoot));
        dungeonRankRewardSerializer = new XmlSerializer(typeof(DungeonRankRewardRoot));
        dungeonConfigSerializer = new XmlSerializer(typeof(DungeonConfigRoot));
        dungeonRewardCouponSerializer = new XmlSerializer(typeof(DungeonRewardCouponRoot));
        enchantScrollSerializer = new XmlSerializer(typeof(EnchantScrollRoot));
        fishSerializer = new XmlSerializer(typeof(FishRoot));
        fishHabitatSerializer = new XmlSerializer(typeof(FishHabitatRoot));
        fishingRodSerializer = new XmlSerializer(typeof(FishingRodRoot));
        fishingSpotSerializer = new XmlSerializer(typeof(FishingSpotRoot));
        guildBuffSerializer = new XmlSerializer(typeof(GuildBuffRoot));
        guildContributionSerializer = new XmlSerializer(typeof(GuildContributionRoot));
        guildEventSerializer = new XmlSerializer(typeof(GuildEventRoot));
        guildExpSerializer = new XmlSerializer(typeof(GuildExpRoot));
        guildHouseSerializer = new XmlSerializer(typeof(GuildHouseRoot));
        guildNpcSerializer = new XmlSerializer(typeof(GuildNpcRoot));
        guildPropertySerializer = new XmlSerializer(typeof(GuildPropertyRoot));
        guildQuestRewardFactorSerializer = new XmlSerializer(typeof(GuildQuestRewardFactorRoot));
        instrumentCategoryInfoSerializer = new XmlSerializer(typeof(InstrumentCategoryInfoRoot));
        instrumentInfoSerializer = new XmlSerializer(typeof(InstrumentInfoRoot));
        interactObjectSerializer = new XmlSerializer(typeof(InteractObjectRoot));
        itemBreakIngredientSerializer = new XmlSerializer(typeof(ItemBreakIngredientRoot));
        itemExchangeScrollSerializer = new XmlSerializer(typeof(ItemExchangeScrollRoot));
        itemExtractionSerializer = new XmlSerializer(typeof(ItemExtractionRoot));
        itemGemstoneUpgradeSerializer = new XmlSerializer(typeof(ItemGemstoneUpgradeRoot));
        itemLapenshardUpgradeSerializer = new XmlSerializer(typeof(ItemLapenshardUpgradeRoot));
        itemRemakeScrollSerializer = new XmlSerializer(typeof(ItemRemakeScrollRoot));
        itemRepackingScrollSerializer = new XmlSerializer(typeof(ItemRepackingScrollRoot));
        itemSocketSerializer = new XmlSerializer(typeof(ItemSocketRoot));
        itemSocketScrollSerializer = new XmlSerializer(typeof(ItemSocketScrollRoot));
        jobSerializer = new XmlSerializer(typeof(JobRoot));
        jobNewSerializer = new XmlSerializer(typeof(JobRootNew));
        magicPathSerializer = new XmlSerializer(typeof(MagicPath));
        mapSpawnTagSerializer = new XmlSerializer(typeof(MapSpawnTag));
        masteryRecipeSerializer = new XmlSerializer(typeof(MasteryRecipeRoot));
        masteryRewardSerializer = new XmlSerializer(typeof(MasteryRewardRoot));
        paletteSerializer = new XmlSerializer(typeof(ColorPaletteRoot));
        petExpSerializer = new XmlSerializer(typeof(PetExpRoot));
        petEvolutionSerializer = new XmlSerializer(typeof(PetEvolutionRoot));
        petPropertySerializer = new XmlSerializer(typeof(PetPropertyRoot));
        petSpawnInfoSerializer = new XmlSerializer(typeof(PetSpawnInfoRoot));
        premiumClubEffectSerializer = new XmlSerializer(typeof(PremiumClubEffectRoot));
        premiumClubItemSerializer = new XmlSerializer(typeof(PremiumClubItemRoot));
        premiumClubPackageSerializer = new XmlSerializer(typeof(PremiumClubPackageRoot));
        setItemInfoSerializer = new XmlSerializer(typeof(SetItemInfoRoot));
        setItemOptionSerializer = new XmlSerializer(typeof(SetItemOptionRoot));
        setItemOptionNewSerializer = new XmlSerializer(typeof(SetItemOptionRootNew));
        titleTagSerializer = new XmlSerializer(typeof(TitleTagRoot));
        individualItemDropSerializer = new XmlSerializer(typeof(IndividualItemDropRoot));
        individualItemDropNewSerializer = new XmlSerializer(typeof(IndividualItemDropRootNew));
        gachaInfoSerializer = new XmlSerializer(typeof(GachaInfoRoot));
        shopBeautyCouponSerializer = new XmlSerializer(typeof(ShopBeautyCouponRoot));
        shopFurnishingSerializer = new XmlSerializer(typeof(ShopFurnishingRoot));
        meretmarketCategorySerializer = new XmlSerializer(typeof(MeretMarketCategoryRoot));
        expBaseSerializer = new XmlSerializer(typeof(ExpBaseRoot));
        nextExpSerializer = new XmlSerializer(typeof(NextExpRoot));
        adventureLevelAbilitySerializer = new XmlSerializer(typeof(AdventureLevelAbilityRoot));
        adventureLevelMissionSerializer = new XmlSerializer(typeof(AdventureLevelMissionRoot));
        adventureLevelRewardSerializer = new XmlSerializer(typeof(AdventureLevelRewardRoot));
        ugcDesignSerializer = new XmlSerializer(typeof(UgcDesignRoot));
        masteryUgcHousingSerializer = new XmlSerializer(typeof(MasteryUgcHousingRoot));
        ugcHousingPointRewardSerializer = new XmlSerializer(typeof(UgcHousingPointRewardRoot));
        bannerSerializer = new XmlSerializer(typeof(BannerRoot));
        nameTagSymbolSerializer = new XmlSerializer(typeof(NameTagSymbolRoot));
        commonExpSerializer = new XmlSerializer(typeof(CommonExpRoot));
        chapterBookSerializer = new XmlSerializer(typeof(ChapterBookRoot));
        learningQuestSerializer = new XmlSerializer(typeof(LearningQuestRoot));
        masteryDifferentialFactorSerializer = new XmlSerializer(typeof(MasteryDifferentialFactorRoot));
        rewardContentSerializer = new XmlSerializer(typeof(RewardContentRoot));
        rewardContentItemSerializer = new XmlSerializer(typeof(RewardContentItemRoot));
        rewardContentExpStaticSerializer = new XmlSerializer(typeof(RewardContentExpStaticRoot));
        rewardContentMesoSerializer = new XmlSerializer(typeof(RewardContentMesoRoot));
        rewardContentMesoStaticSerializer = new XmlSerializer(typeof(RewardContentMesoStaticRoot));
        survivalLevelSerializer = new XmlSerializer(typeof(SurvivalLevelRoot));
        survivalLevelRewardSerializer = new XmlSerializer(typeof(SurvivalLevelRewardRoot));
        blackMarketSerializer = new XmlSerializer(typeof(BlackMarketRoot));
        changeJobSerializer = new XmlSerializer(typeof(ChangeJobRoot));
        fieldMissionSerializer = new XmlSerializer(typeof(FieldMissionRoot));
        fieldMetaDataSerializer = new XmlSerializer(typeof(FieldMetaDataRoot));
        worldMapSerializer = new XmlSerializer(typeof(WorldMapRoot));
        mapleSurvivalSkinInfoSerializer = new XmlSerializer(typeof(MapleSurvivalSkinInfoRoot));
        weddingExpSerializer = new XmlSerializer(typeof(WeddingExpRoot));
        weddingPackageSerializer = new XmlSerializer(typeof(WeddingPackageRoot));
        weddingRewardSerializer = new XmlSerializer(typeof(WeddingRewardRoot));
        weddingSkillSerializer = new XmlSerializer(typeof(WeddingSkillRoot));
        smartPushSerializer = new XmlSerializer(typeof(SmartPushRoot));
        seasonDataSerializer = new XmlSerializer(typeof(SeasonDataRoot));
        statStringSerializer = new XmlSerializer(typeof(StatStringRoot));
        autoActionPricePackageSerializer = new XmlSerializer(typeof(AutoActionPricePackageRoot));
        pvpRankingDuelModeSerializer = new XmlSerializer(typeof(PvpRankingDuelModeRoot));
        questGroupSerializer = new XmlSerializer(typeof(QuestGroupRoot));
        darkStreamSerializer = new XmlSerializer(typeof(DarkStreamRoot));
        clubBuffSerializer = new XmlSerializer(typeof(ClubBuffRoot));
        characterCreateSelectSerializer = new XmlSerializer(typeof(CharacterCreateSelect));
        fameSerializer = new XmlSerializer(typeof(FameRoot));
        fameDefSerializer = new XmlSerializer(typeof(FameDef));
        fameLimitSerializer = new XmlSerializer(typeof(FameLimitRoot));
        fameLogSerializer = new XmlSerializer(typeof(FameLogRoot));
        famePickMethodSerializer = new XmlSerializer(typeof(FamePickMethod));
        characterAbilitySerializer = new XmlSerializer(typeof(CharacterAbilityRoot));
        maidExpSerializer = new XmlSerializer(typeof(MaidExpRoot));
        maidPropertySerializer = new XmlSerializer(typeof(MaidPropertyRoot));
        maidRecipeSerializer = new XmlSerializer(typeof(MaidRecipeRoot));
        maidRecipeGroupSerializer = new XmlSerializer(typeof(MaidRecipeGroupRoot));
        maidSalarySerializer = new XmlSerializer(typeof(MaidSalaryRoot));
        reactorSerializer = new XmlSerializer(typeof(ReactorRoot));
        reactorStateSerializer = new XmlSerializer(typeof(ReactorStateRoot));

        locale = FeatureLocaleFilter.Locale.ToLower();
        this.language = language;

        // var seen = new HashSet<string>();
        // this.bankTypeSerializer.UnknownAttribute += (sender, args) => {
        //     if (!seen.Contains(args.Attr.Name)) {
        //         Console.WriteLine($"Missing {args.Attr.Name}={args.Attr.Value}");
        //         seen.Add(args.Attr.Name);
        //     }
        // };
    }

    public IEnumerable<(string Type, Setting Setting)> ParseFeatureSetting() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/feature_setting.xml"));
        var data = featureSettingSerializer.Deserialize(reader) as FeatureSetting;
        Debug.Assert(data != null);

        foreach (Setting setting in data.setting) {
            yield return (setting.type, setting);
        }
    }

    public IEnumerable<(string Name, Feature Feature)> ParseFeature() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/feature.xml"));
        var data = featureSerializer.Deserialize(reader) as FeatureRoot;
        Debug.Assert(data != null);

        foreach (Feature feature in data.feature) {
            yield return (feature.name, feature);
        }
    }

    public IEnumerable<(int Id, BankType BankType)> ParseBankType() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/banktype.xml"));
        var data = bankTypeSerializer.Deserialize(reader) as BankTypeRoot;
        Debug.Assert(data != null);

        foreach (BankType type in data.bankType) {
            yield return (type.id, type);
        }
    }

    public IEnumerable<(int Id, ChatSticker ChatSticker)> ParseChatSticker() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/chatemoticon.xml"));
        var data = chatStickerSerializer.Deserialize(reader) as ChatStickerRoot;
        Debug.Assert(data != null);

        foreach (ChatSticker sticker in data.chatEmoticon) {
            yield return (sticker.id, sticker);
        }
    }

    public IEnumerable<(int Id, ColorPalette Palette)> ParseColorPalette() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/colorpalette.xml"));
        var data = paletteSerializer.Deserialize(reader) as ColorPaletteRoot;
        Debug.Assert(data != null);

        foreach (ColorPalette palette in data.colorPalette) {
            yield return (palette.id, palette);
        }
    }

    public IEnumerable<(int Id, ColorPalette Palette)> ParseColorPaletteAchieve() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry($"table/{locale}/colorpalette_achieve.xml"));
        var data = paletteSerializer.Deserialize(reader) as ColorPaletteRoot;
        Debug.Assert(data != null);

        foreach (ColorPalette palette in data.colorPalette) {
            yield return (palette.id, palette);
        }
    }

    public IEnumerable<(int JobCode, string Slot, IList<DefaultItems.Item> Items)> ParseDefaultItems() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/defaultitems.xml"));
        var data = defaultItemsSerializer.Deserialize(reader) as DefaultItems;
        Debug.Assert(data != null);

        foreach (DefaultItems.Key key in data.key) {
            foreach (DefaultItems.Slot slot in key.slot) {
                yield return (key.jobCode, slot.name, slot.item);
            }
        }
    }

    public IEnumerable<(int Id, DungeonRoom Dungeon)> ParseDungeonRoom() {
        string filename = $"table/{locale}/dungeonroom.xml";
        if (xmlReader.Files.All(f => f.Name != filename)) {
            filename = "table/dungeonroom.xml";
        }
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry(filename));
        var data = dungeonRoomSerializer.Deserialize(reader) as DungeonRoomRoot;
        Debug.Assert(data != null);

        foreach (DungeonRoom dungeon in data.dungeonRoom) {
            yield return (dungeon.dungeonRoomID, dungeon);
        }
    }

    public IEnumerable<(int Id, DungeonRoundGroup RoundData)> ParseDungeonRoundData() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/dungeonrounddata.xml"));
        var data = dungeonRoundDataSerializer.Deserialize(reader) as DungeonRoundDataRoot;
        Debug.Assert(data != null);

        foreach (DungeonRoundGroup round in data.DungeonRoundGroup) {
            yield return (round.id, round);
        }
    }

    public IEnumerable<(int Id, DungeonRankReward Reward)> ParseDungeonRankReward() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/dungeonrankreward.xml"));
        var data = dungeonRankRewardSerializer.Deserialize(reader) as DungeonRankRewardRoot;
        Debug.Assert(data != null);

        foreach (DungeonRankReward reward in data.DungeonRankReward) {
            yield return (reward.rewardID, reward);
        }
    }

    public IEnumerable<(int Id, DungeonMission Mission)> ParseDungeonMission() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/dungeonmission.xml"));
        var data = dungeonMissionSerializer.Deserialize(reader) as DungeonMissionRoot;
        Debug.Assert(data != null);

        foreach (DungeonMission mission in data.DungeonMission) {
            yield return (mission.id, mission);
        }
    }

    public IEnumerable<DungeonConfig> ParseDungeonConfig() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry($"table/{locale}/dungeonconfig.xml"));
        var data = dungeonConfigSerializer.Deserialize(reader) as DungeonConfigRoot;
        Debug.Assert(data != null);

        foreach (DungeonConfig config in data.DungeonConfig) {
            yield return config;
        }
    }

    public IEnumerable<ReverseRaidConfig> ParseReverseRaidConfig() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry($"table/{locale}/dungeonconfig.xml"));
        var data = dungeonConfigSerializer.Deserialize(reader) as DungeonConfigRoot;
        Debug.Assert(data != null);

        foreach (ReverseRaidConfig config in data.ReverseRaidConfig) {
            yield return config;
        }
    }

    public IEnumerable<UnitedWeeklyReward> ParseUnitedWeeklyReward() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry($"table/{locale}/dungeonconfig.xml"));
        var data = dungeonConfigSerializer.Deserialize(reader) as DungeonConfigRoot;
        Debug.Assert(data != null);

        foreach (UnitedWeeklyReward config in data.UnitedWeeklyReward) {
            yield return config;
        }
    }

    public IEnumerable<(int Id, EnchantScroll Scroll)> ParseEnchantScroll() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/enchantscroll.xml"));
        var data = enchantScrollSerializer.Deserialize(reader) as EnchantScrollRoot;
        Debug.Assert(data != null);

        foreach (EnchantScroll scroll in data.scroll) {
            yield return (scroll.id, scroll);
        }
    }

    public IEnumerable<(int Id, string Name, Fish Fish)> ParseFish() {
        XmlReader nameReader = xmlReader.GetXmlReader(xmlReader.GetEntry($"{language}/stringfishname.xml"));
        var mapping = nameSerializer.Deserialize(nameReader) as StringMapping;
        Debug.Assert(mapping != null);

        Dictionary<int, string> fishNames = mapping.key.ToDictionary(key => int.Parse(key.id), key => key.name);

        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/fish.xml"));
        var data = fishSerializer.Deserialize(reader) as FishRoot;
        Debug.Assert(data != null);

        foreach (Fish fish in data.fish) {
            yield return (fish.id, fishNames.GetValueOrDefault(fish.id, string.Empty), fish);
        }
    }

    public IEnumerable<(int Id, FishHabitat Habitat)> ParseFishHabitat() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/fishhabitat.xml"));
        var data = fishHabitatSerializer.Deserialize(reader) as FishHabitatRoot;
        Debug.Assert(data != null);

        foreach (FishHabitat habitat in data.fish) {
            yield return (habitat.id, habitat);
        }
    }

    public IEnumerable<(int Id, FishingRod Rod)> ParseFishingRod() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/fishingrod.xml"));
        var data = fishingRodSerializer.Deserialize(reader) as FishingRodRoot;
        Debug.Assert(data != null);

        foreach (FishingRod rod in data.rod) {
            yield return (rod.rodCode, rod);
        }
    }

    public IEnumerable<(int Id, FishingSpot Spot)> ParseFishingSpot() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/fishingspot.xml"));
        var data = fishingSpotSerializer.Deserialize(reader) as FishingSpotRoot;
        Debug.Assert(data != null);

        foreach (FishingSpot spot in data.spot) {
            yield return (spot.id, spot);
        }
    }

    public IEnumerable<(int Id, IEnumerable<GuildBuff> Buffs)> ParseGuildBuff() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/guildbuff.xml"));
        var data = guildBuffSerializer.Deserialize(reader) as GuildBuffRoot;
        Debug.Assert(data != null);

        foreach (IGrouping<int, GuildBuff> group in data.guildBuff.GroupBy(guildBuff => guildBuff.id)) {
            yield return (group.Key, group);
        }
    }

    public IEnumerable<(GuildContributionType Type, GuildContribution Contribution)> ParseGuildContribution() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/guildcontribution.xml"));
        var data = guildContributionSerializer.Deserialize(reader) as GuildContributionRoot;
        Debug.Assert(data != null);

        foreach (GuildContribution contribution in data.contribution) {
            yield return (contribution.type, contribution);
        }
    }

    public IEnumerable<(int Id, GuildEvent Event)> ParseGuildEvent() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/guildevent.xml"));
        var data = guildEventSerializer.Deserialize(reader) as GuildEventRoot;
        Debug.Assert(data != null);

        foreach (GuildEvent @event in data.guildEvent) {
            yield return (@event.id, @event);
        }
    }

    public IEnumerable<(int Id, GuildExp Exp)> ParseGuildExp() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/guildexp.xml"));
        var data = guildExpSerializer.Deserialize(reader) as GuildExpRoot;
        Debug.Assert(data != null);

        foreach (GuildExp exp in data.guildExp) {
            yield return (exp.level, exp);
        }
    }

    public IEnumerable<(int Id, IEnumerable<GuildHouse> Houses)> ParseGuildHouse() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/guildhouse.xml"));
        var data = guildHouseSerializer.Deserialize(reader) as GuildHouseRoot;
        Debug.Assert(data != null);

        foreach (IGrouping<int, GuildHouse> group in data.guildHouse.GroupBy(guildHouse => guildHouse.level)) {
            yield return (group.Key, group);
        }
    }

    public IEnumerable<(GuildNpcType Type, IEnumerable<GuildNpc> Npcs)> ParseGuildNpc() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/guildnpc.xml"));
        var data = guildNpcSerializer.Deserialize(reader) as GuildNpcRoot;
        Debug.Assert(data != null);

        foreach (IGrouping<GuildNpcType, GuildNpc> group in data.npc.GroupBy(guildNpc => guildNpc.type)) {
            yield return (group.Key, group);
        }
    }

    public IEnumerable<(int Id, GuildProperty Property)> ParseGuildProperty() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/guildproperty.xml"));
        var data = guildPropertySerializer.Deserialize(reader) as GuildPropertyRoot;
        Debug.Assert(data != null);

        foreach (GuildProperty property in data.property) {
            yield return (property.level, property);
        }
    }

    public IEnumerable<(int Id, GuildQuestRewardFactor Factor)> ParseGuildQuestRewardFactor() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/guildquestrewardfactor.xml"));
        var data = guildQuestRewardFactorSerializer.Deserialize(reader) as GuildQuestRewardFactorRoot;
        Debug.Assert(data != null);

        foreach (GuildQuestRewardFactor factor in data.guildNpc) {
            yield return (factor.level, factor);
        }
    }

    public IEnumerable<(int Id, InstrumentCategoryInfo Info)> ParseInstrumentCategoryInfo() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/instrumentcategoryinfo.xml"));
        var data = instrumentCategoryInfoSerializer.Deserialize(reader) as InstrumentCategoryInfoRoot;
        Debug.Assert(data != null);

        foreach (InstrumentCategoryInfo instrument in data.category) {
            yield return (instrument.id, instrument);
        }
    }

    public IEnumerable<(int Id, InstrumentInfo Info)> ParseInstrumentInfo() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/instrumentinfo.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = instrumentInfoSerializer.Deserialize(reader) as InstrumentInfoRoot;
        Debug.Assert(data != null);

        foreach (InstrumentInfo instrument in data.instrument) {
            yield return (instrument.id, instrument);
        }
    }

    public IEnumerable<(int Id, string Name, InteractObject Info)> ParseInteractObject() {
        XmlReader nameReader = xmlReader.GetXmlReader(xmlReader.GetEntry($"{language}/interactname.xml"));
        var mapping = nameSerializer.Deserialize(nameReader) as StringMapping;
        Debug.Assert(mapping != null);

        Dictionary<int, string> interactNames = mapping.key.ToDictionary(key => int.Parse(key.id), key => key.name);

        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/interactobject.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = interactObjectSerializer.Deserialize(reader) as InteractObjectRoot;
        Debug.Assert(data != null);

        foreach (InteractObject interact in data.interact) {
            yield return (interact.id, interactNames.GetValueOrDefault(interact.id, string.Empty), interact);
        }
    }

    public IEnumerable<(int Id, InteractObject Info)> ParseInteractObjectMastery() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/interactobject_mastery.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = interactObjectSerializer.Deserialize(reader) as InteractObjectRoot;
        Debug.Assert(data != null);

        foreach (InteractObject interact in data.interact) {
            yield return (interact.id, interact);
        }
    }

    public IEnumerable<(int ItemId, ItemBreakIngredient Item)> ParseItemBreakIngredient() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/itembreakingredient.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = itemBreakIngredientSerializer.Deserialize(reader) as ItemBreakIngredientRoot;
        Debug.Assert(data != null);

        foreach (ItemBreakIngredient item in data.item) {
            yield return (item.ItemID, item);
        }
    }

    public IEnumerable<(int ExchangeId, ItemExchangeScroll ScrollExchange)> ParseItemExchangeScroll() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/itemexchangescrolltable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = itemExchangeScrollSerializer.Deserialize(reader) as ItemExchangeScrollRoot;
        Debug.Assert(data != null);

        foreach (ItemExchangeScroll item in data.exchange) {
            yield return (item.id, item);
        }
    }

    public IEnumerable<(int Id, ItemExtraction Extraction)> ParseItemExtraction() {
        string fileName = $"table/{locale}/itemextraction.xml";
        if (xmlReader.Files.All(f => f.Name != fileName)) {
            fileName = "table/itemextraction.xml";
        }

        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry(fileName)));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = itemExtractionSerializer.Deserialize(reader) as ItemExtractionRoot;
        Debug.Assert(data != null);

        foreach (ItemExtraction item in data.key) {
            yield return (item.TargetItemID, item);
        }
    }

    public IEnumerable<(int ItemId, ItemGemstoneUpgrade Item)> ParseItemGemstoneUpgrade() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/itemgemstoneupgrade.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = itemGemstoneUpgradeSerializer.Deserialize(reader) as ItemGemstoneUpgradeRoot;
        Debug.Assert(data != null);

        foreach (ItemGemstoneUpgrade key in data.key) {
            yield return (key.ItemId, key);
        }
    }

    public IEnumerable<(int ItemId, ItemLapenshardUpgrade Item)> ParseItemLapenshardUpgrade() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/itemlapenshardupgrade.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = itemLapenshardUpgradeSerializer.Deserialize(reader) as ItemLapenshardUpgradeRoot;
        Debug.Assert(data != null);

        foreach (ItemLapenshardUpgrade key in data.key) {
            yield return (key.ItemId, key);
        }
    }

    public IEnumerable<(int Id, ItemRemakeScroll Scroll)> ParseItemRemakeScroll() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/itemremakescroll.xml"));
        var data = itemRemakeScrollSerializer.Deserialize(reader) as ItemRemakeScrollRoot;
        Debug.Assert(data != null);

        foreach (ItemRemakeScroll scroll in data.remakeScroll) {
            yield return (scroll.id, scroll);
        }
    }

    public IEnumerable<(int Id, ItemRepackingScroll Scroll)> ParseItemRepackingScroll() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/itemrepackingscroll.xml"));
        var data = itemRepackingScrollSerializer.Deserialize(reader) as ItemRepackingScrollRoot;
        Debug.Assert(data != null);

        foreach (ItemRepackingScroll scroll in data.rePackingScroll) {
            yield return (scroll.id, scroll);
        }
    }

    public IEnumerable<(int Id, ItemSocket Socket)> ParseItemSocket() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/itemsocket.xml"));
        var data = itemSocketSerializer.Deserialize(reader) as ItemSocketRoot;
        Debug.Assert(data != null);

        foreach (ItemSocket socket in data.itemSocket) {
            yield return (socket.id, socket);
        }
    }

    public IEnumerable<(int Id, ItemSocketScroll Scroll)> ParseItemSocketScroll() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/itemsocketscroll.xml"));
        var data = itemSocketScrollSerializer.Deserialize(reader) as ItemSocketScrollRoot;
        Debug.Assert(data != null);

        foreach (ItemSocketScroll scroll in data.scroll) {
            yield return (scroll.id, scroll);
        }
    }

    public IEnumerable<JobTable> ParseJobTable() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/job.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = jobSerializer.Deserialize(reader) as JobRoot;
        Debug.Assert(data != null);

        foreach (JobTable job in data.job) {
            yield return job;
        }
    }

    public IEnumerable<JobTableNew> ParseJobTableNew() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/job.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = jobNewSerializer.Deserialize(reader) as JobRootNew;
        Debug.Assert(data != null);

        foreach (JobTableNew job in data.job) {
            yield return job;
        }
    }

    public IEnumerable<(long Id, MagicType Type)> ParseMagicPath() {
        string sanitized = Sanitizer.SanitizeMagicPath(xmlReader.GetString(xmlReader.GetEntry("table/magicpath.xml")));
        var data = magicPathSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as MagicPath;
        Debug.Assert(data != null);

        foreach (MagicType type in data.type) {
            yield return (type.id, type);
        }
    }

    public IEnumerable<(int MapId, IEnumerable<MapSpawnTag.Region> Region)> ParseMapSpawnTag() {
        string sanitized = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/mapspawntag.xml")));
        var data = mapSpawnTagSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as MapSpawnTag;
        Debug.Assert(data != null);

        foreach (IGrouping<int, MapSpawnTag.Region> group in data.region.GroupBy(region => region.mapCode)) {
            yield return (group.Key, group);
        }
    }

    public IEnumerable<(int Id, MasteryRecipe Recipe)> ParseMasteryRecipe() {
        string sanitized = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/masteryreceipe.xml")));
        var data = masteryRecipeSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as MasteryRecipeRoot;
        Debug.Assert(data != null);

        foreach (MasteryRecipe recipe in data.receipe) {
            yield return (recipe.id, recipe);
        }
    }

    public IEnumerable<(MasteryType Type, MasteryReward Reward)> ParseMasteryReward() {
        string sanitized = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/mastery.xml")));
        var data = masteryRewardSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as MasteryRewardRoot;
        Debug.Assert(data != null);

        foreach (MasteryReward reward in data.mastery) {
            yield return (reward.type, reward);
        }
    }

    public IEnumerable<(short Level, PetExp Exp)> ParsePetExp() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/petexp.xml"));
        var data = petExpSerializer.Deserialize(reader) as PetExpRoot;
        Debug.Assert(data != null);

        foreach (PetExp exp in data.exp) {
            yield return (exp.level, exp);
        }
    }

    public IEnumerable<(short RequireItemLevel, PetEvolution Evolution)> ParsePetEvolution() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/petevolution.xml"));
        var data = petEvolutionSerializer.Deserialize(reader) as PetEvolutionRoot;
        Debug.Assert(data != null);

        foreach (PetEvolution evolution in data.petEvolution) {
            yield return (evolution.requireItemLevel, evolution);
        }
    }

    public IEnumerable<(int Id, PetProperty Property)> ParsePetProperty() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/petproperty.xml"));
        var data = petPropertySerializer.Deserialize(reader) as PetPropertyRoot;
        Debug.Assert(data != null);

        foreach (PetProperty property in data.pets) {
            yield return (property.code, property);
        }
    }

    public IEnumerable<(int FieldId, IEnumerable<PetSpawnInfo> Info)> ParsePetSpawnInfo() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/petspawninfo.xml"));
        var data = petSpawnInfoSerializer.Deserialize(reader) as PetSpawnInfoRoot;
        Debug.Assert(data != null);

        foreach (IGrouping<int, PetSpawnInfo> group in data.SpawnInfo.GroupBy(spawnInfo => spawnInfo.fieldID)) {
            yield return (group.Key, group);
        }
    }

    public IEnumerable<(int Id, PremiumClubEffect Effect)> ParsePremiumClubEffect() {
        string sanitized = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/vipbenefiteffecttable.xml")));
        var data = premiumClubEffectSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as PremiumClubEffectRoot;
        Debug.Assert(data != null);

        foreach (PremiumClubEffect effect in data.benefit) {
            yield return (effect.effectID, effect);
        }
    }

    public IEnumerable<(int Id, PremiumClubItem Item)> ParsePremiumClubItem() {
        string sanitized = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/vipbenefititemtable.xml")));
        var data = premiumClubItemSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as PremiumClubItemRoot;
        Debug.Assert(data != null);

        foreach (PremiumClubItem item in data.benefit) {
            yield return (item.id, item);
        }
    }

    public IEnumerable<(int Id, PremiumClubPackage Package)> ParsePremiumClubPackage() {
        string sanitized = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/vipgoodstable.xml")));
        var data = premiumClubPackageSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as PremiumClubPackageRoot;
        Debug.Assert(data != null);

        foreach (PremiumClubPackage package in data.goods) {
            yield return (package.id, package);
        }
    }

    public IEnumerable<(int Id, string Name, SetItemInfo Info)> ParseSetItemInfo() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry($"{language}/setitemname.xml"));
        var mapping = nameSerializer.Deserialize(reader) as StringMapping;
        Debug.Assert(mapping != null);

        Dictionary<int, string> setNames = mapping.key.ToDictionary(key => int.Parse(key.id), key => key.name);

        string sanitized = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/setiteminfo.xml")));
        var data = setItemInfoSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as SetItemInfoRoot;
        Debug.Assert(data != null);

        foreach (SetItemInfo info in data.set) {
            yield return (info.id, setNames.GetValueOrDefault(info.id, string.Empty), info);
        }
    }

    public IEnumerable<(int Id, SetItemOption Option)> ParseSetItemOption() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/setitemoption.xml"));
        var data = setItemOptionSerializer.Deserialize(reader) as SetItemOptionRoot;
        Debug.Assert(data != null);

        foreach (SetItemOption option in data.option) {
            yield return (option.id, option);
        }
    }

    public IEnumerable<(int Id, string Name, SetItemOptionNew Option)> ParseSetItemOptionNew() {
        XmlReader nameXmlReader = xmlReader.GetXmlReader(xmlReader.GetEntry($"{language}/setitemname.xml"));
        var mapping = nameSerializer.Deserialize(nameXmlReader) as StringMapping;
        Debug.Assert(mapping != null);

        Dictionary<int, string> setNames = mapping.key.ToDictionary(key => int.Parse(key.id), key => key.name);

        IO.Crypto.Common.PackFileEntry entry = xmlReader.GetEntry("table/setiteminfo.xml");
        XmlReader reader = xmlReader.GetXmlReader(entry);
        var data = setItemOptionNewSerializer.Deserialize(reader) as SetItemOptionRootNew;
        Debug.Assert(data != null);

        foreach (SetItemOptionNew option in data.option) {
            yield return (option.id, setNames.GetValueOrDefault(option.id, string.Empty), option);
        }
    }

    public IEnumerable<(int Id, string Name, TitleTag TitleTag)> ParseTitleTag() {
        XmlReader nameReader = xmlReader.GetXmlReader(xmlReader.GetEntry("en/titlename.xml"));
        var mapping = nameSerializer.Deserialize(nameReader) as StringMapping;
        Debug.Assert(mapping != null);

        Dictionary<int, string> titleNames = mapping.key.ToDictionary(key => int.Parse(key.id), key => key.name);

        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/titletag.xml"));
        var data = titleTagSerializer.Deserialize(reader) as TitleTagRoot;
        Debug.Assert(data != null);

        foreach (TitleTag title in data.key) {
            yield return (title.id, titleNames.GetValueOrDefault(title.id, string.Empty), title);
        }
    }

    // One entry per title id that survives the feature and locale filter, which is also the set the
    // client can name. A title missing here shows no row in the client's title dropdown.
    public IEnumerable<(int Id, string Name)> ParseTitleName() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry($"{language}/titlename.xml"));
        var mapping = nameSerializer.Deserialize(reader) as StringMapping;
        Debug.Assert(mapping != null);

        foreach (Key key in mapping.key) {
            yield return (int.Parse(key.id), key.name);
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDrop>>)> ParseIndividualItemDrop() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/individualitemdrop.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropSerializer.Deserialize(reader) as IndividualItemDropRoot;
        Debug.Assert(data != null);
        var groups = data.individualDropBox.GroupBy(individualItemDrop => new {
            individualItemDrop.individualDropBoxID,
            individualItemDrop.dropGroup
        })
            .ToDictionary(group => group.Key, group => group.ToList());
        foreach (var group in groups) {
            yield return (group.Key.individualDropBoxID, group.Value.GroupBy(drop => drop.dropGroup).ToDictionary(drop => drop.Key, drop => drop.ToList()));
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDropItem>>)> ParseIndividualItemDropFinal() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/individualitemdrop_final.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropNewSerializer.Deserialize(reader) as IndividualItemDropRootNew;
        Debug.Assert(data != null);

        foreach (IndividualItemDropBox dropBox in data.DropBoxes) {
            yield return (dropBox.DropBoxID, dropBox.Groups
                .GroupBy(g => (byte) g.DropGroupID)
                .ToDictionary(
                    g => g.Key,
                    g => g.SelectMany(grp => grp.Items).ToList()
                ));
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDrop>>)> ParseIndividualItemDropCharge() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/individualitemdrop_charge.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropSerializer.Deserialize(reader) as IndividualItemDropRoot;
        Debug.Assert(data != null);
        var groups = data.individualDropBox.GroupBy(dropbox => new {
            dropbox.individualDropBoxID,
            dropbox.dropGroup
        })
            .ToDictionary(group => group.Key, group => group.ToList());
        foreach (var group in groups) {
            yield return (group.Key.individualDropBoxID, group.Value.GroupBy(drop => drop.dropGroup).ToDictionary(drop => drop.Key, drop => drop.ToList()));
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDrop>>)> ParseIndividualItemDropEvent() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/individualitemdrop_event.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropSerializer.Deserialize(reader) as IndividualItemDropRoot;
        Debug.Assert(data != null);
        var groups = data.individualDropBox.GroupBy(dropbox => new {
            dropbox.individualDropBoxID,
            dropbox.dropGroup
        })
            .ToDictionary(group => group.Key, group => group.ToList());
        foreach (var group in groups) {
            yield return (group.Key.individualDropBoxID, group.Value.GroupBy(drop => drop.dropGroup).ToDictionary(drop => drop.Key, drop => drop.ToList()));
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDrop>>)> ParseIndividualItemDropEventNpc() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/individualitemdrop_eventnpc.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropSerializer.Deserialize(reader) as IndividualItemDropRoot;
        Debug.Assert(data != null);
        var groups = data.individualDropBox.GroupBy(dropbox => new {
            dropbox.individualDropBoxID,
            dropbox.dropGroup
        })
            .ToDictionary(group => group.Key, group => group.ToList());
        foreach (var group in groups) {
            yield return (group.Key.individualDropBoxID, group.Value.GroupBy(drop => drop.dropGroup).ToDictionary(drop => drop.Key, drop => drop.ToList()));
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDrop>>)> ParseIndividualItemDropNewGacha() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/individualitemdrop_newgacha.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropSerializer.Deserialize(reader) as IndividualItemDropRoot;
        Debug.Assert(data != null);
        var groups = data.individualDropBox.GroupBy(dropbox => new {
            dropbox.individualDropBoxID,
            dropbox.dropGroup
        })
            .ToDictionary(group => group.Key, group => group.ToList());
        foreach (var group in groups) {
            yield return (group.Key.individualDropBoxID, group.Value.GroupBy(drop => drop.dropGroup).ToDictionary(drop => drop.Key, drop => drop.ToList()));
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDrop>>)> ParseIndividualItemDropPet() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/individualitemdrop_pet.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropSerializer.Deserialize(reader) as IndividualItemDropRoot;
        Debug.Assert(data != null);
        var groups = data.individualDropBox.GroupBy(dropbox => new {
            dropbox.individualDropBoxID,
            dropbox.dropGroup
        })
            .ToDictionary(group => group.Key, group => group.ToList());
        foreach (var group in groups) {
            yield return (group.Key.individualDropBoxID, group.Value.GroupBy(drop => drop.dropGroup).ToDictionary(drop => drop.Key, drop => drop.ToList()));
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDrop>>)> ParseIndividualItemDropQuestObj() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/individualitemdrop_quest_obj.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropSerializer.Deserialize(reader) as IndividualItemDropRoot;
        Debug.Assert(data != null);
        var groups = data.individualDropBox.GroupBy(dropbox => new {
            dropbox.individualDropBoxID,
            dropbox.dropGroup
        })
            .ToDictionary(group => group.Key, group => group.ToList());
        foreach (var group in groups) {
            yield return (group.Key.individualDropBoxID, group.Value.GroupBy(drop => drop.dropGroup).ToDictionary(drop => drop.Key, drop => drop.ToList()));
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDrop>>)> ParseIndividualItemDropQuestMob() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/individualitemdrop_quest_mob.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropSerializer.Deserialize(reader) as IndividualItemDropRoot;
        Debug.Assert(data != null);
        var groups = data.individualDropBox.GroupBy(dropbox => new {
            dropbox.individualDropBoxID,
            dropbox.dropGroup
        })
            .ToDictionary(group => group.Key, group => group.ToList());
        foreach (var group in groups) {
            yield return (group.Key.individualDropBoxID, group.Value.GroupBy(drop => drop.dropGroup).ToDictionary(drop => drop.Key, drop => drop.ToList()));
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDrop>>)> ParseIndividualItemDropGacha() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/individualitemdrop_gacha.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropSerializer.Deserialize(reader) as IndividualItemDropRoot;
        Debug.Assert(data != null);
        var groups = data.individualDropBox.GroupBy(dropbox => new {
            dropbox.individualDropBoxID,
            dropbox.dropGroup
        })
            .ToDictionary(group => group.Key, group => group.ToList());
        foreach (var group in groups) {
            yield return (group.Key.individualDropBoxID, group.Value.GroupBy(drop => drop.dropGroup).ToDictionary(drop => drop.Key, drop => drop.ToList()));
        }
    }

    public IEnumerable<(int Id, IDictionary<byte, List<IndividualItemDrop>>)> ParseIndividualItemGearBox() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/individualitemdrop_gearbox.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = individualItemDropSerializer.Deserialize(reader) as IndividualItemDropRoot;
        Debug.Assert(data != null);
        var groups = data.individualDropBox.GroupBy(dropbox => new {
            dropbox.individualDropBoxID,
            dropbox.dropGroup
        })
            .ToDictionary(group => group.Key, group => group.ToList());
        foreach (var group in groups) {
            yield return (group.Key.individualDropBoxID, group.Value.GroupBy(drop => drop.dropGroup).ToDictionary(drop => drop.Key, drop => drop.ToList()));
        }
    }

    public IEnumerable<(int Id, GachaInfo Info)> ParseGachaInfo() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/gacha_info.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = gachaInfoSerializer.Deserialize(reader) as GachaInfoRoot;
        Debug.Assert(data != null);

        foreach (GachaInfo gachaInfo in data.randomBox) {
            yield return (gachaInfo.randomBoxID, gachaInfo);
        }
    }

    public IEnumerable<(int ShopId, ShopBeautyCoupon Item)> ParseShopBeautyCoupon() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/shop_beautycoupon.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = shopBeautyCouponSerializer.Deserialize(reader) as ShopBeautyCouponRoot;
        Debug.Assert(data != null);

        foreach (ShopBeautyCoupon coupon in data.shop) {
            yield return (coupon.shopID, coupon);
        }
    }


    public IEnumerable<(int ItemId, ShopFurnishing UgcItem)> ParseFurnishingShopUgcAll() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/shop_ugcall.xml")));

        var reader = XmlReader.Create(new StringReader(xml));
        var data = shopFurnishingSerializer.Deserialize(reader) as ShopFurnishingRoot;
        Debug.Assert(data != null);

        foreach (ShopFurnishing shop in data.key) {
            yield return (shop.id, shop);
        }
    }

    public IEnumerable<(int ItemId, ShopFurnishing UgcItem)> ParseFurnishingShopMaid() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/shop_maid.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = shopFurnishingSerializer.Deserialize(reader) as ShopFurnishingRoot;
        Debug.Assert(data != null);

        foreach (ShopFurnishing shop in data.key) {
            yield return (shop.id, shop);
        }
    }

    public IEnumerable<(int CategoryId, MeretMarketCategory Category)> ParseMeretMarketCategory() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/meratmarketcategory.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = meretmarketCategorySerializer.Deserialize(reader) as MeretMarketCategoryRoot;
        Debug.Assert(data != null);

        IDictionary<int, MeretMarketCategory> categories = new Dictionary<int, MeretMarketCategory>();
        foreach (MeretMarketCategory category in data.category) {
            categories.Add(category.id, category);
        }

        // Replace any existing categories with the ones under environment.
        foreach (MeretMarketEnvironment environment in data.environment) {
            foreach (MeretMarketCategory category in environment.category) {
                categories[category.id] = category;
            }
        }

        foreach (MeretMarketCategory category in categories.Values) {
            yield return (category.id, category);
        }
    }

    public IEnumerable<(int TableId, ExpBaseTable Table)> ParseExpBaseTable() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/expbasetable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = expBaseSerializer.Deserialize(reader) as ExpBaseRoot;
        Debug.Assert(data != null);

        foreach (ExpBaseTable table in data.table) {
            yield return (table.expTableID, table);
        }
    }

    public IEnumerable<(int Level, NextExp NextExp)> ParseNextExp() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/nextexp.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = nextExpSerializer.Deserialize(reader) as NextExpRoot;
        Debug.Assert(data != null);

        foreach (NextExp entry in data.exp) {
            yield return (entry.level, entry);
        }
    }

    public IEnumerable<(int Level, AdventureLevelAbility Ability)> ParseAdventureLevelAbility() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/adventurelevelability.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = adventureLevelAbilitySerializer.Deserialize(reader) as AdventureLevelAbilityRoot;
        Debug.Assert(data != null);

        foreach (AdventureLevelAbility entry in data.ability) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Id, AdventureLevelMission Mission)> ParseAdventureLevelMission() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/adventurelevelmission.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = adventureLevelMissionSerializer.Deserialize(reader) as AdventureLevelMissionRoot;
        Debug.Assert(data != null);

        foreach (AdventureLevelMission entry in data.mission) {
            yield return (entry.missionId, entry);
        }
    }

    public IEnumerable<(int Id, AdventureLevelReward Reward)> ParseAdventureLevelReward() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/adventurelevelreward.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = adventureLevelRewardSerializer.Deserialize(reader) as AdventureLevelRewardRoot;
        Debug.Assert(data != null);

        foreach (AdventureLevelReward entry in data.reward) {
            yield return (entry.level, entry);
        }
    }

    public IEnumerable<(int Id, UgcDesign Design)> ParseUgcDesign() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/ugcdesign.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = ugcDesignSerializer.Deserialize(reader) as UgcDesignRoot;
        Debug.Assert(data != null);

        foreach (UgcDesign entry in data.list) {
            yield return (entry.itemID, entry);
        }
    }

    public IEnumerable<(int Id, MasteryUgcHousing)> ParseMasteryUgcHousing() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/masteryugchousing.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = masteryUgcHousingSerializer.Deserialize(reader) as MasteryUgcHousingRoot;
        Debug.Assert(data != null);

        foreach (MasteryUgcHousing entry in data.MasteryUgcHousing.Entries) {
            yield return (entry.grade, entry);
        }
    }

    public IEnumerable<(int Id, UgcHousingPointReward)> ParseUgcHousingPointReward() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/ugchousingpointreward.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = ugcHousingPointRewardSerializer.Deserialize(reader) as UgcHousingPointRewardRoot;
        Debug.Assert(data != null);

        foreach (UgcHousingPointReward entry in data.Entries) {
            yield return (entry.housingPoint, entry);
        }
    }

    public IEnumerable<(int Id, Banner Banner)> ParseBanner() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/banner.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = bannerSerializer.Deserialize(reader) as BannerRoot;
        Debug.Assert(data != null);

        foreach (Banner entry in data.banner) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Id, NameTagSymbol Symbol)> ParseNameTagSymbol() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/nametagsymbol.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = nameTagSymbolSerializer.Deserialize(reader) as NameTagSymbolRoot;
        Debug.Assert(data != null);

        foreach (NameTagSymbol entry in data.symbol) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(CommonExpType Type, CommonExp Exp)> ParseCommonExp() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/commonexptable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = commonExpSerializer.Deserialize(reader) as CommonExpRoot;
        Debug.Assert(data != null);

        foreach (CommonExp entry in data.exp) {
            yield return (entry.expType, entry);
        }
    }

    public IEnumerable<(int Id, ChapterBook Book)> ParseChapterBook() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/chapterbook.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = chapterBookSerializer.Deserialize(reader) as ChapterBookRoot;

        Debug.Assert(data != null);

        foreach (ChapterBook entry in data.chapterbook) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Id, LearningQuest Quest)> ParseLearningQuest() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/learningquest.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = learningQuestSerializer.Deserialize(reader) as LearningQuestRoot;

        Debug.Assert(data != null);

        foreach (LearningQuest entry in data.learning) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Differential, MasteryDifferentialFactor Factor)> ParseMasteryDifferentialFactor() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/masterydifferentialfactor.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = masteryDifferentialFactorSerializer.Deserialize(reader) as MasteryDifferentialFactorRoot;

        Debug.Assert(data != null);

        foreach (MasteryDifferentialFactor entry in data.v) {
            yield return (entry.differential, entry);
        }
    }

    public IEnumerable<(int Id, RewardContent Reward)> ParseRewardContent() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/rewardcontent.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = rewardContentSerializer.Deserialize(reader) as RewardContentRoot;

        Debug.Assert(data != null);

        foreach (RewardContent entry in data.v) {
            yield return (entry.rewardID, entry);
        }
    }

    public IEnumerable<(int Id, RewardContentItem Reward)> ParseRewardContentItem() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/rewardcontentitemtable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = rewardContentItemSerializer.Deserialize(reader) as RewardContentItemRoot;

        Debug.Assert(data != null);

        foreach (RewardContentItem entry in data.table) {
            yield return (entry.itemTableID, entry);
        }
    }

    public IEnumerable<(int Id, RewardContentExpStatic Reward)> ParseRewardContentExpStatic() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/rewardcontentexpstatictable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = rewardContentExpStaticSerializer.Deserialize(reader) as RewardContentExpStaticRoot;

        Debug.Assert(data != null);

        foreach (RewardContentExpStatic entry in data.table) {
            yield return (entry.expTableID, entry);
        }
    }

    public IEnumerable<(int Id, RewardContentMeso Reward)> ParseRewardContentMeso() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/rewardcontentmesotable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = rewardContentMesoSerializer.Deserialize(reader) as RewardContentMesoRoot;

        Debug.Assert(data != null);

        foreach (RewardContentMeso entry in data.table) {
            yield return (entry.mesoTableID, entry);
        }
    }

    public IEnumerable<(int Id, RewardContentMesoStatic Reward)> ParseRewardContentMesoStatic() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/rewardcontentmesostatictable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = rewardContentMesoStaticSerializer.Deserialize(reader) as RewardContentMesoStaticRoot;

        Debug.Assert(data != null);

        foreach (RewardContentMesoStatic entry in data.table) {
            yield return (entry.mesoTableID, entry);
        }
    }

    public IEnumerable<(int Level, SurvivalLevel Entry)> ParseSurvivalLevel() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/survivallevel.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = survivalLevelSerializer.Deserialize(reader) as SurvivalLevelRoot;

        Debug.Assert(data != null);

        foreach (SurvivalLevel entry in data.survivalLevelExp) {
            yield return (entry.level, entry);
        }
    }

    public IEnumerable<(int Level, SurvivalLevelReward Reward)> ParseSurvivalLevelReward() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/survivallevelreward.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = survivalLevelRewardSerializer.Deserialize(reader) as SurvivalLevelRewardRoot;

        Debug.Assert(data != null);

        foreach (SurvivalLevelReward entry in data.survivalLevelReward) {
            yield return (entry.level, entry);
        }
    }

    public IEnumerable<(int Id, BlackMarketStatTable StatTable)> ParseBlackMarketStatTable() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/blackmarkettable.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = blackMarketSerializer.Deserialize(reader) as BlackMarketRoot;
        Debug.Assert(data != null);

        foreach (BlackMarketStatTable entry in data.stat_table) {
            yield return (entry.id, entry);
        }
    }

    public (int, BlackMarketOption) ParseBlackMarketOption() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/blackmarkettable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = blackMarketSerializer.Deserialize(reader) as BlackMarketRoot;
        Debug.Assert(data != null);

        return (data.option.id, data.option);
    }

    public (int, BlackMarketCategory) ParseBlackMarketCategory() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/blackmarkettable.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = blackMarketSerializer.Deserialize(reader) as BlackMarketRoot;
        Debug.Assert(data != null);

        return (data.category.id, data.category);
    }

    public IEnumerable<(int JobCode, ChangeJob Job)> ParseChangeJob() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/changejob.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = changeJobSerializer.Deserialize(reader) as ChangeJobRoot;
        Debug.Assert(data != null);

        foreach (ChangeJob entry in data.subjob) {
            yield return (entry.subJobCode, entry);
        }
    }

    public IEnumerable<(int Id, FieldMission Mission)> ParseFieldMission() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/fieldmission.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = fieldMissionSerializer.Deserialize(reader) as FieldMissionRoot;
        Debug.Assert(data != null);

        foreach (FieldMission entry in data.round) {
            yield return (entry.mission, entry);
        }
    }

    public IEnumerable<(int Id, FieldPortal Portal)> ParseFieldMetaData() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/fieldmetadata.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = fieldMetaDataSerializer.Deserialize(reader) as FieldMetaDataRoot;
        Debug.Assert(data != null);

        foreach (FieldPortal entry in data.fieldPortal) {
            yield return (entry.field, entry);
        }
    }

    public IEnumerable<(string Feature, IList<WorldMap.Map> Maps)> ParseWorldMap() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/newworldmap.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = worldMapSerializer.Deserialize(reader) as WorldMapRoot;
        Debug.Assert(data != null);

        foreach (WorldMap entry in data.environment) {
            yield return (entry.Feature, entry.map);
        }
    }

    public IEnumerable<(int Id, MapleSurvivalSkinInfo Info)> ParseMapleSurvivalSkinInfo() {
        string fileName = $"table/{locale}/maplesurvivalskininfo.xml";
        if (xmlReader.Files.All(f => f.Name != fileName)) {
            fileName = "table/maplesurvivalskininfo.xml";
            if (xmlReader.Files.All(f => f.Name != fileName)) {
                yield break; // Return empty if no matching file exists
            }
        }
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry(fileName)));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = mapleSurvivalSkinInfoSerializer.Deserialize(reader) as MapleSurvivalSkinInfoRoot;
        Debug.Assert(data != null);

        foreach (MapleSurvivalSkinInfo entry in data.skinInfo) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Grade, WeddingExp Exp)> ParseWeddingExp() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/weddingexp.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = weddingExpSerializer.Deserialize(reader) as WeddingExpRoot;
        Debug.Assert(data != null);

        foreach (WeddingExp entry in data.v) {
            yield return (entry.grade, entry);
        }
    }

    public IEnumerable<(int Grade, WeddingPackage Package)> ParseWeddingPackage() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/weddingpackage.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = weddingPackageSerializer.Deserialize(reader) as WeddingPackageRoot;
        Debug.Assert(data != null);

        foreach (WeddingPackage entry in data.package) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(WeddingRewardType Type, WeddingReward Reward)> ParseWeddingReward() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/weddingreward.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = weddingRewardSerializer.Deserialize(reader) as WeddingRewardRoot;
        Debug.Assert(data != null);

        foreach (WeddingReward entry in data.v) {
            yield return (entry.type, entry);
        }
    }

    public IEnumerable<(int Id, WeddingSkill Reward)> ParseWeddingSkill() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/weddingskill.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = weddingSkillSerializer.Deserialize(reader) as WeddingSkillRoot;
        Debug.Assert(data != null);

        foreach (WeddingSkill entry in data.v) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Id, SmartPush Button)> ParseSmartPush() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/smartpush.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = smartPushSerializer.Deserialize(reader) as SmartPushRoot;
        Debug.Assert(data != null);

        foreach (SmartPush entry in data.push) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Id, SeasonData Data)> ParseSeasonDataArcade() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/seasondata_arcade.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = seasonDataSerializer.Deserialize(reader) as SeasonDataRoot;
        Debug.Assert(data != null);

        foreach (SeasonData entry in data.Season) {
            yield return (entry.seasonID, entry);
        }
    }

    public IEnumerable<(int Id, SeasonData Data)> ParseSeasonDataBossColosseum() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/seasondata_bosscolosseum.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = seasonDataSerializer.Deserialize(reader) as SeasonDataRoot;
        Debug.Assert(data != null);

        foreach (SeasonData entry in data.Season) {
            yield return (entry.seasonID, entry);
        }
    }

    public IEnumerable<(int Id, SeasonData Data)> ParseSeasonDataDarkStream() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/seasondata_darkstream.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = seasonDataSerializer.Deserialize(reader) as SeasonDataRoot;
        Debug.Assert(data != null);

        foreach (SeasonData entry in data.Season) {
            yield return (entry.seasonID, entry);
        }
    }

    public IEnumerable<(int Id, SeasonData Data)> ParseSeasonDataGuildPvp() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/seasondata_guildpvp.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = seasonDataSerializer.Deserialize(reader) as SeasonDataRoot;
        Debug.Assert(data != null);

        foreach (SeasonData entry in data.Season) {
            yield return (entry.seasonID, entry);
        }
    }

    public IEnumerable<(int Id, SeasonData Data)> ParseSeasonDataMapleSurvival() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/seasondata_maplesurvival.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = seasonDataSerializer.Deserialize(reader) as SeasonDataRoot;
        Debug.Assert(data != null);

        foreach (SeasonData entry in data.Season) {
            yield return (entry.seasonID, entry);
        }
    }

    public IEnumerable<(int Id, SeasonData Data)> ParseSeasonDataMapleSurvivalSquad() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/seasondata_maplesurvival_squad.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = seasonDataSerializer.Deserialize(reader) as SeasonDataRoot;
        Debug.Assert(data != null);

        foreach (SeasonData entry in data.Season) {
            yield return (entry.seasonID, entry);
        }
    }

    public IEnumerable<(int Id, SeasonData Data)> ParseSeasonDataPvp() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/seasondata_pvp.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = seasonDataSerializer.Deserialize(reader) as SeasonDataRoot;
        Debug.Assert(data != null);

        foreach (SeasonData entry in data.Season) {
            yield return (entry.seasonID, entry);
        }
    }

    public IEnumerable<(int Id, SeasonData Data)> ParseSeasonDataUgcMapCommendation() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/seasondata_ugcmapcommendation.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = seasonDataSerializer.Deserialize(reader) as SeasonDataRoot;
        Debug.Assert(data != null);

        foreach (SeasonData entry in data.Season) {
            yield return (entry.seasonID, entry);
        }
    }

    public IEnumerable<(int Id, SeasonData Data)> ParseSeasonDataWorldChampion() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/seasondata_worldchampion.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = seasonDataSerializer.Deserialize(reader) as SeasonDataRoot;
        Debug.Assert(data != null);

        foreach (SeasonData entry in data.Season) {
            yield return (entry.seasonID, entry);
        }
    }

    public IEnumerable<(int Id, StatString Data)> ParseStatString() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/statstringtable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = statStringSerializer.Deserialize(reader) as StatStringRoot;
        Debug.Assert(data != null);

        foreach (StatString entry in data.key) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Id, StatString Data)> ParseSpecialAbilityString() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/specialabilitystringtable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = statStringSerializer.Deserialize(reader) as StatStringRoot;
        Debug.Assert(data != null);

        foreach (StatString entry in data.key) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Id, AutoActionPricePackage Data)> ParseAutoActionPricePackage() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/autoactionpricepackage.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = autoActionPricePackageSerializer.Deserialize(reader) as AutoActionPricePackageRoot;
        Debug.Assert(data != null);

        foreach (AutoActionPricePackage entry in data.package) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Grade, PvpRankingDuelMode Data)> ParsePvpRankingDuelMode() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/pvprankingduelmode.xml"));
        var data = pvpRankingDuelModeSerializer.Deserialize(reader) as PvpRankingDuelModeRoot;
        Debug.Assert(data != null);

        foreach (PvpRankingDuelMode entry in data.gradeInfo) {
            yield return (entry.grade, entry);
        }
    }

    public IEnumerable<(int Id, QuestGroup Data)> ParseQuestGroup() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/questgrouptable.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = questGroupSerializer.Deserialize(reader) as QuestGroupRoot;
        Debug.Assert(data != null);

        foreach (QuestGroup entry in data.group) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Round, DarkStreamReward Reward)> ParseDarkStream() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/darkstream.xml"));
        var data = darkStreamSerializer.Deserialize(reader) as DarkStreamRoot;
        Debug.Assert(data != null);

        foreach (DarkStreamReward entry in data.reward) {
            yield return (entry.round, entry);
        }
    }

    public IEnumerable<(int Id, ClubBuff Buff)> ParseClubBuff() {
        string xml = Sanitizer.RemoveSpaces(xmlReader.GetString(xmlReader.GetEntry("table/clubbuff.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = clubBuffSerializer.Deserialize(reader) as ClubBuffRoot;
        Debug.Assert(data != null);

        foreach (ClubBuff entry in data.clubBuff) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Id, DungeonRewardCoupon Coupon)> ParseDungeonRewardCoupon() {
        string xml = Sanitizer.RemoveSpaces(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/dungeonrewardcoupon.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = dungeonRewardCouponSerializer.Deserialize(reader) as DungeonRewardCouponRoot;
        Debug.Assert(data != null);

        foreach (DungeonRewardCoupon entry in data.dungeonRewardCoupon) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(string Name, CharacterCreateSelectGroup Group)> ParseCharacterCreateSelect() {
        string xml = Sanitizer.RemoveSpaces(xmlReader.GetString(xmlReader.GetEntry($"table/charactercreateselect.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = characterCreateSelectSerializer.Deserialize(reader) as CharacterCreateSelect;
        Debug.Assert(data != null);

        foreach (CharacterCreateSelectGroup entry in data.group) {
            yield return (entry.name, entry);
        }
    }

    public IEnumerable<(string Alliance, int Grade, Fame Fame)> ParseFame() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/fame.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = fameSerializer.Deserialize(reader) as FameRoot;
        Debug.Assert(data != null);

        foreach (Fame entry in data.fame) {
            yield return (entry.alliance, entry.grade, entry);
        }
    }

    public IEnumerable<(string Name, FameSeason Fame)> ParseFameDef() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/famedef.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = fameDefSerializer.Deserialize(reader) as FameDef;
        Debug.Assert(data != null);

        foreach (FameSeason entry in data.season) {
            yield return (entry.name, entry);
        }
    }

    public IEnumerable<(string Alliance, FameLimit Fame)> ParseFameLimit() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/famelimit.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = fameLimitSerializer.Deserialize(reader) as FameLimitRoot;
        Debug.Assert(data != null);

        foreach (FameLimit entry in data.condition) {
            yield return (entry.alliance, entry);
        }
    }

    public IEnumerable<(int Id, FameLog Fame)> ParseFameLog() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/famelog.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = fameLogSerializer.Deserialize(reader) as FameLogRoot;
        Debug.Assert(data != null);

        foreach (FameLog entry in data.fame) {
            yield return (entry.id, entry);
        }
    }


    public IEnumerable<(string Type, int RepeatType, FamePickMethod.PickMethod PickMethod)> ParseFamePickMethod() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/famepickmethod.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = famePickMethodSerializer.Deserialize(reader) as FamePickMethod;
        Debug.Assert(data != null);

        FamePickMethod.Environment environment = data.environment.ResolveFeatureLocale();
        if (environment == null) {
            yield break;
        }

        foreach (FamePickMethod.PickMethod pickMethod in environment.method) {
            yield return (pickMethod.type, pickMethod.repeatType, pickMethod);
        }
    }

    public IEnumerable<(int Id, CharacterAbility Ability)> ParseCharacterAbility() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/characterability.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = characterAbilitySerializer.Deserialize(reader) as CharacterAbilityRoot;
        Debug.Assert(data != null);

        foreach (CharacterAbility entry in data.ability) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Level, MaidExp Exp)> ParseMaidExp() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/maidexp.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = maidExpSerializer.Deserialize(reader) as MaidExpRoot;
        Debug.Assert(data != null);

        foreach (MaidExp entry in data.Exp) {
            yield return (entry.Level, entry);
        }
    }

    public IEnumerable<(int MaidId, MaidProperty Property)> ParseMaidProperty() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/maidproperty.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = maidPropertySerializer.Deserialize(reader) as MaidPropertyRoot;
        Debug.Assert(data != null);

        foreach (MaidProperty entry in data.Property) {
            yield return (entry.MaidID, entry);
        }
    }

    public IEnumerable<(int Id, MaidRecipe Recipe)> ParseMaidRecipe() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/maidrecipe.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = maidRecipeSerializer.Deserialize(reader) as MaidRecipeRoot;
        Debug.Assert(data != null);

        foreach (MaidRecipe entry in data.recipe) {
            yield return (entry.Id, entry);
        }
    }

    public IEnumerable<(int GroupId, MaidRecipeGroup Group)> ParseMaidRecipeGroup() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/maidrecipegroup.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = maidRecipeGroupSerializer.Deserialize(reader) as MaidRecipeGroupRoot;
        Debug.Assert(data != null);

        foreach (MaidRecipeGroup entry in data.group) {
            yield return (entry.GroupID, entry);
        }
    }

    public IEnumerable<(int Id, MaidSalary Salary)> ParseMaidSalary() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry($"table/{locale}/maidsalary.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = maidSalarySerializer.Deserialize(reader) as MaidSalaryRoot;
        Debug.Assert(data != null);

        foreach (MaidSalary entry in data.key) {
            yield return (entry.id, entry);
        }
    }

    public IEnumerable<(int Id, Reactor Reactor)> ParseReactor() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/reactor.xml"));
        var data = reactorSerializer.Deserialize(reader) as ReactorRoot;
        Debug.Assert(data != null);

        foreach (Reactor reactor in data.reactor) {
            yield return (reactor.id, reactor);
        }
    }

    public IEnumerable<(int Id, ReactorState State)> ParseReactorState() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/reactorstate.xml"));
        var data = reactorStateSerializer.Deserialize(reader) as ReactorStateRoot;
        Debug.Assert(data != null);

        foreach (ReactorState state in data.reactorState) {
            yield return (state.id, state);
        }
    }
}
