#include "pch.h"
#include <time.h>

//using namespace Gdiplus;

#define EXPORT extern "C" __declspec(dllexport)

#define WSTYLE WS_CAPTION | WS_MINIMIZEBOX | WS_SYSMENU
//#define WSTYLE WS_POPUP | WS_VISIBLE

EXPORT HWND SetWindowC(const wchar_t* title, const wchar_t* ClassName, int width, int height)
{
	HWND hwnd{};
	WNDCLASSEX wcl{ sizeof(WNDCLASSEX) };
	wcl.lpszClassName = ClassName;
	wcl.cbClsExtra = 0;
	wcl.cbWndExtra = 0;
	wcl.hCursor = LoadCursorW(nullptr, IDC_ARROW);
	wcl.lpfnWndProc = [](HWND hwnd, UINT message, WPARAM wparam, LPARAM lparam)->LRESULT
	{
		switch (message)
		{
		case WM_DESTROY:
			PostQuitMessage(0);
			return 0;
		case WM_SYSKEYDOWN:
			return 0;
		default:
			return DefWindowProcW(hwnd, message, wparam, lparam);
		}
	};
	wcl.hIcon = LoadIcon(nullptr, IDI_APPLICATION);
	RegisterClassEx(&wcl);

	RECT rect;
	rect.left = 0;
	rect.top = 0;
	rect.right = width;
	rect.bottom = height;
	AdjustWindowRectEx(&rect, WSTYLE, FALSE, 0);
	if (rect.left < 0) rect.right += (rect.left * -1);
	if (rect.top < 0) rect.bottom += (rect.top * -1);
	hwnd = CreateWindowW(wcl.lpszClassName, title, WSTYLE, 10, 10, rect.right, rect.bottom, NULL, NULL, NULL, NULL);
	ShowWindow(hwnd, SW_SHOWNORMAL);
	//ShowWindow(hwnd, SW_SHOWMAXIMIZED);
	UpdateWindow(hwnd);
	return hwnd;
}
EXPORT HDC GetPaintStructure(HWND hwnd)
{
	return GetDC(hwnd);
}
EXPORT void Paint(HWND hwnd, HDC hdc)
{
	ReleaseDC(hwnd, hdc);
}
EXPORT int* GetMSG()
{
	int* message = new int[3];
	return message;
}
EXPORT bool CheckMessage(HWND hwnd, int* message)
{
	MSG msg;
	if (PeekMessageW(&msg, hwnd, 0, 0, PM_REMOVE))
	{
		TranslateMessage(&msg);
		DispatchMessageW(&msg);
		message[0] = msg.message;
		message[1] = msg.lParam;
		message[2] = msg.wParam;
		return true;
	}
	else return false;
}
EXPORT bool FinishCheckMessage(HWND* hwnds, int hwndcount, int* message)
{
	MSG msg;
	if (PeekMessageW(&msg, HWND_DESKTOP, 0, 0, PM_NOREMOVE))
	{
		bool get = true;
		for (int i = 0; i < hwndcount; i++)
		{
			if (msg.hwnd == hwnds[i])
			{
				get = false;
				break;
			}
		}
		if (get)
		{
			GetMessageW(&msg, HWND_DESKTOP, 0, 0);
			TranslateMessage(&msg);
			DispatchMessageW(&msg);
			message[0] = msg.message;
			message[1] = msg.lParam;
			message[2] = msg.wParam;
			message[3] = (int)msg.hwnd;
			return true;
		}
	}
	return false;
}
EXPORT void GetRect(HWND hwnd, int* lrect)
{
	RECT rectSize;
	GetClientRect(hwnd, &rectSize);
	POINT pointPos;
	pointPos.x = 0;
	pointPos.y = 0;
	ScreenToClient(hwnd, &pointPos);

	lrect[0] = -pointPos.x;
	lrect[1] = -pointPos.y;
	lrect[2] = rectSize.right - pointPos.x;
	lrect[3] = rectSize.bottom - pointPos.y;
}
EXPORT void SetPos(HWND hwnd, int X, int Y)
{
	SetWindowPos(hwnd, nullptr, X, Y, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
}
EXPORT void SetSize(HWND hwnd, int Width, int Height)
{
	RECT rect;
	rect.left = 0;
	rect.top = 0;
	rect.right = Width;
	rect.bottom = Height;
	MapDialogRect(hwnd, &rect);
	AdjustWindowRectEx(&rect, WSTYLE, FALSE, 0);
	if (rect.left < 0) rect.right += (rect.left * -1);
	if (rect.top < 0) rect.bottom += (rect.top * -1);
	SetWindowPos(hwnd, nullptr, 0, 0, rect.right, rect.bottom, SWP_NOMOVE | SWP_NOZORDER);
}
EXPORT void ShowC(BYTE* bmp, int width, int height, HDC hdc)
{
	BITMAPINFO bif;
	ZeroMemory(&bif, sizeof(BITMAPINFO));

	bif.bmiHeader.biSize = sizeof(bif);
	bif.bmiHeader.biWidth = width;
	bif.bmiHeader.biHeight = height;
	bif.bmiHeader.biSizeImage = width * height * 32;
	bif.bmiHeader.biPlanes = 1;
	bif.bmiHeader.biBitCount = sizeof(BYTE) * 32;

	SetDIBitsToDevice(hdc, 0, 0, width, height, 0, 0, 0, height, bmp, &bif, DIB_PAL_COLORS);
}
EXPORT UINT GetWindowState(HWND hwnd)
{
	WINDOWPLACEMENT wp{ sizeof(WINDOWPLACEMENT) };
	GetWindowPlacement(hwnd, &wp);
	return wp.showCmd;
}

EXPORT double TimeNow()
{
	return (double)clock() / CLOCKS_PER_SEC;
}
EXPORT float* GetFloatMem(int mem)
{
	return new float[mem];
}
EXPORT UINT* GetUintMem(int mem)
{
	return new UINT[mem];
}
EXPORT void DelMem(UINT* arr)
{
	delete arr;
}
EXPORT int GetKeyIsDown(int key)
{
	return GetKeyState(key);
}
EXPORT void GetMousePosC(int* pos)
{
	POINT point;
	GetCursorPos(&point);
	pos[0] = point.x;
	pos[1] = point.y;
}