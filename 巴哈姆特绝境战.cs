using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using KodakkuAssist.Script;
using KodakkuAssist.Module.GameEvent;
using KodakkuAssist.Module.GameEvent.Struct;
using KodakkuAssist.Module.Draw;
using KodakkuAssist.Module.GameOperate;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Utility.Numerics;
using Dalamud.Memory.Exceptions;
using static Dalamud.Interface.Utility.Raii.ImRaii;
using Newtonsoft.Json;
using ECommons;
using System.Security.Cryptography;

//using KodakkuAssist.Module.Draw.Manager.DrawPropertiesEdit;

namespace MyScriptNamespace
{
    [ScriptType(name: "巴哈姆特绝境战", territorys: [733], guid: "48286f7d-aa04-0502-0c01-6c7aa129e4fb", version: "0.2")]
    public class 巴哈姆特绝境战
    {
        uint MyId = 0;
        /// <summary>
        /// This method is called at the start of each battle reset.
        /// If this method is not defined, the program will execute an empty method.
        /// </summary>
        public void Init(ScriptAccessory accessory)
        {
            MyId = accessory.Data.Me;
            accessory.Method.MarkClear();
        }

        //long unixTime = DateTimeOffset.Now.ToUnixTimeSeconds();

        //[ScriptMethod(name: "热离子爆发", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9913"])]
        //public void 热离子爆发(Event @event, ScriptAccessory accessory)
        //{
        //    lock (this)
        //    {
        //        var unixTime  = DateTimeOffset.Now.ToUnixTimeSeconds();
        //        if (this.unixTime != unixTime)
        //        {
        //            this.unixTime = unixTime;
        //            for (var i = 0; i < 8; i++)
        //            {

        //                var dp = accessory.Data.GetDefaultDrawProperties();
        //                dp.Name = $"陨石流{i}";
        //                dp.Scale = new(5);
        //                dp.Owner = accessory.Data.PartyList[i];
        //                dp.DestoryAt = 1000;
        //                dp.Color = accessory.Data.DefaultDangerColor;
        //                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
        //            }
        //        } 
        //    }
        //}

        //[ScriptMethod(name: "凶鸟冲", eventType: EventTypeEnum.ActionEffect, eventCondition: ["ActionId:9918"])]
        //public void 凶鸟冲(Event @event, ScriptAccessory accessory)
        //{
        //    if (!ParseObjectId(@event["TargetId"], out var tid)) return;
        //    var dp = accessory.Data.GetDefaultDrawProperties();
        //    dp.Name = "凶鸟冲";
        //    dp.Scale = new(4);
        //    dp.Owner = tid;
        //    dp.DestoryAt = 1000;
        //    dp.Color = accessory.Data.DefaultDangerColor;
        //    accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
        //}
        [ScriptMethod(name: "旋风", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9898"])]
        public void 旋风(Event @event, ScriptAccessory accessory)
        {
            accessory.Method.TextInfo("旋风",1000, true);

            Task.Delay(2000).ContinueWith(t =>
            {
                for (var i = 0; i < 8; i++)
                {
                    var obj = accessory.Data.Objects.SearchByEntityId(accessory.Data.PartyList[i]);

                    var dp = accessory.Data.GetDefaultDrawProperties();
                    dp.Name = $"旋风{i}";
                    dp.Scale = new(2);
                    dp.Position = obj?.Position;
                    dp.DestoryAt = 2000;
                    dp.Color = accessory.Data.DefaultDangerColor;
                    accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
                }
            });
        }

        [ScriptMethod(name: "雷光链", eventType: EventTypeEnum.ActionEffect, eventCondition: ["ActionId:9927"])]
        public void 雷光链(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["TargetId"], out var tid)) return;
            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = $"雷光链{tid}";
            dp.Scale = new(5);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Owner = tid;
            dp.Delay = 4000;
            dp.DestoryAt = 2000;
            dp.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
        }

