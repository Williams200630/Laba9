using System;
using System.Collections.Generic;

namespace ExpressionValidator
{
    public class TournamentNode
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int? Score1 { get; set; }
        public int? Score2 { get; set; }
        public TournamentNode? Left { get; set; }
        public TournamentNode? Right { get; set; }

        public TournamentNode(string team1 = "", string team2 = "")
        {
            Team1 = team1;
            Team2 = team2;
            Score1 = null;
            Score2 = null;
            Left = null;
            Right = null;
        }
    }

    public class TournamentTree
    {
        private static Random rnd = new Random();
        public TournamentNode Root { get; private set; }
        public List<TournamentNode> Leaves { get; private set; } = new List<TournamentNode>();

        public TournamentTree(List<string> teams)
        {
            Root = BuildTree(teams, 0, teams.Count - 1);
        }

        private TournamentNode BuildTree(List<string> teams, int left, int right)
        {
            if (right - left == 1)
            {
                var node = new TournamentNode(teams[left], teams[right]);
                Leaves.Add(node);
                return node;
            }
            int mid = left + (right - left) / 2;
            var leftNode = BuildTree(teams, left, mid);
            var rightNode = BuildTree(teams, mid + 1, right);
            return new TournamentNode { Left = leftNode, Right = rightNode };
        }

        public void PlayMatches(TournamentNode? node)
        {
            if (node == null) return;
            if (node.Left == null && node.Right == null)
            {
                // Лист — матч между двумя командами
                node.Score1 = rnd.Next(0, 5);
                node.Score2 = rnd.Next(0, 5);
                while (node.Score1 == node.Score2) // ничья не допускается
                {
                    node.Score2 = rnd.Next(0, 5);
                }
                return;
            }
            PlayMatches(node.Left);
            PlayMatches(node.Right);
            // Победители предыдущих матчей
            var winner1 = GetWinner(node.Left!);
            var winner2 = GetWinner(node.Right!);
            node.Team1 = winner1;
            node.Team2 = winner2;
            node.Score1 = rnd.Next(0, 5);
            node.Score2 = rnd.Next(0, 5);
            while (node.Score1 == node.Score2)
            {
                node.Score2 = rnd.Next(0, 5);
            }
        }

        private string GetWinner(TournamentNode node)
        {
            if (node.Score1 > node.Score2) return node.Team1;
            else return node.Team2;
        }

        public void PrintMatches(TournamentNode? node)
        {
            if (node == null) return;
            if (!string.IsNullOrEmpty(node.Team1) && !string.IsNullOrEmpty(node.Team2) && node.Score1.HasValue && node.Score2.HasValue)
            {
                Console.WriteLine($"{node.Team1} - {node.Team2} : {node.Score1} - {node.Score2}");
            }
            PrintMatches(node.Left);
            PrintMatches(node.Right);
        }
    }
} 