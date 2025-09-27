using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.Gear;

namespace DLC_ChooseStartingGear_DLC
{
    // Clothing
    public enum RandomHeadOuter
    {
        GEAR_BaseballCap, GEAR_CottonScarf, GEAR_BasicWoolHat, GEAR_WoolWrapCap, GEAR_WoolWrap, GEAR_RabbitskinHat, GEAR_BasicWoolScarf, GEAR_Toque,
        GEAR_CougarWrap, GEAR_MinersHelmet, GEAR_WolfSkinHat, GEAR_wolfscarf, GEAR_WolfskinCap_MOD
    }
    public enum RandomHeadInner
    {
        GEAR_Balaclava, GEAR_BaseballCap, GEAR_CottonScarf, GEAR_BasicWoolHat, GEAR_WoolWrapCap, GEAR_WoolWrap, GEAR_BasicWoolScarf, GEAR_Toque,
        GEAR_MinersHelmet, GEAR_HatGatorBalaclavaA
    }
    public enum RandomTorsoOuter
    {
        GEAR_BearSkinCoat, GEAR_DownVest, GEAR_PremiumWinterCoat, GEAR_SkiJacket, GEAR_MackinawJacket, GEAR_QualityWinterCoat, GEAR_MilitaryParka, GEAR_MooseHideCloak,
        GEAR_HeavyParka, GEAR_LightParka, GEAR_DownSkiJacket, GEAR_InsulatedVest, GEAR_DownParka, GEAR_BasicWinterCoat, GEAR_WolfSkinCape,
        GEAR_MinersJacket, GEAR_TacticalJacket, GEAR_DeerskinCoat_MOD
    }
    public enum RandomTorsoInner
    {
        GEAR_CowichanSweater, GEAR_CottonShirt, GEAR_FishermanSweater, GEAR_CottonHoodie, GEAR_PlaidShirt, GEAR_FleeceSweater, GEAR_HeavyWoolSweater, GEAR_WoolSweater, GEAR_TeeShirt, GEAR_WoolShirt,
        GEAR_TShirtSnappy, GEAR_TShirtGBI, GEAR_TShirtCM, GEAR_SweaterChristmasA, GEAR_JerseyHockeyA
    }
    public enum RandomHands
    {
        GEAR_BasicGloves, GEAR_FleeceMittens, GEAR_Gauntlets, GEAR_RabbitSkinMittens, GEAR_SkiGloves, GEAR_Mittens, GEAR_WorkGloves,
        GEAR_MittenBrownStripe, GEAR_MittenBlueStripe, GEAR_MittenBurgundyPattern, GEAR_TacticalGloves, GEAR_DeerskinGloves_MOD
    }
    public enum RandomAccessoriesInner
    {
        GEAR_MooseHideBag, GEAR_EarMuffs, GEAR_RifleScabbardA, GEAR_Toolbelt, GEAR_ImprovisedDownInsulation
    }
    public enum RandomLegsOuter
    {
        GEAR_CargoPants, GEAR_CombatPants, GEAR_DeerSkinPants, GEAR_Jeans, GEAR_InsulatedPants, GEAR_WorkPants, GEAR_MinersPants, GEAR_WolfSkinPant
    }
    public enum RandomLegsInner
    {
        GEAR_LongUnderwear, GEAR_LongUnderwearWool, GEAR_BearskinLeggings_MOD
    }
    public enum RandomFeetInner
    {
        GEAR_ClimbingSocks, GEAR_CottonSocks, GEAR_WoolSocks, GEAR_SockPlaid, GEAR_SockMoose, GEAR_SockDots
    }
    public enum RandomFeetOuter
    {
        GEAR_CombatBoots, GEAR_DeerSkinBoots, GEAR_InsulatedBoots, GEAR_LeatherShoes, GEAR_GreyMotherBoots, GEAR_MuklukBoots, GEAR_BasicShoes, GEAR_SkiBoots, GEAR_BasicBoots, GEAR_WorkBoots, GEAR_MinersBoots, GEAR_WolfskinBoots_MOD
    }
    class Patches
    {
        // Move the random variable to class level for easier access
        public static Il2CppSystem.Random random = new Il2CppSystem.Random();

