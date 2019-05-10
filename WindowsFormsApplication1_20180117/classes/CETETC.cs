using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ETCProfiler.classes.ShareFunc;

namespace ETCProfiler.classes
{
    public class CETETC
    {
        byte[] m_byReceiveData = new byte[250];
        char[] m_CommDataBuff = new char[1025 * 1024];
        int m_nCommDataLength = 0;

        public CETETC()
        {
            m_nComID = 15;
            m_nComPort = 115200;

            m_nSampleRate = 20;   // 采样率
            m_nChannelCount = 6;  // 通道数
            m_nBandRate = 115200;

            // 采样板参数

            m_nLinkStatus = 99;
            m_nETCType = 0;   //
            m_nAutoSampleTempl = 20;  // 自动采样温度
            m_nWireLessID = 1;       // 无线通道号

            // 设备状态

            m_IsAutoSample = true;  // 是否自动采样
            m_IsEnableWireLess = true; // 是否开启无线
            m_IsForceCharge = true;    // 是否强制充电
            m_IsDeviceLock = true;     // 是否锁定

            // 采样板环境参数

            m_fBoardTempl = 0;     // 板内温度
            m_fBatteryVoltage = 0; // 电池电压
            m_fUSBVoltage = 0;     // USB电压
            for (int ii = 0; ii < 12; ii++)
            {
                m_nCurrentTemp[ii] = 0;
            }
            m_nCommandMode = 0;
        }

        public int m_nLinkStatus{ get; set; } // 0.不明。1.鏈接 2.正在通訊。3.

        public int m_nCommandMode{ get; set; }
        public int m_nComID{ get; set; }
        public int m_nComPort{ get; set; }

        public uint m_nBandRate{ get; set; }
        public int m_nETCType{ get; set; }
        public string m_strETCName{ get; set; }
        public List<string> m_WirelessArray { get; set; } = new List<string>();
        public List<uint> m_SampleRateArray { get; set; } = new List<uint>();
        // 采样板参数
        public int m_nSampleRate{ get; set; } // 采样率
        public int m_nChannelCount{ get; set; } // 通道数
        public bool m_IsHaveWireLess{ get; set; }


        public int m_nAutoSampleTempl{ get; set; } // 自动采样温度
        public int m_nWireLessID{ get; set; } // 无线通道号

        public byte[] m_byteOptionData { get; set; } = new byte[240]; // 采样板参数数据

        public string m_strDateTime{ get; set; }
        // 设备状态

        public bool m_IsAutoSample{ get; set; } // 是否自动采样
        public bool m_IsEnableWireLess{ get; set; } // 是否开启无线
        public bool m_IsForceCharge{ get; set; } // 是否强制充电
        public bool m_IsDeviceLock{ get; set; } // 是否锁定

        public int m_nChargeStatus{ get; set; } //0:充电完成;2:正在充电；3:禁止充电；
        public int m_nETCStatus{ get; set; } //  设备状态  0：正常; 1:数据传送;2:正在擦除；3:正在充电； 4：设备正忙;5:锁定；6:通讯故障;7:USB接口故障；8：未知。
                                 // 采样板环境参数

        public float m_fBoardTempl{ get; set; } // 板内温度
        public float m_fBatteryVoltage{ get; set; } // 电池电压
        public DateTime m_tmCurrentTime { get; set; } = new DateTime(); // 当前时间
        public float m_fUSBVoltage{ get; set; } // USB电压
        public int[] m_nCurrentTemp { get; set; } = new int[12];
        public string m_strCommnadResult{ get; set; }

        // 设备控制
       
        public bool etSearchDevice()
        {
            return false;
        }
        public bool etOpenDevice()
        {
            return false;
        }
        public bool etCloseDevice()
        {
            return false;
        }

        // 数据
        public int etGetData(ref byte Ch1HardwareData)
        {
            return 0;
        }

