// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "UObject/Object.h"
#include "UObject/Class.h"
#include "Templates/SubclassOf.h"
#include "GameFramework/Actor.h"
#include "Engine/Blueprint.h"
#include "Engine/BlueprintGeneratedClass.h"

class UToolMenu;
class IBlueprintEditor;
class UEdGraph;
class USCS_Node;
struct Rect;

enum class EBlueprintBytecodeRecompileOptions
{
	None = 0x0,

	// in batch compile mode we don't 'BroadcastCompiled/BroadcastBlueprintCompiled'
	BatchCompile           = 0x1,
	// normally we create a REINST_ version even when doing the bytecode compilation
	// this flag can be used of the blueprints GeneratedClass is being reinstanced by 
	// calling code:
	SkipReinstancing	   = 0x2  
};

ENUM_CLASS_FLAGS(EBlueprintBytecodeRecompileOptions)

enum class EBlueprintCompileOptions
{
	None = 0x0,

	/** This flag has several effects, but its behavior is to 'make things work' when regenerating a blueprint on load */
	IsRegeneratingOnLoad = 0x1,
	/** Skips garbage collection at the end of compile, useful if caller will collect garbage themselves */
	SkipGarbageCollection = 0x2,
	/** Prevents intermediate products from being garbage collected, useful for debugging macro/node expansion */
	SaveIntermediateProducts = 0x4,
	/** Indicates that the skeleton is up to date, and therefore the skeleton compile pass can be skipped */
	SkeletonUpToDate = 0x8,
	/** Indicates this is a batch compile and that BroadcastCompiled and BroadcastBlueprintCompiled should be skipped */
	BatchCompile = 0x10,
	/** Skips saving blueprints even if save on compile is enabled */
	SkipSave = 0x20,
	/** Skips creating a reinstancer and running reinstancing routines - useful if calling code is performing reinstancing */
	SkipReinstancing = 0x40,
	/** Simply regenerates the skeleton class */
	RegenerateSkeletonOnly = 0x80,
	/** Skips class-specific validation of the default object - in some cases we may not have a fully-initialized CDO after reinstancing */
	SkipDefaultObjectValidation = 0x100,
	/** Skips Find-in-Blueprint search data update - in some cases (e.g. creating new assets) this is being deferred until after compilation */
	SkipFiBSearchMetaUpdate = 0x200,
};

ENUM_CLASS_FLAGS(EBlueprintCompileOptions)

//////////////////////////////////////////////////////////////////////////
// FKismetEditorUtilities

class UNREALED_API FKismetEditorUtilities
{
public:
	/** 
	 * Event that's broadcast anytime a Blueprint is created
	 */
	DECLARE_DELEGATE_OneParam(FOnBlueprintCreated, UBlueprint* /*InBlueprint*/);

	/** Manages the TargetClass and EventName to use for spawning default "ghost" nodes in a new Blueprint */
	struct FDefaultEventNodeData
	{
		/** If the new Blueprint is a child of the TargetClass an event will be attempted to be spawned.
		 *	Hiding the category and other things can prevent the event from being placed
		 */
		UClass* TargetClass;

		/** Event Name to spawn a node for */
		FName EventName;
	};

	/** Manages the TargetClass and EventName to use for spawning default "ghost" nodes in a new Blueprint */
	struct FOnBlueprintCreatedData
	{
		/** If the new Blueprint is a child of the TargetClass, the callback will be executed */
		UClass* TargetClass;

		/** Callback to execute */
		FOnBlueprintCreated OnBlueprintCreated;
	};

	/**
	 * Create a new Blueprint and initialize it to a valid state.
	 *
	 * @param ParentClass					the parent class of the new blueprint
	 * @param Outer							the outer object of the new blueprint
	 * @param NewBPName						the name of the new blueprint
	 * @param BlueprintType					the type of the new blueprint (normal, const, etc)
	 * @param BlueprintClassType			the actual class of the blueprint asset (UBlueprint or a derived type)
	 * @param BlueprintGeneratedClassType	the actual generated class of the blueprint asset (UBlueprintGeneratedClass or a derived type)
	 * @param CallingContext				the name of the calling method or module used to identify creation methods to engine analytics/usage stats (default None will be ignored)
	 * @return								the new blueprint
	 */
	static UBlueprint* CreateBlueprint(UClass* ParentClass, UObject* Outer, const FName NewBPName, enum EBlueprintType BlueprintType, TSubclassOf<UBlueprint> BlueprintClassType, TSubclassOf<UBlueprintGeneratedClass> BlueprintGeneratedClassType, FName CallingContext = NAME_None);

	/** 
	 * Event that's broadcast anytime a blueprint is unloaded, and becomes 
	 * invalid (with calls to ReplaceBlueprint(), for example).
	 */
	DECLARE_MULTICAST_DELEGATE_OneParam(FOnBlueprintUnloaded, UBlueprint*);
	static FOnBlueprintUnloaded OnBlueprintUnloaded;

	/** 
	 * Unloads the specified Blueprint (marking it pending-kill, and removing it 
	 * from its outer package). Then proceeds to replace all references with a
	 * copy of the one passed.
	 *
	 * @param  Target		The Blueprint you want to unload and replace.
	 * @param  Replacement	The Blueprint you cloned and used to replace Target.
	 * @return The duplicated replacement Blueprint.
	 */
	static UBlueprint* ReplaceBlueprint(UBlueprint* Target, UBlueprint const* Replacement);

	/** 
	 * Determines if the specified blueprint is referenced currently in the undo 
	 * buffer.
	 *
	 * @param  Blueprint	The Blueprint you want to query about.
	 * @return True if the Blueprint is saved in the undo buffer, false if not.
	 */
	static bool IsReferencedByUndoBuffer(UBlueprint* Blueprint);

	/** Create the correct event graphs for this blueprint */
	static void CreateDefaultEventGraphs(UBlueprint* Blueprint);

	/** Tries to compile a blueprint, updating any actors in the editor who are using the old class, etc... */
	static void CompileBlueprint(UBlueprint* BlueprintObj, EBlueprintCompileOptions CompileFlags = EBlueprintCompileOptions::None, class FCompilerResultsLog* pResults = nullptr );

	/** Generates a blueprint skeleton only.  Minimal compile, no notifications will be sent, no GC, etc.  Only successful if there isn't already a skeleton generated */
	static bool GenerateBlueprintSkeleton(UBlueprint* BlueprintObj, bool bForceRegeneration = false);

	/** Tries to make sure that a data-only blueprint is conformed to its native parent, in case any native class flags have changed */
	static void ConformBlueprintFlagsAndComponents(UBlueprint* BlueprintObj);

	/** @return true is it's possible to create a blueprint from the spec