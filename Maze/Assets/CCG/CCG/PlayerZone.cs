ses parameters via parameter struct")
	static UBlueprint* HarvestBlueprintFromActors(const FName BlueprintName, UPackage* Package, const TArray<AActor*>& Actors, bool bReplaceInWorld, UClass* ParentClass = AActor::StaticClass())
	{
		FHarvestBlueprintFromActorsParams Params;
		Params.bReplaceActors = bReplaceInWorld;
		Params.ParentClass = ParentClass;
		return HarvestBlueprintFromActors(BlueprintName, Package, Actors, Params);
	}

	/** 
	 * Creates a new blueprint instance and replaces the provided actor list with the new actor
	 * @param Blueprint             The blueprint class to create an actor instance from
	 * @param SelectedActors        The list of currently selected actors in the editor
	 * @param Location              The location of the newly created actor
	 * @param Rotator               The ro