        [HarmonyPatch(typeof(StartGear), "AddAllToInventory")]
        internal class CustomStartGear
        {
            private static bool Prefix()
            {
                try
                {
                    if (Settings.settings == null)
                    {
                        // Log this error - you'll need to add your logging method here
                        // For now, just return true to let the original method run
                        return true;
                    }

                    // If Default mode, just run the original method
                    if (Settings.settings.modFunction == ModFunction.Default) return true;

                    // For DefaultPlus and Custom modes, add custom gear
                    if (Settings.settings.modFunction == ModFunction.DefaultPlus || Settings.settings.modFunction == ModFunction.Custom)
                    {
                        // Clothing
                        if (Settings.settings.clothingSet)
                        {
                            if (random.Next(100) >= 25) AddClothingItem(RandomEnumValue<RandomHeadInner>().ToString());
                            if (random.Next(100) >= 75) AddClothingItem(RandomEnumValue<RandomHeadOuter>().ToString());
                            if (random.Next(100) >= 25) AddClothingItem(RandomEnumValue<RandomTorsoOuter>().ToString());
                            if (random.Next(100) >= 90) AddClothingItem(RandomEnumValue<RandomTorsoOuter>().ToString());
                            AddClothingItem(RandomEnumValue<RandomTorsoInner>().ToString());
                            if (random.Next(100) >= 90) AddClothingItem(RandomEnumValue<RandomTorsoInner>().ToString());
                            if (random.Next(100) >= 25) AddClothingItem(RandomEnumValue<RandomHands>().ToString());
                            if (random.Next(100) >= 75) AddClothingItem(RandomEnumValue<RandomAccessoriesInner>().ToString());
                            AddClothingItem(RandomEnumValue<RandomLegsOuter>().ToString());
                            if (random.Next(100) >= 90) AddClothingItem(RandomEnumValue<RandomLegsOuter>().ToString());
                            if (random.Next(100) >= 50) AddClothingItem(RandomEnumValue<RandomLegsInner>().ToString());
                            if (random.Next(100) >= 90) AddClothingItem(RandomEnumValue<RandomLegsInner>().ToString());
                            if (random.Next(100) >= 10) AddClothingItem(RandomEnumValue<RandomFeetInner>().ToString());
                            if (random.Next(100) >= 90) AddClothingItem(RandomEnumValue<RandomFeetInner>().ToString());
                            if (random.Next(100) >= 10) AddClothingItem(RandomEnumValue<RandomFeetOuter>().ToString());
                        }
                        else
                        {
                            if (Settings.settings.clothingCondition == Condition.Custom)
                            {
                                if (Settings.settings.headInner != HeadInner.None) AddClothingItem(Settings.settings.headInner.ToString(), Settings.settings.headInnerCondition);
                                if (Settings.settings.headOuter != HeadOuter.None) AddClothingItem(Settings.settings.headOuter.ToString(), Settings.settings.headOuterCondition);
                                if (Settings.settings.torsoOuterInner != TorsoOuter.None) AddClothingItem(Settings.settings.torsoOuterInner.ToString(), Settings.settings.torsoOuterInnerCondition);
                                if (Settings.settings.torsoOuterOuter != TorsoOuter.None) AddClothingItem(Settings.settings.torsoOuterOuter.ToString(), Settings.settings.torsoOuterOuterCondition);
                                if (Settings.settings.torsoInnerInner != TorsoInner.None) AddClothingItem(Settings.settings.torsoInnerInner.ToString(), Settings.settings.torsoInnerInnerCondition);
                                if (Settings.settings.torsoInnerOuter != TorsoInner.None) AddClothingItem(Settings.settings.torsoInnerOuter.ToString(), Settings.settings.torsoInnerOuterCondition);
                                if (Settings.settings.hands != Hands.None) AddClothingItem(Settings.settings.hands.ToString(), Settings.settings.handsCondition);
                                if (Settings.settings.accessoriesInner != AccessoriesInner.None) AddClothingItem(Settings.settings.accessoriesInner.ToString(), Settings.settings.accessoriesInnerCondition);
                                if (Settings.settings.accessoriesOuter != AccessoriesOuter.None) AddClothingItem(Settings.settings.accessoriesOuter.ToString(), Settings.settings.accessoriesOuterCondition);
                                if (Settings.settings.legsOuterInner != LegsOuter.None) AddClothingItem(Settings.settings.legsOuterInner.ToString(), Settings.settings.legsOuterInnerCondition);
                                if (Settings.settings.legsOuterOuter != LegsOuter.None) AddClothingItem(Settings.settings.legsOuterOuter.ToString(), Settings.settings.legsOuterOuterCondition);
                                if (Settings.settings.legsInnerInner != LegsInner.None) AddClothingItem(Settings.settings.legsInnerInner.ToString(), Settings.settings.legsInnerInnerCondition);
                                if (Settings.settings.legsInnerOuter != LegsInner.None) AddClothingItem(Settings.settings.legsInnerOuter.ToString(), Settings.settings.legsInnerOuterCondition);
                                if (Settings.settings.feetInnerInner != FeetInner.None) AddClothingItem(Settings.settings.feetInnerInner.ToString(), Settings.settings.feetInnerInnerCondition);
                                if (Settings.settings.feetInnerOuter != FeetInner.None) AddClothingItem(Settings.settings.feetInnerOuter.ToString(), Settings.settings.feetInnerOuterCondition);
                                if (Settings.settings.feetOuter != FeetOuter.None) AddClothingItem(Settings.settings.feetOuter.ToString(), Settings.settings.feetOuterCondition);
                            }
                            else if (Settings.settings.clothingCondition == Condition.Random)
                            {
                                if (Settings.settings.headInner != HeadInner.None) AddClothingItem(Settings.settings.headInner.ToString());
                                if (Settings.settings.headOuter != HeadOuter.None) AddClothingItem(Settings.settings.headOuter.ToString());
                                if (Settings.settings.torsoOuterInner != TorsoOuter.None) AddClothingItem(Settings.settings.torsoOuterInner.ToString());
                                if (Settings.settings.torsoOuterOuter != TorsoOuter.None) AddClothingItem(Settings.settings.torsoOuterOuter.ToString());
                                if (Settings.settings.torsoInnerInner != TorsoInner.None) AddClothingItem(Settings.settings.torsoInnerInner.ToString());
                                if (Settings.settings.torsoInnerOuter != TorsoInner.None) AddClothingItem(Settings.settings.torsoInnerOuter.ToString());
                                if (Settings.settings.hands != Hands.None) AddClothingItem(Settings.settings.hands.ToString());
                                if (Settings.settings.accessoriesInner != AccessoriesInner.None) AddClothingItem(Settings.settings.accessoriesInner.ToString());
                                if (Settings.settings.accessoriesOuter != AccessoriesOuter.None) AddClothingItem(Settings.settings.accessoriesOuter.ToString());
                                if (Settings.settings.legsOuterInner != LegsOuter.None) AddClothingItem(Settings.settings.legsOuterInner.ToString());
                                if (Settings.settings.legsOuterOuter != LegsOuter.None) AddClothingItem(Settings.settings.legsOuterOuter.ToString());
                                if (Settings.settings.legsInnerInner != LegsInner.None) AddClothingItem(Settings.settings.legsInnerInner.ToString());
                                if (Settings.settings.legsInnerOuter != LegsInner.None) AddClothingItem(Settings.settings.legsInnerOuter.ToString());
                                if (Settings.settings.feetInnerInner != FeetInner.None) AddClothingItem(Settings.settings.feetInnerInner.ToString());
                                if (Settings.settings.feetInnerOuter != FeetInner.None) AddClothingItem(Settings.settings.feetInnerOuter.ToString());
                                if (Settings.settings.feetOuter != FeetOuter.None) AddClothingItem(Settings.settings.feetOuter.ToString());
                            }
                        }

                        // Fire Starting
                        if (Settings.settings.firestarter != FireStarter.None)
                        {
                            int qty = 1;
                            if (Settings.settings.firestarter == FireStarter.GEAR_PackMatches || Settings.settings.firestarter == FireStarter.GEAR_WoodMatches) qty = Settings.settings.matchQty;
                            AddItemToInventory(Settings.settings.firestarter.ToString(), qty);
                        }
                        if (Settings.settings.tinderType != Tinder.None) AddItemToInventory(Settings.settings.tinderType.ToString(), Settings.settings.tinderQty);
                        if (Settings.settings.fuelType != Fuel.None) AddItemToInventory(Settings.settings.fuelType.ToString(), Settings.settings.fuelQty);
                        if (Settings.settings.accelerant != Accelerant.None) AddItemToInventory(Settings.settings.accelerant.ToString());

                        // First Aid
                        if (Settings.settings.antibiotics != 0) AddItemToInventory("GEAR_BottleAntibiotics", Settings.settings.antibiotics);
                        if (Settings.settings.antiseptic != 0)
                        {
                            for (int i = 0; i < Settings.settings.antiseptic; i++)
                            {
                                AddItemToInventory("GEAR_BottleHydrogenPeroxide");
                            }
                        }
                        if (Settings.settings.bandages != 0) AddItemToInventory("GEAR_HeavyBandage", Settings.settings.bandages);
                        if (Settings.settings.emergencyStim != 0)
                        {
                            for (int i = 0; i < Settings.settings.emergencyStim; i++)
                            {
                                AddItemToInventory("GEAR_EmergencyStim");
                            }
                        }
                        if (Settings.settings.oldMansBeardDressing != 0) AddItemToInventory("GEAR_OldMansBeardDressing", Settings.settings.oldMansBeardDressing);
                        if (Settings.settings.painkillers != 0) AddItemToInventory("GEAR_BottlePainKillers", Settings.settings.painkillers);
                        if (Settings.settings.preparedBirchBark != 0) AddItemToInventory("GEAR_BirchbarkPrepared", Settings.settings.preparedBirchBark);
                        if (Settings.settings.preparedReshiMushrooms != 0) AddItemToInventory("GEAR_ReishiPrepared", Settings.settings.preparedReshiMushrooms);
                        if (Settings.settings.preparedRoseHips != 0) AddItemToInventory("GEAR_RosehipsPrepared", Settings.settings.preparedRoseHips);
                        if (Settings.settings.waterPurificationTablets != 0) AddItemToInventory("GEAR_WaterPurificationTablets", Settings.settings.waterPurificationTablets);

                        // DLC First Aid Items
                        if (Settings.settings.caffeinePills != 0) AddItemToInventory("GEAR_BottleCaffeine", Settings.settings.caffeinePills);
                        if (Settings.settings.vitaminCPills != 0) AddItemToInventory("GEAR_BottleVitaminC", Settings.settings.vitaminCPills);
                        if (Settings.settings.heatPad != 0) AddItemToInventory("GEAR_HeatPad", Settings.settings.heatPad);

                        // Food and Drink
                        if (Settings.settings.food1 != Food.None)
                        {
                            AddItemToInventory(Settings.settings.food1.ToString());
                            if (Settings.settings.food2 != Food.None)
                            {
                                AddItemToInventory(Settings.settings.food2.ToString());
                                if (Settings.settings.food3 != Food.None)
                                {
                                    AddItemToInventory(Settings.settings.food3.ToString());
                                    if (Settings.settings.food4 != Food.None)
                                    {
                                        AddItemToInventory(Settings.settings.food4.ToString());
                                        if (Settings.settings.food5 != Food.None)
                                        {
                                            AddItemToInventory(Settings.settings.food5.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        if (Settings.settings.drink1 != Drink.None)
                        {
                            AddItemToInventory(Settings.settings.drink1.ToString());
                            if (Settings.settings.drink2 != Drink.None)
                            {
                                AddItemToInventory(Settings.settings.drink2.ToString());
                                if (Settings.settings.drink3 != Drink.None)
                                {
                                    AddItemToInventory(Settings.settings.drink3.ToString());
                                    if (Settings.settings.drink4 != Drink.None)
                                    {
                                        AddItemToInventory(Settings.settings.drink4.ToString());
                                        if (Settings.settings.drink5 != Drink.None)
                                        {
                                            AddItemToInventory(Settings.settings.drink5.ToString());
                                        }
                                    }
                                }
                            }
                        }

                        // Tools
                        if (Settings.settings.bedroll != Bedroll.None) AddItemToInventory(Settings.settings.bedroll.ToString());
                        if (Settings.settings.canOpener) AddItemToInventory("GEAR_CanOpener");
                        if (Settings.settings.cooking1 != Cooking.None) AddItemToInventory(Settings.settings.cooking1.ToString());
                        if (Settings.settings.cooking1 != Cooking.None && Settings.settings.cooking2 != Cooking.None) AddItemToInventory(Settings.settings.cooking2.ToString());
                        if (Settings.settings.fishingTackle != 0)
                        {
                            for (int i = 0; i < Settings.settings.fishingTackle; i++)
                            {
                                AddItemToInventory("GEAR_HookAndLine");
                            }
                        }
                        if (Settings.settings.hacksaw) AddItemToInventory("GEAR_Hacksaw");
                        if (Settings.settings.hatchet != Hatchet.None) AddItemToInventory(Settings.settings.hatchet.ToString());
                        if (Settings.settings.heavyHammer) AddItemToInventory("GEAR_Hammer");
                        if (Settings.settings.hook != 0) AddItemToInventory("GEAR_Hook", Settings.settings.hook);
                        if (Settings.settings.knife != Knife.None) AddItemToInventory(Settings.settings.knife.ToString());
                        if (Settings.settings.lightSource != LightSources.None) AddItemToInventory(Settings.settings.lightSource.ToString());
                        if (Settings.settings.line != 0) AddItemToInventory("GEAR_Line", Settings.settings.line);
                        if (Settings.settings.mountaineeringRope) AddItemToInventory("GEAR_Rope");
                        if (Settings.settings.prybar) AddItemToInventory("GEAR_Prybar");
                        if (Settings.settings.rifleCleaningKit) AddItemToInventory("GEAR_RifleCleaningKit");
                        if (Settings.settings.sewingKit) AddItemToInventory("GEAR_SewingKit");
                        if (Settings.settings.snare) AddItemToInventory("GEAR_Snare");
                        if (Settings.settings.sprayPaint) AddItemToInventory("GEAR_SprayPaintCan");
                        if (Settings.settings.toolBox != ToolBox.None) AddItemToInventory(Settings.settings.toolBox.ToString());
                        if (Settings.settings.weapon != Weapons.None) AddItemToInventory(Settings.settings.weapon.ToString());
                        if (Settings.settings.ammunitionQty != 0)
                        {
                            switch (Settings.settings.weapon)
                            {
                                case Weapons.None:
                                    break;
                                case Weapons.GEAR_FlareGun:
                                    AddItemToInventory("GEAR_FlareGunAmmoSingle", Settings.settings.ammunitionQty);
                                    break;
                                case Weapons.GEAR_Rifle:
                                case Weapons.GEAR_Rifle_Trader:
                                case Weapons.GEAR_Rifle_Barbs:
                                case Weapons.GEAR_Rifle_Curators:
                                case Weapons.GEAR_Rifle_Vaughns:
                                    AddItemToInventory("GEAR_RifleAmmoSingle", Settings.settings.ammunitionQty);
                                    break;
                                case Weapons.GEAR_Revolver:
                                case Weapons.GEAR_RevolverStubNosed:
                                case Weapons.GEAR_RevolverFancy:
                                case Weapons.GEAR_RevolverGreen:
                                    AddItemToInventory("GEAR_RevolverAmmoSingle", Settings.settings.ammunitionQty);
                                    break;
                                case Weapons.GEAR_Bow:
                                case Weapons.GEAR_Bow_Woodwrights:
                                case Weapons.GEAR_Bow_Manufactured:
                                case Weapons.GEAR_Bow_Bushcraft:
                                    if (Settings.settings.arrowType == ArrowType.GEAR_Arrow)
                                    {
                                        AddItemToInventory("GEAR_Arrow", Settings.settings.ammunitionQty);
                                    }
                                    else if (Settings.settings.arrowType == ArrowType.GEAR_ArrowManufactured)
                                    {
                                        AddItemToInventory("GEAR_ArrowManufactured", Settings.settings.ammunitionQty);
                                    }
                                    break;

                            }
                        }
                        if (Settings.settings.whetstone) AddItemToInventory("GEAR_SharpeningStone");

                        // DLC Tools
                        if (Settings.settings.stickFlask) AddItemToInventory("GEAR_InsulatedFlask_G");
                        if (Settings.settings.camera) AddItemToInventory("GEAR_Camera");
                        if (Settings.settings.filmBoxColour != 0) AddItemToInventory("GEAR_FilmBoxColour", Settings.settings.filmBoxColour);
                        if (Settings.settings.filmBoxBW != 0) AddItemToInventory("GEAR_FilmBoxBW", Settings.settings.filmBoxBW);
                        if (Settings.settings.filmBoxSepia != 0) AddItemToInventory("GEAR_FilmBoxSepia", Settings.settings.filmBoxSepia);
                        if (Settings.settings.wireBundle != 0) AddItemToInventory("GEAR_WireBundle", Settings.settings.wireBundle);
                        if (Settings.settings.fuse != 0) AddItemToInventory("GEAR_Fuse", Settings.settings.fuse);
                        if (Settings.settings.travois) AddItemToInventory("GEAR_Travois");
                        if (Settings.settings.handheldShortwave) AddItemToInventory("GEAR_HandheldShortwave");
                        if (Settings.settings.respirator) AddItemToInventory("GEAR_Respirator");
                        if (Settings.settings.canister != 0) AddItemToInventory("GEAR_Canister", Settings.settings.canister);

                        // Materials
                        if (Settings.settings.arrowhead != 0) AddItemToInventory("GEAR_ArrowHead", Settings.settings.arrowhead);
                        if (Settings.settings.arrowShaft != 0) AddItemToInventory("GEAR_ArrowShaft", Settings.settings.arrowShaft);
                        if (Settings.settings.bullet != 0) AddItemToInventory("GEAR_Bullet", Settings.settings.bullet);
                        if (Settings.settings.gunpowder != 0) AddItemToInventory("GEAR_GunpowderCan", Settings.settings.gunpowder);
                        if (Settings.settings.charcoal != 0) AddItemToInventory("GEAR_Charcoal", Settings.settings.charcoal);
                        if (Settings.settings.crowFeather != 0) AddItemToInventory("GEAR_CrowFeather", Settings.settings.crowFeather);
                        if (Settings.settings.dustingSulfur != 0) AddItemToInventory("GEAR_DustingSulfur", Settings.settings.dustingSulfur);
                        if (Settings.settings.revolverShell != 0) AddItemToInventory("GEAR_RevolverAmmoCasing", Settings.settings.revolverShell);
                        if (Settings.settings.rifleShell != 0) AddItemToInventory("GEAR_RifleAmmoCasing", Settings.settings.rifleShell);
                        if (Settings.settings.scrapLead != 0) AddItemToInventory("GEAR_ScrapLead", Settings.settings.scrapLead);
                        if (Settings.settings.stumpRemover != 0) AddItemToInventory("GEAR_StumpRemover", Settings.settings.stumpRemover);

                        if (Settings.settings.bearHideCured != 0) AddItemToInventory("GEAR_BearHideDried", Settings.settings.bearHideCured);
                        if (Settings.settings.bearHideFresh != 0) AddItemToInventory("GEAR_BearHide", Settings.settings.bearHideFresh);
                        if (Settings.settings.deerHideCured != 0) AddItemToInventory("GEAR_LeatherHideDried", Settings.settings.deerHideCured);
                        if (Settings.settings.deerHideFresh != 0) AddItemToInventory("GEAR_LeatherHide", Settings.settings.deerHideFresh);
                        if (Settings.settings.gutCured != 0) AddItemToInventory("GEAR_GutDried", Settings.settings.gutCured);
                        if (Settings.settings.gutFresh != 0) AddItemToInventory("GEAR_Gut", Settings.settings.gutFresh);
                        if (Settings.settings.mooseHideCured != 0) AddItemToInventory("GEAR_MooseHideDried", Settings.settings.mooseHideCured);
                        if (Settings.settings.mooseHideFresh != 0) AddItemToInventory("GEAR_MooseHide", Settings.settings.mooseHideFresh);
                        if (Settings.settings.rabbitPeltCured != 0) AddItemToInventory("GEAR_RabbitPeltDried", Settings.settings.rabbitPeltCured);
                        if (Settings.settings.rabbitPeltFresh != 0) AddItemToInventory("GEAR_RabbitPelt", Settings.settings.rabbitPeltFresh);
                        if (Settings.settings.wolfPeltCured != 0) AddItemToInventory("GEAR_WolfPeltDried", Settings.settings.wolfPeltCured);
                        if (Settings.settings.wolfPeltFresh != 0) AddItemToInventory("GEAR_WolfPelt", Settings.settings.wolfPeltFresh);

                        // DLC Materials
                        if (Settings.settings.cougarClaw != 0) AddItemToInventory("GEAR_CougarClaw", Settings.settings.cougarClaw);

                        if (Settings.settings.birchSaplingCured != 0) AddItemToInventory("GEAR_BirchSaplingDried", Settings.settings.birchSaplingCured);
                        if (Settings.settings.birchSaplingFresh != 0) AddItemToInventory("GEAR_BirchSapling", Settings.settings.birchSaplingFresh);
                        if (Settings.settings.mapleSaplingCured != 0) AddItemToInventory("GEAR_MapleSaplingDried", Settings.settings.mapleSaplingCured);
                        if (Settings.settings.mapleSaplingFresh != 0) AddItemToInventory("GEAR_MapleSapling", Settings.settings.mapleSaplingFresh);

                        if (Settings.settings.cloth != 0) AddItemToInventory("GEAR_Cloth", Settings.settings.cloth);
                        if (Settings.settings.curedLeather != 0) AddItemToInventory("GEAR_LeatherDried", Settings.settings.curedLeather);
                        if (Settings.settings.scrapMetal != 0) AddItemToInventory("GEAR_ScrapMetal", Settings.settings.scrapMetal);
                    }

                    // Return based on mode:
                    // - DefaultPlus: return true to allow original method to run after adding custom items
                    // - Custom: return false to prevent original method from running
                    return Settings.settings.modFunction == ModFunction.DefaultPlus;

                }
                catch (Exception ex)
                {
                    // Log the full exception details here
                    // For now, return true to let original method run
                    return true;
                }
            }
        }

        // Helper method to add regular items to inventory
        private static void AddItemToInventory(string itemName, int quantity = 1)
        {
            // Get the GearItem prefab from the string name
            GearItem gearItemPrefab = GearItem.LoadGearItemPrefab(itemName);
            if (gearItemPrefab == null)
            {
                // If prefab not found, return early
                return;
            }

            GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(gearItemPrefab, quantity);
        }

        // Helper method to add clothing items and automatically equip them
        private static void AddClothingItem(string clothingItemName, float condition = -1f)
        {
            // Get the GearItem prefab from the string name
            GearItem gearItemPrefab = GearItem.LoadGearItemPrefab(clothingItemName);
            if (gearItemPrefab == null)
            {
                // If prefab not found, return early
                return;
            }

            GearItem newClothingItem;
            if (condition > 0)
            {
                // Use normalized condition (0.0 to 1.0 range)
                float normalizedCondition = condition / 100f; // Assuming condition is passed as percentage
                newClothingItem = GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(gearItemPrefab, 1, normalizedCondition);
            }
            else
            {
                // Generate random condition between 0.1 and 1.0 (10% to 100%)
                float randomCondition = (float)(random.NextDouble() * 0.9 + 0.1);
                newClothingItem = GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(gearItemPrefab, 1, randomCondition);
            }

            // Check if the item has a clothing component before trying to put it on
            if (newClothingItem != null && newClothingItem.m_ClothingItem != null)
            {
                newClothingItem.m_ClothingItem.PutOn();
            }
        }

        private static T RandomEnumValue<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}