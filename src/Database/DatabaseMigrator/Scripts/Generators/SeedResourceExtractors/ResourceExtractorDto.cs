using Newtonsoft.Json;

namespace DatabaseMigrator.Scripts.Generators.ResourceExtractorSeed
{
    public class ResourceExtractorDto
    {
        [JsonProperty("ClassName")]
        public string ClassName { get; set; }

        //[JsonProperty("mParticleMap")]
        //public string MParticleMap { get; set; } // Skip--seems to be about vfx or something similar

        //[JsonProperty("CanPlayAfterStartUpStopped")]
        //public string CanPlayAfterStartUpStopped { get; set; } // Skip -- doesn't seem relevant to planning

        //[JsonProperty("mCanPlayAfterStartUpStopped")]
        //public string MCanPlayAfterStartUpStopped { get; set; } // Skip -- doesn't seem relevant to planning

        //[JsonProperty("mExtractStartupTime")]
        //public string ExtractStartupTime { get; set; }

        //[JsonProperty("mExtractStartupTimer")]
        //public string ExtractStartupTimer { get; set; }

        [JsonProperty("mExtractCycleTime")]
        public decimal ExtractCycleTime { get; set; }

        [JsonProperty("mItemsPerCycle")]
        public decimal ItemsPerCycle { get; set; }

        //[JsonProperty("mPipeOutputConnections")]
        //public string MPipeOutputConnections { get; set; }

        //[JsonProperty("mReplicatedFlowRate")]
        //public string MReplicatedFlowRate { get; set; }

        [JsonProperty("mAllowedResourceForms")]
        public string AllowedResourceForms { get; set; }

        //[JsonProperty("mOnlyAllowCertainResources")]
        //public string MOnlyAllowCertainResources { get; set; }

        //[JsonProperty("mAllowedResources")]
        //public string MAllowedResources { get; set; }

        //[JsonProperty("mExtractorTypeName")]
        //public string MExtractorTypeName { get; set; }

        [JsonProperty("mPowerConsumption")]
        public decimal PowerConsumption { get; set; }

        [JsonProperty("mPowerConsumptionExponent")]
        public decimal PowerConsumptionExponent { get; set; }

        //[JsonProperty("mDoesHaveShutdownAnimation")]
        //public string MDoesHaveShutdownAnimation { get; set; }

        //[JsonProperty("mOnHasPowerChanged")]
        //public string MOnHasPowerChanged { get; set; }

        //[JsonProperty("mOnHasProductionChanged")]
        //public string MOnHasProductionChanged { get; set; }

        //[JsonProperty("mOnHasStandbyChanged")]
        //public string MOnHasStandbyChanged { get; set; }

        //[JsonProperty("mMinimumProducingTime")]
        //public string MMinimumProducingTime { get; set; }

        //[JsonProperty("mMinimumStoppedTime")]
        //public string MMinimumStoppedTime { get; set; }

        //[JsonProperty("mNumCyclesForProductivity")]
        //public string MNumCyclesForProductivity { get; set; }

        //[JsonProperty("mCanChangePotential")]
        //public bool CanChangePotential { get; set; }

        [JsonProperty("mMinPotential")]
        public decimal MinPotential { get; set; }

        [JsonProperty("mMaxPotential")]
        public decimal MaxPotential { get; set; }

        [JsonProperty("mMaxPotentialIncreasePerCrystal")]
        public decimal MaxPotentialIncreasePerCrystal { get; set; }

        //[JsonProperty("mFluidStackSizeDefault")]
        //public string MFluidStackSizeDefault { get; set; } // not sure purpose and all are the same

        //[JsonProperty("mFluidStackSizeMultiplier")]
        //public string MFluidStackSizeMultiplier { get; set; } // not sure purpose and all are the same

        //[JsonProperty("OnReplicationDetailActorCreatedEvent")]
        //public string OnReplicationDetailActorCreatedEvent { get; set; } // rendering

        //[JsonProperty("mEffectUpdateInterval")]
        //public string MEffectUpdateInterval { get; set; } // rendering

        //[JsonProperty("mCachedSkeletalMeshes")]
        //public string MCachedSkeletalMeshes { get; set; } // rendering

        //[JsonProperty("mAddToSignificanceManager")]
        //public string MAddToSignificanceManager { get; set; } // rendering

        //[JsonProperty("mSignificanceRange")]
        //public string MSignificanceRange { get; set; } // rendering

        [JsonProperty("mDisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("mDescription")]
        public string Description { get; set; }

        //[JsonProperty("MaxRenderDistance")]
        //public string MaxRenderDistance { get; set; } // rendering

        //[JsonProperty("mHighlightVector")]
        //public string MHighlightVector { get; set; } // rendering

        //[JsonProperty("mAllowColoring")]
        //public bool AllowColoring { get; set; }

        //[JsonProperty("mSkipBuildEffect")]
        //public string MSkipBuildEffect { get; set; }

        //[JsonProperty("mBuildEffectSpeed")]
        //public string MBuildEffectSpeed { get; set; }

        //[JsonProperty("mForceNetUpdateOnRegisterPlayer")]
        //public string MForceNetUpdateOnRegisterPlayer { get; set; }

        //[JsonProperty("mToggleDormancyOnInteraction")]
        //public string MToggleDormancyOnInteraction { get; set; }

        //[JsonProperty("mShouldShowHighlight")]
        //public string MShouldShowHighlight { get; set; }

        //[JsonProperty("mShouldShowAttachmentPointVisuals")]
        //public string MShouldShowAttachmentPointVisuals { get; set; }

        //[JsonProperty("mCreateClearanceMeshRepresentation")]
        //public string MCreateClearanceMeshRepresentation { get; set; }

        //[JsonProperty("mAttachmentPoints")]
        //public string MAttachmentPoints { get; set; }

        //[JsonProperty("mInteractingPlayers")]
        //public string MInteractingPlayers { get; set; }

        //[JsonProperty("mIsUseable")]
        //public string MIsUseable { get; set; }

        //[JsonProperty("mHideOnBuildEffectStart")]
        //public string MHideOnBuildEffectStart { get; set; }

        //[JsonProperty("mShouldModifyWorldGrid")]
        //public string MShouldModifyWorldGrid { get; set; }

        //[JsonProperty("mInternalMiningState_0")]
        //public string MInternalMiningState0 { get; set; }

        //[JsonProperty("mToggleMiningStateHandle_0")]
        //public string MToggleMiningStateHandle0 { get; set; }

        //[JsonProperty("mMinimumDrillTime_0")]
        //public string MMinimumDrillTime0 { get; set; }

        //[JsonProperty("mMaximumDrillTime_0")]
        //public string MMaximumDrillTime0 { get; set; }
    }
}
