using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using MedicalLibrary.Entity;
using MedicalLibrary.Utility;

namespace MedicalLibrary.Agent
{
    public class Q26
    {
        List<Q26_Item> ItemList = new List<Q26_Item>();

        /// <summary>
        /// true: 登録・修正, false: 削除
        /// </summary>
        bool _Operation = true;

        /// <summary>
        /// 同一の日時に複数の連携ファイルが作られた場合の連番
        /// </summary>
        int _SEQ = 0;

        PatOrder _Order = new PatOrder();

        DateTime _DateTime = DateTime.Now;

        string FileID
        {
            get
            {
                return "Q26_" + this._DateTime.ToString("yyyyMMddHHmmss") + "_" + this._SEQ.ToString().PadLeft(3, '0') + "_" + this._Order.Pat.Id;
            }
        }

        string PerformTime
        {
            get
            {
                string s = this._DateTime.ToString("yyyy-MM-dd");

                if (DateTimeAgent.IsDate(this._Order.SekouDate))
                {
                    s = this._Order.SekouDate.Insert(4, "-").Insert(7, "-"); 
                }

                return s;
            }
        }

        string OrderTime
        {
            get
            {
                string s = this._DateTime.ToString("yyyy-MM-dd");

                if (DateTimeAgent.IsDate(this._Order.OrderDate))
                {
                    s = this._Order.OrderDate.Insert(4, "-").Insert(7, "-");
                }

                return s;
            }
        }

        string AdmitFlag
        {
            get
            {
                return this._Order.InOut.Equals("2") ? "True" : "False";
            }
        }

        /// <summary>
        /// オーダー番号＋SEQ（３バイト）
        /// SEQ は 001 で固定する
        /// </summary>
        string OrderNo
        {
            get
            {
                return this._Order.OrderId.PadLeft(14, '0') + "001";
            }
        }

        /// <summary>
        /// 1オーダー複数実施を紐づける項目
        /// MMDD（月日）+ SEQ（５ケタ）
        /// SEQ は perform（実施）の場合は 00000 以外　→　00001 で固定する
        /// </summary>
        string JissiSeq
        {
            get
            {
                return this._Order.OrderId.PadLeft(14, '0').Substring(2, 4) + "00001";
            }
        }


        /// <summary>
        /// 保険組み合わせ番号情報
        /// ^^^^^0^0 固定
        /// </summary>
        public string HokenNoInfo = "^^^^^0^0";

        /// <summary>
        /// 同日来院数
        /// 入院は 0 固定
        /// </summary>
        int _DoujituRaiinSu = 1;

