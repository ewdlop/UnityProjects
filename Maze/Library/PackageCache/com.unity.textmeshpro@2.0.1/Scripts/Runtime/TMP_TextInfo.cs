// Copyright Epic Games, Inc. All Rights Reserved.

#include "PythonScriptPlugin.h"
#include "PythonScriptPluginSettings.h"
#include "PythonScriptRemoteExecution.h"
#include "PyGIL.h"
#include "PyCore.h"
#include "PySlate.h"
#include "PyEngine.h"
#include "PyEditor.h"
#include "PyConstant.h"
#include "PyConversion.h"
#include "PyMethodWithClosure.h"
#include "PyReferenceCollector.h"
#include "PyWrapperTypeRegistry.h"
#include "EngineAnalytics.h"
#include "Interfaces/IAnalyticsProvider.h"
#include "AssetRegistryModule.h"
#include "Modules/ModuleManager.h"
#include "UObject/PackageReload.h"
#include "Misc/App.h"
#include "Misc/CoreDelegates.h"
#include "Misc/Paths.h"
#include "Misc/FileHelper.h"
#include "Misc/PackageName.h"
#include "HAL/PlatformProcess.h"
#include "Containers/Ticker.h"
#include "Features/IModularFeatures.h"
#include "ProfilingDebugging/ScopedTimers.h"
#include "Stats/Stats.h"

#if WITH_EDITOR
#include "EditorSupportDelegates.h"
#include "DesktopPlatformModule.h"
#include "EditorStyleSet.h"
#include "Engine/Engine.h"
#include "Framework/Application/SlateApplication.h"
#include "Framework/MultiBox/MultiBoxBuilder.h"
#include "ToolMenus.h"
#include "Misc/ConfigCacheIni.h"
#include "Misc/MessageDialog.h"
#include "AssetViewUtils.h"
#include "IContentBrowserDataModule.h"
#include "ContentBrowserDataSubsystem.h"
#include "ContentBrowserFileDataCore.h"
#include "ContentBrowserFileDataSource.h"
#endif	// WITH_EDITOR

#if PLATFORM_WINDOWS
	#include <stdio.h>
	#include <fcntl.h>
	#include <io.h>
#endif	// PLATFORM_WINDOWS
#include <locale.h>

#define LOCTEXT_NAMESPACE "PythonScriptPlugin"

#if WITH_PYTHON

static PyUtil::FPyApiBuffer NullPyArg = PyUtil::TCHARToPyApiBuffer(TEXT(""));
static PyUtil::FPyApiChar* NullPyArgPtrs[] = { NullPyArg.GetData() };

/** Util struct to set the sys.argv data for Python when executing a file with arguments */
struct FPythonScopedArgv
{
	FPythonScopedArgv(const TCHAR* InArgs)
	{
		if (InArgs && *InArgs)
		{
			FString NextToken;
			while (FParse::Token(InArgs, NextToken, false))
			{
				PyCommandLineArgs.Add(PyUtil::TCHARToPyApiBuffer(*NextToken));
			}

			PyCommandLineArgPtrs.Reserve(PyCommandLineArgs.Num());
			for (PyUtil::FPyApiBuffer& PyCommandLineArg : PyCommandLineArgs)
			{
				PyCommandLineArgPtrs.Add(PyCommandLineArg.GetData());
			}
		}
		PySys_SetArgvEx(PyCommandLineArgPtrs.Num(), PyCommandLineArgPtrs.GetData(), 0);
	}

	~FPythonScopedArgv()
	{
		PySys_SetArgvEx(1, NullPyArgPtrs, 0);
	}

	TArray<PyUtil::FPyApiBuffer> PyCommandLineArgs;
	TArray<PyUtil::FPyApiChar*> PyCommandLineArgPtrs;
};

FPythonCommandExecutor::FPythonCommandExecutor(IPythonScriptPlugin* InPythonScriptPlugin)
	: PythonScriptPlugin(InPythonScriptPlugin)
{
}

