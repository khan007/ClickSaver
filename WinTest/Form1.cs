using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinTest
{
    public partial class Form1 : Form
    {
        //public static unsafe class WrapCDll
        //{
            private const string DllName = @"ClickDLL.dll";

        //[DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int Start();
        
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern int Start();

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, EntryPoint = "?fnClickDLL@@YAHXZ")]
        public static extern int fnClickDLL();
        //}

        delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WS_OVERLAPPEDWINDOW = 0xcf0000;
        const UInt32 WS_VISIBLE = 0x10000000;
        const UInt32 CS_USEDEFAULT = 0x80000000;
        const UInt32 CS_DBLCLKS = 8;
        const UInt32 CS_VREDRAW = 1;
        const UInt32 CS_HREDRAW = 2;
        const UInt32 COLOR_WINDOW = 5;
        const UInt32 COLOR_BACKGROUND = 1;
        const UInt32 IDC_CROSS = 32515;
        const UInt32 WM_DESTROY = 2;
        const UInt32 WM_PAINT = 0x0f;
        const UInt32 WM_LBUTTONUP = 0x0202;
        const UInt32 WM_LBUTTONDBLCLK = 0x0203;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct WNDCLASSEX
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public int style;
            public IntPtr lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        public struct POINT
        {
            public Int32 x;
            public Int32 Y;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct MSG
        {
            public IntPtr hwnd;
            public UInt32 message;
            public UIntPtr wParam;
            public UIntPtr lParam;
            public UInt32 time;
            public POINT pt;
        }

        private WndProc delegWndProc = myWndProc;

        [DllImport("user32.dll")]
        static extern bool UpdateWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyWindow(IntPtr hWnd);

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType,
        IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr
           hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess,
           uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);


        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll", SetLastError = true)]
        static extern System.IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private unsafe static extern uint CreateThread(uint* lpThreadAttributes, uint dwStackSize, ThreadStart lpStartAddress, uint* lpParameter, uint dwCreationFlags, out uint lpThreadId);

        //[DllImport("AOHook.dll", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //static extern UInt32 HookManagerThread([Out] StringBuilder buffer);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "CreateWindowEx")]
        public static extern IntPtr CreateWindowEx(
          int dwExStyle,
          UInt16 regResult,
          //string lpClassName,
          string lpWindowName,
          UInt32 dwStyle,
          int x,
          int y,
          int nWidth,
          int nHeight,
          IntPtr hWndParent,
          IntPtr hMenu,
          IntPtr hInstance,
          IntPtr lpParam);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterClassEx")]
        static extern System.UInt16 RegisterClassEx([In] ref WNDCLASSEX lpWndClass);

        [DllImport("kernel32.dll")]
        static extern uint GetLastError();

        [DllImport("user32.dll")]
        static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32.dll")]
        static extern sbyte GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll")]
        static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll")]
        static extern IntPtr DispatchMessage([In] ref MSG lpmsg);


        const uint EVENT_OBJECT_NAMECHANGE = 0x800C;
        const uint WINEVENT_OUTOFCONTEXT = 0;

        // Need to ensure delegate is not collected while we're using it,
        // storing it in a class field is simplest way to do this.
        static WinEventDelegate procDelegate = new WinEventDelegate(WinEventProc);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Listen for name change changes across all processes/threads on current desktop...
            //IntPtr hhook = SetWinEventHook(EVENT_OBJECT_NAMECHANGE, EVENT_OBJECT_NAMECHANGE, IntPtr.Zero,
            //        procDelegate, 0, 0, WINEVENT_OUTOFCONTEXT);

            //// MessageBox provides the necessary mesage loop that SetWinEventHook requires.
            //// In real-world code, use a regular message loop (GetMessage/TranslateMessage/
            //// DispatchMessage etc or equivalent.)
            //MessageBox.Show("Tracking name changes on HWNDs, close message box to exit.");

            //UnhookWinEvent(hhook);

            //HWND AOWnd;
            //DWORD AOProcessId;
            ////HANDLE AOProcessHnd;

            //if (AOWnd = FindWindow("Anarchy client", null))
            //{
            //    char Temp[256];
            //    // Get process id
            //    GetWindowThreadProcessId(AOWnd, &AOProcessId);
            //    sprintf(Temp, "%s\\AOHook.dll", g_CSDir);
            //    if (!InjectDLL(AOProcessId, Temp))
            //    {
            //        // error...
            //    }
            //}


            WNDCLASSEX wind_class = new WNDCLASSEX();
            wind_class.cbSize = Marshal.SizeOf(typeof(WNDCLASSEX));
            wind_class.style = (int)(CS_HREDRAW | CS_VREDRAW | CS_DBLCLKS); //Doubleclicks are active
            wind_class.hbrBackground = (IntPtr)COLOR_BACKGROUND + 1; //Black background, +1 is necessary
            wind_class.cbClsExtra = 0;
            wind_class.cbWndExtra = 0;
            wind_class.hInstance = Marshal.GetHINSTANCE(this.GetType().Module); ;// alternative: Process.GetCurrentProcess().Handle;
            wind_class.hIcon = IntPtr.Zero;
            //wind_class.hCursor = LoadCursor(IntPtr.Zero, (int)IDC_CROSS);// Crosshair cursor;
            wind_class.lpszMenuName = null;
            wind_class.lpszClassName = "ClickSaverHookWindowClass";
            wind_class.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(delegWndProc);
            wind_class.hIconSm = IntPtr.Zero;

            //    wind_class.lpfnWndProc =  Marshal.GetFunctionPointerForDelegate((WndProc)((hWnd, message, wParam, lParam) =>
            //    {
            //        IntPtr hdc;

            //        //switch ((WM) message)  
            //        //{  
            //        //    WinAPI.BeginPaint(hWnd, out ps);  
            //        //    break;  
            //        //}  
            //        switch ((WM)message)
            //        {
            //            case WM.PAINT:

            //                return IntPtr.Zero;
            //                break;
            //            case WM.DESTROY:
            //                WinAPI.PostQuitMessage(0);
            //                return IntPtr.Zero;
            //                break;
            //        }

            //        return WinAPI.DefWindowProc(hWnd, (WM)message, wParam, lParam);
            //    }
            //));

            //unsafe
            //{
            //    //IntPtr address = MainWndProc; -- this is not necessary to put inside a Unsafe context  
            //    IntPtr address2 = Marshal.GetFunctionPointerForDelegate((Delegate)(WndProc)MainWndProc);
            //    wind_class.lpfnWndProc = address2;
            //}

            ushort regResult = RegisterClassEx(ref wind_class);

            if (regResult == 0)
            {
                uint error = GetLastError();
                return;
            }
            string wndClass = wind_class.lpszClassName;

            //The next line did NOT work with me! When searching the web, the reason seems to be unclear! 
            //It resulted in a zero hWnd, but GetLastError resulted in zero (i.e. no error) as well !!??)
            //IntPtr hWnd = CreateWindowEx(0, wind_class.lpszClassName, "MyWnd", WS_OVERLAPPEDWINDOW | WS_VISIBLE, 0, 0, 30, 40, IntPtr.Zero, IntPtr.Zero, wind_class.hInstance, IntPtr.Zero);

            //This version worked and resulted in a non-zero hWnd
            IntPtr hWnd = CreateWindowEx(0, regResult, "ClickSaverHookWindow", WS_OVERLAPPEDWINDOW | WS_VISIBLE, 0, 0, 300, 400, IntPtr.Zero, IntPtr.Zero, wind_class.hInstance, IntPtr.Zero);

            if (hWnd == ((IntPtr)0))
            {
                uint error = GetLastError();
                return;
            }
            ShowWindow(hWnd, 1);
            UpdateWindow(hWnd);
            //return;

            Inject();
            //The explicit message pump is not necessary, messages are obviously dispatched by the framework.
            //However, if the while loop is implemented, the functions are called... Windows mysteries...
            MSG msg;
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 5000;
            tmr.Tick += new EventHandler(delegate (object s, EventArgs ev)
            {
                Inject();
            });
            tmr.Start();
            while (GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
            {
                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
            tmr.Stop();
            DestroyWindow(hWnd);
        }

        public static void Inject()
        {
            var windows = FindWindow("Anarchy client", null);
            IntPtr AOWnd;
            uint AOProcessId;
            if (windows != null)
            {
                char[] Temp = new char[256];
                GetWindowThreadProcessId(windows, out AOProcessId);

                var dll = DllInjector.GetInstance.Inject("", "AOHook.dll", AOProcessId);

                if (dll != DllInjectionResult.Success)
                {
                    System.Diagnostics.Debug.WriteLine($"dll != DllInjectionResult.Success :{dll}");
                }
            }
        }
        public uint WM_COPYDATA = 74;
        public uint WM_TIMER = 275;

        //[StructLayout(LayoutKind.Sequential)]
        //struct COPYDATASTRUCT
        //{
        //    public IntPtr dwData;    // Any value the sender chooses.  Perhaps its main window handle?
        //    public int cbData;       // The count of bytes in the message.
        //    public IntPtr lpData;    // The address of the message.
        //}

        public struct COPYDATASTRUCT
        {
            public int cbData;
            public IntPtr dwData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }


        unsafe IntPtr MainWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            //COPYDATASTRUCT* pData;
            IntPtr hdc;
            //PAINTSTRUCT ps;
            //RECT rect;
            //switch ((WM) message)  
            //{  
            //    WinAPI.BeginPaint(hWnd, out ps);  
            //    break;  
            //}  
            switch (msg)
            {
                case 275:
                    Inject();

                    break;
                case 74:
                    //pData = (PCOPYDATASTRUCT)lParam;
                    //WaitForSingleObject("", WAIT_TIMEOUT);
                    //    WaitForSingleObject(g_Mutex, INFINITE);
                    //    memset(g_CurrentPacket, 0, 65536);
                    //    memcpy(g_CurrentPacket, pData->lpData, pData->cbData);
                    //    ReleaseMutex(g_Mutex);
                    //    puSendAppMessage(CSAM_NEWMISSIONS, 0);
                    break;
                default:
                    return DefWindowProc(hWnd, msg, wParam, lParam);
            }

            return DefWindowProc(hWnd, msg, wParam, lParam);
        }

        public delegate void StartThread();

        unsafe uint StartingThread(ThreadStart ThreadFunc, int StackSize)
        {
            uint a = 0;
            uint* lpThrAtt = &a;
            uint i = 0;
            uint* lpParam = &i;
            uint lpThreadID = 0;

            uint dwHandle = CreateThread(null, (uint)StackSize, ThreadFunc, lpParam, 0, out lpThreadID);
            if (dwHandle == 0) throw new Exception("Unable to create thread!");
            return dwHandle;
        }

        static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            // filter out non-HWND namechanges... (eg. items within a listbox)
            if (idObject != 0 || idChild != 0)
            {
                return;
            }
            Console.WriteLine("Text of hwnd changed {0:x8}", hwnd.ToInt32());
        }

        private static IntPtr myWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                // All GUI painting must be done here
                case 74:

                    var message = Marshal.PtrToStructure<COPYDATASTRUCT>(lParam);
                    
                    break;

                case 275:
                    Inject();
                    MessageBox.Show("Doubleclick");
                    break;

                //case WM_DESTROY:
                //    DestroyWindow(hWnd);

                    //If you want to shutdown the application, call the next function instead of DestroyWindow
                    //PostQuitMessage(0);
                    break;

                default:
                    break;
            }
            return DefWindowProc(hWnd, msg, wParam, lParam);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            try
            {
                //WrapCDll.

                //var resp = fnClickDLL();
                //System.Diagnostics.Debug.WriteLine(resp);

                Start();
            }
            catch (DllNotFoundException exx)
            {
                MessageBox.Show(exx.ToString());
                Console.WriteLine(exx.ToString());
            }
            catch (EntryPointNotFoundException exx)
            {
                MessageBox.Show(exx.ToString());
                Console.WriteLine(exx.ToString());
            }
        }
    }
}
