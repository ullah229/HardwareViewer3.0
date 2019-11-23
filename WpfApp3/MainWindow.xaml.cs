using System;
using System.Windows;
using System.Windows.Controls;
using helper;
using System.IO;
using System.Management;

namespace WpfApp3 {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            //CPU
            CPUName.Text = "Name: " + GetSpecs.getProcessorInfo("Name");
            Cores.Text = "Cores: " + GetSpecs.getProcessorInfo("NumberOfCores");
            Threads.Text = "Threads: " + GetSpecs.getProcessorInfo("ThreadCount");
            MaxClock.Text = "Max Clock Speed: " + GetSpecs.getProcessorInfo("MaxClockSpeed") + "MHz";

            //RAM
            RAM.Text = "Amount of RAM: " + GetSpecs.getMemoryInfo();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * from Win32_PhysicalMemory");

            foreach (ManagementObject obj in searcher.Get()) { RAMTest.Text = "RAM Speed: " + obj["Speed"] + "MHz"; }

            // BIOS Mainboard
            MainboardN.Text = "Name: " + GetSpecs.getMainboardInfo("Name");
            MainboardMan.Text = "Manufacturer: " + GetSpecs.getMainboardInfo("Manufacturer");
            BIOSV.Text = "BIOS-Version: " + GetSpecs.getBIOSInfo("Version");
            BIOSN1.Text = "Developed by: " + GetSpecs.getBIOSInfo("Manufacturer");
            ReleaseD.Text = "BIOS Release Date: " + GetSpecs.getBIOSInfo("ReleaseDate");

            //GPU
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");

            foreach (ManagementObject obj in myVideoObject.Get()) {
                GPUName.Text = "Name  -  " + obj["Name"];
                DeviceID.Text = "DeviceID  -  " + obj["DeviceID"];
                AdapterDACTYPE.Text = "AdapterDACType  -  " + obj["AdapterDACType"];
                Monochrome.Text = "Monochrome  -  " + obj["Monochrome"];
                AdapterRAM.Text = "AdapterRAM - " + obj["AdapterRAM"] + "GB";
                InstalledDisplayDrivers.Text = "Status - " + obj["Status"];
                DriverVersion.Text = "DriverVersion  -  " + obj["DriverVersion"];
                Videoarchitecture.Text = "VideoArchitecture  -  " + obj["VideoArchitecture"];
                VIdeoMemType.Text = "VideoMemoryType  -  " + obj["VideoMemoryType"];
            }

            ManagementObjectSearcher myOS = new ManagementObjectSearcher(" select * from Win32_OperatingSystem");

            foreach (ManagementObject obj in myOS.Get()) {

                String product_type = "";

                switch (obj["ProductType"]) {
                    case 0: product_type = "Server"; break;
                    case 1: product_type = "Domain Controller"; break;
                    default: product_type = "Workstation"; break;
                }

                Caption.Text = "Caption - " + obj["Caption"];
                WindowsDirectory.Text = "WindowsDirectory - " + obj["WindowsDirectory"];
                ProductType.Text = "ProductType - " + product_type;
                SerialNumber.Text = "SerialNumber - " + obj["SerialNumber"];
                SystemDirectory.Text = "SystemDirectory - " + obj["SystemDirectory"];
                CountryCode.Text = "CountryCode - " + obj["CountryCode"];
                CurrentTimeZone.Text = "Current Time Zone - " + obj["CurrentTimeZone"];
                EncryptionLevel.Text = "Encryption Level - " + obj["EncryptionLevel"];
                OSType.Text = "OS Type - " + obj["OSType"];
                OSVersion.Text = "Version - " + obj["Version"];
            }



            ScrollViewer viewer = new ScrollViewer();
            viewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            ManagementObjectSearcher myAudioObject = new ManagementObjectSearcher("select * from Win32_SoundDevice");

            foreach (ManagementObject obj in myAudioObject.Get()) {
                SoundDevName.Text = "Name  -  " + obj["Name"];
                ProductName.Text = "ProductName  -  " + obj["ProductName"];
                Avail.Text = "Availability  -  " + obj["Availability"];
                DeviceID2.Text = "DeviceID  -  " + obj["DeviceID"];
                PowerManagementSupported.Text = "PowerManagementSupported  -  " + obj["PowerManagementSupported"];
                Status1.Text = "Status  -  " + obj["Status"];
                StatusInfo.Text = "StatusInfo  -  " + obj["StatusInfo"];
            }

           

            foreach (DriveInfo di in DriveInfo.GetDrives()) {
                cboDrive.Items.Add(di.Name);
                cboDrive.SelectedIndex = 0;
            }
 

            SelectQuery Sq = new SelectQuery("Win32_Keyboard");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(Sq);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            
            foreach (ManagementObject mo in osDetailsCollection)
            {
                
                
                keyboard.Text = string.Format("InstallDate: {0}", Convert.ToDateTime(mo["InstallDate"]).ToString());
                keyboard_Copy.Text = string.Format("CreationClassName : {0}", (string)mo["CreationClassName"]);
                keyboard_Copy1.Text = string.Format("Description: {0}", (string)mo["Description"]);
                keyboard_Copy2.Text = string.Format("NumberOfFunctionKeys : {0}", (ushort)mo["NumberOfFunctionKeys"]);
                keyboard_Copy3.Text = string.Format("Name : {0}", (string)mo["Name"]);
                keyboard_Copy4.Text = string.Format("PowerManagementSupported : {0}", mo["PowerManagementSupported"]).ToString();
                keyboard_Copy5.Text = "Status : " + (string)mo["Status"];
                
            }
        }
        private void CboDrive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string driveName = cboDrive.SelectedItem.ToString();
            DriveInfo drive = new DriveInfo(driveName);

            Niggerdrive.Text = "Drive Name: " + drive.Name;
            lblName.Text = "Volume Label: " + drive.VolumeLabel;
            lblTotalSize.Text = "Drive Type: " + drive.DriveType.ToString();
            lblTotalFreeSize.Text = "Drive Format: " + drive.DriveFormat;
            lblAvailableFreeSpace.Text = "Total Size: " + (drive.TotalSize / 1024 / 1024 / 1024) + " GB";
            lblDriveFormat.Text = "Free Disk Space: " + (drive.TotalFreeSpace / 1024 / 1024 / 1024) + " GB";
            lblDriveType.Text = "Drive Ready: " + drive.IsReady.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developed by F16");
        }
    }
    }