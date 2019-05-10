

namespace ETCProfiler.classes
{
    public class CETProduct
    {
        public CETProduct()
        {
            for (int i=0;i< m_SamplesPoints.Length;i++)
            {
                m_SamplesPoints[i] = new CETSamplePoint();
            }
        }
        public string m_strName { get; set; } // 产品名称
        public string m_strProductCode { get; set; } // 产品名称
        public string m_strDescription { get; set; } // 产品名称
        public string m_strManufacturers { get; set; } // 产品厂商
        public string m_strCustomor { get; set; } // 产品厂商

        // 设置
        public string[] m_strProductImage = new string[2];
        public CETSamplePoint[] m_SamplesPoints { get; set; } = new CETSamplePoint[24];
        public int m_nChannelCount { get; set; }
        public double m_dWidth { get; set; }
        public double m_dHeight { get; set; }
        public double m_dThickness { get; set; }

        //這邊之後要改
        public void ReadFromDB(int nID)
        {
            /*string strSql = $"select * from ETCProduct where ID = {nID};";
            CppSQLite3Table t = theApp.m_ETCDB.getTable(strSql);
            for (int row = 0; row < t.numRows(); row++)
            {
                t.setRow(row);

                // 产品名称
                string str = t.getStringField(1);
                ConvertUtf8ToGBK(str);

                m_strName = str;

                // 产品名称
                str = t.getStringField("Code");
                ConvertUtf8ToGBK(str);
                m_strProductCode = str;

                // 产品编码
                str = t.getStringField("Description");
                ConvertUtf8ToGBK(str);
                m_strDescription = str;

                // 	用户
                str = t.getStringField("Manufacturers");
                ConvertUtf8ToGBK(str);
                m_strCustomor = str;


                // 图片路径
                str = t.getStringField("productImage");
                ConvertUtf8ToGBK(str);
                m_strProductImage[0] = str;

                // 图片路径
                str = t.getStringField("productImage2");
                ConvertUtf8ToGBK(str);
                m_strProductImage[1] = str;

                // 产品编码
                m_dWidth = t.getFloatField("Width");
                // 产品编码
                m_dHeight = t.getFloatField("Heigth");
                // 产品编码
                m_dThickness = t.getFloatField("Thickness");

            }

            // 更新图形和列表

            strSql = $"select * from ETCChannel where ETCID = {nID};";
            t = theApp.m_ETCDB.getTable(strSql);
            for (int row = 0; row < t.numRows(); row++)
            {
                t.setRow(row);

                // 产品名称
                CString str = t.getStringField("Name");
                ConvertUtf8ToGBK(str);
                m_SamplesPoints[row].m_strPointTitle = str;

                // 产品编码
                double nTempValue = t.getFloatField("XAxis");
                m_SamplesPoints[row].m_dPostionX = nTempValue;
                // 产品编码
                nTempValue = t.getFloatField("YAxis");

                m_SamplesPoints[row].m_dPostionY = nTempValue;
                // 产品编码
                nTempValue = t.getIntField("Color");
                m_SamplesPoints[row].m_clrColor = nTempValue;

                // 是否正面
                int nTempValue1 = t.getIntField("PointPostion");
                m_SamplesPoints[row].m_nPointPostion = nTempValue1;

            }*/

        }
        //這邊之後要改
        public void UpdateDeviceToDB(int nID)
        {
            // 打开一个通用的流，以模板的方式向表中插入多项数据
            /*CString strSQL;
            CString strTempSql;


            strSQL.Format("update ETCProduct set ");

            // cID
            // 	strTempSql.Format("ID = %i,",nID);
            // 	strSQL += strTempSql;

            // 设备名称
            CString strTempValues = m_strName;
            ConvertGBKToUtf8(strTempValues);
            strTempSql.Format("Name = '%s',", strTempValues);
            strSQL += strTempSql;

            // 设备密码
            strTempValues = m_strProductCode;
            ConvertGBKToUtf8(strTempValues);
            strTempSql.Format("Code = '%s',", strTempValues);
            strSQL += strTempSql;

            // 设备物理地址

            strTempValues = m_strDescription;
            ConvertGBKToUtf8(strTempValues);
            strTempSql.Format("Description = '%s',", strTempValues);
            strSQL += strTempSql;
            // 设备地址
            strTempValues = m_strCustomor;
            ConvertGBKToUtf8(strTempValues);
            strTempSql.Format("Manufacturers = '%s',", strTempValues);
            strSQL += strTempSql;
            // 宽度

            strTempValues = m_strProductImage[0];
            ConvertGBKToUtf8(strTempValues);
            strTempSql.Format("productImage = '%s',", strTempValues);
            strSQL += strTempSql;
            // 宽度

            strTempValues = m_strProductImage[1];
            ConvertGBKToUtf8(strTempValues);
            strTempSql.Format("productImage2 = '%s',", strTempValues);
            strSQL += strTempSql;
            // 高度
            strTempSql.Format("Width = %3.1f,", m_dWidth);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("Heigth = %3.1f,", m_dHeight);
            strSQL += strTempSql;

            // 波特率
            strTempSql.Format(_T("Thickness = %3.1f "), m_dThickness);
            strSQL += strTempSql;

            strTempSql.Format("WHERE ID = %i;", nID);
            strSQL += strTempSql;

            // 执行写操作
            int nRows = theApp.m_ETCDB.execDML(strSQL);


            //////////////////////////////


            for (int ii = 0; ii < theApp.m_pETETCStage->m_nChannelCount; ii++)
            {

                // 更新通道数据
                strSQL.Format("update ETCChannel set ");

                // 设备名称

                strTempValues = m_SamplesPoints[ii].m_strPointTitle;
                ConvertGBKToUtf8(strTempValues);
                strTempSql.Format("Name = '%s',", strTempValues);
                strSQL += strTempSql;
                // 设备密码
                strTempSql.Format("XAxis = %3.1f,", m_SamplesPoints[ii].m_dPostionX);
                strSQL += strTempSql;

                // 设备物理地址
                strTempSql.Format("YAxis = %3.1f,", m_SamplesPoints[ii].m_dPostionY);
                strSQL += strTempSql;

                // 设备地址
                strTempSql.Format("Color = %i,", m_SamplesPoints[ii].m_clrColor);
                strSQL += strTempSql;

                // 设备地址
                strTempSql.Format("PointPostion = %i ", m_SamplesPoints[ii].m_nPointPostion);
                strSQL += strTempSql;



                strTempSql.Format("WHERE ETCID = %i AND ID = %i;", nID, ii + 1);
                strSQL += strTempSql;

                // 执行写操作
                int nRows = theApp.m_ETCDB.execDML(strSQL);
            }*/


        }
        //這邊之後要改
        public void InsertProductToDB(int nID)
        {
            // 打开一个通用的流，以模板的方式向表中插入多项数据
            /*CString strSQL;
            CString strTempSql;

            strSQL.
                Format(
                "insert into ETCProduct (Name,Code,Description,Manufacturers,productImage,productImage2,Width,Heigth,Thickness) values ( ");


            // 设备名称
            ConvertGBKToUtf8(m_strName);
            strTempSql.Format("'%s',", m_strName);
            strSQL += strTempSql;

            // 设备密码
            ConvertGBKToUtf8(m_strProductCode);
            strTempSql.Format("'%s',", m_strProductCode);
            strSQL += strTempSql;

            // 设备物理地址
            ConvertGBKToUtf8(m_strDescription);
            strTempSql.Format("'%s',", m_strDescription);
            strSQL += strTempSql;

            // 设备地址
            ConvertGBKToUtf8(m_strCustomor);
            strTempSql.Format("'%s',", m_strCustomor);
            strSQL += strTempSql;

            // 宽度
            ConvertGBKToUtf8(m_strProductImage[0]);
            strTempSql.Format("'%s',", m_strProductImage[0]);
            strSQL += strTempSql;

            // 宽度
            ConvertGBKToUtf8(m_strProductImage[1]);
            strTempSql.Format("'%s',", m_strProductImage[1]);
            strSQL += strTempSql;

            // 高度
            strTempSql.Format("%3.1f,", m_dWidth);
            strSQL += strTempSql;

            // 串口号
            strTempSql.Format("%3.1f,", m_dHeight);
            strSQL += strTempSql;

            // 波特率
            strTempSql.Format(_T(" %3.1f );"), m_dThickness);
            strSQL += strTempSql;

            // 执行写操作
            int nRows = theApp.m_ETCDB.execDML(strSQL);


            ////////////////////////////
            CString strSql;
            strSql.Format("select * from ETCProduct where Name ='%s';", m_strName);
            CppSQLite3Table t = theApp.m_ETCDB.getTable(strSql);
            int nTempID = 0;
            for (int row = 0; row < t.numRows(); row++)
            {
                t.setRow(row);

                // 产品名称
                nTempID = t.getIntField("ID");
            }

            for (int ii = 0; ii < theApp.m_pETETCStage->m_nChannelCount; ii++)
            {

                // 更新通道数据
                strSQL.Format("insert into ETCChannel (ID,Name,XAxis,YAxis,Color,PointPostion,ETCID) values ( ");

                // 设备名称
                strTempSql.Format("%i,", ii + 1);
                strSQL += strTempSql;

                ConvertGBKToUtf8(m_SamplesPoints[ii].m_strPointTitle);
                strTempSql.Format("'%s',", m_SamplesPoints[ii].m_strPointTitle);
                strSQL += strTempSql;

                // 设备密码
                strTempSql.Format("%3.1f,", m_SamplesPoints[ii].m_dPostionX);
                strSQL += strTempSql;

                // 设备物理地址
                strTempSql.Format("%3.1f,", m_SamplesPoints[ii].m_dPostionY);
                strSQL += strTempSql;

                // 设备地址
                strTempSql.Format("%i,", m_SamplesPoints[ii].m_clrColor);
                strSQL += strTempSql;

                // 设备地址
                strTempSql.Format("%i,", m_SamplesPoints[ii].m_nPointPostion);
                strSQL += strTempSql;

                // 设备地址
                strTempSql.Format("%i );", nTempID);
                strSQL += strTempSql;


                // 执行写操作
                int nRows = theApp.m_ETCDB.execDML(strSQL);
            }*/
        }
        //這邊之後要改
        public void DeleteFromDB(int nID)
        {/*
            // 打开一个通用的流，以模板的方式向表中插入多项数据
            CString strSQL;

            strSQL.Format("delete from ETCProduct where ID = %i", nID);
            // 执行写操作
            int nRows = theApp.m_ETCDB.execDML(strSQL);*/
        }
        //沒有實作
        public void DeleteReflowerFromDB(int nID)
        {

        }
    }

}
