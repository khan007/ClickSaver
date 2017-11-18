// ClickDLL.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "ClickDLL.h"
#include "..\ClickSaver\clicksaver.h"
#include <string>
#include <iostream>

using namespace std;

// This is an example of an exported variable
extern CLICKDLL_API int nClickDLL=0;

// This is an example of an exported function.
extern CLICKDLL_API int fnClickDLL()
{
	/*string strMytestString("hello world");
	cout << strMytestString;

	OutputDebugStringW(L"My output string.");

	AllocConsole();
	freopen("CONIN$", "r", stdin);
	freopen("CONOUT$", "w", stdout);
	freopen("CONOUT$", "w", stderr);*/

	Start();
    return 42;
}

// This is the constructor of a class that has been exported.
// see ClickDLL.h for the class definition
CClickDLL::CClickDLL()
{	
    return;
}
