﻿using System.Collections.Generic;
using System.Linq;

namespace CardSharp.Rules
{
    /// <summary>
    ///     3
    /// </summary>
    public class RuleSingle : RuleBase
    {
        public override bool IsMatch(List<CardGroup> cardGroups, List<CardGroup> lastCardGroups)
        {
            return SingleGroupMatch.IsMatch(cardGroups, lastCardGroups, 1);
        }

        public override string ToString()
        {
            return "单张";
        }

        public override (bool exists, List<Card> cards) FirstMatchedCards(List<CardGroup> sourceGroups,
            List<CardGroup> lastCardGroups)
        {
            var bombs = sourceGroups.Where(group => group.Count >= 1).ToList();
            if (bombs.Count == 0) return (false, null); //没有炸弹

            if (lastCardGroups == null)
                return (true,
                    bombs.First().ToEnumerable().Select(cg => new CardGroup(cg.Amount, 1)).ToCards().ToList());

            var sbombs = bombs.Where(bomb => bomb.Amount > lastCardGroups.First().Amount).ToList();
            if (sbombs.Count == 0) return (false, null); //没有大于的炸弹

            return (true, sbombs.First().ToEnumerable().Select(cg => new CardGroup(cg.Amount, 1)).ToCards().ToList());
        }
    }
}