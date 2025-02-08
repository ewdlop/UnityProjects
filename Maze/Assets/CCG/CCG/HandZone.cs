. Uses the Actors type as the parent class.
	 * @param Path					The path to use when creating the package for the new blueprint
	 * @param Actor					The actor to use as the template for the blueprint
	 * @param Params				The parameter struct of additional behaviors
	 * @return The blueprint created from the actor
	 */
	static UBlueprint* CreateBlueprintFromActor(const FString& Path, AActor* Actor, const FCreateBlueprintFromActorParams& Params = FCreateBlueprintFromActorParams());

	/** 
	 * Take an Actor and generate a blueprint based on it. Uses the Actors type as the parent class. 
	 * @param BlueprintName			The name to use for the Blueprint
	 * @param Outer					The outer object to create the blueprint within
	 * @param Actor					The actor to use as the template for the blueprint
	 * @param Params				The parameter struct of additional behaviors
	 * @return The blueprint created from the actor
	 */
	static UBlueprint* CreateBlueprintFromActor(const FName BlueprintName, UObject* Outer, AActor* Actor, const FCreateBlueprintFromActorParams& Params = FCreateBlueprintFromActorParams());

	/** 
	 * Take an Actor and generate a blueprint based on it. Uses the Actors type as the parent class. 
	 * @param Path					The path to use when creating the package for the new blueprint
	 * @param Actor					The actor to use as the template for the blueprint
	 * @param bReplaceActor			If true, replace the actor in the scene with one based on the created blueprint
	 * @param bKeepMobility			If true, The mobility of each actor components will be copy
	 * @param bOpenInEditor			If true, open the created blueprint in the blueprint editor
	 * @return The blueprint created from the actor
	 */
	UE_DEPRECATED(4.26, "Use version that passes parameters via parameter struct")
	static UBlueprint* CreateBlueprintFromActor(const FString& Path, AActor* Actor, bool bReplaceActor, bool bKeepMobility = false, UClass* ParentClassOverride = nullptr, bool bOpenInEditor = true)
	{
		FCreateBlueprintFromActorParams Params;
		Params.bReplaceActor = bReplaceActor;
		Params.bKeepMobility = bKeepMobility;
		Params.bOpenBlueprint = bOpenInEditor;
		Params.ParentClassOverride = ParentClassOverride;
		return CreateBlueprintFromActor(Path, Actor, Params);
	}

	/** 
	 * Take an Actor and generate a blueprint based on it. Uses the Actors type as the parent class. 
	 * @param BlueprintName			The name to use for the Blueprint
	 * @param Outer					The outer object to create the blueprint within
	 * @param Actor					The actor to use as the template for the blueprint
	 * @param bReplaceActor			If true, replace the actor in the scene with one based on the created blueprint
	 * @param bKeepMobility			If true, The mobility of each actor components will be copy
	 * @param ParentClassOverride	The parent class to use when creating the blueprint. If null, the class of Actor will be used. If specified, must be a subclass of the Actor's class.
	 * @param bOpenInEditor			If true, open the created blueprint in the blueprint editor
	 * @return The blueprint created from the actor
	 */
	UE_DEPRECATED(4.26, "Use version that passes parameters via parameter struct")
	static UBlueprint* CreateBlueprintFromActor(const FName BlueprintName, UObject* Outer, AActor* Actor, bool bReplaceInWorld, bool bKeepMobility = false, UClass* ParentClassOverride = nullptr, bool bOpenInEditor = true)
	{
		FCreateBlueprintFromActorParams Params;
		Params.bReplaceActor = bReplaceInWorld;
		Params.bKeepMobility = bKeepMobility;
		Params.bOpenBlueprint = bOpenInEditor;
		Params.ParentClassOverride = ParentClassOverride;
		return CreateBlueprintFromActor(BlueprintName, Outer, Actor, Params);
	}

	/** Parameter struct for customizing calls to CreateBlueprintFromActors */
	struct FCreateBlueprintFromActorsParams
	{
		FCreateBlueprintFromActorsParams(const TArray<AActor*>& Actors)
			: RootActor(nullptr)
			, AdditionalActors(Actors)
			, bReplaceActors(true)
			, bDeferCompilation(false)
			, bOpenBlueprint(true)
			, ParentClass(AActor::StaticClass())
		{
		}

		FCreateBlueprintFromActorsParams(AActor* RootActor, const TArray<AActor*>& ChildActors)
			: RootActor(RootActor)
			, AdditionalActors(ChildActors)
			, bReplaceActors(true)
			, bDeferCompilation(false)
			, bOpenBlueprint(true)
			, ParentClass(RootActor->GetClass())
		{
		}

		/** Optional Actor to use as the template for the blueprint */
		AActor* RootActor;

		/** The actors to use when creating child actor components */
		const TArray<AActor*>& AdditionalActors;

		/** If true, replace the actors in the scene with one based on the created blueprint */
		bool bReplaceActors;

		/** Puts off compilation of the blueprint as additional manipulation is going to be done before it compiles */
		bool bDeferCompilation;

		/** Whether the newly created blueprint should be opened in the editor */
		bool bOpenBlueprint;

		/** The parent class to use when creating the blueprint. If a RootActor is specified, the class must be a subclass of the RootActor's class */
		UClass* ParentClass;
	};

	/**
	 * Take a collection of Actors and generate a blueprint based on it
	 * @param Path					The path to use when creating the package for the new blueprint
	 * @param Params				The parameter struct containing actors and additional behavior definitions
	 * @return The blueprint created from the actor
	 */
	static UBlueprint* CreateBlueprintFromActors(const FString& Path, const FCreateBlueprintFromActorsParams& Params);

	/**
	 * Take a collection of Actors and generate a blueprint based on it
	 * @param BlueprintName			The name to use for the Blueprint
	 * @param Package				The package to create the blueprint within
	 * @param Params				The parameter struct containing actors and additional behavior definitions
	 * @return The blueprint created from the actor
	 */
	static UBlueprint* CreateBlueprintFromActors(const FName BlueprintName, UPackage* Package, const FCreateBlueprintFromActorsParams& Params);

	/** 
	 * Take a list of Actors and generate a blueprint based on it using the Actors as templates for child actor components.
	 * @param Path					The path to use when creating the package for the new blueprint
	 * @param Actors				The actors to use when creating child actor components
	 * @param bReplaceActor			If true, replace the actor in the scene with one based on the created blueprint
	 * @param ParentClass			The parent class to use when creating the blueprint.
	 * @param bOpenInEditor			If true, open the created blueprint in the blueprint editor
	 * @return The blueprint created from the actor
	 */
	UE_DEPRECATED(4.26, "Use version that passes parameters via parameter struct")
	static UBlueprint* CreateBlueprintFromActors(const FString& Path, const TArray<AActor*>& Actors, bool bReplaceActor, UClass* ParentClass = AActor::StaticClass(), bool bOpenInEditor = true)
	{
		FCreateBlueprintFromActorsParams Params(Actors);
		Params.bReplaceActors = bReplaceActor;
		Params.ParentClass = ParentClass;
		Params.bOpenBlueprint = bOpenInEditor;
		return CreateBlueprintFromActors(Path, Params);
	}

	/** 
	 * Take a list of Actors and generate a blueprint based on it using the Actors as templates for child actor components.
	 * @param BlueprintName			The name to use for the Blueprint
	 * @param Outer					The package to create the blueprint within
	 * @param Actors				The actors to use when creating child actor components
	 * @param bReplaceActor			If true, replace the actor in the scene with one based on the created blueprint
	 * @param bKeepMobility			If true, The mobility of each actor components will be copy
	 * @param ParentClass			The parent class to use 