        // 月光啊！照亮铁血霸道！
        [ScriptMethod(name: "月环钢铁", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6492"])]
        public void 月环钢铁(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "月环";
            dp2.Scale = new(22);
            dp2.InnerScale = new(6);
            dp2.Radian = float.Pi * 2;
            dp2.Owner = sid;
            dp2.DestoryAt = 5000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Donut, dp2);

            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = "钢铁";
            dp.Scale = new(10);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Owner = sid;
            dp.Delay = 5000;
            dp.DestoryAt = 3000;
            dp.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
        }

        // 月光啊！用你的炽热烧尽敌人！
        [ScriptMethod(name: "月环分摊", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6493"])]
        public void 月环分摊(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "月环";
            dp2.Scale = new(22);
            dp2.InnerScale = new(6);
            dp2.Radian = float.Pi * 2;
            dp2.Owner = sid;
            dp2.DestoryAt = 5000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Donut, dp2);

            for (var i = 0; i < 8; i++)
            {
                var dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"热离子光束{i}";
                dp.Scale = new(3);
                dp.Owner = accessory.Data.PartyList[i];
                dp.Delay = 5000;
                dp.DestoryAt = 3000;
                dp.Color = accessory.Data.DefaultSafeColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }
        }

        // 被炽热灼烧过的轨迹 乃成铁血霸道！
        [ScriptMethod(name: "分摊钢铁", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6494"])]
        public void 分摊钢铁(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            for (var i = 0; i < 8; i++)
            {
                var dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"热离子光束{i}";
                dp.Scale = new(3);
                dp.Owner = accessory.Data.PartyList[i];
                dp.DestoryAt = 5000;
                dp.Color = accessory.Data.DefaultSafeColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "钢铁";
            dp2.Scale = new(10);
            dp2.ScaleMode = ScaleMode.ByTime;
            dp2.Owner = sid;
            dp2.Delay = 5000;
            dp2.DestoryAt = 3000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp2);
        }

        // 炽热燃烧！给予我月亮的祝福！
        [ScriptMethod(name: "分摊月环", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6495"])]
        public void 分摊月环(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            for (var i = 0; i < 8; i++)
            {
                var dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"热离子光束{i}";
                dp.Scale = new(3);
                dp.Owner = accessory.Data.PartyList[i];
                dp.DestoryAt = 5000;
                dp.Color = accessory.Data.DefaultSafeColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "月环";
            dp2.Scale = new(22);
            dp2.InnerScale = new(6);
            dp2.Radian = float.Pi * 2;
            dp2.Owner = sid;
            dp2.Delay = 5000;
            dp2.DestoryAt = 3000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Donut, dp2);
        }

        // 我降临于此，征战铁血霸道！
        [ScriptMethod(name: "分散钢铁", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6496"])]
        public void 分散钢铁(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            for (var i = 0; i < 8; i++)
            {
                var dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"凶鸟冲{i}";
                dp.Scale = new(4);
                dp.ScaleMode = ScaleMode.ByTime;
                dp.Owner = accessory.Data.PartyList[i];
                dp.DestoryAt = 5000;
                dp.Color = accessory.Data.DefaultDangerColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "钢铁";
            dp2.Scale = new(10);
            dp2.ScaleMode = ScaleMode.ByTime;
            dp2.Owner = sid;
            dp2.Delay = 5000;
            dp2.DestoryAt = 3000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp2);
        }

        // 我降临于此，对月长啸！
        [ScriptMethod(name: "分散月环", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6497"])]
        public void 分散月环(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            for (var i = 0; i < 8; i++)
            {
                var dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"凶鸟冲{i}";
                dp.Scale = new(4);
                dp.ScaleMode = ScaleMode.ByTime;
                dp.Owner = accessory.Data.PartyList[i];
                dp.DestoryAt = 5000;
                dp.Color = accessory.Data.DefaultDangerColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "月环";
            dp2.Scale = new(22);
            dp2.InnerScale = new(6);
            dp2.Radian = float.Pi * 2;
            dp2.Owner = sid;
            dp2.Delay = 5000;
            dp2.DestoryAt = 3000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Donut, dp2);
        }

        // 超新星啊，更加闪耀吧！在星降之夜，称赞红月！
        [ScriptMethod(name: "分散月华冲", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6500"])]
        public void 分散月华冲(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;
            lock (this)
            {
                this.cauterize = 0;
            }

            for (var i = 0; i < 8; i++)
            {
                var dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"陨石流{i}";
                dp.Scale = new(4);
                dp.ScaleMode = ScaleMode.ByTime;
                dp.Owner = accessory.Data.PartyList[i];
                dp.Delay = 12000;
                dp.DestoryAt = 3000;
                dp.Color = accessory.Data.DefaultDangerColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = $"月华冲";
            dp2.Scale = new(5);
            dp2.Owner = sid;
            dp2.CentreResolvePattern = PositionResolvePatternEnum.OwnerEnmityOrder;
            dp2.CentreOrderIndex = 1;
            dp2.Delay = 13000;
            dp2.DestoryAt = 2000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp2);
        }

        // 超新星啊，更加闪耀吧！照亮红月下炽热之地！
        [ScriptMethod(name: "月华冲分摊", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6501"])]
        public void 月华冲分摊(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;
            lock (this)
            {
                this.cauterize = 0;
            }

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = $"月华冲";
            dp2.Scale = new(5);
            dp2.Owner = sid; // bossid
            dp2.CentreResolvePattern = PositionResolvePatternEnum.OwnerEnmityOrder;
            dp2.CentreOrderIndex = 1;
            dp2.Delay = 13000;
            dp2.DestoryAt = 2000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp2);

            for (var i = 0; i < 8; i++)
            {
                var dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"热离子光束{i}";
                dp.Scale = new(2);
                dp.Owner = accessory.Data.PartyList[i];
                dp.Delay = 15000;
                dp.DestoryAt = 2000;
                dp.Color = accessory.Data.DefaultSafeColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }
        }

        long dragonTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        Dictionary<uint, Vector3> dragons = new();

        [ScriptMethod(name: "小龙记录", eventType: EventTypeEnum.AddCombatant, eventCondition: ["DataId:regex:^(816[34567])$"])]
        public void 小龙记录(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            lock (this)
            {
                var now = DateTimeOffset.Now.ToUnixTimeSeconds();
                if (now - this.dragonTime >= 1)
                {
                    this.dragonTime = now;
                    dragons.Clear();
                }

                var pos = JsonConvert.DeserializeObject<Vector3>(@event["SourcePosition"]);
                pos.Y = pos.X >= 0 ? pos.Z : (24 - pos.Z) + 100; // 场地直径是48

                dragons.Add(sid, pos);
            }
        }

        int cauterize = 0;
        [ScriptMethod(name: "俯冲标记", eventType: EventTypeEnum.TargetIcon, eventCondition: ["Id:0014"])]
        public void 俯冲标记(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["TargetId"], out var tid)) return;

            List<uint> dragonList = new List<uint>();
            IEnumerable<uint> query = from dragon in this.dragons
                                        orderby dragon.Value.Y
                                        select dragon.Key;
            foreach (uint sid in query)
            {
                dragonList.Add(sid);
            }

            lock (this)
            {
                this.cauterize++;

                switch (this.cauterize)
                {
                    case 1:
                        this._俯冲标记(dragonList[0], tid, accessory);
                        this._俯冲标记(dragonList[1], tid, accessory);
                        break;
                    case 2:
                        this._俯冲标记(dragonList[2], tid, accessory);
                        break;
                    case 3:
                        this._俯冲标记(dragonList[3], tid, accessory);
                        this._俯冲标记(dragonList[4], tid, accessory);
                        break;
                    default:
                        break;
                }
            }
        }

        private void _俯冲标记(uint sid, uint tid, ScriptAccessory accessory)
        {
            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = $"_俯冲标记{sid}";
            dp.Scale = new(10, 45);
            dp.Owner = sid;
            dp.DestoryAt = 6000;
            dp.Color = accessory.Data.DefaultSafeColor;
            dp.TargetObject = tid;

            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Rect, dp);
        }

        [ScriptMethod(name: "低温俯冲", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:regex:^(993[12345])$"])]
        public void 低温俯冲(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = "低温俯冲";
            dp.Scale = new(10, 45);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Owner = sid;
            dp.DestoryAt = 5000;
            dp.Color = accessory.Data.DefaultDangerColor;

            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Rect, dp);
        }

        [ScriptMethod(name: "旋风冲", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9906"])]
        public void 旋风冲(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = "旋风冲";
            dp.Scale = new(10, 45);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Color = accessory.Data.DefaultDangerColor;
            dp.Owner = sid;
            dp.DestoryAt = 4000;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Rect, dp);
        }

        [ScriptMethod(name: "旋风冲旋风", eventType: EventTypeEnum.ActionEffect, eventCondition: ["ActionId:9906"])]
        public void 旋风冲旋风(Event @event, ScriptAccessory accessory)
        {
            Task.Delay(1000).ContinueWith(t =>
            {
                for (var i = 0; i < 8; i++)
                {
                    var obj = accessory.Data.Objects.SearchByEntityId(accessory.Data.PartyList[i]);

                    var dp = accessory.Data.GetDefaultDrawProperties();
                    dp.Name = $"旋风{i}";
                    dp.Scale = new(2);
                    dp.Position = obj?.Position;
                    dp.DestoryAt = 2000;
                    dp.Color = accessory.Data.DefaultDangerColor;
                    accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
                }
            });
        }

        [ScriptMethod(name: "月流冲", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9923"])]
        public void 月流冲(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = "月流冲";
            dp.Scale = new(10, 45);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Color = accessory.Data.DefaultDangerColor;
            dp.Owner = sid;
            dp.DestoryAt = 4000;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Rect, dp);
        }

        [ScriptMethod(name: "百万核爆冲", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9953"])]
        public void 百万核爆冲(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = "百万核爆冲";
            dp.Scale = new(10, 45);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Color = accessory.Data.DefaultDangerColor;
            dp.Owner = sid;
            dp.DestoryAt = 4000;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Rect, dp);
        }

        [ScriptMethod(name: "大地摇动", eventType: EventTypeEnum.TargetIcon, eventCondition: ["Id:0028"])]
        public void 大地摇动(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["TargetId"], out var tid)) return;

            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = $"大地摇动{tid}";
            dp.Position = new(0, 0, 0);
            dp.Scale = new(50);
            dp.Radian = float.Pi / 2;
            dp.Color = accessory.Data.DefaultDangerColor;
            dp.TargetObject = tid;
            dp.DestoryAt = 5000;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Fan, dp);
        }

        bool blackfireTrio = false;

        [ScriptMethod(name: "黑炎的三重奏", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9955"])]
        public void 黑炎的三重奏(Event @event, ScriptAccessory accessory)
        {
            this.blackfireTrio = true;
        }


        Vector3 NaelPosition = new(0, 0, 0);

        [ScriptMethod(name: "奈尔位置", eventType: EventTypeEnum.SetObjPos, eventCondition: ["SourceDataId:8161"])]
        public void 奈尔位置(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            this.NaelPosition = JsonConvert.DeserializeObject<Vector3>(@event["SourcePosition"]);
            if (Math.Sqrt(this.NaelPosition.X * this.NaelPosition.X + this.NaelPosition.Z * this.NaelPosition.Z) < 23) return;

            if (this.blackfireTrio)
            {
                this.blackfireTrio = false;

                var dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = "奈尔位置";
                dp.Scale = new(1.5f, 24);
                dp.ScaleMode |= ScaleMode.YByDistance;
                dp.Color = accessory.Data.DefaultSafeColor;
                dp.Owner = MyId;
                dp.TargetObject = sid;
                dp.DestoryAt = 2000;

                accessory.Method.SendDraw(DrawModeEnum.Imgui, DrawTypeEnum.Displacement, dp);
            }
        }

        // 我降临于此对月长啸！召唤星降之夜！
        [ScriptMethod(name: "灾厄分散月环", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6502"])]
        public void 灾厄分散月环(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            for (var i = 0; i < 8; i++)
            {
                var dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"凶鸟冲{i}";
                dp.Scale = new(4);
                dp.ScaleMode = ScaleMode.ByTime;
                dp.Owner = accessory.Data.PartyList[i];
                dp.DestoryAt = 5000;
                dp.Color = accessory.Data.DefaultDangerColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "月环";
            dp2.Scale = new(22);
            dp2.InnerScale = new(6);
            dp2.Radian = float.Pi * 2;
            dp2.Owner = sid;
            dp2.Delay = 5000;
            dp2.DestoryAt = 3000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Donut, dp2);
        }

        // 我自月而来降临于此，召唤星降之夜！
        [ScriptMethod(name: "灾厄月环分散", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6503"])]
        public void 灾厄月环分散(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "月环";
            dp2.Scale = new(22);
            dp2.InnerScale = new(6);
            dp2.Radian = float.Pi * 2;
            dp2.Owner = sid;
            dp2.DestoryAt = 5000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Donut, dp2);

            for (var i = 0; i < 8; i++)
            {
                var dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"凶鸟冲{i}";
                dp.Scale = new(4);
                dp.ScaleMode = ScaleMode.ByTime;
                dp.Owner = accessory.Data.PartyList[i];
                dp.Delay = 5000;
                dp.DestoryAt = 3000;
                dp.Color = accessory.Data.DefaultDangerColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }
        }

        [ScriptMethod(name: "以太失控", eventType: EventTypeEnum.ActionEffect, eventCondition: ["ActionId:9905"])]
        public void 以太失控(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["TargetId"], out var tid)) return;

            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = $"陨石流{tid}";
            dp.Scale = new(4);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Owner = tid;
            dp.DestoryAt = 4000;
            dp.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);

            this.heavensfallTrio = false;
        }

        bool heavensfallTrio = false;

        [ScriptMethod(name: "天地的三重奏", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9957"])]
        public void 天地的三重奏(Event @event, ScriptAccessory accessory)
        {
            this.heavensfallTrio = true;
        }

        Dictionary<int, Vector3> towers = new();
        Object o = new Object();
        // 天地塔
        [ScriptMethod(name: "天地塔", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9951"])]
        public void 天地塔(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            lock (this)
            {
                if (!this.heavensfallTrio) return;

                var pos = JsonConvert.DeserializeObject<Vector3>(@event["SourcePosition"]);
                var dir = this.PositionTo16Dir(pos, new(0, 0, 0));

                towers.Add(dir, pos);

                if (this.towers.Count == 8)
                {
                    var NaelDir = this.PositionTo16Dir(NaelPosition, new(0, 0, 0)); 

                    IEnumerable<int> query = from kv in towers
                                             orderby kv.Key
                                              select kv.Key;

                    var tmp = new List<int>();
                    foreach (var v in query)
                    {
                        if (v >= NaelDir) tmp.Add(v);
                    }
                    foreach (var v in query)
                    {
                        if (v < NaelDir) tmp.Add(v);
                    }

                    var myIndex = 0;
                    switch (accessory.Data.PartyList.ToList().IndexOf(MyId))
                    {
                        case 0:
                            myIndex = 7; break;
                        case 1:
                            myIndex = 0; break;
                        case 2:
                            myIndex = 6; break;
                        case 3:
                            myIndex = 1; break;
                        case 4:
                            myIndex = 5; break;
                        case 5:
                            myIndex = 2; break;
                        case 6:
                            myIndex = 4; break;
                        case 7:
                            myIndex = 3; break;
                    }

                    var dp = accessory.Data.GetDefaultDrawProperties();
                    dp.Name = $"天地塔{sid}";
                    dp.Scale = new(1.5f, 48);
                    dp.ScaleMode |= ScaleMode.YByDistance;
                    dp.Position = towers[tmp[myIndex]];
                    dp.TargetObject = MyId;
                    dp.DestoryAt = 7000;
                    dp.Color = accessory.Data.DefaultSafeColor;
                    accessory.Method.SendDraw(DrawModeEnum.Imgui, DrawTypeEnum.Displacement, dp);

                    this.towers.Clear();
                }
            }
        }

        [ScriptMethod(name: "天崩地裂", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9911"])]
        public void 天崩地裂(Event @event, ScriptAccessory accessory)
        {
            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = "天崩地裂";
            dp.Scale = new(4);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Position = new(0, 0, 0);
            dp.DestoryAt = 5000;
            dp.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "天崩地裂击退";
            dp2.Scale = new(1.5f, 12);
            dp2.Owner = MyId;
            dp2.TargetPosition = new(0, 0, 0);
            dp2.Rotation = float.Pi;
            dp2.Color = accessory.Data.DefaultSafeColor;
            dp2.DestoryAt = 6000;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Displacement, dp2);
        }

        private int PositionTo16Dir(Vector3 point, Vector3 centre)
        {
            var r = Math.Round(8 - 8 * Math.Atan2(point.X - centre.X, point.Z - centre.Z) / Math.PI) % 16;
            return (int)r;
        }

        [ScriptMethod(name: "群龙的八重奏", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9959"])]
        public void 群龙的八重奏(Event @event, ScriptAccessory accessory)
        {
            this.grandOctetIcons.Clear();
        }

        List<uint> grandOctetIcons = new();
        [ScriptMethod(name: "群龙标记", eventType: EventTypeEnum.TargetIcon, eventCondition: ["Id:regex:^(0014|0077|0029)$"])]
        public void 群龙标记(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["TargetId"], out var tid)) return;

            lock (this)
            {
                this.grandOctetIcons.Add(tid);
                if (this.grandOctetIcons.Count == 7)
                {
                    var find = false;
                    for (int i = 0; i < this.grandOctetIcons.Count; i++)
                    {
                        if (this.grandOctetIcons[i] == MyId) find = true;
                    }

                    if (!find)
                    {
                        var dp = accessory.Data.GetDefaultDrawProperties();
                        dp.Name = $"双塔尼亚";
                        dp.Scale = new(1.5f, 24);
                        dp.ScaleMode |= ScaleMode.YByDistance;
                        dp.Owner = MyId;
                        dp.TargetObject = Twintania;
                        dp.DestoryAt = 8000;
                        dp.Color = accessory.Data.DefaultSafeColor;
                        accessory.Method.SendDraw(DrawModeEnum.Imgui, DrawTypeEnum.Displacement, dp);
                    }
                }
            }
        }

        uint Twintania = 0;

        [ScriptMethod(name: "双塔尼亚位置", eventType: EventTypeEnum.SetObjPos, eventCondition: ["SourceDataId:8159"])]
        public void 双塔尼亚位置(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out this.Twintania)) return;
        }

        // 钢铁燃烧吧！成为我降临于此的刀剑吧！
        [ScriptMethod(name: "钢铁热离子光束凶鸟冲", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6504"])]
        public void 钢铁热离子光束凶鸟冲(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = "钢铁";
            dp.Scale = new(10);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Owner = sid;
            dp.DestoryAt = 5000;
            dp.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);

            for (var i = 0; i < 8; i++)
            {
                dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"热离子光束{i}";
                dp.Scale = new(2);
                dp.Owner = accessory.Data.PartyList[i];
                dp.Delay = 5000;
                dp.DestoryAt = 3000;
                dp.Color = accessory.Data.DefaultSafeColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);

                dp.Name = $"凶鸟冲{i}";
                dp.Scale = new(4);
                dp.Delay = 8000;
                dp.DestoryAt = 3000;
                dp.Color = accessory.Data.DefaultDangerColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }
        }


        [ScriptMethod(name: "钢铁凶鸟冲热离子光束", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6505"])]
        public void 钢铁凶鸟冲热离子光束(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = "钢铁";
            dp.Scale = new(10);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Owner = sid;
            dp.DestoryAt = 5000;
            dp.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);

            for (var i = 0; i < 8; i++)
            {
                dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"凶鸟冲{i}";
                dp.Scale = new(4);
                dp.Owner = accessory.Data.PartyList[i];
                dp.Delay = 5000;
                dp.DestoryAt = 3000;
                dp.Color = accessory.Data.DefaultDangerColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);

                dp.Name = $"热离子光束{i}";
                dp.Scale = new(2);
                dp.Delay = 8000;
                dp.DestoryAt = 3000;
                dp.Color = accessory.Data.DefaultSafeColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }
        }


        [ScriptMethod(name: "月环凶鸟冲热离子光束", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6506"])]
        public void 月环凶鸟冲热离子光束(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "月环";
            dp2.Scale = new(22);
            dp2.InnerScale = new(6);
            dp2.Radian = float.Pi * 2;
            dp2.Owner = sid;
            dp2.DestoryAt = 5000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Donut, dp2);

            for (var i = 0; i < 8; i++)
            {
                var dp = accessory.Data.GetDefaultDrawProperties();
                dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"凶鸟冲{i}";
                dp.Scale = new(4);
                dp.Owner = accessory.Data.PartyList[i];
                dp.Delay = 5000;
                dp.DestoryAt = 3000;
                dp.Color = accessory.Data.DefaultDangerColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);

                dp.Name = $"热离子光束{i}";
                dp.Scale = new(2);
                dp.Delay = 8000;
                dp.DestoryAt = 3000;
                dp.Color = accessory.Data.DefaultSafeColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }
        }

        // 我自月而来携钢铁降临于此！
        [ScriptMethod(name: "月环钢铁凶鸟冲", eventType: EventTypeEnum.NpcYell, eventCondition: ["Id:6507"])]
        public void 月环钢铁凶鸟冲(Event @event, ScriptAccessory accessory)
        {
            if (!ParseObjectId(@event["SourceId"], out var sid)) return;

            var dp2 = accessory.Data.GetDefaultDrawProperties();
            dp2.Name = "月环";
            dp2.Scale = new(22);
            dp2.InnerScale = new(6);
            dp2.Radian = float.Pi * 2;
            dp2.Owner = sid;
            dp2.DestoryAt = 5000;
            dp2.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Donut, dp2);

            var dp = accessory.Data.GetDefaultDrawProperties();
            dp.Name = "钢铁";
            dp.Scale = new(10);
            dp.ScaleMode = ScaleMode.ByTime;
            dp.Owner = sid;
            dp.Delay = 5000;
            dp.DestoryAt = 3000;
            dp.Color = accessory.Data.DefaultDangerColor;
            accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);

            for (var i = 0; i < 8; i++)
            {
                dp = accessory.Data.GetDefaultDrawProperties();
                dp.Name = $"凶鸟冲{i}";
                dp.Scale = new(4);
                dp.Owner = accessory.Data.PartyList[i];
                dp.Delay = 8000;
                dp.DestoryAt = 3000;
                dp.Color = accessory.Data.DefaultDangerColor;
                accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
            }
        }

        long exaflareTime = DateTimeOffset.Now.ToUnixTimeSeconds();

        [ScriptMethod(name: "百京核爆", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:9968"])]
        public void 百京核爆(Event @event, ScriptAccessory accessory)
        {
            var pos = JsonConvert.DeserializeObject<Vector3>(@event["TargetPosition"]);

            lock (this)
            {
                var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                if (timestamp - this.exaflareTime > 10)
                {
                    this.exaflareTime = timestamp;

                    var dp = accessory.Data.GetDefaultDrawProperties();
                    dp.Name = "百京核爆";
                    dp.Scale = new(1.5f, 24);
                    dp.ScaleMode |= ScaleMode.YByDistance;
                    dp.Color = accessory.Data.DefaultSafeColor;
                    dp.Owner = MyId;
                    dp.TargetPosition = pos;
                    //dp.Delay = 1000;
                    dp.DestoryAt = 3000;

                    accessory.Method.SendDraw(DrawModeEnum.Imgui, DrawTypeEnum.Displacement, dp);
                }
            }
        }
        private static bool ParseObjectId(string? idStr, out uint id)
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
    }
}