FName FPythonCommandExecutor::StaticName()
{
	static const FName CmdExecName = TEXT("Python");
	return CmdExecName;
}

FName FPythonCommandExecutor::GetName() const
{
	return StaticName();
}

FText FPythonCommandExecutor::GetDisplayName() const
{
	return LOCTEXT("PythonCommandExecutorDisplayName", "Python");
}

FText FPythonCommandExecutor::GetDescription() const
{
	return LOCTEXT("PythonCommandExecutorDescription", "Execute Python scripts (including files)");
}

FText FPythonCommandExecutor::GetHintText() const
{
	return LOCTEXT("PythonCommandExecutorHintText", "Enter Python script or a filename to execute");
}

void FPythonCommandExecutor::GetAutoCompleteSuggestions(const TCHAR* Input, TArray<FString>& Out)
{
}

void FPythonCommandExecutor::GetExecHistory(TArray<FString>& Out)
{
	IConsoleManager::Get().GetConsoleHistory(TEXT("Python"), Out);
}

bool FPythonCommandExecutor::Exec(const TCHAR* Input)
{
	IConsoleManager::Get().AddConsoleHistoryEntry(TEXT("Python"), Input);

	UE_LOG(LogPython, Log, TEXT("%s"), Input);

	PythonScriptPlugin->ExecPythonCommand(Input);

	return true;
}

bool FPythonCommandExecutor::AllowHotKeyClose() const
{
	return false;
}

bool FPythonCommandExecutor::AllowMultiLine() const
{
	return true;
}

FPythonREPLCommandExecutor::FPythonREPLCommandExecutor(IPythonScriptPlugin* InPythonScriptPlugin)
	: PythonScriptPlugin(InPythonScriptPlugin)
{
}

FName FPythonREPLCommandExecutor::StaticName()
{
	static const FName CmdExecName = TEXT("PythonREPL");
	return CmdExecName;
}

FName FPythonREPLCommandExecutor::GetName() const
{
	return StaticName();
}

FText FPythonREPLCommandExecutor::GetDisplayName() const
{
	return LOCTEXT("PythonREPLCommandExecutorDisplayName", "Python (REPL)");
}

FText FPythonREPLCommandExecutor::GetDescription() const
{
	return LOCTEXT("PythonREPLCommandExecutorDescription", "Execute a single Python statement and show its result");
}

FText FPythonREPLCommandExecutor::GetHintText() const
{
	return LOCTEXT("PythonREPLCommandExecutorHintText", "Enter a single Python statement");
}

void FPythonREPLCommandExecutor::GetAutoCompleteSuggestions(const TCHAR* Input, TArray<FString>& Out)
{
}

void FPythonREPLCommandExecutor::GetExecHistory(TArray<FString>& Out)
{
	IConsoleManager::Get().GetConsoleHistory(TEXT("PythonREPL"), Out);
}

bool FPythonREPLCommandExecutor::Exec(const TCHAR* Input)
{
	IConsoleManager::Get().AddConsoleHistoryEntry(TEXT("PythonREPL"), Input);

	UE_LOG(LogPython, Log, TEXT("%s"), Input);

	FPythonCommandEx PythonCommand;
	PythonCommand.ExecutionMode = EPythonCommandExecutionMode::ExecuteStatement;
	PythonCommand.Command = Input;
	PythonScriptPlugin->ExecPythonCommandEx(PythonCommand);

	return true;
}

bool FPythonREPLCommandExecutor::AllowHotKeyClose() const
{
	return false;
}

bool FPythonREPLCommandExecutor::AllowMultiLine() const
{
	return true;
}

#if WITH_EDITOR
class FPythonCommandMenuImpl : public IPythonCommandMenu
{
public:
	FPythonCommandMenuImpl()
		: bRecentsFilesDirty(false)
	{
	}

