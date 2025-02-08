ility;

		/** Which SCSNode to attach the new components to, if null attachment will be to Root */
		USCS_Node* OptionalNewRootNode;

		/** Optional pointer to an array for the caller to get a list of the created SCSNodes */
		TArray<USCS_Node*>* OutNodes;
	};

	/** Take a list of components that belong to a single Actor and add them to a blueprint as SCSNodes */
	static void AddComponentsToBlueprint(UBlueprint* Blueprint, const TArray<UActorComponent*>& Components, const FAddComponentsToBlueprintParams& Params = FAddComponentsToBlueprintParams());

	UE_DEPRECATED(4.26, "Use version that uses parameter struct instead of individual parameters")
	static void AddComponentsToBlueprint(UBlueprint* Blueprint, const TArray<UActorComponent*>& Components, EAddComponentToBPHarvestMode HarvestMode, USCS_Node* OptionalNewRootNode = nullptr, bool bKeepMobility = false)
	{
		FAddComponentsToBlueprintParams Params;
		Params.HarvestMode = HarvestMode;
		Params.OptionalNewRootNode = OptionalNewRootNode;
		Params.bKeepMobility = bKeepMobility;
		AddComponentsToBlueprint(Blueprint, Components, Params);
	}

	UE_DEPRECATED(4.25, "Use version that specifies harvest mode via enum instead of boolean")
	static void AddComponentsToBlueprint(UBlueprint* Blueprint, const TArray<UActorComponent*>& Components, bool bHarvesting, 