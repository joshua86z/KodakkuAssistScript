using Dalamud.Utility.Numerics;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.STD.Helper;
using KodakkuAssist.Data;
using KodakkuAssist.Module.Draw;
using KodakkuAssist.Module.GameEvent;
using KodakkuAssist.Module.GameOperate;
using KodakkuAssist.Script;
using Lumina.Data.Parsing;
using MyScriptNamespace;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;



namespace MyScripts2
{

    [ScriptType(name: "妖星P3黑洞TTS",
         territorys: [1363],
        //territorys: [],
        guid: "12345678-1234-1234-1234-123456789012",
        version: "0.1",
        note: "",
        author: "乌克拉玛特")]

    public class MyScripts2
    {


        public void Init(ScriptAccessory accessory)
        {
            P3二运 = false;
        }

        bool P3二运 = false;
        int 黑洞轮次 = 1;
        int 黑洞连线序号 = 0;


        private static Dictionary<MarkType, uint> 目标标记 = new Dictionary<MarkType, uint>();
        private static Dictionary<uint, uint> 黑洞记录表 = new Dictionary<uint, uint>();

        [ScriptMethod(name: "黑洞准备", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:50546"], userControl: false)]

        public async void 黑洞准备(Event @event, ScriptAccessory accessory)
        {
            if (string.Equals(@event["SourceId"], "00000000")) return;

            P3二运 = true;
            int 黑洞轮次 = 1;
            int 黑洞连线序号 = 0;
            目标标记.Clear();
            黑洞记录表.Clear();
        }

        [ScriptMethod(name: "目标标记收集", eventType: EventTypeEnum.Marker, eventCondition: ["Operate:Add", "Id:regex:^(0[123467])$"], userControl: false)]
        public void 目标标记收集(Event ev, ScriptAccessory sa)
        {
            if (!P3二运) return;

            lock (目标标记)
            {
                var mark = ev.Id();

                var tid = ev.TargetId();
                var tidx = sa.Data.PartyList.IndexOf(tid);

                目标标记.Add((MarkType)mark, tid);

                if (目标标记.Count != 8) return;
                sa.Method.SendChat($"/e 妖星接线提示记录完毕。");
            }
        }

        [ScriptMethod(name: "黑洞连线第一轮", eventType: EventTypeEnum.Tether, eventCondition: ["Id:0054"])]
        public async void 黑洞连线第一轮(Event @event, ScriptAccessory accessory)
        {
            if (string.Equals(@event["SourceId"], "00000000")) return;
            if (黑洞轮次 != 1) return;

            var sId = @event.SourceId();
            lock (黑洞记录表)
            {
                if (黑洞记录表.ContainsKey(sId)) return;
                黑洞记录表.Add(sId, @event.TargetId());

                黑洞连线序号++;
            }

            switch (黑洞连线序号)
                {
                    case 1:
                        if (目标标记.TryGetValue(MarkType.Attack2, out var attack2) && attack2 == accessory.Data.Me)
                        {
                            var text = "接线接线";
                            this.提示三连(accessory, text);
                        }

                        if (目标标记.TryGetValue(MarkType.Attack1, out var attack1) && attack1 == accessory.Data.Me)
                        {
                            var text = "准备接2线3线";
                            this.提示三连(accessory, text);
                        }

                        break;
                    case 2:

                        if (目标标记.TryGetValue(MarkType.Attack1, out var attack1_1) && attack1_1 == accessory.Data.Me)
                        {
                            var text = "接线接线";
                            this.提示三连(accessory, text);
                        }

                        break;

                    case 3:

                        break;
                }

                if (黑洞连线序号 % 3 == 0)
                {
                    黑洞连线序号 = 0;
                    黑洞轮次++;
                }
            
        }

        [ScriptMethod(name: "黑洞连线第二轮", eventType: EventTypeEnum.Tether, eventCondition: ["Id:0054"])]
        public async void 黑洞连线第二轮(Event @event, ScriptAccessory accessory)
        {
            if (string.Equals(@event["SourceId"], "00000000")) return;
            if (黑洞轮次 != 2) return;

            var sId = @event.SourceId();
            lock (黑洞记录表)
            {
                if (黑洞记录表.ContainsKey(sId)) return;
                黑洞记录表.Add(sId, @event.TargetId());

                黑洞连线序号++;

            }
            switch (黑洞连线序号)
            {
                case 1:
                    if (目标标记.TryGetValue(MarkType.Attack1, out var pId) && pId == accessory.Data.Me)
                    {
                        var text = "接线接线";
                        this.提示三连(accessory, text);
                    }

                    if (目标标记.TryGetValue(MarkType.Bind1, out var bind1) && bind1 == accessory.Data.Me)
                    {
                        var text = "准备接攻击1的线";
                        this.提示三连(accessory, text);

                        await Task.Delay(5000);
                        var text2 = "接攻击1的线";
                        this.提示三连(accessory, text2);
                    }

                    break;
                case 2:

                    if (目标标记.TryGetValue(MarkType.Attack2, out var attack2) && attack2 == accessory.Data.Me)
                    {
                        var text = "接线接线";
                        this.提示三连(accessory, text);
                    }

                    if (目标标记.TryGetValue(MarkType.Bind2, out var bind2) && bind2 == accessory.Data.Me)
                    {
                        await Task.Delay(5000);
                        var text = "准备接攻击2的线";
                        this.提示三连(accessory, text);

                        await Task.Delay(5000);
                        var text2 = "接攻击2的线";
                        this.提示三连(accessory, text2);
                    }

                    break;

                case 3:
                    if (目标标记.TryGetValue(MarkType.Attack3, out var attack3) && attack3 == accessory.Data.Me)
                    {
                        var text = "接线接线";
                        this.提示三连(accessory, text);
                    }

                    break;
            }

            if (黑洞连线序号 % 3 == 0)
            {
                黑洞连线序号 = 0;
                黑洞轮次++;
                黑洞记录表.Clear();
            }
        }

        [ScriptMethod(name: "黑洞连线第三轮", eventType: EventTypeEnum.Tether, eventCondition: ["Id:0054"])]
        public async void 黑洞连线第三轮(Event @event, ScriptAccessory accessory)
        {
            if (string.Equals(@event["SourceId"], "00000000")) return;
            if (黑洞轮次 != 3) return;

            var sId = @event.SourceId();
            lock (黑洞记录表)
            {
                if (黑洞记录表.ContainsKey(sId)) return;
                黑洞记录表.Add(sId, @event.TargetId());
                黑洞连线序号++;
            }

            switch (黑洞连线序号)
            {
                case 1:
                    if (目标标记.TryGetValue(MarkType.Bind1, out var bind1) && bind1 == accessory.Data.Me)
                    {
                        var text = "接线接线";
                        this.提示三连(accessory, text);
                    }

                    if (目标标记.TryGetValue(MarkType.Stop1, out var stop1) && stop1 == accessory.Data.Me)
                    {
                        var text = "准备接锁链1的线";
                        this.提示三连(accessory, text);

                        await Task.Delay(5000);
                        var text2 = "接锁链1的线";
                        this.提示三连(accessory, text2);
                    }

                    break;
                case 2:

                    if (目标标记.TryGetValue(MarkType.Bind2, out var bind2) && bind2 == accessory.Data.Me)
                    {
                        var text = "接线接线";
                        this.提示三连(accessory, text);
                    }

                    if (目标标记.TryGetValue(MarkType.Stop2, out var stop2) && stop2 == accessory.Data.Me)
                    {
                        await Task.Delay(5000);
                        var text = "准备接锁链2的线";
                        this.提示三连(accessory, text);

                        await Task.Delay(5000);
                        var text2 = "接锁链2的线";
                        this.提示三连(accessory, text2);
                    }

                    break;

                case 3:
                    if (目标标记.TryGetValue(MarkType.Bind3, out var bind3) && bind3 == accessory.Data.Me)
                    {
                        var text = "接线接线";
                        this.提示三连(accessory, text);
                    }

                    break;
            }

            if (黑洞连线序号 % 3 == 0)
            {
                黑洞连线序号 = 0;
                黑洞轮次++;
                黑洞记录表.Clear();
            }
        }

        [ScriptMethod(name: "黑洞连线第四轮", eventType: EventTypeEnum.Tether, eventCondition: ["Id:0054"])]
        public async void 黑洞连线第四轮(Event @event, ScriptAccessory accessory)
        {
            if (string.Equals(@event["SourceId"], "00000000")) return;
            if (黑洞轮次 != 4) return;

            var sId = @event.SourceId();
            lock (黑洞记录表)
            {
                if (黑洞记录表.ContainsKey(sId)) return;
                黑洞记录表.Add(sId, @event.TargetId());

                黑洞连线序号++;
            }

            switch (黑洞连线序号)
            {
                case 1:
                    if (目标标记.TryGetValue(MarkType.Stop2, out var stop2) && stop2 == accessory.Data.Me)
                    {
                        var text = "接线接两根";
                        this.提示三连(accessory, text);
                    }

                    if (目标标记.TryGetValue(MarkType.Stop1, out var stop1) && stop1 == accessory.Data.Me)
                    {
                        var text = "准备接第三根线";
                        this.提示三连(accessory, text);
                    }

                    break;
                case 2:

                    //if (目标标记.TryGetValue(MarkType.Stop2, out var stop2) && stop2 == accessory.Data.Me)
                    //{

                    //}

                    break;

                case 3:
                    if (目标标记.TryGetValue(MarkType.Stop1, out var stop_1) && stop_1 == accessory.Data.Me)
                    {
                        var text = "接第三根线";
                        this.提示三连(accessory, text);
                    }

                    break;
            }

        }

 
        private void 提示三连(ScriptAccessory accessory, string text)
        {
            accessory.Method.SendChat($"/e {text}");
            accessory.Method.TTS($"{text}");
            accessory.Method.TextInfo(text, 3000);
        }




    }



    public static class EventExtensions
    {
        private static bool ParseHexId(string? idStr, out uint id)
        {
            id = 0;
            if (string.IsNullOrEmpty(idStr)) return false;
            try
            {
                var idStr2 = idStr.Replace("0x", "");
                id = uint.Parse(idStr2, System.Globalization.NumberStyles.HexNumber);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static uint Id(this Event evt) => JsonConvert.DeserializeObject<uint>(evt["Id"]);
        public static uint ActionId(this Event evt) => JsonConvert.DeserializeObject<uint>(evt["ActionId"]);
        public static uint SourceId(this Event evt) => ParseHexId(evt["SourceId"], out var id) ? id : 0;
        public static uint TargetId(this Event evt) => ParseHexId(evt["TargetId"], out var id) ? id : 0;
        public static uint IconId(this Event evt) => ParseHexId(evt["Id"], out var id) ? id : 0;
        public static Vector3 SourcePosition(this Event evt) => JsonConvert.DeserializeObject<Vector3>(evt["SourcePosition"]);
        public static Vector3 TargetPosition(this Event evt) => JsonConvert.DeserializeObject<Vector3>(evt["TargetPosition"]);
        public static Vector3 EffectPosition(this Event evt) => JsonConvert.DeserializeObject<Vector3>(evt["EffectPosition"]);
        public static float SourceRotation(this Event evt) => JsonConvert.DeserializeObject<float>(evt["SourceRotation"]);
        public static float TargetRotation(this Event evt) => JsonConvert.DeserializeObject<float>(evt["TargetRotation"]);
        public static string SourceName(this Event evt) => evt["SourceName"];
        public static string TargetName(this Event evt) => evt["TargetName"];
        public static uint DurationMilliseconds(this Event evt) => JsonConvert.DeserializeObject<uint>(evt["DurationMilliseconds"]);
        public static uint Index(this Event evt) => ParseHexId(evt["Index"], out var id) ? id : 0;
        public static uint State(this Event evt) => ParseHexId(evt["State"], out var id) ? id : 0;
        public static uint DirectorId(this Event evt) => ParseHexId(evt["DirectorId"], out var id) ? id : 0;
        public static uint StatusId(this Event evt) => JsonConvert.DeserializeObject<uint>(evt["StatusID"]);
        public static uint StackCount(this Event evt) => JsonConvert.DeserializeObject<uint>(evt["StackCount"]);
        public static uint Param(this Event evt) => JsonConvert.DeserializeObject<uint>(evt["Param"]);
        public static uint TargetIndex(this Event evt) => JsonConvert.DeserializeObject<uint>(evt["TargetIndex"]);
        public static uint DataId(this Event evt) => JsonConvert.DeserializeObject<uint>(evt["DataId"]);
    }
}


