using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        List<IntPtr> ListHandles = new List<IntPtr>();

        private void Form1_Load(object sender, EventArgs e)
        {
            var handles = Process.GetProcesses()
                     .Where(process => process.MainWindowHandle != IntPtr.Zero)
                     .Select(process => process.MainWindowHandle)
            .ToArray();

            var processName = Process.GetProcesses()
                     .Where(process => process.MainWindowHandle != IntPtr.Zero)
                     .Select(process => process.ProcessName)
                     .ToArray();

            ListHandles.AddRange(handles);
            comboBox1.Items.AddRange(processName);
            comboBox1.SelectedIndex = 0;
            
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            var handles = Process.GetProcesses()
                     .Where(process => process.MainWindowHandle != IntPtr.Zero)
                     .Select(process => process.MainWindowHandle)
            .ToArray();

            var processName = Process.GetProcesses()
                     .Where(process => process.MainWindowHandle != IntPtr.Zero)
                     .Select(process => process.ProcessName)
                     .ToArray();

            ListHandles.Clear();
            ListHandles.AddRange(handles);
            comboBox1.DataSource = processName;
            //comboBox1.Items.AddRange(processName);
            comboBox1.SelectedIndex = 0;
            comboBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get a handle to the Calculator application. The window class
            // and window name were obtained using the Spy++ tool.
            IntPtr calculatorHandle = ListHandles[comboBox1.SelectedIndex];

            if (calculatorHandle == IntPtr.Zero)
            {
                MessageBox.Show("Calculator is not running.");
                return;
            }

            //выводим на передний фон окно именно того калькулятора, 
            //дескриптор которого выбрали в listBox1, что и требовалось
            SetForegroundWindow(calculatorHandle);

            //Делаем с нужным окном что хотим
            SendKeys.SendWait("111");
            SendKeys.SendWait("*");
            SendKeys.SendWait("11");
            SendKeys.SendWait("=");
        }
    }
}