        // 设置
        //
        public bool etTS(string strDevicetype)
        {
            string strTEmp = "TS\r\n";            
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etReadByTime(string tmReadByTime)
        {
            string strTEmp = $"ReadByTime{tmReadByTime}\r\n";
            bool bResult = SendBySerial(strTEmp);
            m_nCommandMode = 1;
            return bResult;
        }
        public bool etReadAll(byte SamplesData)
        {
            string strTEmp = $"ReadAll{SamplesData}\r\n";
            bool bResult = SendBySerial(strTEmp);
            m_nCommandMode = 2;
            return bResult;
        }
        public bool etReadCurr()
        {
            string strTEmp = $"ReadCurr\r\n";
            bool bResult = SendBySerial(strTEmp);
            m_nCommandMode = 3;
            return bResult;
        }
        public bool etReadEnd()
        {
            string strTEmp = $"ReadEnd\r\n";
            bool bResult = SendBySerial(strTEmp);
            m_nCommandMode = 4;
            return bResult;
        }
        public bool etReadDeviceStatus()
        {
            string strTEmp = $"ReadInTemp\r\n\r\nReadValtage\r\n\r\nReadUSBValtage\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etReadValtage()
        {
            string strTEmp = $"ReadValtage\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etCharge()
        {
            string strTEmp = $"Charge\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult; 
        }
        public bool etErase()
        {
            string strTEmp = $"Erase\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etHelp()
        {
            string strTEmp = $"Help\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etReadUSBValtage()
        {
            string strTEmp = $"ReadUSBValtage\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etSetTime(ref string cDeviceTime)
        {
            string strTEmp = $"SetTime:{cDeviceTime}\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etReadTime()
        {
            string strTEmp = $"ReadTime\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etSetRfCh(int nRfch)
        {
            string strTEmp = $"SetRfCh:{nRfch}\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etSetRfEnable()
        {
            string strTEmp = $"SetRfEnable\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etSetRfDisable()
        {
            string strTEmp = $"SetRfDisable\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etLock()
        {
            string strTEmp = $"Lock\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etUnlock8328(ref string cPassword)
        {
            string strTEmp = $"Unlock{cPassword}\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etWritePcCfg()
        {
            string strTEmp = $"WritePcCfg\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etReadPcCfg()
        {
            string strTEmp = $"ReadPcCfg\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etSetSpeed(int nSampleSpeed)
        {
            string strTEmp = $"SetSpeed:{nSampleSpeed}\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etSetChs(int nChs)
        {
            string strTEmp = $"SetChs:{nChs}\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etSetAutoTemp()
        {
            string strTEmp = $"SetAutoTemp\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etSetAutoEnable()
        {
            string strTEmp = $"SetAutoEnable\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }
        public bool etSetAutoDisable()
        {
            string strTEmp = $"SetAutoDisable\r\n";
            bool bResult = SendBySerial(strTEmp);
            return bResult;
        }

        public string m_strCurrCommand;
        //  sss
        public void DecodeETCCommand(ref string bufReceive, int nReceiveLen)
        {

            string strCMD = bufReceive;
            int ii = 0;

            if (m_nCommandMode == 1 || m_nCommandMode == 2 || m_nCommandMode == 3)
            {

                if (strCMD.IndexOf("End") >= 0)
                {
                    m_nETCStatus = 0;
                    switch (m_nCommandMode)
                    {
                        case 0:   // 实时数据状态读取
                            {
                                string strData = new string(m_CommDataBuff);
                                if (strData.IndexOf("Data:") >= 0)
                                {
                                    string strLineString1 = strData.Right(5);
                                    for (ii = 0; ii < 6; ii++)
                                    {
                                        int nTempdata = 0;
                                        string strTemp = "";

                                        AfxExtractSubString(ref strTemp, strLineString1, ii, ',');
                                        if (strTemp.IndexOf("-----") >= 0)
                                        {
                                            nTempdata = 0;
                                        }
                                        else
                                        {
                                            nTempdata = atoi(strTemp);
                                        }

                                        m_nCurrentTemp[ii] = nTempdata;
                                    }
                                }
                            }
                            break;
                        case 1:   // 实时监控信息
                            {

                            }
                            break;
                        case 2:   // 数据下载
                            {
                                
                                /*memcpy(Marshal.StringToHGlobalAnsi(new string(m_CommDataBuff) + m_nCommDataLength), Marshal.StringToHGlobalAnsi(bufReceive), (UIntPtr)nReceiveLen);
                                m_nCommDataLength += nReceiveLen;
                                m_CommDataBuff[m_nCommDataLength] = '\0';

                                CETCManagerApp.Instance.m_HistoryFile.SeekToEnd();
                                CETCManagerApp.Instance.m_HistoryFile.Write(m_CommDataBuff, m_nCommDataLength);

                                CETCManagerApp.Instance.m_HistoryFile.Flush();

                                m_nCommandMode = 0;
                                CETCManagerApp.Instance.m_HistoryFile.Close();

                                ShowErrorMessage("数据停止传送");
                                */
                            }

                            break;
                    }

                }
                else
                {
                    m_nETCStatus = 1;
                    switch (m_nCommandMode)
                    {
                        case 0:   // 实时数据状态读取
                            {
                            }
                            break;
                        case 1:   // 实时数据信息
                            {
                            }
                            break;
                        case 2:   // 数据下载
                            {
                               /* memcpy(m_CommDataBuff + m_nCommDataLength, bufReceive, nReceiveLen);

                                if (m_nCommDataLength + nReceiveLen >= 1024 * 1024)
                                {
                                    theApp.m_HistoryFile.SeekToEnd();
                                    theApp.m_HistoryFile.Write(m_CommDataBuff, m_nCommDataLength + nReceiveLen);
                                    theApp.m_HistoryFile.Flush();
                                    m_nCommDataLength = 0;
                                }
                                else
                                {
                                    m_nCommDataLength += nReceiveLen;
                                }*/
                                break;
                            }
                    }
                }
            }
            else
            {
                /*string strCMD(_T(""));
                string strTotalCMD = bufReceive;
                CMainFrame* pMainFrame = (CMainFrame*)theApp.m_pMainWnd;
                while (AfxExtractSubString(strCMD, strTotalCMD, ii++, _T('\n')))
                {
                    //	m_nETCStatus = 0;
                    if (strCMD.IndexOf("Busy") >= 0)
                    {
                        m_nETCStatus = 4;
                        ShowErrorMessage("设备忙");
                    }
                    else if (strCMD.IndexOf("Unsupported Cmd!") >= 0)
                    {
                        m_nETCStatus = 8;
                        ShowErrorMessage("命令不支持");
                    }
                    else if (strCMD.IndexOf("TS12 OK") >= 0 || strCMD.IndexOf("TS06 OK") >= 0 || strCMD.IndexOf("RF OK") >= 0)
                    {
                        if (strCMD.IndexOf("TS12 OK") >= 0)
                        {
                            m_nETCType = 1;
                            ShowErrorMessage("12通道温度检测装置");
                        }
                        else if (strCMD.IndexOf("TS06 OK") >= 0)
                        {
                            m_nETCType = 0;
                            ShowErrorMessage("6通道温度检测装置");
                        }
                        else
                        {
                            m_nETCType = 2;
                            ShowErrorMessage("无线温度检测装置");
                        }

                    }
                    else if (strCMD.IndexOf("SetTime OK") >= 0 || strCMD.IndexOf("SetTime ERROR") >= 0)
                    {
                        if (strCMD.IndexOf("SetTime OK") >= 0)
                        {
                            ShowErrorMessage("设置时间成功");
                        }
                        else
                        {
                            ShowErrorMessage("设置时间失败");
                        }
                    }
                    else if (strCMD.IndexOf("Temp:") >= 0)
                    {
                        string strData = strCMD.Mid(5, 4);
                        float nTemp = atol((LPSTR)(LPCTSTR)strData) / 10.0;
                        strData.Format("板内温度：%.1f", nTemp);
                        m_fBoardTempl = nTemp;
                        pMainFrame->m_wndStatusBar.Invalidate();
                        ShowErrorMessage(strData);
                    }
                    else if (strCMD.IndexOf("Valtage:") >= 0 && strCMD.IndexOf("USB") < 0)
                    {
                        string strData = strCMD.Mid(8, 2);
                        float nTemp = atoi((LPSTR)(LPCTSTR)strData) / 10.0;
                        m_fBatteryVoltage = nTemp;
                        strData.Format("板内电池电压：%.1f", nTemp);
                        pMainFrame->m_wndStatusBar.RedrawWindow();
                        ShowErrorMessage(strData);
                    }
                    else if (strCMD.IndexOf("Charg") >= 0)
                    {
                        if (strCMD.IndexOf("Can’t Charge") >= 0)
                        {
                            m_nChargeStatus = 2;
                            ShowErrorMessage("未能开始充电");
                        }
                        else if (strCMD.IndexOf("Charging") >= 0)
                        {
                            m_nChargeStatus = 1;
                            ShowErrorMessage("正在充电");
                        }
                        else
                        {
                            m_nChargeStatus = 0;
                            ShowErrorMessage("充电完成");
                        }
                    }
                    else if (strCMD.IndexOf("Erase OK") >= 0 || strCMD.IndexOf("Erasing") >= 0)
                    {
                        if (strCMD.IndexOf("Erase OK") >= 0)
                        {
                            ShowErrorMessage("擦除完成");
                        }
                        else
                        {
                            m_nETCStatus = 2;
                            ShowErrorMessage("正在擦除");
                        }
                    }
                    else if (strCMD.IndexOf("USBValtage:") >= 0)
                    {
                        string strData = strCMD.Mid(11, 2);
                        float nTemp = atoi((LPSTR)(LPCTSTR)strData) / 10.0;
                        m_fUSBVoltage = nTemp;
                        strData.Format("USB接口供电电压：%.1f", nTemp);
                        pMainFrame->m_wndStatusBar.Invalidate();
                        ShowErrorMessage(strData);
                    }
                    else if (strCMD.IndexOf("Time:") == 0)
                    {
                        string strData = strCMD.Mid(5, 15);
                        m_strDateTime = "当前时间为：" + strData;
                        ShowErrorMessage(m_strDateTime);
                    }
                    else if (strCMD.IndexOf("SetRfCh OK") >= 0 || strCMD.IndexOf("SetRfCh ERROR") >= 0)
                    {
                        if (strCMD.IndexOf("SetRfCh OK") >= 0)
                        {
                            ShowErrorMessage("设置无线频道号成功");
                        }
                        else
                        {
                            ShowErrorMessage("设置无线频道号失败");
                        }
                    }
                    else if (strCMD.IndexOf("SetRfEnable OK") >= 0)
                    {
                        m_IsEnableWireLess = TRUE;
                        ShowErrorMessage("使能无线功能成功");
                    }
                    else if (strCMD.IndexOf("SetRfDisable OK") >= 0)
                    {
                        m_IsEnableWireLess = FALSE;
                        ShowErrorMessage("关闭无线功能成功");
                    }
                    else if (strCMD.IndexOf("Lock OK") >= 0)
                    {
                        m_nETCStatus = 5;
                        ShowErrorMessage("锁定成功");
                    }
                    else if (strCMD.IndexOf("Unlock OK") >= 0)
                    {
                        m_nETCStatus = 0;
                        ShowErrorMessage("解锁成功");
                    }
                    else if (strCMD.IndexOf("WritePcCfg OK") >= 0)
                    {
                        ShowErrorMessage("写入PC软件的配置数据成功");
                    }
                    else if (strCMD.IndexOf("SetSpeed OK") >= 0 || strCMD.IndexOf("SetSpeed ERROR") >= 0)
                    {
                        if (strCMD.IndexOf("SetSpeed OK") >= 0)
                        {
                            ShowErrorMessage("设置数据速率成功");
                        }
                        else
                        {
                            ShowErrorMessage("设置数据速率失败");
                        }
                    }
                    else if (strCMD.IndexOf("SetChs OK") >= 0 || strCMD.IndexOf("SetChs ERROR") >= 0)
                    {
                        if (strCMD.IndexOf("SetChs OK") >= 0)
                        {
                            ShowErrorMessage("设置实际使用通道数成功");
                        }
                        else
                        {
                            ShowErrorMessage("设置实际使用通道数失败");
                        }
                    }
                    else if (strCMD.IndexOf("SetAutoTemp OK") >= 0 || strCMD.IndexOf("SetAutoTemp ERROR") >= 0)
                    {
                        if (strCMD.IndexOf("SetAutoTemp OK") >= 0)
                        {
                            ShowErrorMessage("设置自动记录温度成功");
                        }
                        else
                        {
                            ShowErrorMessage("设置自动记录温度失败");
                        }
                    }
                    else if (strCMD.IndexOf("SetAutoEnable OK") >= 0 || strCMD.IndexOf("SetAutoEnable ERROR") >= 0)
                    {
                        if (strCMD.IndexOf("SetAutoEnable OK") >= 0)
                        {
                            m_IsAutoSample = TRUE;
                            ShowErrorMessage("使能自动记录功能成功");
                        }
                        else
                        {
                            ShowErrorMessage("使能自动记录功能失败");
                        }
                    }
                    else if (strCMD.IndexOf("SetAutoDisable OK") >= 0 || strCMD.IndexOf("SetAutoDisable ERROR") >= 0)
                    {
                        if (strCMD.IndexOf("SetAutoDisable OK") >= 0)
                        {
                            m_IsAutoSample = FALSE;
                            ShowErrorMessage("关闭自动记录功能成功");
                        }
                        else
                        {
                            ShowErrorMessage("关闭自动记录功能失败");
                        }
                    }
                    else if (strCMD.IndexOf("ReadPcCfg:") >= 0)
                    {
                        string strData = strCMD.Mid(10, 240);
                        ShowErrorMessage(strData);
                    }
                    else if (strCMD.IndexOf("WritePcCfg OK") >= 0)
                    {
                        string strData = strCMD.Mid(10, 240);
                        ShowErrorMessage(strData);
                    }
                }*/
            }
        }
        public string ETCCommandResult(string strMessage){ return ""; }
        public int OpenDeviceByCom(){ return 0; }
        public void ParseCMD(ref string buf, int len){}
        public bool SendBySerial(string strCMD)
        {
            m_strCommnadResult = "命令执行失败";
            Main_Form.Instance.m_nCommandType = 2;

            //pMainFrame.m_nCommandType = 2;
            //Frm_Main.Instance.Write(strCMD);

            // 	m_strCommnadResult = "命令执行失败";
            // 	CMainFrame* pMainFrame = (CMainFrame*)AfxGetMainWnd ();
            //
            // 	if (pMainFrame->m_CnComm.IsOverlappedMode () || pMainFrame->m_CnComm.IsTxBufferMode ())
            // 		pMainFrame->m_CnComm.Write ((LPSTR)(LPCSTR)strCMD);
            // 	else
            // 	{//! 阻塞非缓冲区模式 必须检查返回值，确保数据完全发送出
            // 		for (int i = 0 ; i < nlength ; i++)
            // 			i += pMainFrame->m_CnComm.Write ((LPSTR)(LPCSTR)strCMD + i, nlength - i);
            // 	}
            return true;
        }

        public int SendBySerialAsyn(string strCMD, uint nlength){ return 0; }
        //	BOOL OpenComm(){}
        public void ShowErrorMessage(string strErrorMSG){}
        public void UpdateETCToDB(int nID){}
        public void ReadETCFromDB(int nID){}
        public void ReadETCFromDB(string strETCType){}
        public void InsertETCToDB(int nID){}
        public void DeleteETCFromDB(int nID){}
        public void DecodeETCCommandEX(string strCMD){}        
        public void DecodeETCRealTimeData(ref string bufReceive, int nReceiveLen){}
        //	CnComm m_Comm{}
    }

}