        /// <summary>
        /// 同日来院数
        /// 入院は 0 固定
        /// </summary>
        public int DoujituRaiinSu
        {
            set
            {
                if (this._Order.InOut.Equals("1"))
                {
                    this._DoujituRaiinSu = (value > 0) ? value : 1;
                }
                else
                {
                    this._DoujituRaiinSu = 0;
                }
            }
            get
            {
                if (this._Order.InOut.Equals("1"))
                {
                    return this._DoujituRaiinSu > 0 ? this._DoujituRaiinSu : 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string ClassCode
        {
            get
            {
                string s = "";

                // 診療区分に該当する ClassCode を取得する
                foreach (ProasShinku obj in LibSettings.Current.Proas.ShinkuList)
                {
                    if (!obj.Code.Equals(this._Order.Shinku))
                    {
                        continue;
                    }

                    s = obj.ClassCode;
                    break;
                }

                if (this._Order.Shinku.Equals("0"))
                {
                    // 自費コメントの場合は、そちらを優先
                    foreach (Q26_Item item in this.ItemList)
                    {
                        if (item._OrderDetail.Code.Equals("88888889") ||
                            item._OrderDetail.GetReceCode(this._Order.InOut).Equals("CH00003095"))
                        {
                            s = "998";
                            break;
                        }
                    }
                }
                else
                {
                    // 医事診療区分がある場合は、そちらを優先
                    string ss = "";

                    foreach (Q26_Item item in this.ItemList)
                    {
                        ss = item._OrderDetail.GetReceShinku(this._Order.InOut);
                        break;
                    }

                    if (ss.Length > 0) s = ss;
                }

                return s;
            }
        }

        /// <summary>
        /// 院内・院外
        /// </summary>
        public string Memo
        {
            get
            {
                if (this._Order.InnaiFlg.Equals("1"))
                {
                    return "院外^^";
                }
                else
                {
                    return "院内^^";
                }
            }
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="order"></param>
        /// <param name="dt">処理日時</param>
        /// <param name="seq">同一の日時に複数の連携ファイルが作られた場合の連番</param>
        /// <param name="doujitu_raiinsu">同日来院数（外来 >= 1, 入院 = 0）</param>
        /// <param name="operation">true: 登録・修正, false: 削除</param>
        public Q26(PatOrder order, DateTime dt, int seq = 1, int doujitu_raiinsu = 1, bool operation = true)
        {
            this._Order = order;
            this._SEQ = seq;
            this._DateTime = dt;
            this._Operation = operation;
            this.DoujituRaiinSu = doujitu_raiinsu;

            if (operation)
            {
                List<PatOrderDetail> detail_list = this._Order.DetailList;

                if (detail_list.Count == 0)
                {
                    detail_list = PatOrderDetail.Load(this._Order.OrderId);
                }

                foreach (PatOrderDetail detail in detail_list)
                {
                    this.ItemList.Add(new Q26_Item(detail));
                }
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="order"></param>
        /// <param name="dt">処理日時</param>
        /// <param name="seq">同一の日時に複数の連携ファイルが作られた場合の連番</param>
        /// <param name="doujitu_raiinsu">同日来院数（外来 >= 1, 入院 = 0）</param>
        /// <param name="operation">true: 登録・修正, false: 削除</param>
        public Q26(string order_id, DateTime dt, int seq = 1, int doujitu_raiinsu = 1, bool operation = true)
        {
            this._Order = PatOrder.Load(order_id);
            this._SEQ = seq;
            this._DateTime = dt;
            this._Operation = operation;
            this.DoujituRaiinSu = doujitu_raiinsu;

            if (operation)
            {
                List<PatOrderDetail> detail_list = PatOrderDetail.Load(order_id);

                foreach (PatOrderDetail detail in detail_list)
                {
                    this.ItemList.Add(new Q26_Item(detail));
                }
            }
        }

        /// <summary>
        /// XML ファイルを生成する
        /// </summary>
        /// <returns></returns>
        private StdReturn MakeXML(string file_path)
        {
            StdReturn sr = new StdReturn();

            try
            {
                if (file_path.Length == 0)
                {
                    throw new Exception("出力先フォルダが指定されていません");
                }
                else if (!Directory.Exists(file_path))
                {
                    Directory.CreateDirectory(file_path);
                }

                // ファイル生成するか
                bool b = true;

                string s = string.Format(@"
<Mml version=""2.3"" createDate=""{0}"" xmlns:mml=""http://www.medxml.net/MML"" xmlns:xhtml=""http://www.w3.org/1999/xhtml"" xmlns:mmlCm=""http://www.medxml.net/MML/SharedComponent/Common/1.0"" xmlns:mmlNm=""http://www.medxml.net/MML/SharedComponent/Name/1.0"" xmlns:mmlFc=""http://www.medxml.net/MML/SharedComponent/Facility/1.0"" xmlns:mmlDp=""http://www.medxml.net/MML/SharedComponent/Department/1.0"" xmlns:mmlAd=""http://www.medxml.net/MML/SharedComponent/Address/1.0"" xmlns:mmlPh=""http://www.medxml.net/MML/SharedComponent/Phone/1.0"" xmlns:mmlPsi=""http://www.medxml.net/MML/SharedComponent/PersonalizedInfo/1.0"" xmlns:mmlCi=""http://www.medxml.net/MML/SharedComponent/CreatorInfo/1.0"" xmlns:mmlPi=""http://www.medxml.net/MML/ContentModule/PatientInfo/1.0"" xmlns:mmlBc=""http://www.medxml.net/MML/ContentModule/BaseClinic/1.0"" xmlns:mmlFcl=""http://www.medxml.net/MML/ContentModule/FirstClinic/1.0"" xmlns:mmlHi=""http://www.medxml.net/MML/ContentModule/HealthInsurance/1.1"" xmlns:mmlLs=""http://www.medxml.net/MML/ContentModule/Lifestyle/1.0"" xmlns:mmlPc=""http://www.medxml.net/MML/ContentModule/ProgressCourse/1.0"" xmlns:mmlRd=""http://www.medxml.net/MML/ContentModule/RegisteredDiagnosis/1.0"" xmlns:mmlSg=""http://www.medxml.net/MML/ContentModule/Surgery/1.0"" xmlns:mmlSm=""http://www.medxml.net/MML/ContentModule/Summary/1.0"" xmlns:mmlLb=""http://www.medxml.net/MML/ContentModule/test/1.0"" xmlns:mmlRp=""http://www.medxml.net/MML/ContentModule/report/1.0"" xmlns:mmlRe=""http://www.medxml.net/MML/ContentModule/Referral/1.0"" xmlns:mmlSc=""http://www.medxml.net/MML/SharedComponent/Security/1.0"" xmlns:claim=""http://www.medxml.net/claim/claimModule/2.1"" xmlns:claimA=""http://www.medxml.net/claim/claimAmountModule/2.1"">

<MmlHeader>
	<mmlCi:CreatorInfo>
		<mmlPsi:PersonalizedInfo>
			<mmlCm:Id mmlCm:type=""facility"" mmlCm:tableId=""MML0024"">0000000</mmlCm:Id>
			<mmlPsi:personName>
				<mmlNm:Name mmlNm:repCode=""I"" mmlNm:tableId=""MML:0025"">
					<mmlNm:fullname>ＡＰＥＸ</mmlNm:fullname>
				</mmlNm:Name>
			</mmlPsi:personName>
			<mmlFc:Facility>
				<mmlFc:name mmlFc:repCode=""I"" mmlFc:tableId=""MML0025"">医療法人真生会　真生会富山病院</mmlFc:name>
				<mmlCm:Id mmlCm:type=""insurance"" mmlCm:tableId=""MML0027""></mmlCm:Id>
			</mmlFc:Facility>
			<mmlPsi:addresses>
				<mmlAd:address mmlAd:repCode=""I"">
					<mmlAd:full>富山県射水市下若８９－１０^</mmlAd:full>
					<mmlAd:zip>939-0243</mmlAd:zip>
				</mmlAd:address>
			</mmlPsi:addresses>
			<mmlPsi:phones>
				<mmlPh:Phone>
					<mmlPh:number>0766-52-2156</mmlPh:number>
				</mmlPh:Phone>
			</mmlPsi:phones>
		</mmlPsi:PersonalizedInfo>
	</mmlCi:CreatorInfo>
	<masterId>
		<mmlCm:Id mmlCm:type=""facility"" mmlCm:tableId=""MML0024"">{1}</mmlCm:Id>
	</masterId>
	<toc>
		<tocItem>http://www.medxml.net/MML/SharedComponent/Name/1.0</tocItem>
		<tocItem>http://www.medxml.net/MML/SharedComponent/Facility/1.0</tocItem>
		<tocItem>http://www.medxml.net/MML/SharedComponent/Address/1.0</tocItem>
		<tocItem>http://www.medxml.net/MML/SharedComponent/Phone/1.0</tocItem>
		<tocItem>http://www.medxml.net/MML/SharedComponent/PersonalizedInfo/1.0</tocItem>
		<tocItem>http://www.medxml.net/MML/SharedComponent/CreatorInfo/1.0</tocItem>
		<tocItem>http://www.medxml.net/MML/ContentModule/PatientInfo/1.0</tocItem>
	</toc>
</MmlHeader>

<MmlBody>
	<MmlModuleItem>
		<docInfo contentModuleType=""registeredDiagnosis"" moduleVersion="""">
			<securityLevel>
				<accessRight permit=""all""></accessRight>
			</securityLevel>
			<title generationPurpose=""claim"">患者情報 連携ＩＦ（ＡＰＥＸ→カルテ）</title>
			<docId>
				<uid>{2}</uid>
			</docId>
			<confirmDate>{3}</confirmDate>
			<mmlCi:CreatorInfo>
				<mmlPsi:PersonalizedInfo>
					<mmlCm:Id mmlCm:type=""facility"" mmlCm:tableId=""MML0024"">0000000</mmlCm:Id>
					<mmlPsi:personName>
						<mmlNm:Name mmlNm:repCode=""I"" mmlNm:tableId=""MML0025"">
							<mmlNm:fullname>ＡＰＥＸ</mmlNm:fullname>
						</mmlNm:Name>
					</mmlPsi:personName>
					<mmlFc:Facility>
						<mmlFc:name mmlFc:repCode=""I"" mmlFc:tableId=""MML0025"">医療法人真生会　真生会富山病院</mmlFc:name>
						<mmlCm:Id mmlCm:type=""insurance"" mmlCm:tableId=""MML0027""></mmlCm:Id>
					</mmlFc:Facility>
					<mmlPsi:addresses>
						<mmlAd:address mmlAd:repCode=""I"">
							<mmlAd:full>富山県射水市下若８９－１０^</mmlAd:full>
							<mmlAd:zip>939-0243</mmlAd:zip>
						</mmlAd:address>
					</mmlPsi:addresses>
					<mmlPsi:phones>
						<mmlPh:Phone>
							<mmlPh:number>0766-52-2156</mmlPh:number>
						</mmlPh:Phone>
					</mmlPsi:phones>
				</mmlPsi:PersonalizedInfo>
			</mmlCi:CreatorInfo>
			<extRefs>http://www.medxml.net/claim/claimModule/2.1</extRefs>
		</docInfo>
"
					, this._DateTime.ToString("yyyy-MM-ddTHH:mm:ss")
					, this._Order.Pat.Id
                    , this.FileID
					, this._DateTime.ToString("yyyyMMdd"));

                if (this._Operation)
                {
                    // 登録・修正の場合
                    s += string.Format(@"
		<content>
			<claim:ClaimModule>
				<claim:information claim:status=""perform"" claim:performTime=""{0}"" claim:oderTime=""{1}"" claim:admitFlag=""{2}"" claim:timeClass=""0"" claim:operationFlag=""True"" claim:orderNo=""{3}"" claim:jissiSeq=""{4}"" claim:nyuuinForm="""">
					<claim:patientDepartment>
						<mmlDp:Department>
							<mmlDp:name mmlDp:repCode=""I"" mmlDp:tableId=""MML0025""></mmlDp:name>
							<mmlCm:Id mmlCm:type=""facility"" mmlCm:tableId=""MML0028"">{5}</mmlCm:Id>
						</mmlDp:Department>
					</claim:patientDepartment>
					<mmlHi:localextend mmlHi:hokenNo=""{6}"" mmlHi:hokenNoInfo=""{7}""></mmlHi:localextend>
					<claim:localextend claim:orderDc=""{8}"" claim:doujituraiinSu=""{9}"" claim:souryouFlag=""{10}""></claim:localextend>
				</claim:information>
				<claim:bundle claim:classCode=""{11}"" claim:classCodeId=""Claim002"">
					<claim:className></claim:className>
					<claim:admMemo>{12}</claim:admMemo>
					<claim:bundleNumber>{13}</claim:bundleNumber>"
						, this.PerformTime
						, this.OrderTime
						, this.AdmitFlag
						, this.OrderNo
						, this.JissiSeq
						, this._Order.Dept
						, this._Order.Pat.Ins
						, this.HokenNoInfo
						, this._Order.Doctor
						, this.DoujituRaiinSu
						, "0"
						, this.ClassCode
						, ""
						, this._Order.Times);

                    // ItemList が空の場合は、ファイル生成しない
                    b = false;

                    foreach (Q26_Item item in this.ItemList)
                    {
                        // ReceCode が無い場合は飛ばさない
                        string rece_code = item._OrderDetail.GetReceCode(this._Order.InOut);

                        if (rece_code.Length == 0)
                        {
                            continue;
                        }

                        // 区切りが単独でない場合は CH00003095（自費コメント 88888889）を CA10000001 に変換する
                        // 2019/04/23 山本洋介補佐より
                        if (rece_code.Equals("CH00003095") && this.ItemList.Count > 1)
                        {
                            rece_code = "CA10000001";
                        }

                        s += string.Format(@"
					<claim:item claim:subclassCode=""{0}"" claim:subclassCodeId=""Claim003"" claim:code=""{1}"" claim:tableId="""">
						<claim:name>{2}</claim:name>
						<claim:number claim:numberCode=""{3}"" claim:numberCodeId=""Claim004"" claim:unit=""{4}"">{5}</claim:number>
						<claim:memo>{6}</claim:memo>
						<claim:localextend claim:kouhatuhinHenkouFlag=""{7}"" claim:souryou=""{8}""></claim:localextend>
					</claim:item>"
							, item.SubClassCode
							, rece_code
							, item.Name
							, "10"
							, item._OrderDetail.Unit
							, item._OrderDetail.Qty
							, item.Memo
							, item.KouhatuhinHenkouFlag
							, "0");

                        // ItemList が１件でもあればファイル生成する
                        b = true;
                    }

                    s += string.Format(@"
					<claim:memo>{0}</claim:memo>
					<claim:localextend claim:taiinSyohoFlag=""{1}"" claim:rinjiSyohoFlag=""{2}""></claim:localextend>
				</claim:bundle>
			</claim:ClaimModule>
		</content>"
						, this.Memo
						, this._Order.TaiinFlg.Equals("1") ? "1" : "0"
						, this._Order.RinjiFlg.Equals("1") ? "1" : "0");
                }
                else
                {
                    // 削除の場合
                    s += string.Format(@"
		<content>
			<claim:ClaimModule>
				<claim:information claim:status=""perform"" claim:performTime=""{0}""  claim:admitFlag=""{1}""  claim:timeClass=""0""  claim:operationFlag=""False"" claim:orderNo=""{2}"" claim:jissiSeq=""{3}"" >
					<claim:patientDepartment>
						<mmlDp:Department>
							<mmlDp:name mmlDp:repCode=""I"" mmlDp:tableId=""MML0025""></mmlDp:name>
							<mmlCm:Id mmlCm:type=""facility"" mmlCm:tableId=""MML0028"">1</mmlCm:Id>
						</mmlDp:Department>
					</claim:patientDepartment>
					<mmlHi:localextend mmlHi:hokenNo=""1"" mmlHi:hokenNoInfo=""^^^^^0^0""></mmlHi:localextend>
					<claim:localextend claim:orderDc=""1"" claim:doujituraiinSu=""1"" claim:souryouFlag=""0""></claim:localextend>
				</claim:information>
			</claim:ClaimModule>
		</content>"
                        , this.PerformTime
                        , this.AdmitFlag
                        , this.OrderNo
                        , this.JissiSeq);
                }

				s += "\r\n";
                s += "	</MmlModuleItem>\r\n";
                s += "</MmlBody>\r\n\r\n";
                s += "</Mml>\r\n";

                // ファイル生成する場合
                if (b)
                {
                    string file_id = "Q26_" + this._DateTime.ToString("yyyyMMddHHmmss") + "_" + this._SEQ.ToString().PadLeft(3, '0') + "_" + this._Order.Pat.Id;

                    XmlTextWriter writer = new XmlTextWriter(file_path + "\\" + file_id + ".xml", Encoding.Default);
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartDocument();

                    writer.WriteRaw(s);
                    writer.Close();

                    // ファイル名
                    sr.Value = file_id + ".xml";
                    sr.Msgs.Add(file_id + ".xml");
                }

                return sr;
            }
            catch (Exception ex)
            {
                sr.Errs.Add(ex.Message);
                return sr;
            }
            finally
            {
            }
        }

        /// <summary>
        /// PatOrder リストの XML ファイルを生成する
        /// </summary>
        /// <param name="list">PatOrder リスト</param>
        /// <param name="dt">日時</param>
        /// <param name="seq">ファイル連番の開始番号</param>
        /// <param name="doujitu_raiinsu">同日来院数（外来 >= 1, 入院 = 0）</param>
        /// <param name="operation">true: 登録・修正, false: 削除</param>
        /// <param name="tmp_path">出力先フォルダ（ローカル）</param>
        /// <param name="dst_path">移動先フォルダ（サーバー）</param>
        /// <param name="set_kaikei_flg">true: 会計フラグをセットする</param>
		/// <param name="user_id">会計入力者</param>
		/// <returns></returns>
        public static StdReturn Execute(List<PatOrder> list, DateTime dt, int seq = 1, int doujitu_raiinsu = 1, bool operation = true, string tmp_path = "", string dst_path = "", bool set_kaikei_flg = true, string user_id = "")
        {
            StdReturn sr = new StdReturn();

            try
            {
                if (tmp_path.Length == 0)
                {
                    throw new Exception("出力先フォルダが指定されていません");
                }
                else if (!Directory.Exists(tmp_path))
                {
                    Directory.CreateDirectory(tmp_path);
                }

                if (dst_path.Length == 0)
                {
                    throw new Exception("移動先フォルダが指定されていません");
                }
                else if (!Directory.Exists(dst_path))
                {
                    throw new Exception("移動先フォルダ " + dst_path + " が存在しません");
                }

                if (operation)
                {
					// 前処理として
                    // 処方オーダーの場合、同一のRp・診療区分・用法・日数のオーダーをまとめる
                    List<PatOrder> list2 = new List<PatOrder>();

                    foreach (PatOrder order in list)
                    {
                        // 会計フラグを立てる
                        if (set_kaikei_flg)
                        {
                            PatOrder.SetKaikeiFlg(order.OrderId, 1, user_id);
                        }

                        // 医事に飛ばすデータがなければ飛ばす
                        if (!order.IsRece)
                        {
                            continue;
                        }

                        if (order.Shinku.CompareTo("20") > 0 &&
                            order.Shinku.CompareTo("30") < 0)
                        {
                            // 診療区分が20台の場合

                            // Rp・診療区分・用法・コメント・日数が
                            // すべて同じ場合は、薬剤をまとめる

                            // 全部一致するか判定するフラグ
                            bool b = false;

                            foreach (PatOrder order2 in list2)
                            {
                                if (!order2.UkeId.Equals(order.UkeId))
                                {
                                    continue;
                                }

                                if (!order2.Shinku.Equals(order.Shinku))
                                {
                                    continue;
                                }

                                if (!order2.Times.Equals(order.Times))
                                {
                                    continue;
                                }

                                // 用法・コメントの比較
                                string comment1 = "";
                                string comment2 = "";

                                foreach (PatOrderDetail detail in order.DetailDrugDirectionCommentList)
                                {
                                    if (comment1.Length > 0)
                                    {
                                        comment1 += " ";
                                    }

                                    comment1 += detail.Name;
                                }

                                foreach (PatOrderDetail detail in order2.DetailDrugDirectionCommentList)
                                {
                                    if (comment2.Length > 0)
                                    {
                                        comment2 += " ";
                                    }

                                    comment2 += detail.Name;
                                }

                                if (!comment1.Equals(comment2))
                                {
                                    continue;
                                }

                                // ここまで全部一致したら追加
                                foreach (PatOrderDetail detail in order.DetailDrugList)
                                {
                                    order2.DetailList.Add(detail);
                                }

                                b = true;
                                break;
                            }

                            if (!b)
                            {
                                list2.Add(order);
                            }
                        }
                        else
                        {
                            // 診療区分が20台以外ならそのまま追加
                            list2.Add(order);
                        }
					}


                    foreach (PatOrder order in list2)
                    {
                        // 診療区分が20台なら DetailList の順番を並べ替える

                        if (order.Shinku.CompareTo("20") > 0 &&
                            order.Shinku.CompareTo("30") < 0)
                        {
                            order.DetailList.Sort((x, y) =>
                            {
                                int j = 0;

                                // DataType = 2（薬剤）を上に持って来る
                                if (x.DataType.Equals(2) && !y.DataType.Equals(2))
                                {
                                    j = -1;
                                }
                                else if (!x.DataType.Equals(2) && y.DataType.Equals(2))
                                {
                                    j = 1;
                                }

                                // 同一ならばオーダー番号で並べる
                                if (j.Equals(0))
                                {
                                    j = x.OrderId.CompareTo(y.OrderId);
                                }

                                // 同一ならば明細連番で並べる
                                if (j.Equals(0))
                                {
                                    j = x.DetailId.CompareTo(y.DetailId);
                                }

                                return j;
                            });
                        }

                        Q26 obj = new Q26(order, dt, seq, doujitu_raiinsu, true);
                        StdReturn srr = obj.MakeXML(tmp_path);

                        if (srr.MsgExist)
                        {
                            // ファイル名
                            sr.Msgs.Add(srr.Msg);

                            // 医事パスへ移動する
                            if (!tmp_path.Equals(dst_path) && File.Exists(tmp_path + "\\" + srr.Msg))
                            {
                                File.Move(tmp_path + "\\" + srr.Msg, dst_path + "\\" + srr.Msg);
                            }
                        }

                        if (srr.ErrExist)
                        {
                            sr.Errs.Add(srr.Err);
                        }

                        seq++;
                    }
                }
                else
                {
                    foreach (PatOrder order in list)
                    {
						// 会計フラグを立てる
						if (set_kaikei_flg)
						{
							PatOrder.SetKaikeiFlg(order.OrderId, 1, user_id);
						}

						Q26 obj = new Q26(order, dt, seq, doujitu_raiinsu, false);
                        StdReturn srr = obj.MakeXML(tmp_path);

                        if (srr.MsgExist)
                        {
                            // ファイル名
                            sr.Msgs.Add(srr.Msg);

                            // 医事パスへ移動する
                            if (!tmp_path.Equals(dst_path) && File.Exists(tmp_path + "\\" + srr.Msg))
                            {
                                File.Move(tmp_path + "\\" + srr.Msg, dst_path + "\\" + srr.Msg);
                            }
						}

                        if (srr.ErrExist)
                        {
                            sr.Errs.Add(srr.Err);
                        }

						seq++;
					}
                }

                return sr;
            }
            catch (Exception ex)
            {
                sr.Errs.Add(ex.Message);
                return sr;
            }
            finally
            {
            }
        }
    }

    public class Q26_Item
    {
        public PatOrderDetail _OrderDetail = new PatOrderDetail();

        /// <summary>
        /// 診療種別区分コード
        /// 0:手技, 1:材料, 2:薬剤, 9:コメント
        /// </summary>
        public string SubClassCode
        {
            get
            {
                string s = "";

                switch (this._OrderDetail.DataType)
                {
                    case 1:
                        s = "0";
                        break;
                    case 2:
                        s = "2";
                        break;
                    case 3:
                        s = "1";
                        break;
                    case 4:
                        s = "0";
                        break;
                    default:
                        s = "0";
                        break;
                }

                return s;
            }
        }

        public string Name
        {
            get
            {
                string s = "";

                // < > & " ' を除く
                s = this._OrderDetail.Name;
                s = s.Replace("<", "");
                s = s.Replace(">", "");
                s = s.Replace("&", "");
                s = s.Replace("\"", "");
                s = s.Replace("'", "");

                return s;
            }
        }

        /// <summary>
        /// コメント
        /// </summary>
        public string Memo
        {
            get
            {
                string s = "";

                // 不均等は要らない

                if (this._OrderDetail.Code.Equals("88888889"))
                {
                    s = this._OrderDetail.Name;
                }

                if (this._OrderDetail.ReceCode1.Equals("CH00003095") ||
                    this._OrderDetail.ReceCode2.Equals("CH00003095"))
                {
                    s = this._OrderDetail.Name;
                }
                else if (this._OrderDetail.ReceCode1.StartsWith("CA") ||
                    this._OrderDetail.ReceCode1.StartsWith("CB") ||
                    this._OrderDetail.ReceCode2.StartsWith("CA") ||
                    this._OrderDetail.ReceCode2.StartsWith("CB"))
                {
                    // 2020/05/13 山本補佐の要望
                    s = this._OrderDetail.ShortName;
                }

                return s;
            }
        }

        public string KouhatuhinHenkouFlag
        {
            get
            {
                string s = "";

                if (this._OrderDetail.IppanNameFlg.Equals("1"))
                {
                    // IPPAN_FLG = 1 の場合は 5
                    s = "5";
                }
                else
                {
                    s = " ";
                }

                return s;
            }
        }

        public Q26_Item(PatOrderDetail detail)
        {
            this._OrderDetail = detail;
        }
    }
}
