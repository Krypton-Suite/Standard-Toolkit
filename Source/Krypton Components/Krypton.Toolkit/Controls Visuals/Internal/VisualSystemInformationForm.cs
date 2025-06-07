using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    internal partial class VisualSystemInformationForm : KryptonForm
    {
        public VisualSystemInformationForm()
        {
            InitializeComponent();
        }

        private void VisualSystemInformationForm_Load(object sender, EventArgs e)
        {
            kwlblHeader.Text = $@"About {Environment.MachineName}";

            pbxThisPC.Image = GenericImageResources.This_PC;
        }

        private void kbtnOk_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

        private void RetrieveGraphicsInformation()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Graphics Device Information:");

            var videoObject = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

            foreach (var obj in videoObject.Get())
            {
                builder.AppendLine($"   Name - {obj["Name"]}");
                builder.AppendLine($"   AdapterRAM - {obj["AdapterRAM"]} bytes");
                builder.AppendLine($"   DriverVersion - {obj["DriverVersion"]}");
                builder.AppendLine($"   VideoProcessor - {obj["VideoProcessor"]}");
                builder.AppendLine($"   VideoModeDescription - {obj["VideoModeDescription"]}");
                builder.AppendLine($"   CurrentBitsPerPixel - {obj["CurrentBitsPerPixel"]}");
                builder.AppendLine($"   CurrentHorizontalResolution - {obj["CurrentHorizontalResolution"]} pixels");
                builder.AppendLine($"   CurrentVerticalResolution - {obj["CurrentVerticalResolution"]} pixels");
                builder.AppendLine($"   Status - {obj["Status"]}");
                builder.AppendLine($"   StatusInfo - {obj["StatusInfo"]}");
                builder.AppendLine($"   AdapterCompatibility - {obj["AdapterCompatibility"]}");
                builder.AppendLine($"   Caption - {obj["Caption"]}");
                builder.AppendLine($"   DeviceID - {obj["DeviceID"]}");
                builder.AppendLine($"   PNPDeviceID - {obj["PNPDeviceID"]}");
                builder.AppendLine($"   AdapterDACType - {obj["AdapterDACType"]}");
                builder.AppendLine($"   CurrentRefreshRate - {obj["CurrentRefreshRate"]} Hz");
                builder.AppendLine($"   Monochrome - {obj["Monochrome"]}");
                builder.AppendLine($"   InstalledDisplayDrivers - {obj["InstalledDisplayDrivers"]}");
                builder.AppendLine($"   VideoArchitecture - {obj["VideoArchitecture"]}");
                builder.AppendLine($"   VideoMemoryType - {obj["VideoMemoryType"]}");
                builder.AppendLine($"   CurrentScanMode - {obj["CurrentScanMode"]}");
                builder.AppendLine($"   CurrentNumberOfColors - {obj["CurrentNumberOfColors"]}");
                builder.AppendLine($"   MaxMemorySupported - {obj["MaxMemorySupported"]} bytes");
                builder.AppendLine($"   DriverDate - {obj["DriverDate"]}");
                builder.AppendLine($"   VideoMode - {obj["VideoMode"]}");
                builder.AppendLine($"   VideoModeDescription - {obj["VideoModeDescription"]}");
                builder.AppendLine($"   VideoProcessor - {obj["VideoProcessor"]}");
            }

            krtbDetails.Text = builder.ToString();
        }

        static readonly string[] _sizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return $"{adjustedSize:n1} {_sizeSuffixes[mag]}";
        }

        private void RetrieveDriveInformation()
        {
            StringBuilder builder = new StringBuilder();
           
            builder.AppendLine("Drive Information:");
            
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives) {
                builder.AppendLine($"   Drive Name: {drive.Name}");
                builder.AppendLine($"   Drive Type: {drive.DriveType}");
                if (drive.IsReady)
                {
                    builder.AppendLine($"   Volume Label: {drive.VolumeLabel}");
                    builder.AppendLine($"   File System: {drive.DriveFormat}");
                    builder.AppendLine($"   Available Space: {drive.AvailableFreeSpace / 1024 / 1024} MB");
                    builder.AppendLine($"   Total Size: {drive.TotalSize / 1024 / 1024} MB");
                    builder.AppendLine($"   Total Free Space: {drive.TotalFreeSpace / 1024 / 1024} MB");
                    builder.AppendLine($"   Root Directory: {drive.RootDirectory.FullName}");
                }
                else
                {
                    builder.AppendLine("   Drive is not ready.");
                }
            }

            /*var driveObject = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");
            foreach (var obj in driveObject.Get())
            {
                builder.AppendLine($"   DeviceID - {obj["DeviceID"]}");
                builder.AppendLine($"   VolumeName - {obj["VolumeName"]}");
                builder.AppendLine($"   FileSystem - {obj["FileSystem"]}");
                builder.AppendLine($"   FreeSpace - {obj["FreeSpace"]} bytes");
                builder.AppendLine($"   Size - {obj["Size"]} bytes");
                builder.AppendLine($"   DriveType - {obj["DriveType"]}");
                builder.AppendLine($"   MediaType - {obj["MediaType"]}");
                builder.AppendLine($"   MediaLoaded - {obj["MediaLoaded"]}");
            }*/
            krtbDetails.Text = builder.ToString();
        }

        private string GetOperatingSystemInstallDate()
        {
            var installDate = new ManagementObjectSearcher("SELECT InstallDate FROM Win32_OperatingSystem").Get().Cast<ManagementObject>().FirstOrDefault()?["InstallDate"]?.ToString();
            if (installDate != null)
            {
                DateTime dateTime = ManagementDateTimeConverter.ToDateTime(installDate);
                return $"Installed on: {dateTime.ToString(CultureInfo.CurrentCulture)}";
            }
            return "Unknown";
        }

        private void tsbtnGraphics_Click(object sender, EventArgs e) => RetrieveGraphicsInformation();

        private void tsbStorage_Click(object sender, EventArgs e) => RetrieveDriveInformation();
    }
}
