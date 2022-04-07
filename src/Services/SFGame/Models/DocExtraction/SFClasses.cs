using Newtonsoft.Json;
using System.Collections.Generic;

namespace Services.SFGame.Models.DocExtraction
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class MUnlock
    {
        public string Class { get; set; }
        public string mRecipes { get; set; }
        public string mNumInventorySlotsToUnlock { get; set; }
        public string mResourcesToAddToScanner { get; set; }
        public string mResourcePairsToAddToScanner { get; set; }
        public string mSchematics { get; set; }
    }

    public class MSchematicDependency
    {
        public string Class { get; set; }
        public string mSchematics { get; set; }
        public string mRequireAllSchematicsToBePurchased { get; set; }
    }

    public class MFuel
    {
        public string mFuelClass { get; set; }
        public string mSupplementalResourceClass { get; set; }
        public string mByproduct { get; set; }
        public string mByproductAmount { get; set; }
    }

    public class Class
    {
        public string ClassName { get; set; }

        /// <summary>
        /// Used to get the resource name in blueprints.
        /// </summary>
        [JsonProperty("mDisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Used to get the resource description in blueprints.
        /// </summary>
        [JsonProperty("mDescription")]
        public string Description { get; set; }

        /// <summary>
        /// Used to get the abbreviated name of the item in blueprints.
        /// </summary>
        [JsonProperty("mAbbreviatedDisplayName")]
        public string AbbreviatedDisplayName { get; set; }

        /// <summary>
        /// The number of items of a certain type we can stack in one inventory slot.
        /// </summary>
        [JsonProperty("mStackSize")]
        public StackSize? StackSize { get; set; }

        /// <summary>
        /// Returns if this item can be discarded.
        /// </summary>
        [JsonProperty("mCanBeDiscarded")]
        public bool? CanBeDiscarded { get; set; }

        /// <summary>
        /// Returns if we should store if this item ever has been picked up.
        /// </summary>
        [JsonProperty("mRememberPickUp")]
        public bool? RememberPickUp { get; set; }

        /// <summary>
        /// Energy value for this resource if used as fuel.
        /// </summary>
        [JsonProperty("mEnergyValue")]
        public decimal? EnergyValue { get; set; }

        /// <summary>
        /// How much radiation this item gives out, 0 means it's not radioactive.
        /// </summary>
        [JsonProperty("mRadioactiveDecay")]
        public decimal? RadioactiveDecay { get; set; }

        [JsonProperty("mForm")]
        public ResourceForm? Form { get; set; }

        /// <summary>
        /// The small icon of the item.
        /// </summary>
        [JsonProperty("mSmallIcon")]
        public string SmallIcon { get; set; }

        /// <summary>
        /// The big icon of the item.
        /// </summary>
        [JsonProperty("mPersistentBigIcon")]
        public string BigIcon { get; set; }

        /// <summary>
        /// Returns the color of this is a fluid.
        /// </summary>
        [JsonProperty("mFluidColor")]
        public string FluidColor { get; set; } // TODO convert to color

        /// <summary>
        /// Returns the color of this is a gas.
        /// </summary>
        [JsonProperty("mGasColor")]
        public string GasColor { get; set; } // TODO convert to color

        [JsonProperty("mResourceSinkPoints")]
        public long? ResourceSinkPoints { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        /// <summary>
        /// Returns the type of schematic.
        /// </summary>
        [JsonProperty("mType")]
        public SchematicType? Type { get; set; }

        [JsonProperty("mSubCategories")]
        public string SubCategories { get; set; }

        [JsonProperty("mMenuPriority")]
        public decimal? MenuPriority { get; set; }

        [JsonProperty("mTechTier")]
        public long? TechTier { get; set; }

        [JsonProperty("mCost")]
        public string Cost { get; set; }

        [JsonProperty("mTimeToComplete")]
        public decimal? TimeToComplete { get; set; }

        [JsonProperty("mRelevantShopSchematics")]
        public string RelevantShopSchematics { get; set; }

        [JsonProperty("mUnlocks")]
        public List<MUnlock> Unlocks { get; set; }

        [JsonProperty("mSchematicIcon")]
        public string SchematicIcon { get; set; }

        [JsonProperty("mSmallSchematicIcon")]
        public string SmallSchematicIcon { get; set; }

        [JsonProperty("mSchematicDependencies")]
        public List<MSchematicDependency> SchematicDependencies { get; set; }

        [JsonProperty("mRelevantEvents")]
        public string RelevantEvents { get; set; }

        [JsonProperty("mIncludeInBuilds")]
        public string IncludeInBuilds { get; set; }
        public string mRandomAnim { get; set; }
        public string mCanPress { get; set; }
        public string mEquipmentSlot { get; set; }
        public string mAttachSocket { get; set; }
        public string mCostToUse { get; set; }
        public string mArmAnimation { get; set; }
        public string mBackAnimation { get; set; }
        public string mHasPersistentOwner { get; set; }
        public string mUseDefaultPrimaryFire { get; set; }
        public string mAnimData { get; set; }
        public string mCurrentAnimData { get; set; }
        public string IsPowered { get; set; }
        public string mCurrentRecipeCheck { get; set; }
        public string mPreviousRecipeCheck { get; set; }
        public string mCurrentRecipeChanged { get; set; }
        public string mManufacturingSpeed { get; set; }
        public string mFactoryInputConnections { get; set; }
        public string mPipeInputConnections { get; set; }
        public string mFactoryOutputConnections { get; set; }
        public string mPipeOutputConnections { get; set; }
        public string mPowerConsumption { get; set; }
        public string mPowerConsumptionExponent { get; set; }
        public string mOnProductionStatusChanged { get; set; }
        public string mOnHasPowerChanged { get; set; }
        public string mOnHasProductionChanged { get; set; }
        public string mOnHasStandbyChanged { get; set; }
        public string mMinimumProducingTime { get; set; }
        public string mMinimumStoppedTime { get; set; }
        public string mNumCyclesForProductivity { get; set; }
        public string mCanChangePotential { get; set; }
        public string mMinPotential { get; set; }
        public string mMaxPotential { get; set; }
        public string mMaxPotentialIncreasePerCrystal { get; set; }
        public string mFluidStackSizeDefault { get; set; }
        public string mFluidStackSizeMultiplier { get; set; }
        public string OnReplicationDetailActorCreatedEvent { get; set; }
        public string mEffectUpdateInterval { get; set; }
        public string mCachedSkeletalMeshes { get; set; }
        public string mAddToSignificanceManager { get; set; }
        public string mSignificanceRange { get; set; }
        public string MaxRenderDistance { get; set; }
        public string mHighlightVector { get; set; }
        public string mFogPlaneTransforms { get; set; }
        public string mMaterialNameToInstanceManager { get; set; }
        public string mBlockSharingMaterialInstanceMapping { get; set; }
        public string mExcludeFromMaterialInstancing { get; set; }
        public string mSkipBuildEffect { get; set; }
        public string mBuildEffectSpeed { get; set; }
        public string mForceNetUpdateOnRegisterPlayer { get; set; }
        public string mToggleDormancyOnInteraction { get; set; }
        public string mShouldShowHighlight { get; set; }
        public string mAllowCleranceSeparationEvenIfStackedOn { get; set; }
        public string mInteractingPlayers { get; set; }
        public string mIsUseable { get; set; }
        public string mHideOnBuildEffectStart { get; set; }
        public string mShouldModifyWorldGrid { get; set; }
        public string mProductionEffectsRunning { get; set; }
        public string mCurrentColor_VFX { get; set; }
        public string CurrentPackagingMode { get; set; }
        public string mCurrentColorVFX { get; set; }
        public string m_NotifyNameREferences { get; set; }
        public string mColor { get; set; }
        public string mIsRadioActive { get; set; }
        public string mStoppedProducingAnimationSounds { get; set; }
        public string mStoppedAkComponents { get; set; }
        public string mSocketStoppedAkComponents { get; set; }
        public string mHeight { get; set; }
        public string mUseStaticHeight { get; set; }
        public string mCanStack { get; set; }
        public string mStackHeight { get; set; }
        public string mBuildMenuPriority { get; set; }

        /// <summary>
        /// The items that are used to produce this recipe.
        /// </summary>
        [JsonProperty("mIngredients")]
        public string Ingredients { get; set; }

        /// <summary>
        /// The items that are produced by this recipe.
        /// </summary>
        [JsonProperty("mProduct")]
        public string Product { get; set; }

        public string mManufacturingMenuPriority { get; set; }
        

        /// <summary>
        /// The seconds it requires to produce the item.
        /// </summary>
        [JsonProperty("mManufactoringDuration")]
        public decimal ManufactoringDuration { get; set; }
        public string mManualManufacturingMultiplier { get; set; }
        public string mProducedIn { get; set; }
        public string mVariablePowerConsumptionConstant { get; set; }
        public string mVariablePowerConsumptionFactor { get; set; }
        public string mMeshLength { get; set; }
        public string mItemMeshMap { get; set; }
        public string mSplineData { get; set; }
        public string mSpeed { get; set; }
        public string mItems { get; set; }
        public string mMaxLength { get; set; }
        public string mLengthPerCost { get; set; }
        public string mConnections { get; set; }
        public string mPowerConnections { get; set; }
        public string mPowerPoleType { get; set; }
        public string mHasPower { get; set; }

        [JsonProperty("mHealthGain")]
        public decimal HealthGain { get; set; }
        public string mCustomHandsMeshScale { get; set; }
        public string mCustomRotation { get; set; }
        public string mCustomLocation { get; set; }
        public string mDecalSize { get; set; }
        public string mPingColor { get; set; }
        public string mCollectSpeedMultiplier { get; set; }
        public string mManualMiningAudioName { get; set; }
        public string mWorkBenchOccupied { get; set; }
        public string mWorkBenchFree { get; set; }
        public string Meshes { get; set; }
        public string mShipUpgradeLevel { get; set; }
        public string mStorageText { get; set; }
        public string mMamFreeText { get; set; }
        public string mMamOccupiedText { get; set; }
        public string mMapText { get; set; }
        public string mMeshes { get; set; }
        public string ABClass { get; set; }
        public string mSkeletalMeshSoftPtr { get; set; }
        public string mGenerators { get; set; }
        public string mStorageInventorySize { get; set; }
        public string mStorageVisibilityLevel { get; set; }
        public string mSpawningGroundZOffset { get; set; }
        public string mGroundSearchZDistance { get; set; }
        public string mDefaultResources { get; set; }
        public string mNeedPlayingBuildEffectNotification { get; set; }
        public string mMontageLength { get; set; }
        public string mInterpSawProgress { get; set; }
        public string mWasSawing { get; set; }
        public string mCurrentState { get; set; }
        public string mPlayingSound { get; set; }
        public string mCurrentAudioState { get; set; }
        public string mPreviousAudioState { get; set; }
        public string mEnergyConsumption { get; set; }
        public string mSawDownTreeTime { get; set; }
        public string mCollateralPickupRadius { get; set; }
        public string mExcludeChainsawableFoliage { get; set; }
        public string mEnergyStored { get; set; }
        public string mPlaceDistanceMax { get; set; }
        public string mImmunity { get; set; }
        public string mIsWorking { get; set; }
        public string mHasNegatedDamage { get; set; }
        public string mDamageNegated { get; set; }
        public string mFilterDuration { get; set; }
        public string mCountdown { get; set; }
        public string mDisableEffectTimer { get; set; }
        public string mSuit1PMeshMaterials { get; set; }
        public string mThrustPower { get; set; }
        public string mVelocityZExtreme { get; set; }
        public string mVelocityZExtremesDamper { get; set; }
        public string mJumpBeforeThrustTime { get; set; }
        public string mMaxFuel { get; set; }
        public string mCurrentFuel { get; set; }
        public string mFuelRegenRate { get; set; }
        public string mFuelConsumeRate { get; set; }
        public string mThrustCooldown { get; set; }
        public string mFuelWorth { get; set; }
        public string mPaidForFuel { get; set; }
        public string mThrustAirControl { get; set; }
        public string mDefaultAirControl { get; set; }
        public string mRTPCInterval { get; set; }
        public string mJumpTimeStamp { get; set; }
        public string mIsThrusting { get; set; }
        public string mSprintSpeedFactor { get; set; }
        public string mJumpSpeedFactor { get; set; }
        public string mMuteDryFire { get; set; }
        public string mRandomReloadAnim { get; set; }
        public string mRandomStingerAnim { get; set; }
        public string mProjectileData { get; set; }
        public string mMagSize { get; set; }
        public string mCurrentAmmo { get; set; }
        public string mReloadTime { get; set; }
        public string mFireRate { get; set; }
        public string mBlockSprintWhenFiring { get; set; }
        public string Fire { get; set; }
        public string mLockAngle { get; set; }
        public string mHasReloadedOnce { get; set; }
        public string mInstantHitDamage { get; set; }
        public string mWeaponTraceLength { get; set; }
        public string mRandomAttackAnim { get; set; }
        public string mSecondSwingMaxTime { get; set; }
        public string mSecondSwingCooldDownTime { get; set; }
        public string mDamage { get; set; }
        public string mAttackDistance { get; set; }
        public string mCurrentMontageSection { get; set; }
        public string mSecondAttackTimer { get; set; }
        public string mFirstAttackTimer { get; set; }
        public string mRandomEquipAnim { get; set; }
        public string mPostProcessEnabled { get; set; }
        public string m_DockingStates { get; set; }
        public string m_OffsetTime { get; set; }
        public string mDroneDockingStartLocationLocal { get; set; }
        public string mDroneDockingLocationLocal { get; set; }
        public string mBatteryClasses { get; set; }
        public string mDroneDockingQueue { get; set; }
        public string mStationHasDronesInQueue { get; set; }
        public string mItemTransferringStage { get; set; }
        public string mTransferProgress { get; set; }
        public string mTransferSpeed { get; set; }
        public string mStackTransferSize { get; set; }
        public string mDroneQueueRadius { get; set; }
        public string mDroneQueueSeparationRadius { get; set; }
        public string mDroneQueueVerticalSeparation { get; set; }
        public string mTripPowerCost { get; set; }
        public string mTripPowerPerMeterCost { get; set; }
        public string mTripInformationSampleCount { get; set; }
        public string mStorageSizeX { get; set; }
        public string mStorageSizeY { get; set; }
        public string mBatteryStorageSizeX { get; set; }
        public string mBatteryStorageSizeY { get; set; }
        public string mAllowedResourceForms { get; set; }
        public string mParticleMap { get; set; }
        public string CanPlayAfterStartUpStopped { get; set; }
        public string mExtractStartupTime { get; set; }
        public string mExtractStartupTimer { get; set; }
        public string mExtractCycleTime { get; set; }
        public string mItemsPerCycle { get; set; }
        public string mReplicatedFlowRate { get; set; }
        public string mOnlyAllowCertainResources { get; set; }
        public string mAllowedResources { get; set; }
        public string mExtractorTypeName { get; set; }
        public string mCanPlayAfterStartUpStopped { get; set; }
        public string mInternalMiningState_0 { get; set; }
        public string mToggleMiningStateHandle_0 { get; set; }
        public string mMinimumDrillTime_0 { get; set; }
        public string mMaximumDrillTime_0 { get; set; }
        public string mStackingHeight { get; set; }
        public string mInventorySizeX { get; set; }
        public string mInventorySizeY { get; set; }
        public string mOccupiedText { get; set; }
        public string Tier { get; set; }
        public string mCachedLoadPercentage { get; set; }
        public string mFuelClasses { get; set; }
        public string mDefaultFuelClasses { get; set; }
        public List<MFuel> mFuel { get; set; }
        public string mAvailableFuelClasses { get; set; }
        public string mFuelResourceForm { get; set; }
        public string mFuelLoadAmount { get; set; }
        public string mRequiresSupplementalResource { get; set; }
        public string mSupplementalLoadAmount { get; set; }
        public string mSupplementalToPowerRatio { get; set; }
        public string mIsFullBlast { get; set; }
        public string mCachedInputConnections { get; set; }
        public string mCachedPipeInputConnections { get; set; }
        public string mPowerProduction { get; set; }
        public string mPowerProductionExponent { get; set; }
        public string mLoadPercentage { get; set; }
        public string m_SFXSockets { get; set; }
        public string m_CurrentPotential { get; set; }
        public string mCurrentResearchState { get; set; }
        public string mWidth { get; set; }
        public string JumpForceCharacter { get; set; }
        public string JumpForcePhysics { get; set; }
        public string mDampeningFactor { get; set; }
        public string mPlayerList { get; set; }
        public string mSize { get; set; }
        public string mElevation { get; set; }
        public string mDisableSnapOn { get; set; }
        public string mDisableAttachmentSnapOn { get; set; }
        public string mLastFlowUpdate { get; set; }
        public string mUpdateFlowTime { get; set; }
        public string mAnimSpeed { get; set; }
        public string mLastFlowValue { get; set; }
        public string mTimeScaleOffset { get; set; }
        public string mMaxPressure { get; set; }
        public string mDesignPressure { get; set; }
        public string mDefaultFlowLimit { get; set; }
        public string mUserFlowLimit { get; set; }
        public string mMinimumFlowPercentForStandby { get; set; }
        public string mIndicatorData { get; set; }
        public string mRadius { get; set; }
        public string mFluidBoxVolume { get; set; }
        public string mFluidBox { get; set; }
        public string mPipeConnections { get; set; }
        public string mIsPipePumpPlaying { get; set; }
        public string mIsExceedingHeadLift { get; set; }
        public string mCurrentAudioHeadLift { get; set; }
        public string mPistonAudioTimer { get; set; }
        public string mLength { get; set; }
        public string mVerticalAngle { get; set; }
        public string mFlowLimit { get; set; }
        public string mFlowIndicatorMinimumPipeLength { get; set; }
        public string mMaxIndicatorTurnAngle { get; set; }
        public string mIgnoreActorsForIndicator { get; set; }
        public string mFluidNames { get; set; }
        public string mCurrentFluid { get; set; }
        public string mQuantiziedContent { get; set; }
        public string mQuantiziedFlow { get; set; }
        public string mRattleLimit { get; set; }
        public string mIsRattling { get; set; }
        public string mStorageCapacity { get; set; }
        public string mWaterpumpTimeline_RTPC_B8FA6F944E717E3B7A286E84901F620E { get; set; }
        public string mWaterpumpTimeline__Direction_B8FA6F944E717E3B7A286E84901F620E { get; set; }
        public string PlayPitchAndVolumeRTPCTimeline_RTPC_2B435F41466C37D2AD809A88AA21BA89 { get; set; }
        public string PlayPitchAndVolumeRTPCTimeline__Direction_2B435F41466C37D2AD809A88AA21BA89 { get; set; }
        public string mAudioTimerCounter { get; set; }
        public string mAudioTimerMS { get; set; }
        public string mAudioTimerReference { get; set; }
        public string mAudioTimelineCounter { get; set; }
        public string HasLostSignificance { get; set; }
        public string mMinimumDepthForPlacement { get; set; }
        public string mDepthTraceOriginOffset { get; set; }
        public string mFuelConsumption { get; set; }
        public string mInventorySize { get; set; }
        public string CurrentPotentialChangedDelegate { get; set; }
        public string ConnectedExtractorCountChangedDelegate { get; set; }
        public string mActivationStartupTime { get; set; }
        public string mActivationStartupTimer { get; set; }
        public string mSatelliteActivationComplete { get; set; }
        public string mSatelliteNodeCount { get; set; }
        public string mConnectedExtractorCount { get; set; }
        public string mDefaultPotentialExtractionPerMinute { get; set; }
        public string mSequenceDuration { get; set; }
        public string mLightningTimer { get; set; }
        public string mGameTimeAtProducing { get; set; }
        public string mCurrentProducingSeekTime { get; set; }
        public string mStartVector_VFX_Small_Start { get; set; }
        public string mStartVector_VFX_Small_End { get; set; }
        public string mStartVector_VFX_Medium_Start { get; set; }
        public string mStartVector_VFX_Medium_End { get; set; }
        public string mStartVector_VFX_Large_Start { get; set; }
        public string mStartVector_VFX_Large_End { get; set; }
        public string mEstimatedMininumPowerConsumption { get; set; }
        public string mEstimatedMaximumPowerConsumption { get; set; }
        public string mSpentFuelClass { get; set; }
        public string mAmountOfWaste { get; set; }
        public string mWasteLeftFromCurrentFuel { get; set; }
        public string mCurrentGeneratorNuclearWarning { get; set; }
        public string mMeshHeight { get; set; }
        public string mTopTransform { get; set; }
        public string mIsReversed { get; set; }
        public string OnSortRulesChangedDelegate { get; set; }
        public string mMaxNumSortRules { get; set; }
        public string mLastItem { get; set; }
        public string mItemToLastOutputMap { get; set; }
        public string mLastOutputIndex { get; set; }
        public string mCurrentOutputIndex { get; set; }
        public string mCurrentInventoryIndex { get; set; }
        public string mDistributionTable { get; set; }
        public string mCurrentInputIndex { get; set; }
        public string mScreenUpdateTimer { get; set; }
        public string mScanlineLerpT { get; set; }
        public string mScreenUpdateTime { get; set; }
        public string mNormalizedCloesnessToObject { get; set; }
        public string mObjectIsWithinRange { get; set; }
        public string mBeepDelayMax { get; set; }
        public string mBeepDelayMin { get; set; }
        public string mDetectionRange { get; set; }
        public string mUpdateClosestObjectTime { get; set; }
        public string mObjectDetails { get; set; }
        public string mShouldBeepEvenIfNoObject { get; set; }
        public string mPowerBankCapacity { get; set; }
        public string mLaunchPowerCost { get; set; }
        public string mChargeRateMultiplier { get; set; }
        public string mCurrentPowerLevel { get; set; }
        public string mLaunchVelocity { get; set; }
        public string mLaunchAngle { get; set; }
        public string mPlayerChainJumpResetTime { get; set; }
        public string mHasPowerForLaunch { get; set; }
        public string ComponentsToLaunch { get; set; }
        public string CharactersToLaunch { get; set; }
        public string VehiclesToLaunch { get; set; }
        public string mTrajectoryData { get; set; }
        public string mTrajectoryMeshScale { get; set; }
        public string mTrajectoryMeshRotation { get; set; }
        public string mDestinationMeshHeightOffset { get; set; }
        public string mNumArrows { get; set; }
        public string mKillTimer { get; set; }
        public string mTrajectoryGravityMultiplier { get; set; }
        public string mShowTrajectoryCounter { get; set; }
        public string IsAnimationProducing { get; set; }
        public string EnableTickGrinder { get; set; }
        public string EnableTickEngine { get; set; }
        public string mGrinderInterpDuration { get; set; }
        public string mEngineInterpDuration { get; set; }
        public string mProcessingTime { get; set; }
        public string mProducingTimer { get; set; }
        public string mShopInventoryDefaultSize { get; set; }
        public string mFuelTransferSpeed { get; set; }
        public string InterpolateEngineSound_InterpolateEngineAlpha_064FA8194B7224F6F187999413D1C8A6 { get; set; }
        public string InterpolateEngineSound__Direction_064FA8194B7224F6F187999413D1C8A6 { get; set; }
        public string mWindDirectionFromTurbine { get; set; }

        [JsonProperty("mIsWindSoundPlaying?")]
        public string MIsWindSoundPlaying { get; set; }
        public string AudioCounterTimer { get; set; }
        public string IsEnginePlaying { get; set; }
        public string mOpeningOffset { get; set; }
        public string mInitialMinSpeedFactor { get; set; }
        public string m_PreviousBatteryStatus { get; set; }
        public string mCurrentGameTimeSinceStateChange { get; set; }
        public string mActivationEventID { get; set; }
        public string mBatteryStatus { get; set; }
        public string mPowerStore { get; set; }
        public string mPowerStoreCapacity { get; set; }
        public string mPowerInputCapacity { get; set; }
        public string mIndicatorLevelMax { get; set; }
        public string mIndicatorLevel { get; set; }
        public string mPotentialDockers { get; set; }
        public string mFreightCargoType { get; set; }
        public string mCanUnloadAny { get; set; }
        public string mIsFullUnload { get; set; }
        public string mCanLoadAny { get; set; }
        public string mIsFullLoad { get; set; }
        public string mTimeToCompleteLoad { get; set; }
        public string mTimeToSwapLoadVisibility { get; set; }
        public string mTimeToCompleteUnload { get; set; }
        public string mTimeToSwapUnloadVisibility { get; set; }
        public string mStorageInputConnections { get; set; }
        public string mShouldExecuteLoadOrUnload { get; set; }
        public string mSwapCargoVisibilityTimerHandle { get; set; }
        public string mReplicatedOutflowRate { get; set; }
        public string mReplicatedInflowRate { get; set; }
        public string mPlatformConnections { get; set; }
        public string mIsOrientationReversed { get; set; }
        public string mPlatformDockingStatus { get; set; }
        public string mSavedDockingStatus { get; set; }
        public string mDockingSequenceTimerHandle { get; set; }
        public string mDockedPlatformList { get; set; }
        public string mIsOwnedByPlatform { get; set; }
        public string mHoverPackActiveTimer { get; set; }
        public string mCurrentPlayerVelocity { get; set; }
        public string mCurrentMouseDelta { get; set; }
        public string mHoverpackJoystickTimer { get; set; }
        public string mCurrentBatteryPowerLevel { get; set; }
        public string m_PreviousHoverMode { get; set; }
        public string ConnectionLocationUpdatedDelegate { get; set; }
        public string ConnectionStatusUpdatedDelegate { get; set; }
        public string HoverModeChangedDelegate { get; set; }
        public string RangeWarningToggleDelegate { get; set; }
        public string mHoverSpeed { get; set; }
        public string mHoverAccelerationSpeed { get; set; }
        public string mHoverSprintMultiplier { get; set; }
        public string mRailRoadSurfSpeed { get; set; }
        public string mRailroadSurfSensitivity { get; set; }
        public string mHoverFriction { get; set; }
        public string mJumpKeyHoldActivationTime { get; set; }
        public string mFallSpeedLimitWhenPowered { get; set; }
        public string mPowerConnectionSearchRadius { get; set; }
        public string mPowerConnectionSearchTickRate { get; set; }
        public string mPowerConnectionDisconnectionTime { get; set; }
        public string mPowerCapacity { get; set; }
        public string mPowerDrainRate { get; set; }
        public string mRangeWarningNormalizedDistanceThreshold { get; set; }
        public string mCurrentHoverMode { get; set; }
        public string mHasConnection { get; set; }
        public string mShouldAutomaticallyHoverWhenConnected { get; set; }
        public string mCrouchHoverCancelTime { get; set; }
        public string mCharacterUseDistanceWhenActive { get; set; }
        public string mCurrentConnectionLocation { get; set; }
        public string mShouldPlayDeactivateSound { get; set; }
        public string mZiplineJumpLaunchVelocity { get; set; }
        public string mMaxZiplineAngle { get; set; }
        public string mTraceDistance { get; set; }
        public string mTraceStartOffset { get; set; }
        public string mTraceRadius { get; set; }
        public string mVisualizeTraceDistance { get; set; }
        public string mZiplineReattachCooldown { get; set; }
        public string mTextRenderers { get; set; }
        public string bIsSignificant { get; set; }
        public string mMaxCharacters { get; set; }
        public string mOnIsSwitchOnChanged { get; set; }
        public string mOnIsConnectedChanged { get; set; }
        public string mIsSwitchOn { get; set; }
        public string mHasBuildingTag { get; set; }
        public string mBuildingTag { get; set; }
        public string mIsBridgeConnected { get; set; }
        public string mVariablePowerProductionConstant { get; set; }
        public string mVariablePowerProductionFactor { get; set; }
        public string mVariablePowerProductionCycleLength { get; set; }
        public string mMinPowerProduction { get; set; }
        public string mMaxPowerProduction { get; set; }
        public string mVariablePowerProductionCycleOffset { get; set; }
        public string mPrimaryColor { get; set; }
        public string mSecondaryColor { get; set; }
        public string mRedundantTargetCrosshairColor { get; set; }
        public string mNoTargetCrosshairColor { get; set; }
        public string mNonColorableTargetCrosshairColor { get; set; }
        public string mColorSlot { get; set; }
        public string mTerminalVelocityZ { get; set; }
        public string mIsDeployed { get; set; }
        public string OnRadarTowerRadiusUpdated { get; set; }
        public string mMinRevealRadius { get; set; }
        public string mMaxRevealRadius { get; set; }
        public string mNumRadarExpansionSteps { get; set; }
        public string mRadarExpansionInterval { get; set; }
        public string mCurrentExpansionStep { get; set; }
        public string mTimeToNextExpansion { get; set; }
        public string mExplosiveData { get; set; }
        public string mDispensedExplosives { get; set; }
        public string mIsPendingExecuteFire { get; set; }
        public string mMaxChargeTime { get; set; }
        public string mMaxThrowForce { get; set; }
        public string mMinThrowForce { get; set; }
        public string mDelayBetweenExplosions { get; set; }
        public string mTimeToProduceItem { get; set; }
        public string mEventType { get; set; }
        public string mIsEnabled { get; set; }
        public string mLightControlData { get; set; }
        public string mIsDay { get; set; }
        public string mMaxSegmentCount { get; set; }
        public string mNumSegments { get; set; }
        public string mLadderMeshes { get; set; }
        public string mFixtureAngle { get; set; }
        public string mOnControlledBuildablesChanged { get; set; }
        public string mControlledBuildables { get; set; }
    }

    public class Root
    {
        public string NativeClass { get; set; }
        public List<Class> Classes { get; set; }
    }


}
