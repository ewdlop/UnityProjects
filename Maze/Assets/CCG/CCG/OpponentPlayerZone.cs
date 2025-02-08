 ParentClass = AActor::StaticClass(), bool bOpenInEditor = true)
	{
		FCreateBlueprintFromActorsParams Params(Actors);
		Params.bReplaceActors = bReplaceInWorld;
		Params.ParentClass = ParentClass;
		Params.bOpenBlueprint = bOpenInEditor;
		return CreateBlueprintFromActors(BlueprintName, Package, Params);
	}

	/** Parameter struct for customizing calls to CreateBlueprintFromActor */
	struct FHarvestBlueprintFromActorsParams
	{
		FHarvestBlueprintFromActorsParams()
			: bReplaceActors(true)
			, bOpenBlueprint(true)
			, ParentClass(AActor::StaticClass())
		{
		}

		/** If true, replace the actors in the scene with one based on the created blueprint */
		bool bReplaceActors;

		/** Whether the newly created blueprint should be opened in the editor */
		bool bOpenBlueprint;

		/** The parent class to use when creating the blueprint. If a RootActor is specified, the class must be a subclass of the RootActor's class */
		UClass* ParentClass;
	};

	/**
	 * Take a list of Actors and generate a blueprint by harvesting the components they have. 
	 * @param Path					The path to use when creating the package for the new blueprint
	 * @param Actors				The actor list to use as the template for the new blueprint, typicall