	virtual void OnStartupMenu() override
	{
		LoadConfig();

		RegisterMenus();
	}

	virtual void OnShutdownMenu() override
	{
		UToolMenus::UnregisterOwner(this);

		// Write to file
		if (bRecentsFilesDirty)
		{
			SaveConfig();
			bRecentsFilesDirty = false;
		}
	}

	virtual void OnRunFile(const FString& InFile, bool bAdd) override
	{
		if (bAdd)
		{
			int32 Index = RecentsFiles.Find(InFile);
			if (Index != INDEX_NONE)
			{
				// If already in the list but not at the last position
				if (Index != RecentsFiles.Num() - 1)
				{
					RecentsFiles.RemoveAt(Index);
					RecentsFiles.Add(InFile);
					bRecentsFilesDirty = true;
				}
			}
			else
			{
				if (RecentsFiles.Num() >= MaxNumberOfFiles)
				{
					RecentsFiles.RemoveAt(0);
				}
				RecentsFiles.Add(InFile);
				bRecentsFilesDirty = true;
			}
		}
		else
		{
			if (RecentsFiles.RemoveSingle(InFile) > 0)
			{
				bRecentsFilesDirty = true;
			}
		}
	}

private:
	const TCHAR* STR_ConfigSection = TEXT("Python");
	const TCHAR* STR_ConfigDirectoryKey = TEXT("LastDirectory");
	const FName NAME_ConfigRecentsFilesyKey = TEXT("RecentsFiles");
	static const int32 MaxNumberOfFiles = 10;

	void LoadConfig()
	{
		RecentsFiles.Reset();

		GConfig->GetString(STR_ConfigSection, STR_ConfigDirectoryKey, LastDirectory, GEditorPerProjectIni);

		FConfigSection* Sec = GConfig->GetSectionPrivate(STR_ConfigSection, false, true, GEditorPerProjectIni);
		if (Sec)
		{
			TArray<FConfigValue> List;
			Sec->MultiFind(NAME_ConfigRecentsFilesyKey, List);

			int32 ListNum = FMath::Min(List.Num(), MaxNumberOfFiles);

			RecentsFiles.Reserve(ListNum);
			for (int32 Index = 0; Index < ListNum; ++Index)
			{
				RecentsFiles.Add(List[Index].GetValue());
			}
		}
	}

	void SaveConfig() const
	{
		GConfig->SetString(STR_ConfigSection, STR_ConfigDirectoryKey, *LastDirectory, GEditorPerProjectIni);

		FConfigSection* Sec = GConfig->GetSectionPrivate(STR_ConfigSection, true, false, GEditorPerProjectIni);
		if (Sec)
		{
			Sec->Remove(NAME_ConfigRecentsFilesyKey);
			for (int32 Index = RecentsFiles.Num() - 1; Index >= 0; --Index)
			{
				Sec->Add(NAME_ConfigRecentsFilesyKey, *RecentsFiles[Index]);
			}
		}

		GConfig->Flush(false);
	}

	void MakeRecentPythonScriptMenu(UToolMenu* InMenu)
	{
		FToolMenuOwnerScoped OwnerScoped(this);
		FToolMenuSection& Section = InMenu->AddSection("Files");
		for (int32 Index = RecentsFiles.Num() - 1; Index >= 0; --Index)
		{
			Section.AddMenuEntry(
				NAME_None,
				FText::FromString(RecentsFiles[Index]),
				FText::GetEmpty(),
				FSlateIcon(),
				FUIAction(FExecuteAction::CreateRaw(this, &FPythonCommandMenuImpl::Menu_ExecutePythonRecent, Index))
			);
		}

		FToolMenuSection& ClearSection = InMenu->AddSection("Clear");
		Section.AddMenuEntry(
			"ClearRecentPython",
			LOCTEXT("ClearRecentPython", "Clear Recent Python Scripts"),
			FText::GetEmpty(),
