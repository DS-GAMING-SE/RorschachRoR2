using BepInEx.Configuration;
using HG;
using R2API.Networking;
using RoR2;
using RoR2.ContentManagement;
using RoR2.Skills;
using RorschachMod.Characters.Survivors.Rorschach.Components;
using RorschachMod.Characters.Survivors.Rorschach.SkillStates;
using RorschachMod.Modules;
using RorschachMod.Modules.Characters;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RorschachMod.Characters.Survivors.Rorschach
{
    public class RorschachSurvivor : SurvivorBase<RorschachSurvivor>
    {
        //the name of the prefab we will create. conventionally ending in "Body". must be unique
        public override string bodyName => "RorschachBody"; //if you do not change this, you get the point by now

        //name of the ai master for vengeance and goobo. must be unique
        public override string masterName => "RorschachMonsterMaster"; //if you do not

        //the names of the prefabs you set up in unity that we will use to build your character
        public override string modelPrefabGUID => RorschachAssets.characterModel.AssetGUID;
        public override string displayPrefabGUID => RorschachAssets.displayPrefab.AssetGUID;

        public const string RORSCHACH_PREFIX = RorschachPlugin.DEVELOPER_PREFIX + "_RORSCHACH_";

        //used when registering your survivor's language tokens
        public override string survivorTokenPrefix => RORSCHACH_PREFIX;
        
        public override BodyInfo bodyInfo => new BodyInfo
        {
            bodyName = bodyName,
            bodyNameToken = RORSCHACH_PREFIX + "NAME",
            subtitleNameToken = RORSCHACH_PREFIX + "SUBTITLE",

            characterPortrait = RorschachAssets.characterIcon.LoadAssetAsync().WaitForCompletion(),
            bodyColor = Color.white * 0.85f,
            sortPosition = 110,

            crosshair = Asset.LoadCrosshair("Standard"),
            podPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/SurvivorPod"),

            maxHealth = 160f,
            healthRegen = 2.5f,
            armor = 20f,

            jumpCount = 1,
        };

        public override CustomRendererInfo[] customRendererInfos => new CustomRendererInfo[]
        {
                new CustomRendererInfo
                {
                    childName = "Model",
                    materialReference = RorschachAssets.defaultSkinMaterial
                },
                new CustomRendererInfo
                {
                    childName = "SwordModel",
                    materialReference = RorschachAssets.defaultSkinMaterial
                },
                new CustomRendererInfo
                {
                    childName = "GunModel",
                    materialReference = RorschachAssets.defaultSkinMaterial
                }
        };

        public override UnlockableDef characterUnlockableDef => RorschachUnlockables.characterUnlockableDef;
        
        public override ItemDisplaysBase itemDisplays => null;

        //set in base classes

        public override GameObject bodyPrefab { get; protected set; }
        public override CharacterBody prefabCharacterBody { get; protected set; }
        public override GameObject characterModelObject { get; protected set; }
        public override CharacterModel prefabCharacterModel { get; protected set; }
        public override GameObject displayPrefab { get; protected set; }

        public override void Initialize()
        {
            //uncomment if you have multiple characters
            //ConfigEntry<bool> characterEnabled = Config.CharacterEnableConfig("Survivors", "Rorschach");

            //if (!characterEnabled.Value)
            //    return;

            base.Initialize();
        }

        public override void InitializeCharacter()
        {
            //need the character unlockable before you initialize the survivordef
            RorschachUnlockables.Init();

            base.InitializeCharacter();

            RorschachConfig.Init();
            RorschachStates.Init();
            RorschachTokens.Init();

            RorschachAssets.Init();
            RorschachBuffs.Init();

            InitializeEntityStateMachines();
            InitializeSkills();
            ImprovisedWeapons.ImprovisedWeaponItemDefs.Initialize();
            InitializeSkins();
            InitializeCharacterMaster();

            AdditionalBodySetup();

            RecalculateStats.Initialize();
        }

        private void AdditionalBodySetup()
        {
            AddHitboxes();
            //bodyPrefab.AddComponent<HedgehogUtils.Miscellaneous.MomentumPassive>();
            //bodyPrefab.AddComponent<HedgehogUtils.Boost.BoostLogic>();
            //anything else here
        }

        public void AddHitboxes()
        {
            //example of how to create a HitBoxGroup. see summary for more details
            Prefabs.SetupHitBoxGroup(characterModelObject, "SwordGroup", "SwordHitbox");
        }

        public override void InitializeEntityStateMachines() 
        {
            //clear existing state machines from your cloned body (probably commando)
            //omit all this if you want to just keep theirs
            Prefabs.ClearEntityStateMachines(bodyPrefab);

            //the main "Body" state machine has some special properties
            Prefabs.AddMainEntityStateMachine(bodyPrefab, "Body", typeof(EntityStates.GenericCharacterMain), typeof(EntityStates.SpawnTeleporterState));
            //if you set up a custom main characterstate, set it up here
                //don't forget to register custom entitystates in your RorschachStates.cs

            Prefabs.AddEntityStateMachine(bodyPrefab, "Weapon");
            Prefabs.AddEntityStateMachine(bodyPrefab, "Weapon2");
        }

        #region skills
        public override void InitializeSkills()
        {
            //remove the genericskills from the commando body we cloned
            Skills.ClearGenericSkills(bodyPrefab);
            //add our own
            AddPassiveSkill();
            AddPrimarySkills();
            AddSecondarySkills();
            AddUtiitySkills();
            AddSpecialSkills();
        }

        //skip if you don't have a passive
        //also skip if this is your first look at skills
        private void AddPassiveSkill()
        {
            //option 1. fake passive icon just to describe functionality we will implement elsewhere
            bodyPrefab.GetComponent<SkillLocator>().passiveSkill = new SkillLocator.PassiveSkill
            {
                enabled = true,
                skillNameToken = RORSCHACH_PREFIX + "PASSIVE_NAME",
                skillDescriptionToken = RORSCHACH_PREFIX + "PASSIVE_DESCRIPTION",
                keywordToken = RORSCHACH_PREFIX + "PASSIVE_IMPROVISED_WEAPON_KEYWORD",
                icon = RorschachAssets.passiveSkillIcon.LoadAssetAsync().WaitForCompletion(),
            };
        }

        //if this is your first look at skilldef creation, take a look at Secondary first
        private void AddPrimarySkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Primary);

            //the primary skill is created using a constructor for a typical primary
            //it is also a SteppedSkillDef. Custom Skilldefs are very useful for custom behaviors related to casting a skill. see ror2's different skilldefs for reference
            SteppedSkillDef primarySkillDef1 = Skills.CreateSkillDef<SteppedSkillDef>(new SkillDefInfo
                (
                    "RorschachPrimaryDefault",
                    RORSCHACH_PREFIX + "PRIMARY_DEFAULT_NAME",
                    RORSCHACH_PREFIX + "PRIMARY_DEFAULT_DESCRIPTION",
                    RorschachAssets.primarySkillIcon.LoadAssetAsync().WaitForCompletion(),
                    new EntityStates.SerializableEntityStateType(typeof(PrimaryDefault)),
                    "Weapon"
                ));
            //custom Skilldefs can have additional fields that you can set manually
            primarySkillDef1.stepCount = 3;
            primarySkillDef1.stepGraceDuration = 0.5f;

            // Remember DamageType.BleedOnHit for cleaver

            Skills.AddPrimarySkills(bodyPrefab, primarySkillDef1);
        }

        private void AddSecondarySkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Secondary);

            //here is a basic skill def with all fields accounted for
            SkillDef secondarySkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "RorschachSecondaryDefault",
                skillNameToken = RORSCHACH_PREFIX + "SECONDARY_DEFAULT_NAME",
                skillDescriptionToken = RORSCHACH_PREFIX + "SECONDARY_DEFAULT_DESCRIPTION",
                keywordTokens = new string[] { RORSCHACH_PREFIX + "JUDGEMENT_KEYWORD" },
                skillIcon = RorschachAssets.secondarySkillIcon.LoadAssetAsync().WaitForCompletion(),

                activationState = new EntityStates.SerializableEntityStateType(typeof(SecondaryDefaultChargedAttack)),
                activationStateMachineName = "Body",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 3f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                mustKeyPress = true,
                beginSkillCooldownOnSkillEnd = true,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = false,
                forceSprintDuringState = true,

            });

            Skills.AddSecondarySkills(bodyPrefab, secondarySkillDef1);
            NetworkingAPI.RegisterMessageType<NetworkJudgement>();
        }

        private void AddUtiitySkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Utility);

            //here's a skilldef of a typical movement skill.
            SkillDef utilitySkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "RorschachUtilityDefault",
                skillNameToken = RORSCHACH_PREFIX + "UTILITY_DEFAULT_NAME",
                skillDescriptionToken = RORSCHACH_PREFIX + "UTILITY_DEFAULT_DESCRIPTION",
                keywordTokens = new string[] { "KEYWORD_STUNNING" },
                skillIcon = RorschachAssets.utilitySkillIcon.LoadAssetAsync().WaitForCompletion(),

                activationState = new EntityStates.SerializableEntityStateType(typeof(Roll)),
                activationStateMachineName = "Body",
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,

                baseRechargeInterval = 4f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = false,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = false,
                forceSprintDuringState = true,
            });

            Skills.AddUtilitySkills(bodyPrefab, utilitySkillDef1);
        }

        private void AddSpecialSkills()
        {
            Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, SkillSlot.Special);

            //a basic skill. some fields are omitted and will just have default values
            SkillDef specialSkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "RorschachSpecialDefault",
                skillNameToken = RORSCHACH_PREFIX + "SPECIAL_DEFAULT_NAME",
                skillDescriptionToken = RORSCHACH_PREFIX + "SPECIAL_DEFAULT_DESCRIPTION",
                keywordTokens = new string[] { RORSCHACH_PREFIX + "JUDGEMENT_KEYWORD", RORSCHACH_PREFIX + "SPECIAL_ON_KILL_BUFF_KEYWORD", RORSCHACH_PREFIX + "PASSIVE_IMPROVISED_WEAPON_KEYWORD" },
                skillIcon = RorschachAssets.specialSkillIcon.LoadAssetAsync().WaitForCompletion(),

                activationState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.ThrowBomb)),
                //setting this to the "weapon2" EntityStateMachine allows us to cast this skill at the same time primary, which is set to the "weapon" EntityStateMachine
                activationStateMachineName = "Weapon2", interruptPriority = EntityStates.InterruptPriority.Skill,

                baseMaxStock = 1,
                baseRechargeInterval = 10f,

                isCombatSkill = true,
                mustKeyPress = false,
            });

            // remember AimThrowableBase for Flame Can Special

            Skills.AddSpecialSkills(bodyPrefab, specialSkillDef1);
        }
        #endregion skills
        
        #region skins
        public override void InitializeSkins()
        {
            ModelSkinController skinController = prefabCharacterModel.gameObject.AddComponent<ModelSkinController>();
            ModelSkinController skinController2 = displayPrefab.gameObject.AddComponent<ModelSkinController>();
            skinController._animatorControllerAddress = RorschachAssets.animator;
            skinController._avatarAddress = RorschachAssets.animatorAvatar;
            skinController2._animatorControllerAddress = RorschachAssets.displayAnimator;
            skinController2._avatarAddress = RorschachAssets.animatorAvatar;
            ChildLocator childLocator = prefabCharacterModel.GetComponent<ChildLocator>();
            CharacterModel.RendererInfo[] defaultRendererinfos = prefabCharacterModel.baseRendererInfos;

            skinController.skins = Skins.InitializeSkins(prefabCharacterModel, defaultRendererinfos);
            skinController2.skins = ArrayUtils.Clone(skinController.skins);
        }
        #endregion skins

        //Character Master is what governs the AI of your character when it is not controlled by a player (artifact of vengeance, goobo)
        public override void InitializeCharacterMaster()
        {
            //you must only do one of these. adding duplicate masters breaks the game.

            //if you're lazy or prototyping you can simply copy the AI of a different character to be used
            //Modules.Prefabs.CloneDopplegangerMaster(bodyPrefab, masterName, "Merc");

            //how to set up AI in code
            RorschachAI.Init(bodyPrefab, masterName);

            //how to load a master set up in unity, can be an empty gameobject with just AISkillDriver components
            //assetBundle.LoadMaster(bodyPrefab, masterName);
        }
    }
}