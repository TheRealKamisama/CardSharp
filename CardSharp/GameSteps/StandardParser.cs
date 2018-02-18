﻿using System;
using System.Globalization;
using CardSharp.GameComponents;
using Humanizer;
using Humanizer.Localisation;

namespace CardSharp.GameSteps
{
    public class StandardParser : ICommandParser
    {
        public void Parse(Desk desk, Player player, string command)
        {
            var pconfig = PlayerConfig.GetConfig(player);
            switch (command)
            {
                case "获取积分":
                    var px = DateTime.Now - pconfig.LastTime;
                    if (px.TotalSeconds.Seconds() > 12.Hours())
                    {
                        pconfig.AddPoint();
                        desk.AddMessage($"领取成功. 你当前积分为{pconfig.Point}");
                    }
                    else
                    {
                        desk.AddMessage(
                            $"你现在不能这么做. 你可以在{(12.Hours() - px).Humanize(culture: new CultureInfo("zh-CN"), maxUnit: TimeUnit.Hour)}后领取.");
                    }

                    break;
                case "我的信息":
                    desk.AddMessage($"你的积分为 {pconfig.Point}");
                    break;
                case "重新发牌":
                    if (desk.State == GameState.StartGame || desk.State == GameState.DiscussLandlord)
                        desk.SendCardsMessage();
                    break;
                case "命令列表":
                    desk.AddMessage(@"=    命令列表    =

/////没牌的直接添加机器人好友(有牌的最好也添加)机器人会自动同意请求\\\\\
Powered by Cy.
命令说明：
         带有[D]的命令 还未开发完成
         带有[B]的命令 是测试功能，可能会更改
         带有[R]的命令 是正式功能，'一般'不会做更改
群聊命令：
[R]|上桌|fork table|：加入游戏
[R]|下桌|：退出游戏
[R]|过|不出|不要|出你妈|要你妈|pass|passs|passss|passsss|passsssssssssssssssss|：过牌
[R]|抢地主|抢他妈的|：抢地主
[R]|不抢|抢你妈|抢个鸡毛掸子|：不抢地主
[R]|开始游戏|：准备环节→开始游戏
[R]|重新发牌|:不是重置牌！是会把你的牌通过私聊再发一次！ 
[R]|加倍|:加倍或超级加倍在一局游戏中只能使用一次
[R]|超级加倍|:加倍再加倍
[B]|SUDDEN_DEATH_DUEL_CARD|: 死亡决斗卡
[R]|下桌|：退出游戏，只能在准备环节使用
[R]|明牌|：显示自己的牌给所有玩家，明牌会导致积分翻倍，只能在发完牌后以及出牌之前使用。
[B]|托管|：自动出牌
[B]|结束托管|：结束自动出牌
[B]|弃牌|：放弃本局游戏，当地主或者两名农民弃牌游戏结束,弃牌农民玩家赢了不得分，输了双倍扣分
[B]|结束游戏|：只有参与游戏的人可以使用
[R]|获取积分|：获取积分，12小时可获取10000分。
[R]|我的信息|：你的积分
[B]|记牌器|：显示每种牌在场上还剩下多少张
[B]|安静出牌启用|：所有信息都会私聊发送
[B]|安静出牌禁用|：所有信息不都会私聊发送

如果崩溃请大家多多包涵，游戏愉快。
");
                    break;
                case "安静出牌启用":
                    desk.Silence = true;
                    break;
                case "安静出牌禁用":
                    desk.Silence = false;
                    break;
            }
        }
    }
}